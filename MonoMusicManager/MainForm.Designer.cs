using System.Collections.Generic;

namespace MonoMusicManager
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelMusicFolder = new System.Windows.Forms.Label();
            this.musicFolderField = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.musicFileList = new System.Windows.Forms.ListView();
            this.folderHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.discHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bitRateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.durationHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonMusicFolder = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.labelPlaylistFolder = new System.Windows.Forms.Label();
            this.playlistFolderField = new System.Windows.Forms.TextBox();
            this.checkBoxPlaylist = new System.Windows.Forms.CheckBox();
            this.labelSettings = new System.Windows.Forms.Label();
            this.checkBoxOverride = new System.Windows.Forms.CheckBox();
            this.buttonPlaylistFolder = new System.Windows.Forms.Button();
            this.selectMusicFiles = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelMusicFolder, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.musicFolderField, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.musicFileList, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonMusicFolder, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCopy, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelPlaylistFolder, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.playlistFolderField, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxPlaylist, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelSettings, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxOverride, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonPlaylistFolder, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.selectMusicFiles, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 761);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelMusicFolder
            // 
            this.labelMusicFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMusicFolder.AutoSize = true;
            this.labelMusicFolder.Location = new System.Drawing.Point(3, 8);
            this.labelMusicFolder.Name = "labelMusicFolder";
            this.labelMusicFolder.Size = new System.Drawing.Size(195, 13);
            this.labelMusicFolder.TabIndex = 3;
            this.labelMusicFolder.Text = "Music Folder: ";
            this.labelMusicFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // musicFolderField
            // 
            this.musicFolderField.AllowDrop = true;
            this.musicFolderField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.musicFolderField, 2);
            this.musicFolderField.Location = new System.Drawing.Point(204, 5);
            this.musicFolderField.Name = "musicFolderField";
            this.musicFolderField.ReadOnly = true;
            this.musicFolderField.ShortcutsEnabled = false;
            this.musicFolderField.Size = new System.Drawing.Size(598, 20);
            this.musicFolderField.TabIndex = 0;
            this.musicFolderField.TabStop = false;
            this.musicFolderField.Text = "E:\\";
            this.musicFolderField.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDirectoryMusicDrop);
            this.musicFolderField.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnFileDrag);
            // 
            // progressBar
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar, 3);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 732);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(799, 26);
            this.progressBar.TabIndex = 2;
            // 
            // musicFileList
            // 
            this.musicFileList.AllowDrop = true;
            this.musicFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.folderHeader,
            this.titleHeader,
            this.artistHeader,
            this.discHeader,
            this.trackHeader,
            this.fileHeader,
            this.bitRateHeader,
            this.durationHeader});
            this.tableLayoutPanel1.SetColumnSpan(this.musicFileList, 4);
            this.musicFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.musicFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.musicFileList.GridLines = true;
            this.musicFileList.HoverSelection = true;
            this.musicFileList.LabelEdit = true;
            this.musicFileList.Location = new System.Drawing.Point(3, 93);
            this.musicFileList.MultiSelect = false;
            this.musicFileList.Name = "musicFileList";
            this.musicFileList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.musicFileList.Size = new System.Drawing.Size(1002, 633);
            this.musicFileList.TabIndex = 1;
            this.musicFileList.UseCompatibleStateImageBehavior = false;
            this.musicFileList.View = System.Windows.Forms.View.Details;
            this.musicFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnFileMusicDrop);
            this.musicFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnFileDrag);
            this.musicFileList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnListMouseClick);
            // 
            // folderHeader
            // 
            this.folderHeader.Text = "Folder";
            this.folderHeader.Width = 34;
            // 
            // titleHeader
            // 
            this.titleHeader.Text = "Title";
            this.titleHeader.Width = 38;
            // 
            // artistHeader
            // 
            this.artistHeader.Text = "Artist";
            this.artistHeader.Width = 41;
            // 
            // discHeader
            // 
            this.discHeader.Text = "Disc";
            // 
            // trackHeader
            // 
            this.trackHeader.Text = "Track";
            // 
            // fileHeader
            // 
            this.fileHeader.DisplayIndex = 7;
            this.fileHeader.Text = "File";
            this.fileHeader.Width = 485;
            // 
            // bitRateHeader
            // 
            this.bitRateHeader.DisplayIndex = 5;
            this.bitRateHeader.Text = "Bitrate";
            // 
            // durationHeader
            // 
            this.durationHeader.DisplayIndex = 6;
            this.durationHeader.Text = "Duration";
            // 
            // buttonMusicFolder
            // 
            this.buttonMusicFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMusicFolder.Location = new System.Drawing.Point(808, 3);
            this.buttonMusicFolder.Name = "buttonMusicFolder";
            this.buttonMusicFolder.Size = new System.Drawing.Size(197, 24);
            this.buttonMusicFolder.TabIndex = 4;
            this.buttonMusicFolder.Text = "Change Music Folder";
            this.buttonMusicFolder.UseVisualStyleBackColor = true;
            this.buttonMusicFolder.Click += new System.EventHandler(this.OnMusicFolderClick);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCopy.Location = new System.Drawing.Point(808, 732);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(197, 26);
            this.buttonCopy.TabIndex = 5;
            this.buttonCopy.Text = "Copy Files";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // labelPlaylistFolder
            // 
            this.labelPlaylistFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlaylistFolder.AutoSize = true;
            this.labelPlaylistFolder.Location = new System.Drawing.Point(3, 38);
            this.labelPlaylistFolder.Name = "labelPlaylistFolder";
            this.labelPlaylistFolder.Size = new System.Drawing.Size(195, 13);
            this.labelPlaylistFolder.TabIndex = 6;
            this.labelPlaylistFolder.Text = "Playlist Folder: ";
            this.labelPlaylistFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // playlistFolderField
            // 
            this.playlistFolderField.AllowDrop = true;
            this.playlistFolderField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.playlistFolderField, 2);
            this.playlistFolderField.Location = new System.Drawing.Point(204, 35);
            this.playlistFolderField.Name = "playlistFolderField";
            this.playlistFolderField.ReadOnly = true;
            this.playlistFolderField.ShortcutsEnabled = false;
            this.playlistFolderField.Size = new System.Drawing.Size(598, 20);
            this.playlistFolderField.TabIndex = 7;
            this.playlistFolderField.Text = "F:\\Playlist";
            this.playlistFolderField.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDirectoryPlaylistDrop);
            this.playlistFolderField.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnFileDrag);
            // 
            // checkBoxPlaylist
            // 
            this.checkBoxPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPlaylist.AutoSize = true;
            this.checkBoxPlaylist.Location = new System.Drawing.Point(506, 66);
            this.checkBoxPlaylist.Name = "checkBoxPlaylist";
            this.checkBoxPlaylist.Size = new System.Drawing.Size(296, 17);
            this.checkBoxPlaylist.TabIndex = 8;
            this.checkBoxPlaylist.Text = "Create Playlist";
            this.checkBoxPlaylist.UseVisualStyleBackColor = true;
            // 
            // labelSettings
            // 
            this.labelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSettings.AutoSize = true;
            this.labelSettings.Location = new System.Drawing.Point(3, 68);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(195, 13);
            this.labelSettings.TabIndex = 9;
            this.labelSettings.Text = "Settings: ";
            this.labelSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxOverride
            // 
            this.checkBoxOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOverride.AutoSize = true;
            this.checkBoxOverride.Location = new System.Drawing.Point(204, 66);
            this.checkBoxOverride.Name = "checkBoxOverride";
            this.checkBoxOverride.Size = new System.Drawing.Size(296, 17);
            this.checkBoxOverride.TabIndex = 10;
            this.checkBoxOverride.Text = "Override Files";
            this.checkBoxOverride.UseVisualStyleBackColor = true;
            // 
            // buttonPlaylistFolder
            // 
            this.buttonPlaylistFolder.AutoSize = true;
            this.buttonPlaylistFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPlaylistFolder.Location = new System.Drawing.Point(808, 33);
            this.buttonPlaylistFolder.Name = "buttonPlaylistFolder";
            this.buttonPlaylistFolder.Size = new System.Drawing.Size(197, 24);
            this.buttonPlaylistFolder.TabIndex = 11;
            this.buttonPlaylistFolder.Text = "Change Playlist Folder";
            this.buttonPlaylistFolder.UseVisualStyleBackColor = true;
            this.buttonPlaylistFolder.Click += new System.EventHandler(this.OnPlaylistFolderClick);
            // 
            // selectMusicFiles
            // 
            this.selectMusicFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectMusicFiles.Location = new System.Drawing.Point(808, 63);
            this.selectMusicFiles.Name = "selectMusicFiles";
            this.selectMusicFiles.Size = new System.Drawing.Size(197, 24);
            this.selectMusicFiles.TabIndex = 12;
            this.selectMusicFiles.Text = "Import Music Files";
            this.selectMusicFiles.UseVisualStyleBackColor = true;
            this.selectMusicFiles.Click += new System.EventHandler(this.OnImportFilesClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainWindow";
            this.Text = "Music Manager V9";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.ListView musicFileList;
        private System.Windows.Forms.ColumnHeader folderHeader;
        private System.Windows.Forms.ColumnHeader titleHeader;
        private System.Windows.Forms.ColumnHeader artistHeader;
        private System.Windows.Forms.ColumnHeader discHeader;
        private System.Windows.Forms.ColumnHeader trackHeader;
        private System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.TextBox musicFolderField;
        internal System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelMusicFolder;
        private System.Windows.Forms.Button buttonMusicFolder;
        internal System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label labelPlaylistFolder;
        internal System.Windows.Forms.CheckBox checkBoxPlaylist;
        internal System.Windows.Forms.TextBox playlistFolderField;
        private System.Windows.Forms.Label labelSettings;
        private System.Windows.Forms.Button buttonPlaylistFolder;
        internal System.Windows.Forms.CheckBox checkBoxOverride;
        private System.Windows.Forms.Button selectMusicFiles;
        private System.Windows.Forms.ColumnHeader bitRateHeader;
        private System.Windows.Forms.ColumnHeader durationHeader;

    }
}

