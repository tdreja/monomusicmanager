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
        internal string mainFolder = "C:\\Users\\thoma\\Music\\";
        internal bool AllowOverride;

        public MainWindow()
        {
            InitializeComponent();

            buttonCopy.Enabled = false;
            AllowOverride = true;

            mainFolder = Application.StartupPath;
            folderField.Text = mainFolder;
        }

        private void OnMusicItemDrag(object sender, DragEventArgs e)
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

        private void OnMusicItemDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                progressBar.Style = ProgressBarStyle.Marquee;
                new Thread(new ImportWorker(e.Data.GetData(DataFormats.FileDrop) as string[], this).Import).Start();
            }
        }

        private void OnFolderDrag(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnFolderDrop(object sender, DragEventArgs e)
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
                            mainFolder = directory.FullName;
                            folderField.Text = mainFolder;
                        }
                    }
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
            Playlist playlist = new Playlist();
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
                    song = file.CopyToDestination(window.mainFolder, window.AllowOverride);
                }

                if(song != null)
                {
                    playlist.AddSong(song);
                }
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

            window.importedFiles = MusicFolder.ReadMusicFiles(window.mainFolder, files);

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
