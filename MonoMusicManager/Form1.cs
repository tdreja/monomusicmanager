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
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnFileMusicDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
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
                    string folder = Directory.GetFiles(fbd.SelectedPath)[0];
                    if (folder != null)
                    {
                        FileInfo info = new FileInfo(folder);
                        musicDirectory = info.Directory.FullName;
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
                    string folder = Directory.GetFiles(fbd.SelectedPath)[0];
                    if (folder != null)
                    {
                        FileInfo info = new FileInfo(folder);
                        playlistDirectory = info.Directory.FullName;
                        playlistFolderField.Text = playlistDirectory;
                        checkBoxPlaylist.Checked = true;
                    }
                }
            }
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
                ListViewItem item = new ListViewItem(new System.IO.FileInfo(file.Source).Name);
                item.SubItems.Add(file.Title);
                item.SubItems.Add(file.Artist);
                item.SubItems.Add(file.FormatDiscNr());
                item.SubItems.Add(file.FormatTrackNr());
                item.SubItems.Add(MusicFolder.GetFolderName(file.Folder));

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
                        ListViewGroup group = new ListViewGroup(file.Album.ToLower(), file.HasParent() ? "'" + file.AlbumParentFolder + "' / '" + file.Album + "'" : file.Album);
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
