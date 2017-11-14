using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonoMusicManager
{
    public partial class EditNameDialog : Form
    {
        private MainWindow mainWindow;
        private MusicFile currentFile;

        public EditNameDialog(MainWindow mainWindow, MusicFile musicFile)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            currentFile = musicFile;

            checkOverrideParent.Checked = currentFile.AlternateParentFolder != null;
            parentFolderText.Text = currentFile.GetParent();
            parentFolderText.ReadOnly = !checkOverrideParent.Checked;

            checkOverrideAlbum.Checked = currentFile.AlternateAlbumFolder != null;
            albumFolderText.Text = currentFile.CreateFolderName();
            albumFolderText.ReadOnly = !checkOverrideAlbum.Checked;

            
        }

        private void OnOverrideParentChanged(object sender, EventArgs e)
        {
            parentFolderText.ReadOnly = !checkOverrideParent.Checked;
            parentFolderText.Text = currentFile.AlbumParentFolder;
        }

        private void OnOverrideAlbumChanged(object sender, EventArgs e)
        {
            albumFolderText.ReadOnly = !checkOverrideAlbum.Checked;
            albumFolderText.Text = currentFile.CreateFolderName();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            checkOverrideAlbum.Checked = false;
            checkOverrideParent.Checked = false;
            
            parentFolderText.Text = currentFile.AlbumParentFolder;
            parentFolderText.ReadOnly = true;

            albumFolderText.Text = currentFile.CreateFolderName();
            albumFolderText.ReadOnly = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            mainWindow.OnOverrideFolders(currentFile, checkOverrideParent.Checked, parentFolderText.Text, checkOverrideAlbum.Checked, albumFolderText.Text);
            Close();
        }
    }
}
