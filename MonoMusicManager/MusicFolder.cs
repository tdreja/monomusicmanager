using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MonoMusicManager
{
    #region Folder & AlbumInfo

    /// <summary>
    /// The music collection is split into different subfolders: For albums, playlists, single songs etc.
    /// This class handles sorting music files into this structure
    /// </summary>
    public static class MusicFolder
    {
        /// <summary>
        /// This constant defines how many songs an album must contain to be counted as an album
        /// </summary>
        public const int MINIMUM_ALBUM_SONG_NUMBER = 3;

        public static string[] FILE_EXTENSIONS = new string[] { ".mp3", ".ogg", ".m4a" };

        /// <summary>
        /// This enum represents the different subfolders the music collection is split into
        /// </summary>
        public enum Folders
        {
            NONE,
            ALBUM,
            LIEDER,
            FILMMUSIK,
            PODCASTS,
            PLAYLISTS
        }

        /// <summary>
        /// Adds the given folder onto the given base path
        /// </summary>
        /// <param name="basePath">Root folder for the music collection</param>
        /// <param name="folder">Folder that is to be added</param>
        /// <returns>New path with the given folder added onto the base</returns>
        public static string GetPath(string basePath, Folders folder)
        {
            return Path.Combine(basePath, GetFolderName(folder));
        }

        /// <summary>
        /// This method assigns actual names to the enum values
        /// </summary>
        /// <param name="folder">Enum value of a subfolder</param>
        /// <returns>Name of a subfolder</returns>
        public static string GetFolderName(Folders folder)
        {
            switch(folder)
            {
                case Folders.ALBUM:
                    return "Album";
                case Folders.LIEDER:
                    return "Lieder";
                case Folders.FILMMUSIK:
                    return "Soundtracks";
                case Folders.PLAYLISTS:
                    return "Playlists";
                case Folders.PODCASTS:
                    return "Podcasts - Hörbücher";
                default:
                    return "NoFolder";
            }
        }

        /// <summary>
        /// Returns a list of all subfolder names (not the enum, but the actual string names!)
        /// </summary>
        /// <returns>Returns a list of all subfolder names</returns>
        public static List<string> GetAllFolderNames()
        {
            List<string> names = new List<string>();

            foreach (Folders fold in Enum.GetValues(typeof(Folders))) {
                names.Add(GetFolderName(fold));
            }

            return names;
        }

        /// <summary>
        /// This method receives a file drag, loads all music files from it and (pre-)sorts them into the target path. 
        /// Note: This will not copy the files!
        /// </summary>
        /// <param name="targetPath">Path for the music collection</param>
        /// <param name="dragEvent">Files dragged in by the user</param>
        /// <returns>A list of all (sorted) music files read from the file drag</returns>
        public static List<MusicFile> ReadMusicFiles(string targetPath, DragEventArgs dragEvent)
        {
            return ReadMusicFiles(targetPath, dragEvent.Data.GetData(DataFormats.FileDrop) as string[]);
        }

        /// <summary>
        /// This method receives a list of music files, reads them and (pre-)sorts them into the target path.
        /// Note: This will not copy the files!
        /// </summary>
        /// <param name="targetPath">Path for the music collection</param>
        /// <param name="filePaths">File paths from the user/system</param>
        /// <returns>A list of all (sorted) music files based upon the given files</returns>
        public static List<MusicFile> ReadMusicFiles(string targetPath, params string[] filePaths)
        {
            //Console.WriteLine(filePaths);

            List<MusicFile> musicFiles = new List<MusicFile>();

            foreach(string path in filePaths)
            {
                FileAttributes attr = File.GetAttributes(path);
                if(attr.HasFlag(FileAttributes.Directory))
                {
                    CheckDirectory(new DirectoryInfo(path), musicFiles);
                }
                else
                {
                    CheckPath(path, musicFiles);
                }
            }
            
            return SortFiles(targetPath, musicFiles.ToArray());
        }

        private static void CheckDirectory(DirectoryInfo directory, List<MusicFile> musicFiles)
        {
            foreach(FileInfo file in directory.EnumerateFiles())
            {
                CheckPath(file.FullName, musicFiles);
            }

            foreach(DirectoryInfo dir in directory.EnumerateDirectories())
            {
                CheckDirectory(dir, musicFiles);
            }
        }

        /// <summary>
        /// This method checks a given file and if it exists and is a music file, adds it to the given list
        /// Note: This uses the MusicFile constructor and thus will also imported the ID3 tags of a valid file
        /// </summary>
        /// <param name="filePath">File to be checked</param>
        /// <param name="otherMusicFiles">List of already checked and imported music files</param>
        private static void CheckPath(string filePath, List<MusicFile> otherMusicFiles)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Exists && CheckExtension(info.Extension))
            {
                otherMusicFiles.Add(new MusicFile(filePath));
            }
        } 

        /// <summary>
        /// Checks whether the given extension matches any of the valid music file extensions for this program
        /// </summary>
        /// <param name="extension">Extension from a file</param>
        /// <returns>True: This program can handle the file, False: This file cannot be handled here</returns>
        private static bool CheckExtension(string extension)
        {
            foreach(string ext in FILE_EXTENSIONS)
            {
                if(ext.Equals(extension))
                {
                    return true;
                }
            }
            return false;
        }

        #region Sorting

        /// <summary>
        /// This method sorts the given music files into the collection at the given root folder.
        /// Note: This includes checking for albums, artists, parent folders etc.
        /// </summary>
        /// <param name="targetFolder">Root folder of the music collection</param>
        /// <param name="unsortedFiles">Music files to be sorted</param>
        /// <returns>A list of music files sorted into the collection</returns>
        public static List<MusicFile> SortFiles(string targetFolder, params MusicFile[] unsortedFiles)
        {
            List<MusicFile> sortedFiles = new List<MusicFile>();

            Dictionary<string, AlbumInfo> albumInfos = new Dictionary<string, AlbumInfo>();

            // Group in albums and check how many songs are in each album
            foreach(MusicFile file in unsortedFiles)
            {
                if(file.IsValid() && file.Album != null)
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

                        bool isPodcast = CheckForPodcast(file);
                        if (albumInfos.ContainsKey(file.Album) && (albumInfos[file.Album].SongCount >= MINIMUM_ALBUM_SONG_NUMBER || isPodcast))
                        {
                            if(isPodcast)
                            {
                                file.Folder = Folders.PODCASTS;
                            }
                            else if (CheckForSoundtrack(file))
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

        /// <summary>
        /// Returns whether this file should be sorted into soundtracks instead of albums
        /// </summary>
        /// <param name="file">File to be checked</param>
        /// <returns>True: Should be a soundtrack, False: Not a soundtrack</returns>
        private static bool CheckForSoundtrack(MusicFile file)
        {
            return file.Genre != null && (file.Genre.Contains("Soundtrack") || file.Genre.Contains("Showtune") || file.Genre.Contains("Musical"));
        }

        /// <summary>
        /// Returns whether this file should be sorted into podcasts instead of albums
        /// </summary>
        /// <param name="file">File to be checked</param>
        /// <returns>True: Should be a podcast, False: Not a podcast</returns>
        private static bool CheckForPodcast(MusicFile file)
        {
            return file.Genre != null && (file.Genre.Contains("Podcast") || file.Genre.Contains("Audiobook") || file.Genre.Contains("Speech"));
        }

        /// <summary>
        /// This method checks whether a potential parent folder for the album already exists in the music collection or not.
        /// </summary>
        /// <param name="artist">Artist of the Album</param>
        /// <param name="various">Is this a various artists album (True) or a single artist (False)</param>
        /// <param name="album">Album name</param>
        /// <param name="folder">Folder the album was sorted into</param>
        /// <param name="targetRoot">Root of the music collection</param>
        /// <returns>The potential parent folder for the album (empty string if no parent was found)</returns>
        private static string CheckParentFolder(string artist, bool various, string album, Folders folder, string targetRoot)
        {
            string mainPath = GetPath(targetRoot, folder);

            if (various)
            {
                string[] parts = album.Split(' ');
                StringBuilder builder = new StringBuilder();

                for (int i = 1; i < parts.Length; i++)
                {
                    builder.Clear();

                    for (int j = 0; j < parts.Length - i; j++)
                    {
                        builder.Append(parts[j]);
                        if (j < parts.Length - 1 - i)
                        {
                            builder.Append(" ");
                        }
                    }

                    if (System.IO.Directory.Exists(Path.Combine(mainPath, builder.ToString())))
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

        #endregion

        /// <summary>
        /// Resorts the given music files into the new folder within the music collection given by mainFolder
        /// </summary>
        /// <param name="oldFiles">Files to be resorted</param>
        /// <param name="mainFolder">Root folder of the music collection</param>
        /// <param name="newFolder">New folder the files should now be in</param>
        /// <returns>A list of resorted files</returns>
        public static List<MusicFile> RegroupFiles(List<MusicFile> oldFiles, string mainFolder, Folders newFolder)
        {
            List<MusicFile> newList = new List<MusicFile>();

            if(oldFiles.Count != 0)
            {
                MusicFile first = oldFiles[0];
                string newParent = CheckParentFolder(first.Artist, first.HasVariousArtists, first.Album, newFolder, mainFolder);

                foreach(MusicFile file in oldFiles)
                {
                    file.AlbumParentFolder = newParent;
                    file.Folder = newFolder;
                    newList.Add(file);
                }
            }

            return newList;
        }

        /// <summary>
        /// This method alters the parent folder and album folder name of the given music files (all should be in one album). This method is used to allow users to override the default sorting.
        /// </summary>
        /// <param name="files">Files that will be altered</param>
        /// <param name="overrideParent">Should the parent folder be altered?</param>
        /// <param name="parentFolder">New name of the parent folder</param>
        /// <param name="overrideAlbum">Should the album name be altered? (Only affects the folder name, not the tag!)</param>
        /// <param name="albumFolder">New name of the album (Only affects the folder name, not the tag!)</param>
        /// <returns></returns>
        public static List<MusicFile> AlterParent(List<MusicFile> files, bool overrideParent, string parentFolder, bool overrideAlbum, string albumFolder)
        {
            foreach(MusicFile file in files)
            {
                if(overrideAlbum && albumFolder != null && albumFolder.Length > 0)
                {
                    file.AlternateAlbumFolder = albumFolder;
                }
                else
                {
                    file.AlternateAlbumFolder = null;
                }

                if(overrideParent && parentFolder != null && parentFolder.Length > 0)
                {
                    file.AlternateParentFolder = parentFolder;
                }
                else
                {
                    file.AlternateParentFolder = null;
                }
            }

            return files;
        }
    }

    #endregion

}
