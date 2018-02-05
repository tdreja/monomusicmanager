using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace MonoMusicManager
{
    public partial class MainWindow : Form
    {
        internal List<MusicFile> importedFiles;
        internal string musicDirectory;
        internal string playlistDirectory;
        internal bool AllowOverride;

        //private static MenuItem[] folderMenu = new MenuItem[] { new MenuItem(MusicFolder.GetFolderName(MusicFolder.Folders.ALBUM)), new MenuItem(MusicFolder.GetFolderName(MusicFolder.Folders.FILMMUSIK)), new MenuItem(MusicFolder.GetFolderName(MusicFolder.Folders.PODCASTS)) };

        public MainWindow()
        {
            InitializeComponent();

            buttonCopy.Enabled = false;
            AllowOverride = true;

            musicDirectory = Application.StartupPath;
            playlistDirectory = MusicFolder.GetPath(musicDirectory, MusicFolder.Folders.PLAYLISTS);

            musicFolderField.Text = musicDirectory;
            playlistFolderField.Text = playlistDirectory;
        }

        private void OnFileDrag(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Console.WriteLine("Can do!");
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnFileMusicDrop(object sender, DragEventArgs e)
        {
            //Console.WriteLine("Has " + e.Data.GetDataPresent(DataFormats.FileDrop));

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                /*foreach(string str in e.Data.GetData(DataFormats.FileDrop) as string[])
                {
                    Console.WriteLine("File " + str);
                }*/

                progressBar.Style = ProgressBarStyle.Marquee;
                new Thread(new ImportWorker(e.Data.GetData(DataFormats.FileDrop) as string[], this).Import).Start();
            }
        }
        
        private void OnDirectoryMusicDrop(object sender, DragEventArgs e)
        {
            string folder = GetDirectoryFrom(e);
            if(folder != null)
            {
                musicDirectory = folder;
                musicFolderField.Text = folder;

                if(importedFiles != null && importedFiles.Count != 0)
                {
                    new Thread(new ImportWorker(GetImportedPaths(), this).Import).Start();
                }
            }
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            if(importedFiles.Count > 0)
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                new Thread(new CopyWorker(this).Copy).Start();
            }
        }

        private void OnDirectoryPlaylistDrop(object sender, DragEventArgs e)
        {
            string folder = GetDirectoryFrom(e);
            if(folder != null)
            {
                playlistDirectory = folder;
                playlistFolderField.Text = folder;
                checkBoxPlaylist.Checked = true;
            }
        }

        private string GetDirectoryFrom(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files.Length > 0 && files[0] != null)
                {
                    FileAttributes attr = File.GetAttributes(files[0]);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        DirectoryInfo directory = new DirectoryInfo(files[0]);
                        if (directory.Exists)
                        {
                            return directory.FullName;
                        }
                    }
                }
            }
            return null;
        }

        private string[] GetImportedPaths()
        {
            string[] paths = new string[importedFiles.Count];
            for(int i=0; i < paths.Length; i++)
            {
                paths[i] = importedFiles[i].Source;
            }

            return paths;
        }

        private void OnMusicFolderClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(fbd.SelectedPath);
                    if (dir.Exists)
                    {
                        musicDirectory = dir.FullName;
                        musicFolderField.Text = musicDirectory;

                        if (importedFiles != null && importedFiles.Count != 0)
                        {
                            new Thread(new ImportWorker(GetImportedPaths(), this).Import).Start();
                        }
                    }
                }
            }
        }

        private void OnPlaylistFolderClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(fbd.SelectedPath);
                    if(dir.Exists){
                        playlistDirectory = dir.FullName;
                        playlistFolderField.Text = playlistDirectory;
                        checkBoxPlaylist.Checked = true;
                    }
                }
            }
        }

        private void OnImportFilesClick(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Multiselect = true;
                DialogResult result = fbd.ShowDialog();
                if(result == DialogResult.OK && fbd.FileNames != null && fbd.FileNames.Length > 0)
                {
                    new Thread(new ImportWorker(fbd.FileNames, this).Import).Start();
                }
            }
        }

        private void OnListMouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if (musicFileList.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    if(!musicFileList.FocusedItem.Group.Name.Equals("lieder"))
                    {
                        MenuItem[] items = new MenuItem[4];

                        items[0] = new MenuItem(MusicFolder.GetFolderName(MusicFolder.Folders.ALBUM));
                        items[0].Click += OnClickMenuAlbum;
                        items[1] = new MenuItem(MusicFolder.GetFolderName(MusicFolder.Folders.FILMMUSIK));
                        items[1].Click += OnClickMenuSoundtrack;
                        items[2] = new MenuItem(MusicFolder.GetFolderName(MusicFolder.Folders.PODCASTS));
                        items[2].Click += OnClickMenuPodcast;
                        items[3] = new MenuItem("Edit Folder Name");
                        items[3].Click += OnClickMenuEditName;


                        musicFileList.ContextMenu = new ContextMenu(items);
                    }
                }
            }
        }

        private void OnClickMenuSoundtrack(object sender, EventArgs e)
        {
            MusicFile file = FindMusicFile(musicFileList.FocusedItem);
            if (file != null && file.Folder != MusicFolder.Folders.FILMMUSIK)
            {
                SwitchItemGroup(musicFileList.FocusedItem.Group, MusicFolder.Folders.FILMMUSIK);
            }
        }

        private void SwitchItemGroup(ListViewGroup group, MusicFolder.Folders folder)
        {
            new Thread(new UpdateWorker(group, folder, false, null, null, this).UpdateFolder).Start();
        }

        private void OnClickMenuAlbum(object sender, EventArgs e)
        {
            MusicFile file = FindMusicFile(musicFileList.FocusedItem);
            if (file != null && file.Folder != MusicFolder.Folders.ALBUM)
            {
                SwitchItemGroup(musicFileList.FocusedItem.Group, MusicFolder.Folders.ALBUM);
            }
        }

        private void OnClickMenuPodcast(object sender, EventArgs e)
        {
            //Console.WriteLine("Yey "+ musicFileList.FocusedItem.SubItems[0].Text);
            MusicFile file = FindMusicFile(musicFileList.FocusedItem);
            if(file != null && file.Folder != MusicFolder.Folders.PODCASTS)
            {
                SwitchItemGroup(musicFileList.FocusedItem.Group, MusicFolder.Folders.PODCASTS);
            }
        }

        private void OnClickMenuEditName(object sender, EventArgs e)
        {
            EditNameDialog dialog = new EditNameDialog(this, FindMusicFile(musicFileList.FocusedItem));
            dialog.Show();
        }

        public void OnOverrideFolders(MusicFile currentFile, bool overrideParent, string parentText, bool overrideAlbum, string albumText)
        {
            new Thread(new UpdateWorker(musicFileList.FocusedItem.Group, currentFile.Folder, overrideParent, parentText, overrideAlbum ? albumText : null, this).UpdateParent).Start();
        }
        
        internal MusicFile FindMusicFile(ListViewItem item)
        {
            foreach(MusicFile mf in importedFiles)
            {
                if(mf.Title.Equals(item.SubItems[1].Text) && mf.Artist.Equals(item.SubItems[2].Text))
                {
                    return mf;
                }
            }
            return null;
        }
    }

    class CopyWorker
    {
        private MainWindow window;

        internal CopyWorker(MainWindow window)
        {
            this.window = window;
        }

        internal void Copy()
        {
            Playlist playlist = new Playlist(window.playlistDirectory.Contains(window.musicDirectory));
            MusicFile file;
            string song;

            for(int i=0; i < window.importedFiles.Count; i++)
            {
                window.Invoke((MethodInvoker)delegate
                {
                    window.progressBar.Value = (i*100) / window.importedFiles.Count;
                });

                file = window.importedFiles[i];
                song = null;

                if(file.CanCopy())
                {
                    song = file.CopyToDestination(window.musicDirectory, window.checkBoxOverride.Checked);
                }

                if(song != null)
                {
                    playlist.AddSong(song);
                }
            }

            if(window.checkBoxPlaylist.Checked)
            {
                int index = 0;
                FileInfo info = new FileInfo(Path.Combine(window.playlistDirectory, playlist.Name + ".wpl"));
                while(info.Exists)
                {
                    index++;
                    info = new FileInfo(Path.Combine(window.playlistDirectory, playlist.Name + "_" + index.ToString("000") + ".wpl"));
                }

                if(index != 0)
                {
                    playlist.Name = playlist.Name + "_" + index.ToString("000");
                }

                if(!info.Directory.Exists)
                {
                    Directory.CreateDirectory(info.Directory.FullName);
                }

                //File.CreateText(info.FullName);
                File.WriteAllText(info.FullName, playlist.CreateXMLstring());
            }

            window.Invoke((MethodInvoker)delegate
            {
                window.progressBar.Value = 0;
                window.buttonCopy.Enabled = false;

                MessageBox.Show("Copying "+window.importedFiles.Count+" files finished");
            });
        }
    }

    class UpdateWorker
    {
        private ListViewGroup viewGroup;
        private MusicFolder.Folders folder;
        private string AlternateAlbumFolder;
        private bool overrideParent;
        private string AlternateParentFolder;
        private MainWindow window;

        internal UpdateWorker(ListViewGroup group, MusicFolder.Folders folder, bool overrideParent, string AlternateParent, string AlternateAlbum, MainWindow window)
        {
            this.viewGroup = group;
            this.folder = folder;
            this.window = window;
            this.overrideParent = overrideParent;

            this.AlternateAlbumFolder = AlternateAlbum;
            this.AlternateParentFolder = AlternateParent;
        }

        internal void UpdateParent()
        {
            List<MusicFile> files = new List<MusicFile>();
            foreach (ListViewItem item in viewGroup.Items)
            {
                MusicFile mf = window.FindMusicFile(item);
                window.importedFiles.Remove(mf);
                files.Add(mf);
            }

            files = MusicFolder.AlterParent(files, overrideParent, AlternateParentFolder, AlternateAlbumFolder != null, AlternateAlbumFolder);

            window.importedFiles.AddRange(files.ToArray());

            window.Invoke((MethodInvoker)delegate
            {
                if(files.Count > 0)
                {
                    viewGroup.Header = files[0].HasParent() ? "'" + files[0].GetParent() + "' / '" + files[0].CreateFolderName() + "'" : files[0].CreateFolderName();
                }
                

                window.musicFileList.Refresh();
                /*for (int i = 0; i < window.musicFileList.Columns.Count; i++)
                {
                    window.musicFileList.AutoResizeColumn(i, i != 0 ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent);
                }*/
            });
        }

        internal void UpdateFolder()
        {
            List<MusicFile> files = new List<MusicFile>();
            foreach (ListViewItem item in viewGroup.Items)
            {
                MusicFile mf = window.FindMusicFile(item);
                window.importedFiles.Remove(mf);
                files.Add(mf);
            }

            files = MusicFolder.RegroupFiles(files, window.musicDirectory, folder);

            window.importedFiles.AddRange(files.ToArray());

            window.Invoke((MethodInvoker)delegate
            {
                for(int i=0; i < viewGroup.Items.Count; i++)
                {
                    ListViewItem item = viewGroup.Items[i];
                    MusicFile file = files[i];
                    item.SubItems[0].Text = MusicFolder.GetFolderName(file.Folder);
                }

                window.musicFileList.Refresh();
                for (int i = 0; i < window.musicFileList.Columns.Count; i++)
                {
                    window.musicFileList.AutoResizeColumn(i, i != 0 ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            });
        }
    }

    class ImportWorker
    {
        private string[] files;
        private MainWindow window;

        internal ImportWorker(string[] files, MainWindow window)
        {
            this.files = files;
            this.window = window;
        }

        internal void Import()
        {

            Dictionary<string, ListViewGroup> albumGroups = new Dictionary<string, ListViewGroup>();
            List<ListViewItem> items = new List<ListViewItem>();
            ListViewGroup liederGroup = new ListViewGroup("lieder", "Lieder");

            window.importedFiles = MusicFolder.ReadMusicFiles(window.musicDirectory, files);

            foreach (MusicFile file in window.importedFiles)
            {
                ListViewItem item = new ListViewItem(MusicFolder.GetFolderName(file.Folder));
                item.SubItems.Add(file.Title);
                item.SubItems.Add(file.Artist);
                item.SubItems.Add(file.FormatDiscNr());
                item.SubItems.Add(file.FormatTrackNr());
                item.SubItems.Add(new System.IO.FileInfo(file.Source).Name);
                item.SubItems.Add(file.BiteRate.ToString() + " kbit/s");
                item.SubItems.Add(file.Duration.ToString(@"mm\:ss"));

                if (file.Folder == MusicFolder.Folders.LIEDER)
                {
                    item.Group = liederGroup;
                }
                else
                {
                    if (albumGroups.ContainsKey(file.Album))
                    {
                        item.Group = albumGroups[file.Album];
                    }
                    else
                    {
                        ListViewGroup group = new ListViewGroup(file.Album.ToLower(), file.HasParent() ? "'" + file.GetParent() + "' / '" + file.CreateFolderName() + "'" : file.CreateFolderName());
                        albumGroups.Add(file.Album, group);
                        item.Group = group;
                    }
                }

                items.Add(item);
            }

            window.Invoke((MethodInvoker)delegate
            {
                window.musicFileList.Items.Clear();
                window.musicFileList.Groups.Clear();
                window.musicFileList.Groups.Add(liederGroup);
                foreach (ListViewGroup gr in albumGroups.Values)
                {
                    window.musicFileList.Groups.Add(gr);
                }

                window.musicFileList.Items.AddRange(items.ToArray());

                for (int i = 0; i < window.musicFileList.Columns.Count; i++)
                {
                    window.musicFileList.AutoResizeColumn(i, i != 0 ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                window.buttonCopy.Enabled = window.importedFiles.Count != 0;

                window.progressBar.Style = ProgressBarStyle.Continuous;
                window.progressBar.Value = 0;

                if(window.importedFiles.Count == 0)
                {
                    MessageBox.Show("No valid files found!");
                }
            });
        }
    }
}
