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
            this.musicFileList = new System.Windows.Forms.ListView();
            this.fileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.discHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // musicFileList
            // 
            this.musicFileList.AllowDrop = true;
            this.musicFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileHeader,
            this.titleHeader,
            this.artistHeader,
            this.discHeader,
            this.trackHeader,
            this.folderHeader});
            this.musicFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.musicFileList.GridLines = true;
            this.musicFileList.HoverSelection = true;
            this.musicFileList.LabelEdit = true;
            this.musicFileList.Location = new System.Drawing.Point(0, 0);
            this.musicFileList.MaximumSize = new System.Drawing.Size(782, 533);
            this.musicFileList.Name = "musicFileList";
            this.musicFileList.Size = new System.Drawing.Size(782, 533);
            this.musicFileList.TabIndex = 0;
            this.musicFileList.UseCompatibleStateImageBehavior = false;
            this.musicFileList.View = System.Windows.Forms.View.Details;
            this.musicFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnMusicItemDrop);
            this.musicFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnMusicItemDrag);
            // 
            // fileHeader
            // 
            this.fileHeader.Text = "File";
            this.fileHeader.Width = 34;
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
            // folderHeader
            // 
            this.folderHeader.Text = "Folder";
            this.folderHeader.Width = 485;
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.musicFileList);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.Text = "Music Manager V5";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.ColumnHeader titleHeader;
        private System.Windows.Forms.ColumnHeader artistHeader;
        private System.Windows.Forms.ColumnHeader folderHeader;
        private System.Windows.Forms.ColumnHeader discHeader;
        private System.Windows.Forms.ColumnHeader trackHeader;
        internal System.Windows.Forms.ListView musicFileList;
    }
}

