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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.encodeTabPage = new System.Windows.Forms.TabPage();
            this.resultImageTb = new System.Windows.Forms.TextBox();
            this.resultImageBtn = new System.Windows.Forms.Button();
            this.resultImageLbl = new System.Windows.Forms.Label();
            this.containerLbl = new System.Windows.Forms.Label();
            this.containerImageTb = new System.Windows.Forms.TextBox();
            this.containerImageBtn = new System.Windows.Forms.Button();
            this.hiddenTextTb = new System.Windows.Forms.TextBox();
            this.hiddenImageBtn = new System.Windows.Forms.Button();
            this.hiddenImageTb = new System.Windows.Forms.TextBox();
            this.hiddenTextRb = new System.Windows.Forms.RadioButton();
            this.hiddenImageRb = new System.Windows.Forms.RadioButton();
            this.decodeTabPage = new System.Windows.Forms.TabPage();
            this.decodedImageTb = new System.Windows.Forms.TextBox();
            this.decodedImageBtn = new System.Windows.Forms.Button();
            this.decodedImageLbl = new System.Windows.Forms.Label();
            this.encodedImageTb = new System.Windows.Forms.TextBox();
            this.encodedImageBtn = new System.Windows.Forms.Button();
            this.encodedImageLbl = new System.Windows.Forms.Label();
            this.goBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainTabControl.SuspendLayout();
            this.encodeTabPage.SuspendLayout();
            this.decodeTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "bmp";
            this.saveFileDialog.Filter = "Bitmap|*.bmp|PNG|*.png|JPEG|*.jpg,*.jpeg";
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
            this.MainTabControl.Size = new System.Drawing.Size(493, 219);
            this.MainTabControl.TabIndex = 0;
            // 
            // encodeTabPage
            // 
            this.encodeTabPage.Controls.Add(this.resultImageTb);
            this.encodeTabPage.Controls.Add(this.resultImageBtn);
            this.encodeTabPage.Controls.Add(this.resultImageLbl);
            this.encodeTabPage.Controls.Add(this.containerLbl);
            this.encodeTabPage.Controls.Add(this.containerImageTb);
            this.encodeTabPage.Controls.Add(this.containerImageBtn);
            this.encodeTabPage.Controls.Add(this.hiddenTextTb);
            this.encodeTabPage.Controls.Add(this.hiddenImageBtn);
            this.encodeTabPage.Controls.Add(this.hiddenImageTb);
            this.encodeTabPage.Controls.Add(this.hiddenTextRb);
            this.encodeTabPage.Controls.Add(this.hiddenImageRb);
            this.encodeTabPage.Location = new System.Drawing.Point(4, 22);
            this.encodeTabPage.Name = "encodeTabPage";
            this.encodeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.encodeTabPage.Size = new System.Drawing.Size(485, 193);
            this.encodeTabPage.TabIndex = 0;
            this.encodeTabPage.Text = "Encode";
            this.encodeTabPage.UseVisualStyleBackColor = true;
            // 
            // resultImageTb
            // 
            this.resultImageTb.AllowDrop = true;
            this.resultImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultImageTb.Location = new System.Drawing.Point(96, 65);
            this.resultImageTb.Name = "resultImageTb";
            this.resultImageTb.Size = new System.Drawing.Size(383, 20);
            this.resultImageTb.TabIndex = 11;
            // 
            // resultImageBtn
            // 
            this.resultImageBtn.Location = new System.Drawing.Point(15, 63);
            this.resultImageBtn.Name = "resultImageBtn";
            this.resultImageBtn.Size = new System.Drawing.Size(75, 23);
            this.resultImageBtn.TabIndex = 10;
            this.resultImageBtn.Text = "Choose...";
            this.resultImageBtn.UseVisualStyleBackColor = true;
            this.resultImageBtn.Click += new System.EventHandler(this.GenericFileSave_Click);
            // 
            // resultImageLbl
            // 
            this.resultImageLbl.AutoSize = true;
            this.resultImageLbl.Location = new System.Drawing.Point(6, 47);
            this.resultImageLbl.Name = "resultImageLbl";
            this.resultImageLbl.Size = new System.Drawing.Size(69, 13);
            this.resultImageLbl.TabIndex = 9;
            this.resultImageLbl.Text = "Result Image";
            // 
            // containerLbl
            // 
            this.containerLbl.AutoSize = true;
            this.containerLbl.Location = new System.Drawing.Point(6, 3);
            this.containerLbl.Name = "containerLbl";
            this.containerLbl.Size = new System.Drawing.Size(84, 13);
            this.containerLbl.TabIndex = 8;
            this.containerLbl.Text = "Container Image";
            // 
            // containerImageTb
            // 
            this.containerImageTb.AllowDrop = true;
            this.containerImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerImageTb.Location = new System.Drawing.Point(96, 23);
            this.containerImageTb.Name = "containerImageTb";
            this.containerImageTb.Size = new System.Drawing.Size(383, 20);
            this.containerImageTb.TabIndex = 7;
            // 
            // containerImageBtn
            // 
            this.containerImageBtn.Location = new System.Drawing.Point(15, 21);
            this.containerImageBtn.Name = "containerImageBtn";
            this.containerImageBtn.Size = new System.Drawing.Size(75, 23);
            this.containerImageBtn.TabIndex = 6;
            this.containerImageBtn.Text = "Choose...";
            this.containerImageBtn.UseVisualStyleBackColor = true;
            this.containerImageBtn.Click += new System.EventHandler(this.GenericFileUpload_Click);
            // 
            // hiddenTextTb
            // 
            this.hiddenTextTb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenTextTb.Enabled = false;
            this.hiddenTextTb.Location = new System.Drawing.Point(15, 167);
            this.hiddenTextTb.Multiline = true;
            this.hiddenTextTb.Name = "hiddenTextTb";
            this.hiddenTextTb.Size = new System.Drawing.Size(464, 20);
            this.hiddenTextTb.TabIndex = 4;
            // 
            // hiddenImageBtn
            // 
            this.hiddenImageBtn.Location = new System.Drawing.Point(15, 115);
            this.hiddenImageBtn.Name = "hiddenImageBtn";
            this.hiddenImageBtn.Size = new System.Drawing.Size(75, 23);
            this.hiddenImageBtn.TabIndex = 3;
            this.hiddenImageBtn.Text = "Choose...";
            this.hiddenImageBtn.UseVisualStyleBackColor = true;
            this.hiddenImageBtn.Click += new System.EventHandler(this.GenericFileUpload_Click);
            // 
            // hiddenImageTb
            // 
            this.hiddenImageTb.AllowDrop = true;
            this.hiddenImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenImageTb.Location = new System.Drawing.Point(96, 118);
            this.hiddenImageTb.Name = "hiddenImageTb";
            this.hiddenImageTb.Size = new System.Drawing.Size(383, 20);
            this.hiddenImageTb.TabIndex = 2;
            // 
            // hiddenTextRb
            // 
            this.hiddenTextRb.AutoSize = true;
            this.hiddenTextRb.Enabled = false;
            this.hiddenTextRb.Location = new System.Drawing.Point(6, 144);
            this.hiddenTextRb.Name = "hiddenTextRb";
            this.hiddenTextRb.Size = new System.Drawing.Size(83, 17);
            this.hiddenTextRb.TabIndex = 1;
            this.hiddenTextRb.Text = "Hidden Text";
            this.hiddenTextRb.UseVisualStyleBackColor = true;
            // 
            // hiddenImageRb
            // 
            this.hiddenImageRb.AutoSize = true;
            this.hiddenImageRb.Checked = true;
            this.hiddenImageRb.Location = new System.Drawing.Point(6, 92);
            this.hiddenImageRb.Name = "hiddenImageRb";
            this.hiddenImageRb.Size = new System.Drawing.Size(91, 17);
            this.hiddenImageRb.TabIndex = 0;
            this.hiddenImageRb.TabStop = true;
            this.hiddenImageRb.Text = "Hidden Image";
            this.hiddenImageRb.UseVisualStyleBackColor = true;
            // 
            // decodeTabPage
            // 
            this.decodeTabPage.Controls.Add(this.decodedImageTb);
            this.decodeTabPage.Controls.Add(this.decodedImageBtn);
            this.decodeTabPage.Controls.Add(this.decodedImageLbl);
            this.decodeTabPage.Controls.Add(this.encodedImageTb);
            this.decodeTabPage.Controls.Add(this.encodedImageBtn);
            this.decodeTabPage.Controls.Add(this.encodedImageLbl);
            this.decodeTabPage.Location = new System.Drawing.Point(4, 22);
            this.decodeTabPage.Name = "decodeTabPage";
            this.decodeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.decodeTabPage.Size = new System.Drawing.Size(485, 193);
            this.decodeTabPage.TabIndex = 1;
            this.decodeTabPage.Text = "Decode";
            this.decodeTabPage.UseVisualStyleBackColor = true;
            // 
            // decodedImageTb
            // 
            this.decodedImageTb.AllowDrop = true;
            this.decodedImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decodedImageTb.Location = new System.Drawing.Point(96, 65);
            this.decodedImageTb.Name = "decodedImageTb";
            this.decodedImageTb.Size = new System.Drawing.Size(383, 20);
            this.decodedImageTb.TabIndex = 11;
            // 
            // decodedImageBtn
            // 
            this.decodedImageBtn.Location = new System.Drawing.Point(15, 63);
            this.decodedImageBtn.Name = "decodedImageBtn";
            this.decodedImageBtn.Size = new System.Drawing.Size(75, 23);
            this.decodedImageBtn.TabIndex = 10;
            this.decodedImageBtn.Text = "Choose...";
            this.decodedImageBtn.UseVisualStyleBackColor = true;
            this.decodedImageBtn.Click += new System.EventHandler(this.GenericFileSave_Click);
            // 
            // decodedImageLbl
            // 
            this.decodedImageLbl.AutoSize = true;
            this.decodedImageLbl.Location = new System.Drawing.Point(6, 47);
            this.decodedImageLbl.Name = "decodedImageLbl";
            this.decodedImageLbl.Size = new System.Drawing.Size(83, 13);
            this.decodedImageLbl.TabIndex = 9;
            this.decodedImageLbl.Text = "Decoded Image";
            this.decodedImageLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // encodedImageTb
            // 
            this.encodedImageTb.AllowDrop = true;
            this.encodedImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encodedImageTb.Location = new System.Drawing.Point(96, 23);
            this.encodedImageTb.Name = "encodedImageTb";
            this.encodedImageTb.Size = new System.Drawing.Size(383, 20);
            this.encodedImageTb.TabIndex = 8;
            // 
            // encodedImageBtn
            // 
            this.encodedImageBtn.Location = new System.Drawing.Point(15, 21);
            this.encodedImageBtn.Name = "encodedImageBtn";
            this.encodedImageBtn.Size = new System.Drawing.Size(75, 23);
            this.encodedImageBtn.TabIndex = 7;
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
            this.goBtn.Location = new System.Drawing.Point(431, 238);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(75, 23);
            this.goBtn.TabIndex = 9;
            this.goBtn.Text = "Go";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 238);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(413, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Visible = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.goBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 273);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.MainTabControl);
            this.MinimumSize = new System.Drawing.Size(534, 312);
            this.Name = "MainWindow";
            this.Text = "Steganography Utility";
            this.MainTabControl.ResumeLayout(false);
            this.encodeTabPage.ResumeLayout(false);
            this.encodeTabPage.PerformLayout();
            this.decodeTabPage.ResumeLayout(false);
            this.decodeTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage decodeTabPage;
        private System.Windows.Forms.TabPage encodeTabPage;
        private System.Windows.Forms.Button hiddenImageBtn;
        private System.Windows.Forms.TextBox hiddenImageTb;
        private System.Windows.Forms.RadioButton hiddenTextRb;
        private System.Windows.Forms.RadioButton hiddenImageRb;
        private System.Windows.Forms.TextBox hiddenTextTb;
        private System.Windows.Forms.TextBox containerImageTb;
        private System.Windows.Forms.Button containerImageBtn;
        private System.Windows.Forms.Label containerLbl;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.Label encodedImageLbl;
        private System.Windows.Forms.TextBox encodedImageTb;
        private System.Windows.Forms.Button encodedImageBtn;
        private System.Windows.Forms.Label decodedImageLbl;
        private System.Windows.Forms.TextBox decodedImageTb;
        private System.Windows.Forms.Button decodedImageBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TextBox resultImageTb;
        private System.Windows.Forms.Button resultImageBtn;
        private System.Windows.Forms.Label resultImageLbl;
    }
}

