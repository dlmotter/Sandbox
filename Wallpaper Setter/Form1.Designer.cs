namespace Wallpaper_Setter
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
            "Popular"});
            this.filterDdl.Location = new System.Drawing.Point(79, 66);
            this.filterDdl.Name = "filterDdl";
            this.filterDdl.Size = new System.Drawing.Size(300, 21);
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
            this.categoryDdl.Size = new System.Drawing.Size(300, 21);
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
            "Category"});
            this.typeDdl.Location = new System.Drawing.Point(79, 12);
            this.typeDdl.Name = "typeDdl";
            this.typeDdl.Size = new System.Drawing.Size(300, 21);
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
            "Daily",
            "Hourly"});
            this.frequencyDdl.Location = new System.Drawing.Point(79, 93);
            this.frequencyDdl.Name = "frequencyDdl";
            this.frequencyDdl.Size = new System.Drawing.Size(300, 21);
            this.frequencyDdl.TabIndex = 5;
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(304, 128);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 163);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.frequencyDdl);
            this.Controls.Add(this.frequencyLbl);
            this.Controls.Add(this.filterDdl);
            this.Controls.Add(this.filterLbl);
            this.Controls.Add(this.categoryDdl);
            this.Controls.Add(this.typeLbl);
            this.Controls.Add(this.typeDdl);
            this.Controls.Add(this.categoryLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}

