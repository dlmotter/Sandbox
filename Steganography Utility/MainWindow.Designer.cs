namespace Steganography_Utility
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.encodeTabPage = new System.Windows.Forms.TabPage();
            this.hiddenFileLbl = new System.Windows.Forms.Label();
            this.resultImageTb = new System.Windows.Forms.TextBox();
            this.resultImageBtn = new System.Windows.Forms.Button();
            this.resultImageLbl = new System.Windows.Forms.Label();
            this.containerLbl = new System.Windows.Forms.Label();
            this.containerImageTb = new System.Windows.Forms.TextBox();
            this.containerImageBtn = new System.Windows.Forms.Button();
            this.hiddenFileBtn = new System.Windows.Forms.Button();
            this.hiddenFileTb = new System.Windows.Forms.TextBox();
            this.decodeTabPage = new System.Windows.Forms.TabPage();
            this.noteLbl = new System.Windows.Forms.Label();
            this.encodedImageTb = new System.Windows.Forms.TextBox();
            this.encodedImageBtn = new System.Windows.Forms.Button();
            this.encodedImageLbl = new System.Windows.Forms.Label();
            this.goBtn = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.openFolderCb = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainTabControl.SuspendLayout();
            this.encodeTabPage.SuspendLayout();
            this.decodeTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "PNG|*.png|Bitmap|*.bmp";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.encodeTabPage);
            this.MainTabControl.Controls.Add(this.decodeTabPage);
            this.MainTabControl.Location = new System.Drawing.Point(13, 13);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(493, 163);
            this.MainTabControl.TabIndex = 5;
            // 
            // encodeTabPage
            // 
            this.encodeTabPage.Controls.Add(this.hiddenFileLbl);
            this.encodeTabPage.Controls.Add(this.resultImageTb);
            this.encodeTabPage.Controls.Add(this.resultImageBtn);
            this.encodeTabPage.Controls.Add(this.resultImageLbl);
            this.encodeTabPage.Controls.Add(this.containerLbl);
            this.encodeTabPage.Controls.Add(this.containerImageTb);
            this.encodeTabPage.Controls.Add(this.containerImageBtn);
            this.encodeTabPage.Controls.Add(this.hiddenFileBtn);
            this.encodeTabPage.Controls.Add(this.hiddenFileTb);
            this.encodeTabPage.Location = new System.Drawing.Point(4, 22);
            this.encodeTabPage.Name = "encodeTabPage";
            this.encodeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.encodeTabPage.Size = new System.Drawing.Size(485, 137);
            this.encodeTabPage.TabIndex = 0;
            this.encodeTabPage.Text = "Encode";
            this.encodeTabPage.UseVisualStyleBackColor = true;
            // 
            // hiddenFileLbl
            // 
            this.hiddenFileLbl.AutoSize = true;
            this.hiddenFileLbl.Location = new System.Drawing.Point(6, 47);
            this.hiddenFileLbl.Name = "hiddenFileLbl";
            this.hiddenFileLbl.Size = new System.Drawing.Size(60, 13);
            this.hiddenFileLbl.TabIndex = 4;
            this.hiddenFileLbl.Text = "Hidden File";
            // 
            // resultImageTb
            // 
            this.resultImageTb.AllowDrop = true;
            this.resultImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultImageTb.Location = new System.Drawing.Point(96, 107);
            this.resultImageTb.Name = "resultImageTb";
            this.resultImageTb.ReadOnly = true;
            this.resultImageTb.Size = new System.Drawing.Size(383, 20);
            this.resultImageTb.TabIndex = 0;
            this.resultImageTb.TabStop = false;
            // 
            // resultImageBtn
            // 
            this.resultImageBtn.Location = new System.Drawing.Point(15, 105);
            this.resultImageBtn.Name = "resultImageBtn";
            this.resultImageBtn.Size = new System.Drawing.Size(75, 23);
            this.resultImageBtn.TabIndex = 2;
            this.resultImageBtn.Text = "Choose...";
            this.resultImageBtn.UseVisualStyleBackColor = true;
            this.resultImageBtn.Click += new System.EventHandler(this.GenericFileSave_Click);
            // 
            // resultImageLbl
            // 
            this.resultImageLbl.AutoSize = true;
            this.resultImageLbl.Location = new System.Drawing.Point(6, 89);
            this.resultImageLbl.Name = "resultImageLbl";
            this.resultImageLbl.Size = new System.Drawing.Size(69, 13);
            this.resultImageLbl.TabIndex = 0;
            this.resultImageLbl.Text = "Result Image";
            // 
            // containerLbl
            // 
            this.containerLbl.AutoSize = true;
            this.containerLbl.Location = new System.Drawing.Point(6, 3);
            this.containerLbl.Name = "containerLbl";
            this.containerLbl.Size = new System.Drawing.Size(84, 13);
            this.containerLbl.TabIndex = 0;
            this.containerLbl.Text = "Container Image";
            // 
            // containerImageTb
            // 
            this.containerImageTb.AllowDrop = true;
            this.containerImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerImageTb.Location = new System.Drawing.Point(96, 23);
            this.containerImageTb.Name = "containerImageTb";
            this.containerImageTb.ReadOnly = true;
            this.containerImageTb.Size = new System.Drawing.Size(383, 20);
            this.containerImageTb.TabIndex = 0;
            this.containerImageTb.TabStop = false;
            // 
            // containerImageBtn
            // 
            this.containerImageBtn.Location = new System.Drawing.Point(15, 21);
            this.containerImageBtn.Name = "containerImageBtn";
            this.containerImageBtn.Size = new System.Drawing.Size(75, 23);
            this.containerImageBtn.TabIndex = 1;
            this.containerImageBtn.Text = "Choose...";
            this.containerImageBtn.UseVisualStyleBackColor = true;
            this.containerImageBtn.Click += new System.EventHandler(this.GenericFileUpload_Click);
            // 
            // hiddenFileBtn
            // 
            this.hiddenFileBtn.Location = new System.Drawing.Point(15, 63);
            this.hiddenFileBtn.Name = "hiddenFileBtn";
            this.hiddenFileBtn.Size = new System.Drawing.Size(75, 23);
            this.hiddenFileBtn.TabIndex = 3;
            this.hiddenFileBtn.Text = "Choose...";
            this.hiddenFileBtn.UseVisualStyleBackColor = true;
            this.hiddenFileBtn.Click += new System.EventHandler(this.GenericFileUpload_Click);
            // 
            // hiddenFileTb
            // 
            this.hiddenFileTb.AllowDrop = true;
            this.hiddenFileTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenFileTb.Location = new System.Drawing.Point(96, 65);
            this.hiddenFileTb.Name = "hiddenFileTb";
            this.hiddenFileTb.ReadOnly = true;
            this.hiddenFileTb.Size = new System.Drawing.Size(383, 20);
            this.hiddenFileTb.TabIndex = 0;
            this.hiddenFileTb.TabStop = false;
            // 
            // decodeTabPage
            // 
            this.decodeTabPage.Controls.Add(this.noteLbl);
            this.decodeTabPage.Controls.Add(this.encodedImageTb);
            this.decodeTabPage.Controls.Add(this.encodedImageBtn);
            this.decodeTabPage.Controls.Add(this.encodedImageLbl);
            this.decodeTabPage.Location = new System.Drawing.Point(4, 22);
            this.decodeTabPage.Name = "decodeTabPage";
            this.decodeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.decodeTabPage.Size = new System.Drawing.Size(485, 137);
            this.decodeTabPage.TabIndex = 1;
            this.decodeTabPage.Text = "Decode";
            this.decodeTabPage.UseVisualStyleBackColor = true;
            // 
            // noteLbl
            // 
            this.noteLbl.AutoSize = true;
            this.noteLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.noteLbl.Location = new System.Drawing.Point(12, 60);
            this.noteLbl.Name = "noteLbl";
            this.noteLbl.Size = new System.Drawing.Size(153, 15);
            this.noteLbl.TabIndex = 12;
            this.noteLbl.Text = "TODO: Update this text";
            // 
            // encodedImageTb
            // 
            this.encodedImageTb.AllowDrop = true;
            this.encodedImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encodedImageTb.Location = new System.Drawing.Point(96, 23);
            this.encodedImageTb.Name = "encodedImageTb";
            this.encodedImageTb.ReadOnly = true;
            this.encodedImageTb.Size = new System.Drawing.Size(383, 20);
            this.encodedImageTb.TabIndex = 0;
            this.encodedImageTb.TabStop = false;
            // 
            // encodedImageBtn
            // 
            this.encodedImageBtn.Location = new System.Drawing.Point(15, 21);
            this.encodedImageBtn.Name = "encodedImageBtn";
            this.encodedImageBtn.Size = new System.Drawing.Size(75, 23);
            this.encodedImageBtn.TabIndex = 6;
            this.encodedImageBtn.Text = "Choose...";
            this.encodedImageBtn.UseVisualStyleBackColor = true;
            this.encodedImageBtn.Click += new System.EventHandler(this.GenericFileUpload_Click);
            // 
            // encodedImageLbl
            // 
            this.encodedImageLbl.AutoSize = true;
            this.encodedImageLbl.Location = new System.Drawing.Point(6, 3);
            this.encodedImageLbl.Name = "encodedImageLbl";
            this.encodedImageLbl.Size = new System.Drawing.Size(82, 13);
            this.encodedImageLbl.TabIndex = 0;
            this.encodedImageLbl.Text = "Encoded Image";
            // 
            // goBtn
            // 
            this.goBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.goBtn.Location = new System.Drawing.Point(431, 182);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(75, 23);
            this.goBtn.TabIndex = 8;
            this.goBtn.Text = "Go";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // openFolderCb
            // 
            this.openFolderCb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openFolderCb.AutoSize = true;
            this.openFolderCb.Checked = true;
            this.openFolderCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openFolderCb.Location = new System.Drawing.Point(23, 186);
            this.openFolderCb.Name = "openFolderCb";
            this.openFolderCb.Size = new System.Drawing.Size(201, 17);
            this.openFolderCb.TabIndex = 9;
            this.openFolderCb.Text = "Open containing folder when finished";
            this.openFolderCb.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AcceptButton = this.goBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 217);
            this.Controls.Add(this.openFolderCb);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.MainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(2048, 256);
            this.MinimumSize = new System.Drawing.Size(534, 256);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Steganography Utility";
            this.MainTabControl.ResumeLayout(false);
            this.encodeTabPage.ResumeLayout(false);
            this.encodeTabPage.PerformLayout();
            this.decodeTabPage.ResumeLayout(false);
            this.decodeTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage decodeTabPage;
        private System.Windows.Forms.TabPage encodeTabPage;
        private System.Windows.Forms.Button hiddenFileBtn;
        private System.Windows.Forms.TextBox hiddenFileTb;
        private System.Windows.Forms.TextBox containerImageTb;
        private System.Windows.Forms.Button containerImageBtn;
        private System.Windows.Forms.Label containerLbl;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.Label encodedImageLbl;
        private System.Windows.Forms.TextBox encodedImageTb;
        private System.Windows.Forms.Button encodedImageBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TextBox resultImageTb;
        private System.Windows.Forms.Button resultImageBtn;
        private System.Windows.Forms.Label resultImageLbl;
        private System.Windows.Forms.Label noteLbl;
        private System.Windows.Forms.CheckBox openFolderCb;
        private System.Windows.Forms.Label hiddenFileLbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

