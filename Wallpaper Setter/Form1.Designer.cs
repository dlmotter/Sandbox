﻿namespace Wallpaper_Setter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.filterDdl = new System.Windows.Forms.ComboBox();
            this.categoryDdl = new System.Windows.Forms.ComboBox();
            this.typeDdl = new System.Windows.Forms.ComboBox();
            this.filterLbl = new System.Windows.Forms.Label();
            this.categoryLbl = new System.Windows.Forms.Label();
            this.typeLbl = new System.Windows.Forms.Label();
            this.frequencyLbl = new System.Windows.Forms.Label();
            this.frequencyDdl = new System.Windows.Forms.ComboBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.fileTb = new System.Windows.Forms.TextBox();
            this.fileLbl = new System.Windows.Forms.Label();
            this.fileBtn = new System.Windows.Forms.Button();
            this.keepFileDdl = new System.Windows.Forms.ComboBox();
            this.keepFileLbl = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.infoLbl = new System.Windows.Forms.Label();
            this.linkLbl = new System.Windows.Forms.LinkLabel();
            this.queryTb = new System.Windows.Forms.TextBox();
            this.queryLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filterDdl
            // 
            this.filterDdl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterDdl.FormattingEnabled = true;
            this.filterDdl.Items.AddRange(new object[] {
            "Recent",
            "Popular (Last 24 Hours)",
            "Shuffle"});
            this.filterDdl.Location = new System.Drawing.Point(79, 66);
            this.filterDdl.Name = "filterDdl";
            this.filterDdl.Size = new System.Drawing.Size(442, 21);
            this.filterDdl.TabIndex = 3;
            // 
            // categoryDdl
            // 
            this.categoryDdl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDdl.FormattingEnabled = true;
            this.categoryDdl.Items.AddRange(new object[] {
            "Animals",
            "Architecture",
            "Bikes",
            "Cars",
            "Cartoon/Comic",
            "CityScape",
            "Computer/Tech",
            "Digital/Artwork",
            "Dual Monitor",
            "Fantasy",
            "Food/Drink",
            "Girls",
            "Guns",
            "Holiday",
            "Humor",
            "Industrial",
            "Informational",
            "Insects",
            "iPhone/Mobile",
            "Love/Hate",
            "Misc",
            "Movies",
            "Music",
            "Nature",
            "People",
            "Quotes/Worded",
            "Sci-Fi",
            "Science",
            "Space",
            "Sports",
            "Television",
            "Video Games",
            "Vintage/Historical",
            "War/Military"});
            this.categoryDdl.Location = new System.Drawing.Point(79, 39);
            this.categoryDdl.Name = "categoryDdl";
            this.categoryDdl.Size = new System.Drawing.Size(442, 21);
            this.categoryDdl.TabIndex = 2;
            // 
            // typeDdl
            // 
            this.typeDdl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeDdl.FormattingEnabled = true;
            this.typeDdl.Items.AddRange(new object[] {
            "Wallpaper of the Day",
            "Category",
            "Shuffle",
            "Search"});
            this.typeDdl.Location = new System.Drawing.Point(79, 12);
            this.typeDdl.Name = "typeDdl";
            this.typeDdl.Size = new System.Drawing.Size(442, 21);
            this.typeDdl.TabIndex = 1;
            this.typeDdl.SelectedIndexChanged += new System.EventHandler(this.typeDdl_SelectedIndexChanged);
            // 
            // filterLbl
            // 
            this.filterLbl.AutoSize = true;
            this.filterLbl.Location = new System.Drawing.Point(12, 69);
            this.filterLbl.Name = "filterLbl";
            this.filterLbl.Size = new System.Drawing.Size(29, 13);
            this.filterLbl.TabIndex = 0;
            this.filterLbl.Text = "Filter";
            // 
            // categoryLbl
            // 
            this.categoryLbl.AutoSize = true;
            this.categoryLbl.Location = new System.Drawing.Point(12, 42);
            this.categoryLbl.Name = "categoryLbl";
            this.categoryLbl.Size = new System.Drawing.Size(49, 13);
            this.categoryLbl.TabIndex = 0;
            this.categoryLbl.Text = "Category";
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Location = new System.Drawing.Point(12, 15);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(31, 13);
            this.typeLbl.TabIndex = 0;
            this.typeLbl.Text = "Type";
            // 
            // frequencyLbl
            // 
            this.frequencyLbl.AutoSize = true;
            this.frequencyLbl.Location = new System.Drawing.Point(12, 96);
            this.frequencyLbl.Name = "frequencyLbl";
            this.frequencyLbl.Size = new System.Drawing.Size(57, 13);
            this.frequencyLbl.TabIndex = 4;
            this.frequencyLbl.Text = "Frequency";
            // 
            // frequencyDdl
            // 
            this.frequencyDdl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frequencyDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frequencyDdl.FormattingEnabled = true;
            this.frequencyDdl.Items.AddRange(new object[] {
            "Day",
            "Hour",
            "Minute"});
            this.frequencyDdl.Location = new System.Drawing.Point(79, 93);
            this.frequencyDdl.Name = "frequencyDdl";
            this.frequencyDdl.Size = new System.Drawing.Size(442, 21);
            this.frequencyDdl.TabIndex = 5;
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.BackColor = System.Drawing.Color.PaleGreen;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.ForeColor = System.Drawing.Color.DarkGreen;
            this.saveBtn.Location = new System.Drawing.Point(446, 183);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // fileTb
            // 
            this.fileTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTb.Enabled = false;
            this.fileTb.Location = new System.Drawing.Point(157, 120);
            this.fileTb.Name = "fileTb";
            this.fileTb.ReadOnly = true;
            this.fileTb.Size = new System.Drawing.Size(364, 20);
            this.fileTb.TabIndex = 7;
            // 
            // fileLbl
            // 
            this.fileLbl.AutoSize = true;
            this.fileLbl.Location = new System.Drawing.Point(12, 123);
            this.fileLbl.Name = "fileLbl";
            this.fileLbl.Size = new System.Drawing.Size(23, 13);
            this.fileLbl.TabIndex = 8;
            this.fileLbl.Text = "File";
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(78, 119);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(73, 22);
            this.fileBtn.TabIndex = 9;
            this.fileBtn.Text = "Choose...";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.fileBtn_Click);
            // 
            // keepFileDdl
            // 
            this.keepFileDdl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keepFileDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keepFileDdl.FormattingEnabled = true;
            this.keepFileDdl.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.keepFileDdl.Location = new System.Drawing.Point(79, 147);
            this.keepFileDdl.Name = "keepFileDdl";
            this.keepFileDdl.Size = new System.Drawing.Size(442, 21);
            this.keepFileDdl.TabIndex = 10;
            // 
            // keepFileLbl
            // 
            this.keepFileLbl.AutoSize = true;
            this.keepFileLbl.Location = new System.Drawing.Point(12, 150);
            this.keepFileLbl.Name = "keepFileLbl";
            this.keepFileLbl.Size = new System.Drawing.Size(57, 13);
            this.keepFileLbl.TabIndex = 11;
            this.keepFileLbl.Text = "Keep File?";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "bmp";
            this.saveFileDialog1.Filter = "Bitmap Files (*.bmp)|*.bmp";
            // 
            // deleteBtn
            // 
            this.deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteBtn.BackColor = System.Drawing.Color.LightCoral;
            this.deleteBtn.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteBtn.ForeColor = System.Drawing.Color.Maroon;
            this.deleteBtn.Location = new System.Drawing.Point(365, 183);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 12;
            this.deleteBtn.Text = "Stop";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.infoLbl.Location = new System.Drawing.Point(12, 188);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(123, 13);
            this.infoLbl.TabIndex = 13;
            this.infoLbl.Text = "All wallpapers come from";
            // 
            // linkLbl
            // 
            this.linkLbl.AutoSize = true;
            this.linkLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLbl.Location = new System.Drawing.Point(132, 188);
            this.linkLbl.Name = "linkLbl";
            this.linkLbl.Size = new System.Drawing.Size(90, 13);
            this.linkLbl.TabIndex = 14;
            this.linkLbl.TabStop = true;
            this.linkLbl.Text = "thepaperwall.com";
            this.linkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_LinkClicked);
            // 
            // queryTb
            // 
            this.queryTb.Location = new System.Drawing.Point(78, 39);
            this.queryTb.Name = "queryTb";
            this.queryTb.Size = new System.Drawing.Size(443, 20);
            this.queryTb.TabIndex = 15;
            // 
            // queryLbl
            // 
            this.queryLbl.AutoSize = true;
            this.queryLbl.Location = new System.Drawing.Point(12, 42);
            this.queryLbl.Name = "queryLbl";
            this.queryLbl.Size = new System.Drawing.Size(35, 13);
            this.queryLbl.TabIndex = 16;
            this.queryLbl.Text = "Query";
            // 
            // Form1
            // 
            this.AcceptButton = this.saveBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 218);
            this.Controls.Add(this.queryLbl);
            this.Controls.Add(this.queryTb);
            this.Controls.Add(this.linkLbl);
            this.Controls.Add(this.infoLbl);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.keepFileLbl);
            this.Controls.Add(this.keepFileDdl);
            this.Controls.Add(this.fileBtn);
            this.Controls.Add(this.fileLbl);
            this.Controls.Add(this.fileTb);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.frequencyDdl);
            this.Controls.Add(this.frequencyLbl);
            this.Controls.Add(this.filterDdl);
            this.Controls.Add(this.filterLbl);
            this.Controls.Add(this.categoryDdl);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.typeDdl);
            this.Controls.Add(this.categoryLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(549, 257);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(549, 257);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wallpaper Setter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label typeLbl;
        private System.Windows.Forms.Label filterLbl;
        private System.Windows.Forms.Label categoryLbl;
        private System.Windows.Forms.ComboBox filterDdl;
        private System.Windows.Forms.ComboBox categoryDdl;
        private System.Windows.Forms.ComboBox typeDdl;
        private System.Windows.Forms.Label frequencyLbl;
        private System.Windows.Forms.ComboBox frequencyDdl;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox fileTb;
        private System.Windows.Forms.Label fileLbl;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.ComboBox keepFileDdl;
        private System.Windows.Forms.Label keepFileLbl;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Label infoLbl;
        private System.Windows.Forms.LinkLabel linkLbl;
        private System.Windows.Forms.TextBox queryTb;
        private System.Windows.Forms.Label queryLbl;
    }
}

