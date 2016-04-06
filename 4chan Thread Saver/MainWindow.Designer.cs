namespace _4chan_Thread_Saver
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
            this.urlLbl = new System.Windows.Forms.Label();
            this.urlTb = new System.Windows.Forms.TextBox();
            this.goBtn = new System.Windows.Forms.LinkLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.encryptCb = new System.Windows.Forms.CheckBox();
            this.passwordTb = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.saveTab = new System.Windows.Forms.TabPage();
            this.openSavedFolderBtn = new System.Windows.Forms.LinkLabel();
            this.decryptTab = new System.Windows.Forms.TabPage();
            this.openEncFolderBtn = new System.Windows.Forms.LinkLabel();
            this.decryptPasswordTb = new System.Windows.Forms.TextBox();
            this.decryptPasswordLbl = new System.Windows.Forms.Label();
            this.directoryTb = new System.Windows.Forms.TextBox();
            this.directoryLbl = new System.Windows.Forms.Label();
            this.optionsBtn = new System.Windows.Forms.LinkLabel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.saveTab.SuspendLayout();
            this.decryptTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // urlLbl
            // 
            this.urlLbl.Location = new System.Drawing.Point(6, 3);
            this.urlLbl.Name = "urlLbl";
            this.urlLbl.Size = new System.Drawing.Size(111, 19);
            this.urlLbl.TabIndex = 0;
            this.urlLbl.Text = "4chan Thread URL";
            this.urlLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // urlTb
            // 
            this.urlTb.AllowDrop = true;
            this.urlTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlTb.Location = new System.Drawing.Point(6, 25);
            this.urlTb.Name = "urlTb";
            this.urlTb.Size = new System.Drawing.Size(534, 21);
            this.urlTb.TabIndex = 1;
            // 
            // goBtn
            // 
            this.goBtn.AutoSize = true;
            this.goBtn.DisabledLinkColor = System.Drawing.Color.Blue;
            this.goBtn.Location = new System.Drawing.Point(9, 151);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(79, 15);
            this.goBtn.TabIndex = 2;
            this.goBtn.TabStop = true;
            this.goBtn.Text = "Save Images";
            this.goBtn.VisitedLinkColor = System.Drawing.Color.Blue;
            this.goBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.goBtn_LinkClicked);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(125, 151);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(387, 15);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // encryptCb
            // 
            this.encryptCb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.encryptCb.Location = new System.Drawing.Point(6, 52);
            this.encryptCb.Name = "encryptCb";
            this.encryptCb.Size = new System.Drawing.Size(138, 19);
            this.encryptCb.TabIndex = 4;
            this.encryptCb.Text = "Require Password?";
            this.encryptCb.UseVisualStyleBackColor = false;
            this.encryptCb.CheckedChanged += new System.EventHandler(this.encryptCb_CheckedChanged);
            // 
            // passwordTb
            // 
            this.passwordTb.Enabled = false;
            this.passwordTb.Location = new System.Drawing.Point(6, 77);
            this.passwordTb.Name = "passwordTb";
            this.passwordTb.PasswordChar = '*';
            this.passwordTb.Size = new System.Drawing.Size(272, 21);
            this.passwordTb.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.saveTab);
            this.tabControl1.Controls.Add(this.decryptTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 136);
            this.tabControl1.TabIndex = 6;
            // 
            // saveTab
            // 
            this.saveTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(224)))), ((int)(((byte)(214)))));
            this.saveTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.saveTab.Controls.Add(this.openSavedFolderBtn);
            this.saveTab.Controls.Add(this.urlLbl);
            this.saveTab.Controls.Add(this.passwordTb);
            this.saveTab.Controls.Add(this.urlTb);
            this.saveTab.Controls.Add(this.encryptCb);
            this.saveTab.Location = new System.Drawing.Point(4, 24);
            this.saveTab.Name = "saveTab";
            this.saveTab.Padding = new System.Windows.Forms.Padding(3);
            this.saveTab.Size = new System.Drawing.Size(548, 108);
            this.saveTab.TabIndex = 0;
            this.saveTab.Text = "Save Images";
            // 
            // openSavedFolderBtn
            // 
            this.openSavedFolderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openSavedFolderBtn.AutoSize = true;
            this.openSavedFolderBtn.DisabledLinkColor = System.Drawing.Color.Blue;
            this.openSavedFolderBtn.Location = new System.Drawing.Point(379, 80);
            this.openSavedFolderBtn.Name = "openSavedFolderBtn";
            this.openSavedFolderBtn.Size = new System.Drawing.Size(161, 15);
            this.openSavedFolderBtn.TabIndex = 6;
            this.openSavedFolderBtn.TabStop = true;
            this.openSavedFolderBtn.Text = "Open Saved Threads Folder";
            this.openSavedFolderBtn.VisitedLinkColor = System.Drawing.Color.Blue;
            this.openSavedFolderBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openSavedFolderBtn_LinkClicked);
            // 
            // decryptTab
            // 
            this.decryptTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(224)))), ((int)(((byte)(214)))));
            this.decryptTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decryptTab.Controls.Add(this.openEncFolderBtn);
            this.decryptTab.Controls.Add(this.decryptPasswordTb);
            this.decryptTab.Controls.Add(this.decryptPasswordLbl);
            this.decryptTab.Controls.Add(this.directoryTb);
            this.decryptTab.Controls.Add(this.directoryLbl);
            this.decryptTab.Location = new System.Drawing.Point(4, 24);
            this.decryptTab.Name = "decryptTab";
            this.decryptTab.Padding = new System.Windows.Forms.Padding(3);
            this.decryptTab.Size = new System.Drawing.Size(548, 108);
            this.decryptTab.TabIndex = 1;
            this.decryptTab.Text = "Decrypt Images";
            // 
            // openEncFolderBtn
            // 
            this.openEncFolderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openEncFolderBtn.AutoSize = true;
            this.openEncFolderBtn.DisabledLinkColor = System.Drawing.Color.Blue;
            this.openEncFolderBtn.Location = new System.Drawing.Point(359, 80);
            this.openEncFolderBtn.Name = "openEncFolderBtn";
            this.openEncFolderBtn.Size = new System.Drawing.Size(181, 15);
            this.openEncFolderBtn.TabIndex = 8;
            this.openEncFolderBtn.TabStop = true;
            this.openEncFolderBtn.Text = "Open Encrypted Threads Folder";
            this.openEncFolderBtn.VisitedLinkColor = System.Drawing.Color.Blue;
            this.openEncFolderBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openEncFolderBtn_LinkClicked);
            // 
            // decryptPasswordTb
            // 
            this.decryptPasswordTb.Location = new System.Drawing.Point(6, 77);
            this.decryptPasswordTb.Name = "decryptPasswordTb";
            this.decryptPasswordTb.PasswordChar = '*';
            this.decryptPasswordTb.Size = new System.Drawing.Size(276, 21);
            this.decryptPasswordTb.TabIndex = 6;
            // 
            // decryptPasswordLbl
            // 
            this.decryptPasswordLbl.Location = new System.Drawing.Point(6, 52);
            this.decryptPasswordLbl.Name = "decryptPasswordLbl";
            this.decryptPasswordLbl.Size = new System.Drawing.Size(63, 19);
            this.decryptPasswordLbl.TabIndex = 3;
            this.decryptPasswordLbl.Text = "Password";
            this.decryptPasswordLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // directoryTb
            // 
            this.directoryTb.AllowDrop = true;
            this.directoryTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryTb.Location = new System.Drawing.Point(6, 25);
            this.directoryTb.Name = "directoryTb";
            this.directoryTb.Size = new System.Drawing.Size(534, 21);
            this.directoryTb.TabIndex = 2;
            // 
            // directoryLbl
            // 
            this.directoryLbl.Location = new System.Drawing.Point(6, 3);
            this.directoryLbl.Name = "directoryLbl";
            this.directoryLbl.Size = new System.Drawing.Size(232, 19);
            this.directoryLbl.TabIndex = 0;
            this.directoryLbl.Text = "Directory (drag and drop supported)";
            this.directoryLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optionsBtn
            // 
            this.optionsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsBtn.AutoSize = true;
            this.optionsBtn.Location = new System.Drawing.Point(518, 151);
            this.optionsBtn.Name = "optionsBtn";
            this.optionsBtn.Size = new System.Drawing.Size(50, 15);
            this.optionsBtn.TabIndex = 7;
            this.optionsBtn.TabStop = true;
            this.optionsBtn.Text = "Options";
            this.optionsBtn.VisitedLinkColor = System.Drawing.Color.Blue;
            this.optionsBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.optionsBtn_LinkClicked);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.goBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(580, 182);
            this.Controls.Add(this.optionsBtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.goBtn);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.ForeColor = System.Drawing.Color.Maroon;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2048, 220);
            this.MinimumSize = new System.Drawing.Size(596, 220);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4chan Thread Saver";
            this.tabControl1.ResumeLayout(false);
            this.saveTab.ResumeLayout(false);
            this.saveTab.PerformLayout();
            this.decryptTab.ResumeLayout(false);
            this.decryptTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label urlLbl;
        private System.Windows.Forms.TextBox urlTb;
        private System.Windows.Forms.LinkLabel goBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox encryptCb;
        private System.Windows.Forms.TextBox passwordTb;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage saveTab;
        private System.Windows.Forms.TabPage decryptTab;
        private System.Windows.Forms.TextBox directoryTb;
        private System.Windows.Forms.Label directoryLbl;
        private System.Windows.Forms.TextBox decryptPasswordTb;
        private System.Windows.Forms.Label decryptPasswordLbl;
        private System.Windows.Forms.LinkLabel openSavedFolderBtn;
        private System.Windows.Forms.LinkLabel openEncFolderBtn;
        private System.Windows.Forms.LinkLabel optionsBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}