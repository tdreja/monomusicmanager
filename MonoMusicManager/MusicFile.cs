﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MonoMusicManager
{
    class MusicFile
    {
        public string Artist { get; protected set; }
        public string Title { get; protected set; }
        public string Album { get; protected set; }
        public string Genre { get; protected set; }
        public string Source { get; protected set; }

        public uint TrackNr { get; protected set; }
        public uint DiscNr { get; protected set; }

        public MusicFolder.Folders Folder { get; internal set; }

        public string AlbumParentFolder { get; internal set; }
        public bool HasVariousArtists { get; internal set; }
        public uint MaxDiscNr { get; internal set; }
        public uint MaxTrackNr { get; internal set; }

        public MusicFile(string path)
        {
            FileInfo info = new System.IO.FileInfo(path);
            Source = info.FullName;
            if(info.Exists)
            {
                TagLib.File file = TagLib.File.Create(this.Source);

                Artist = file.Tag.FirstPerformer;
                Title = file.Tag.Title;
                Album = file.Tag.Album;
                Genre = file.Tag.FirstGenre;
                TrackNr = file.Tag.Track;
                DiscNr = file.Tag.Disc;
            }
            else
            {
                Artist = null;
                Title = null;
                Album = null;
                Genre = null;
                TrackNr = 0;
                DiscNr = 0;
            }

            Folder = MusicFolder.Folders.NONE;
            AlbumParentFolder = null;
            HasVariousArtists = false;
            MaxDiscNr = 0;
            MaxTrackNr = 0;
        }

        public MusicFile(string artist, string title, string album, string genre, string path)
        {
            Artist = artist;
            Title = title;
            Album = album;
            Source = path;
            Genre = genre;
            TrackNr = 1;
            DiscNr = 1;
            Folder = MusicFolder.Folders.NONE;
            AlbumParentFolder = null;
            HasVariousArtists = false;
            MaxDiscNr = 0;
            MaxTrackNr = 0;
        }

        public string CopyToDestination(string targetFolder, bool overide)
        {
            if(CanCopy())
            {
                FileInfo dest = new FileInfo(GetDestination(targetFolder));
                if(!dest.Directory.Exists)
                {
                    Directory.CreateDirectory(dest.Directory.FullName);
                }
                File.Copy(Source, dest.FullName, overide);

                return dest.FullName;
            }
            else
            {
                return Source;
            }
            
        }

        public string GetDestination(string targetFolder)
        {
            FileInfo sourceFile = new FileInfo(Source);
            string newPath = MusicFolder.GetPath(targetFolder, Folder);
            //Console.WriteLine("Source " + sourceFile + " new " + newPath);

            if (Folder == MusicFolder.Folders.ALBUM || Folder == MusicFolder.Folders.FILMMUSIK 
                || Folder == MusicFolder.Folders.PODCASTS || Folder == MusicFolder.Folders.LIEDER)
            {
                if (AlbumParentFolder != null && AlbumParentFolder.Length > 0)
                {
                    newPath = Path.Combine(newPath, AlbumParentFolder);
                }

                //Console.WriteLine("Folder: "+ CreateFolderName() + " File: " + CreateFileName(sourceFile, true));

                return Path.Combine(newPath, CreateFolderName(), CreateFileName(sourceFile, true));
            }
            else
            {
                return Source;
            }
        }

        public bool IsValid()
        {
            return Source != null && Title != null && Title.Length > 0 && Artist != null && Artist.Length > 0;
        }

        public bool CanCopy()
        {
            return IsValid() && Folder != MusicFolder.Folders.NONE && Folder != MusicFolder.Folders.PLAYLISTS;
        }

        private string CreateFolderName()
        {
            if(Artist != null)
            {
                if (Folder == MusicFolder.Folders.LIEDER || Album == null || Album.Length == 0)
                {
                    return Artist;
                }
                else if (HasVariousArtists)
                {
                    return Album;
                }
                else
                {
                    return Artist + " - " + Album;
                }
            }
            else
            {
                return "";
            }
            
        }

        private string CreateFileName(FileInfo sourceFile, bool useTrackNr)
        {
            if(useTrackNr && TrackNr > 0)
            {
                return FormatDiscNr() + FormatTrackNr() + " - " + Title + sourceFile.Extension;
            }
            else
            {
                return Title + sourceFile.Extension;
            }
        }

        public string FormatTrackNr()
        {
            if(MaxTrackNr >= 100)
            {
                return TrackNr.ToString("000");
            }
            else
            {
                return TrackNr.ToString("00");
            }
        }

        public string FormatDiscNr()
        {
            uint disc = Math.Max(1, DiscNr);

            if(MaxDiscNr >= 10)
            {
                return disc.ToString("00");
            }
            else
            {
                return disc.ToString("0");
            }
        }
    }

    static class MusicFolder
    {
        public const int MINIMUM_ALBUM_SONG_NUMBER = 3;

        public enum Folders
        {
            NONE,
            ALBUM,
            LIEDER,
            FILMMUSIK,
            PODCASTS,
            PLAYLISTS
        }

        public static string GetPath(string basePath, Folders folder)
        {
            return Path.Combine(basePath, GetFolderName(folder));
        }

        public static string GetFolderName(Folders folder)
        {
            switch(folder)
            {
                case Folders.ALBUM:
                    return "Album";
                case Folders.LIEDER:
                    return "Lieder";
                case Folders.FILMMUSIK:
                    return "Filmmusik";
                case Folders.PLAYLISTS:
                    return "Playlists";
                case Folders.PODCASTS:
                    return "Podcasts";
                default:
                    return "NoFolder";
            }
        }

        public static List<string> GetAllFolderNames()
        {
            List<string> names = new List<string>();

            foreach (Folders fold in Enum.GetValues(typeof(Folders))) {
                names.Add(GetFolderName(fold));
            }

            return names;
        }

        public static List<MusicFile> SortFiles(string targetFolder, params MusicFile[] unsortedFiles)
        {
            List<MusicFile> sortedFiles = new List<MusicFile>();

            Dictionary<string, AlbumInfo> albumInfos = new Dictionary<string, AlbumInfo>();

            // Group in albums and check how many songs are in each album
            foreach(MusicFile file in unsortedFiles)
            {
                if(file.Album != null)
                {
                    if(albumInfos.ContainsKey(file.Album))
                    {
                        AlbumInfo info = albumInfos[file.Album];
                        info.SongCount++;
                        if(!info.IsVarious && !info.Artist.Equals(file.Artist))
                        {
                            info.IsVarious = true;
                        }

                        if(info.MaxDisc <= file.DiscNr)
                        {
                            info.MaxDisc = file.DiscNr;
                        }

                        if(info.MaxTrack <= file.TrackNr)
                        {
                            info.MaxTrack = file.TrackNr;
                        }
                    }
                    else
                    {
                        albumInfos.Add(file.Album, new AlbumInfo(file.Artist, file.DiscNr, file.TrackNr));
                    }
                }
            }

            Dictionary<string, string> parentFolder = new Dictionary<string, string>();

            foreach(MusicFile file in unsortedFiles)
            {
                if(file.IsValid())
                {
                    if (file.Album != null)
                    {
                        if (albumInfos.ContainsKey(file.Album))
                        {
                            file.HasVariousArtists = albumInfos[file.Album].IsVarious;
                            file.MaxDiscNr = albumInfos[file.Album].MaxDisc;
                            file.MaxTrackNr = albumInfos[file.Album].MaxTrack;
                        }

                        if (albumInfos.ContainsKey(file.Album) && albumInfos[file.Album].SongCount >= MINIMUM_ALBUM_SONG_NUMBER)
                        {
                            if (file.Genre != null && file.Genre.Contains("Soundtrack"))
                            {
                                file.Folder = Folders.FILMMUSIK;
                            }
                            else
                            {
                                file.Folder = Folders.ALBUM;
                            }

                            if (parentFolder.ContainsKey(file.Album))
                            {
                                file.AlbumParentFolder = parentFolder[file.Album];
                            }
                            else
                            {
                                file.AlbumParentFolder = CheckParentFolder(albumInfos[file.Album].Artist, file.HasVariousArtists, file.Album, file.Folder, targetFolder);
                                parentFolder.Add(file.Album, file.AlbumParentFolder);
                            }
                        }
                        else
                        {
                            file.Folder = Folders.LIEDER;
                        }
                    }
                    else
                    {
                        file.Folder = Folders.LIEDER;
                    }

                    sortedFiles.Add(file);
                }                
            }

            return sortedFiles;
        }

        private static string CheckParentFolder(string artist, bool various, string album, Folders folder, string targetRoot)
        {
            string mainPath = GetPath(targetRoot, folder);

            if(various)
            {
                string[] parts = album.Split(' ');
                StringBuilder builder = new StringBuilder();

                for(int i=0; i < parts.Length; i++)
                {
                    builder.Clear();

                    for(int j=0; j < parts.Length -i; j++)
                    {
                        builder.Append(parts[j]);
                        if(j < parts.Length-1-i)
                        {
                            builder.Append(" ");
                        }
                    }

                    if(System.IO.Directory.Exists(Path.Combine(mainPath, builder.ToString())))
                    {
                        return builder.ToString();
                    }
                }
            }
            else if (System.IO.Directory.Exists(Path.Combine(mainPath, artist)))
            {
                return artist;
            }

            return "";
        }
    }

    class AlbumInfo
    {
        public int SongCount { get; internal set; }
        public uint MaxDisc { get; internal set; }
        public uint MaxTrack { get; internal set; }
        public string Artist { get; internal set; }
        public bool IsVarious { get; internal set; }

        public AlbumInfo(string artist, uint discNr, uint trackNr)
        {
            this.Artist = artist;
            IsVarious = false;
            SongCount = 1;
            MaxDisc = discNr;
            MaxTrack = trackNr;
        }
    }

    
}