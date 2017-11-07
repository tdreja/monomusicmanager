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
            this.musicFileList = new System.Windows.Forms.ListView();
            this.fileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.discHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderField = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.folderLabel = new System.Windows.Forms.Label();
            this.buttonFolder = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.musicFileList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.folderField, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.folderLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonFolder, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCopy, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(782, 553);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.tableLayoutPanel1.SetColumnSpan(this.musicFileList, 3);
            this.musicFileList.Cursor = System.Windows.Forms.Cursors.Default;
            this.musicFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.musicFileList.GridLines = true;
            this.musicFileList.HoverSelection = true;
            this.musicFileList.LabelEdit = true;
            this.musicFileList.Location = new System.Drawing.Point(3, 47);
            this.musicFileList.MaximumSize = new System.Drawing.Size(782, 533);
            this.musicFileList.Name = "musicFileList";
            this.musicFileList.Size = new System.Drawing.Size(776, 458);
            this.musicFileList.TabIndex = 1;
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
            // folderField
            // 
            this.folderField.AllowDrop = true;
            this.folderField.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.folderField.Location = new System.Drawing.Point(159, 11);
            this.folderField.Name = "folderField";
            this.folderField.ReadOnly = true;
            this.folderField.ShortcutsEnabled = false;
            this.folderField.Size = new System.Drawing.Size(463, 22);
            this.folderField.TabIndex = 0;
            this.folderField.TabStop = false;
            this.folderField.Text = "E:\\";
            this.folderField.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnFolderDrop);
            this.folderField.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnFolderDrag);
            // 
            // progressBar
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar, 2);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 511);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(619, 39);
            this.progressBar.TabIndex = 2;
            // 
            // folderLabel
            // 
            this.folderLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.folderLabel.AutoSize = true;
            this.folderLabel.Location = new System.Drawing.Point(57, 13);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(96, 17);
            this.folderLabel.TabIndex = 3;
            this.folderLabel.Text = "Music Folder: ";
            // 
            // buttonFolder
            // 
            this.buttonFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonFolder.Enabled = false;
            this.buttonFolder.Location = new System.Drawing.Point(628, 3);
            this.buttonFolder.Name = "buttonFolder";
            this.buttonFolder.Size = new System.Drawing.Size(151, 38);
            this.buttonFolder.TabIndex = 4;
            this.buttonFolder.Text = "Change Music Folder";
            this.buttonFolder.UseVisualStyleBackColor = true;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCopy.Location = new System.Drawing.Point(628, 511);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(151, 39);
            this.buttonCopy.TabIndex = 5;
            this.buttonCopy.Text = "Copy Files";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // MainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.Text = "Music Manager V5";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.ListView musicFileList;
        private System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.ColumnHeader titleHeader;
        private System.Windows.Forms.ColumnHeader artistHeader;
        private System.Windows.Forms.ColumnHeader discHeader;
        private System.Windows.Forms.ColumnHeader trackHeader;
        private System.Windows.Forms.ColumnHeader folderHeader;
        private System.Windows.Forms.TextBox folderField;
        internal System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label folderLabel;
        private System.Windows.Forms.Button buttonFolder;
        internal System.Windows.Forms.Button buttonCopy;
    }
}

