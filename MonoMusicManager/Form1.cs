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

        public MainWindow()
        {
            InitializeComponent();
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
            //Console.WriteLine("Has " + e.Data.GetDataPresent(DataFormats.FileDrop));
            
            //musicFileList.Groups.Add(liederGroup);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                /*importedFiles = MusicFolder.ReadMusicFiles(mainFolder, e);

                foreach (MusicFile file in importedFiles)
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
                        if(albumGroups.ContainsKey(file.Album))
                        {
                            item.Group = albumGroups[file.Album];
                        }
                        else
                        {
                            ListViewGroup group = new ListViewGroup(file.Album.ToLower(), file.HasParent() ? "'" + file.AlbumParentFolder + "' / '" + file.Album+ "'" : file.Album);
                            albumGroups.Add(file.Album, group);
                            musicFileList.Groups.Add(group);
                            item.Group = group;
                        }
                    }

                    musicFileList.Invoke((MethodInvoker)delegate
                    {
                        musicFileList.Items.Add(item);
                    });

                }*/

                new Thread(new ImportWorker(e.Data.GetData(DataFormats.FileDrop) as string[], this).Import).Start();
            }
            
            
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
            /*window.musicFileList.Invoke((MethodInvoker)delegate
            {
                window.musicFileList.Items.Clear();
                window.musicFileList.Groups.Clear();
            });*/

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
                /*window.musicFileList.Invoke((MethodInvoker)delegate
                {
                    window.musicFileList.Items.Add(item);
                });*/
            }

            window.musicFileList.Invoke((MethodInvoker)delegate
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
            });
        }
    }
}
