using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MonoMusicManager
{
    class MusicFile
    {
        public string artist { get; protected set; }
        public string title { get; protected set; }
        public string album { get; protected set; }
        public string genre { get; protected set; }
        public string path { get; protected set; }

        public uint trackNr { get; protected set; }
        public uint discNr { get; protected set; }

        public MusicFolder.Folders folder;
        public string albumParentFolder;
        public bool variousArtists;

        public MusicFile(string path)
        {
            FileInfo info = new System.IO.FileInfo(path);
            if(info.Exists)
            {
                this.path = info.FullName;
                TagLib.File file = TagLib.File.Create(this.path);

                artist = file.Tag.FirstPerformer;
                title = file.Tag.Title;
                album = file.Tag.Album;
                genre = file.Tag.FirstGenre;
                trackNr = file.Tag.Track;
                discNr = file.Tag.Disc;
            } else
            {
                Console.WriteLine("No Exist " + path);
            }

            folder = MusicFolder.Folders.NONE;
            albumParentFolder = null;
            variousArtists = false;
        }

        public MusicFile(string artist, string title, string album, string genre, string path)
        {
            this.artist = artist;
            this.title = title;
            this.album = album;
            this.path = path;
            this.genre = genre;
            trackNr = 1;
            discNr = 1;
            folder = MusicFolder.Folders.NONE;
            albumParentFolder = null;
            variousArtists = false;
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
            return Path.Combine(basePath, GetPathFor(folder));
        }

        private static string GetPathFor(Folders folder)
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

        public static List<MusicFile> sortFiles(string targetRoot, params MusicFile[] unsortedFiles)
        {
            List<MusicFile> sortedFiles = new List<MusicFile>();

            Dictionary<string, AlbumInfo> albumInfos = new Dictionary<string, AlbumInfo>();

            // Group in albums and check how many songs are in each album
            foreach(MusicFile file in unsortedFiles)
            {
                if(file.album != null)
                {
                    if(albumInfos.ContainsKey(file.album))
                    {
                        AlbumInfo info = albumInfos[file.album];
                        info.songCount++;
                        if(!info.various && !info.artist.Equals(file.artist))
                        {
                            info.various = true;
                        }
                    }
                    else
                    {
                        albumInfos.Add(file.album, new AlbumInfo(file.artist));
                    }
                }
            }

            Dictionary<string, string> parentFolder = new Dictionary<string, string>();

            foreach(MusicFile file in unsortedFiles)
            {
                if(file.album != null)
                {
                    if(albumInfos.ContainsKey(file.album))
                    {
                        file.variousArtists = albumInfos[file.album].various;
                    }

                    if(albumInfos.ContainsKey(file.album) && albumInfos[file.album].songCount >= MINIMUM_ALBUM_SONG_NUMBER)
                    {
                        if(file.genre != null && file.genre.Contains("Soundtrack"))
                        {
                            file.folder = Folders.FILMMUSIK;
                        }
                         else
                        {
                            file.folder = Folders.ALBUM;
                        }

                        if(parentFolder.ContainsKey(file.album))
                        {
                            file.albumParentFolder = parentFolder[file.album];
                        }
                        else
                        {
                            file.albumParentFolder = checkParentFolder(albumInfos[file.album].artist, file.variousArtists, file.album, file.folder, targetRoot);
                            parentFolder.Add(file.album, file.albumParentFolder);
                        }
                    }
                    else
                    {
                        file.folder = Folders.LIEDER;
                    }
                }
                else
                {
                    file.folder = Folders.LIEDER;
                }

                sortedFiles.Add(file);
            }

            return sortedFiles;
        }

        private static string checkParentFolder(string artist, bool various, string album, Folders folder, string targetRoot)
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
        public int songCount;
        public string artist;
        public bool various;

        public AlbumInfo(string artist)
        {
            this.artist = artist;
            various = false;
            songCount = 1;
        }
    }

    
}
