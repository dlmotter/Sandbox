using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.ComponentModel;

namespace _4chan_Thread_Saver
{
    public partial class MainWindow : Form
    {
        #region Global Variables
        private string BASEDIRECTORY, ENCRYPTEDSUBDIRECTORY, BASEURL, NOTFOUNDTITLE, TITLEXPATH, IMAGEANCHORXPATH, URLREGEX, SALT;
        #endregion

        #region System Functions
        public MainWindow()
        {
            InitializeComponent();
            getGlobalVarsFromConfig();

            urlTb.DragEnter += new DragEventHandler(urlTb_DragEnter);
            urlTb.DragDrop += new DragEventHandler(urlTb_DragDrop);

            directoryTb.DragEnter += new DragEventHandler(directoryTb_DragEnter);
            directoryTb.DragDrop += new DragEventHandler(directoryTb_DragDrop);

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            saveBackgroundWorker.WorkerReportsProgress = true;
            saveBackgroundWorker.RunWorkerCompleted += backgroundWorker_Callback;
            saveBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            decryptBackgroundWorker.WorkerReportsProgress = true;
            decryptBackgroundWorker.RunWorkerCompleted += backgroundWorker_Callback;
            decryptBackgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            if (clipboardTextIsThreadURL())
            {
                urlTb.Text = Clipboard.GetText();
            }
        }

        #region Async Operations Handlers
        private void saveBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            save();
        }

        private void decryptBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            decrypt();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_Callback(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            finishRun();
        }
        #endregion

        #region Button Click Handlers
        private void goBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            startRun();
        }

        private void optionsBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Options options = new Options(this);
            options.Show();
        }

        private void openSavedFolderBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string directory = BASEDIRECTORY.Replace("%userprofile%", Environment.GetEnvironmentVariable("userprofile"));
            Directory.CreateDirectory(directory);
            Process.Start(directory);
        }

        private void openEncFolderBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string directory = Path.Combine(BASEDIRECTORY.Replace("%userprofile%", Environment.GetEnvironmentVariable("userprofile")), ENCRYPTEDSUBDIRECTORY);
            Directory.CreateDirectory(directory);
            Process.Start(directory);
        }
        #endregion

        #region Drag/Drop Event Handlers
        private void urlTb_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void urlTb_DragDrop(object sender, DragEventArgs e)
        {
            string url = (string)e.Data.GetData(DataFormats.UnicodeText, false);

            if (URLIsValid(url))
            {
                urlTb.Text = url;
            }
        }

        private void directoryTb_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void directoryTb_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            string s = "";

            foreach (string File in FileList)
            {
                s = s + " " + File;
            }
            directoryTb.Text = s;
        }
        #endregion

        #region Control State Changed Event Handlers
        private void encryptCb_CheckedChanged(object sender, EventArgs e)
        {
            passwordTb.Enabled = encryptCb.Checked;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "saveTab")
            {
                goBtn.Text = "Save Images";
            }
            else
            {
                goBtn.Text = "Decrypt";
            }
        }
        #endregion
        #endregion

        #region Action Functions
        private void startRun()
        {
            if (tabControl1.SelectedTab.Name == "saveTab")
            {
                goBtn.Text = "Saving Images";
            }
            else
            {
                goBtn.Text = "Decrypting Images";
            }

            goBtn.Enabled = false;
            progressBar1.Show();

            if (tabControl1.SelectedTab.Name == "saveTab")
            {
                saveBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                decryptBackgroundWorker.RunWorkerAsync();
            }
        }

        private void finishRun()
        {
            progressBar1.Hide();
            progressBar1.Value = 0;
            goBtn.Enabled = true;

            if (tabControl1.SelectedTab.Name == "saveTab")
            {
                goBtn.Text = "Save Images";
            }
            else
            {
                goBtn.Text = "Decrypt";
            }
        }

        private void save()
        {
            try {
                string url = urlTb.Text;

                if (URLIsValid(url) && (!encryptCb.Checked || passwordTb.Text.Length > 0))
                {
                    HtmlAgilityPack.HtmlDocument doc = getTheadDocument(url);

                    if (threadIsFound(doc))
                    {
                        string fullDirectory, fileName, href = string.Empty;

                        HtmlNodeCollection nodes = getImageNodes(doc);
                        WebClient webClient = new WebClient();

                        int currentItemIndex = 1;
                        int totalItemsCount = nodes.Count;
                        foreach (HtmlNode node in nodes)
                        {
                            try
                            {
                                int percentComplete = (int)((float)currentItemIndex / (float)totalItemsCount * 100);
                                saveBackgroundWorker.ReportProgress(percentComplete);

                                href = string.Format("https:{0}", node.Attributes.Where(x => x.Name == "href").Select(x => x.Value).First());

                                if (encryptCb.Checked)
                                {
                                    fullDirectory = getFullEncryptedDirectory(url, passwordTb.Text, SALT);
                                    Directory.CreateDirectory(fullDirectory);
                                    fileName = EncryptFileString(node.InnerText, passwordTb.Text, SALT);

                                    int numFilesThatAlreadyHaveThisName = 0;
                                    while (File.Exists(Path.Combine(fullDirectory, fileName))) {
                                        numFilesThatAlreadyHaveThisName++;
                                        fileName = EncryptFileString(Path.GetFileNameWithoutExtension(node.InnerText) +
                                            " (" + numFilesThatAlreadyHaveThisName.ToString() + ")" +
                                            Path.GetExtension(node.InnerText), passwordTb.Text, SALT);
                                    }

                                    byte[] data = EncryptBytes(webClient.DownloadData(href), passwordTb.Text, SALT);
                                    using (MemoryStream mem = new MemoryStream(data))
                                    {
                                        Stream output = File.Create(Path.Combine(fullDirectory, fileName));

                                        byte[] buffer = new byte[8 * 1024];
                                        int len;
                                        while ((len = mem.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            output.Write(buffer, 0, len);
                                        }
                                    }
                                }
                                else
                                {
                                    fullDirectory = getFullDirectory(url);
                                    Directory.CreateDirectory(fullDirectory);
                                    fileName = node.InnerText;

                                    int numFilesThatAlreadyHaveThisName = 0;
                                    while (File.Exists(Path.Combine(fullDirectory, fileName)))
                                    {
                                        numFilesThatAlreadyHaveThisName++;
                                        fileName = Path.GetFileNameWithoutExtension(node.InnerText) + 
                                            " (" + numFilesThatAlreadyHaveThisName.ToString() + ")" + 
                                            Path.GetExtension(node.InnerText);
                                    }

                                    webClient.DownloadFile(href, Path.Combine(fullDirectory, fileName));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error saving image \"" + node.InnerText + "\".\nIf there are additional images in the thread, they will still be saved.\n\nTechnical error:\n" + ex.Message,
                                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            currentItemIndex++;
                        }

                        webClient.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Thread not found. It is likely this thread has 404'ed.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Make sure the url is a valid 4chan thread URL, and that if you are requiring a password that it is at least one character.", 
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving images\n\nTechnical error:\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void decrypt()
        {
            try {
                string encryptedFolderName = directoryTb.Text.Split('\\')[directoryTb.Text.Split('\\').Length - 1];
                string decryptedFolderName = DecryptFileString(encryptedFolderName, decryptPasswordTb.Text, SALT);
                string decryptedDirectory = directoryTb.Text.Replace(encryptedFolderName, decryptedFolderName);

                if (string.IsNullOrEmpty(Path.GetExtension(decryptedFolderName)))
                {
                    string[] files = Directory.GetFiles(directoryTb.Text);
                    Directory.CreateDirectory(decryptedDirectory);

                    int currentItemIndex = 1;
                    int totalItemsCount = files.Count();
                    foreach (string file in files)
                    {
                        try
                        {
                            int percentComplete = (int)((float)currentItemIndex / (float)totalItemsCount * 100);
                            saveBackgroundWorker.ReportProgress(percentComplete);

                            var decryptedBytes = DecryptBytes(File.ReadAllBytes(file), decryptPasswordTb.Text, SALT);
                            var encryptedFileName = file.Split('\\')[file.Split('\\').Length - 1];
                            var decryptedFileName = DecryptFileString(encryptedFileName, decryptPasswordTb.Text, SALT);

                            var fs = new BinaryWriter(new FileStream(Path.Combine(decryptedDirectory, decryptedFileName), FileMode.Append, FileAccess.Write));
                            fs.Write(decryptedBytes);
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error decrypting file \"" + file + "\".\nIf there are additional files in the directory, they will still be decrypted.\n\nTechnical error:\n" + ex.Message,
                                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        currentItemIndex++;
                    }
                }
                else
                {
                    MessageBox.Show("Encrypted directory leads to a file (" + decryptedFolderName + "), not a directory.", 
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error decrypting images, likely due to an invalid password.\n\nTechnical error:\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Validation Functions
        private bool clipboardTextIsThreadURL()
        {
            return Clipboard.ContainsText() && Regex.IsMatch(Clipboard.GetText(), URLREGEX);
        }

        private bool URLIsValid(string url)
        {
            return Regex.IsMatch(url, URLREGEX);
        }

        private bool threadIsFound(HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode(TITLEXPATH).InnerText != NOTFOUNDTITLE;
        }
        #endregion

        #region Getter Functions
        private string getFullDirectory(string url)
        {
            string[] urlParts = url.Replace(BASEURL, "").Split('/');
            string board = urlParts[0];
            string thread = string.Format("{0} ({1})", urlParts[urlParts.Length - 1], urlParts[urlParts.Length - 2]);
            return Path.Combine(Environment.ExpandEnvironmentVariables(BASEDIRECTORY), board, thread);
        }

        private string getFullEncryptedDirectory(string url, string password, string salt)
        {
            string[] urlParts = url.Replace(BASEURL, "").Split('/');
            string board = Environment.ExpandEnvironmentVariables(ENCRYPTEDSUBDIRECTORY);
            string thread = EncryptFileString(string.Format("{0} ({1})", urlParts[urlParts.Length - 1], urlParts[urlParts.Length - 2]), password, salt);
            return Path.Combine(Environment.ExpandEnvironmentVariables(BASEDIRECTORY), board, thread);
        }

        private HtmlAgilityPack.HtmlDocument getTheadDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }

        private HtmlNodeCollection getImageNodes(HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.SelectNodes(IMAGEANCHORXPATH);
        }

        public void getGlobalVarsFromConfig()
        {
            BASEDIRECTORY = ConfigurationManager.AppSettings.Get("baseDirectory");
            ENCRYPTEDSUBDIRECTORY = ConfigurationManager.AppSettings.Get("encryptedSubDirectory");
            BASEURL = ConfigurationManager.AppSettings.Get("baseUrl");
            NOTFOUNDTITLE = ConfigurationManager.AppSettings.Get("notFoundTitle");
            TITLEXPATH = ConfigurationManager.AppSettings.Get("titleXPath");
            IMAGEANCHORXPATH = ConfigurationManager.AppSettings.Get("imageAnchorXPath");
            URLREGEX = ConfigurationManager.AppSettings.Get("urlRegEx");
            SALT = ConfigurationManager.AppSettings.Get("salt");
        }
        #endregion

        #region Encryption Functions
        public byte[] EncryptBytes(byte[] inputBytes, string passPhrase, string saltValue)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            RijndaelCipher.Padding = PaddingMode.PKCS7;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(password.GetBytes(32), password.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            return CipherBytes;
        }

        public byte[] DecryptBytes(byte[] encryptedBytes, string passPhrase, string saltValue)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            RijndaelCipher.Mode = CipherMode.CBC;
            RijndaelCipher.Padding = PaddingMode.PKCS7;
            byte[] salt = Encoding.ASCII.GetBytes(saltValue);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, salt, "SHA1", 2);

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(password.GetBytes(32), password.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(encryptedBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] plainBytes = new byte[encryptedBytes.Length];

            int DecryptedCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return plainBytes;
        }

        public string EncryptFileString(string inputString, string passPhrase, string saltValue)
        {
            var byteArray = Encoding.UTF8.GetBytes(inputString);
            var encryptedBytes = EncryptBytes(byteArray, passPhrase, saltValue);
            var encryptedString = Convert.ToBase64String(encryptedBytes).Replace("/", "-");

            return encryptedString;
        }

        public string DecryptFileString(string encryptedString, string passPhrase, string saltValue)
        {
            var byteArray = Convert.FromBase64String(encryptedString.Replace("-", "/"));
            var decryptedBytes = DecryptBytes(byteArray, passPhrase, saltValue);
            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            return new string(decryptedString.Where(x => !Path.GetInvalidFileNameChars().Contains(x)).ToArray());
        }
        #endregion
    }
}
