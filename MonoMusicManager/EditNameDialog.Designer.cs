namespace MonoMusicManager
{
    partial class EditNameDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkOverrideParent = new System.Windows.Forms.CheckBox();
            this.checkOverrideAlbum = new System.Windows.Forms.CheckBox();
            this.parentFolderText = new System.Windows.Forms.TextBox();
            this.albumFolderText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.checkOverrideParent, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkOverrideAlbum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.parentFolderText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.albumFolderText, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.resetButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 161);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // checkOverrideParent
            // 
            this.checkOverrideParent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkOverrideParent.AutoSize = true;
            this.checkOverrideParent.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkOverrideParent.Location = new System.Drawing.Point(88, 7);
            this.checkOverrideParent.Name = "checkOverrideParent";
            this.checkOverrideParent.Size = new System.Drawing.Size(193, 17);
            this.checkOverrideParent.TabIndex = 0;
            this.checkOverrideParent.Text = "Overide Parent Folder";
            this.checkOverrideParent.UseVisualStyleBackColor = true;
            this.checkOverrideParent.CheckedChanged += new System.EventHandler(this.OnOverrideParentChanged);
            // 
            // checkOverrideAlbum
            // 
            this.checkOverrideAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkOverrideAlbum.AutoSize = true;
            this.checkOverrideAlbum.Location = new System.Drawing.Point(88, 71);
            this.checkOverrideAlbum.Name = "checkOverrideAlbum";
            this.checkOverrideAlbum.Size = new System.Drawing.Size(193, 17);
            this.checkOverrideAlbum.TabIndex = 1;
            this.checkOverrideAlbum.Text = "Override Album Folder";
            this.checkOverrideAlbum.UseVisualStyleBackColor = true;
            this.checkOverrideAlbum.CheckedChanged += new System.EventHandler(this.OnOverrideAlbumChanged);
            // 
            // parentFolderText
            // 
            this.parentFolderText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.parentFolderText.Location = new System.Drawing.Point(88, 38);
            this.parentFolderText.Name = "parentFolderText";
            this.parentFolderText.ReadOnly = true;
            this.parentFolderText.Size = new System.Drawing.Size(193, 20);
            this.parentFolderText.TabIndex = 2;
            // 
            // albumFolderText
            // 
            this.albumFolderText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.albumFolderText.Location = new System.Drawing.Point(88, 102);
            this.albumFolderText.Name = "albumFolderText";
            this.albumFolderText.ReadOnly = true;
            this.albumFolderText.Size = new System.Drawing.Size(193, 20);
            this.albumFolderText.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.tableLayoutPanel1.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Parent Folder";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 89);
            this.label3.Name = "label3";
            this.tableLayoutPanel1.SetRowSpan(this.label3, 2);
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Album Folder";
            // 
            // resetButton
            // 
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetButton.Location = new System.Drawing.Point(3, 131);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(79, 27);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(88, 131);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(193, 27);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // EditNameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditNameDialog";
            this.Text = "Edit Name";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkOverrideParent;
        private System.Windows.Forms.CheckBox checkOverrideAlbum;
        private System.Windows.Forms.TextBox parentFolderText;
        private System.Windows.Forms.TextBox albumFolderText;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button resetButton;
        internal System.Windows.Forms.Button saveButton;
    }
}