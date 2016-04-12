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
            this.decodeTabPage = new System.Windows.Forms.TabPage();
            this.hiddenImageRb = new System.Windows.Forms.RadioButton();
            this.hiddenTextRb = new System.Windows.Forms.RadioButton();
            this.hiddenImageTb = new System.Windows.Forms.TextBox();
            this.encodeTabPage = new System.Windows.Forms.TabPage();
            this.hiddenImageBtn = new System.Windows.Forms.Button();
            this.hiddenTextTb = new System.Windows.Forms.TextBox();
            this.containerImageBtn = new System.Windows.Forms.Button();
            this.containerImageTb = new System.Windows.Forms.TextBox();
            this.containerLbl = new System.Windows.Forms.Label();
            this.goBtn = new System.Windows.Forms.Button();
            this.encodedImageLbl = new System.Windows.Forms.Label();
            this.encodedImageBtn = new System.Windows.Forms.Button();
            this.encodedImageTb = new System.Windows.Forms.TextBox();
            this.decodedImageLbl = new System.Windows.Forms.Label();
            this.decodedImageBtn = new System.Windows.Forms.Button();
            this.decodedImageTb = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainTabControl.SuspendLayout();
            this.decodeTabPage.SuspendLayout();
            this.encodeTabPage.SuspendLayout();
            this.SuspendLayout();
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
            this.MainTabControl.Size = new System.Drawing.Size(493, 177);
            this.MainTabControl.TabIndex = 0;
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
            this.decodeTabPage.Size = new System.Drawing.Size(485, 151);
            this.decodeTabPage.TabIndex = 1;
            this.decodeTabPage.Text = "Decode";
            this.decodeTabPage.UseVisualStyleBackColor = true;
            // 
            // hiddenImageRb
            // 
            this.hiddenImageRb.AutoSize = true;
            this.hiddenImageRb.Checked = true;
            this.hiddenImageRb.Location = new System.Drawing.Point(6, 50);
            this.hiddenImageRb.Name = "hiddenImageRb";
            this.hiddenImageRb.Size = new System.Drawing.Size(91, 17);
            this.hiddenImageRb.TabIndex = 0;
            this.hiddenImageRb.TabStop = true;
            this.hiddenImageRb.Text = "Hidden Image";
            this.hiddenImageRb.UseVisualStyleBackColor = true;
            // 
            // hiddenTextRb
            // 
            this.hiddenTextRb.AutoSize = true;
            this.hiddenTextRb.Enabled = false;
            this.hiddenTextRb.Location = new System.Drawing.Point(6, 102);
            this.hiddenTextRb.Name = "hiddenTextRb";
            this.hiddenTextRb.Size = new System.Drawing.Size(83, 17);
            this.hiddenTextRb.TabIndex = 1;
            this.hiddenTextRb.Text = "Hidden Text";
            this.hiddenTextRb.UseVisualStyleBackColor = true;
            // 
            // hiddenImageTb
            // 
            this.hiddenImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenImageTb.Location = new System.Drawing.Point(96, 75);
            this.hiddenImageTb.Name = "hiddenImageTb";
            this.hiddenImageTb.Size = new System.Drawing.Size(383, 20);
            this.hiddenImageTb.TabIndex = 2;
            // 
            // encodeTabPage
            // 
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
            this.encodeTabPage.Size = new System.Drawing.Size(485, 151);
            this.encodeTabPage.TabIndex = 0;
            this.encodeTabPage.Text = "Encode";
            this.encodeTabPage.UseVisualStyleBackColor = true;
            // 
            // hiddenImageBtn
            // 
            this.hiddenImageBtn.Location = new System.Drawing.Point(15, 73);
            this.hiddenImageBtn.Name = "hiddenImageBtn";
            this.hiddenImageBtn.Size = new System.Drawing.Size(75, 23);
            this.hiddenImageBtn.TabIndex = 3;
            this.hiddenImageBtn.Text = "Choose...";
            this.hiddenImageBtn.UseVisualStyleBackColor = true;
            // 
            // hiddenTextTb
            // 
            this.hiddenTextTb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenTextTb.Enabled = false;
            this.hiddenTextTb.Location = new System.Drawing.Point(15, 125);
            this.hiddenTextTb.Multiline = true;
            this.hiddenTextTb.Name = "hiddenTextTb";
            this.hiddenTextTb.Size = new System.Drawing.Size(464, 20);
            this.hiddenTextTb.TabIndex = 4;
            // 
            // containerImageBtn
            // 
            this.containerImageBtn.Location = new System.Drawing.Point(15, 21);
            this.containerImageBtn.Name = "containerImageBtn";
            this.containerImageBtn.Size = new System.Drawing.Size(75, 23);
            this.containerImageBtn.TabIndex = 6;
            this.containerImageBtn.Text = "Choose...";
            this.containerImageBtn.UseVisualStyleBackColor = true;
            // 
            // containerImageTb
            // 
            this.containerImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerImageTb.Location = new System.Drawing.Point(96, 23);
            this.containerImageTb.Name = "containerImageTb";
            this.containerImageTb.Size = new System.Drawing.Size(383, 20);
            this.containerImageTb.TabIndex = 7;
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
            // goBtn
            // 
            this.goBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.goBtn.Location = new System.Drawing.Point(431, 196);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(75, 23);
            this.goBtn.TabIndex = 9;
            this.goBtn.Text = "Go";
            this.goBtn.UseVisualStyleBackColor = true;
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
            // encodedImageBtn
            // 
            this.encodedImageBtn.Location = new System.Drawing.Point(15, 21);
            this.encodedImageBtn.Name = "encodedImageBtn";
            this.encodedImageBtn.Size = new System.Drawing.Size(75, 23);
            this.encodedImageBtn.TabIndex = 7;
            this.encodedImageBtn.Text = "Choose...";
            this.encodedImageBtn.UseVisualStyleBackColor = true;
            // 
            // encodedImageTb
            // 
            this.encodedImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encodedImageTb.Location = new System.Drawing.Point(96, 23);
            this.encodedImageTb.Name = "encodedImageTb";
            this.encodedImageTb.Size = new System.Drawing.Size(383, 20);
            this.encodedImageTb.TabIndex = 8;
            // 
            // decodedImageLbl
            // 
            this.decodedImageLbl.Location = new System.Drawing.Point(6, 50);
            this.decodedImageLbl.Name = "decodedImageLbl";
            this.decodedImageLbl.Size = new System.Drawing.Size(83, 17);
            this.decodedImageLbl.TabIndex = 9;
            this.decodedImageLbl.Text = "Decoded Image";
            this.decodedImageLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // decodedImageBtn
            // 
            this.decodedImageBtn.Location = new System.Drawing.Point(15, 73);
            this.decodedImageBtn.Name = "decodedImageBtn";
            this.decodedImageBtn.Size = new System.Drawing.Size(75, 23);
            this.decodedImageBtn.TabIndex = 10;
            this.decodedImageBtn.Text = "Choose...";
            this.decodedImageBtn.UseVisualStyleBackColor = true;
            // 
            // decodedImageTb
            // 
            this.decodedImageTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decodedImageTb.Location = new System.Drawing.Point(96, 75);
            this.decodedImageTb.Name = "decodedImageTb";
            this.decodedImageTb.Size = new System.Drawing.Size(383, 20);
            this.decodedImageTb.TabIndex = 11;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 196);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(413, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 231);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.MainTabControl);
            this.MinimumSize = new System.Drawing.Size(534, 270);
            this.Name = "MainWindow";
            this.Text = "Steganography Utility";
            this.MainTabControl.ResumeLayout(false);
            this.decodeTabPage.ResumeLayout(false);
            this.decodeTabPage.PerformLayout();
            this.encodeTabPage.ResumeLayout(false);
            this.encodeTabPage.PerformLayout();
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
    }
}

