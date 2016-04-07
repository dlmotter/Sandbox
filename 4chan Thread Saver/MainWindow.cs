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
            // Set our global variables
            getGlobalVarsFromConfig();

            // Wire up drag/drop event handlers
            urlTb.DragEnter += new DragEventHandler(urlTb_DragEnter);
            urlTb.DragDrop += new DragEventHandler(urlTb_DragDrop);

            directoryTb.DragEnter += new DragEventHandler(directoryTb_DragEnter);
            directoryTb.DragDrop += new DragEventHandler(directoryTb_DragDrop);

            // Wire up the tab changed event handler
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            // Wire up the async operations event handlers
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_Callback;

            // If the clipboard contains a 4chan thread URL, pre-populate the URL textbox with it
            if (clipboardTextIsThreadURL())
            {
                urlTb.Text = Clipboard.GetText();
            }
        }

        #region Async Operations Handlers
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Since there's only one background worker, check to see which function we are supposed to do
            if ((string)e.Argument == "save")
            {
                save();
            }
            else
            {
                decrypt();
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_Callback(object sender, RunWorkerCompletedEventArgs e)
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
            // Open the options window, making sure to pass a reference to this window. 
            // This is so the Options window can set our global vars when it's saved.
            // We can't do it here since the window is shown async
            Options options = new Options(this);
            options.Show();
        }

        private void openSavedFolderBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the saved threads folder, creating it if necessary
            string directory = Environment.ExpandEnvironmentVariables(BASEDIRECTORY);
            Directory.CreateDirectory(directory);
            Process.Start(directory);
        }

        private void openEncFolderBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the encrypted threads folder, creating it if necessary
            string directory = Path.Combine(Environment.ExpandEnvironmentVariables(BASEDIRECTORY), Environment.ExpandEnvironmentVariables(ENCRYPTEDSUBDIRECTORY));
            Directory.CreateDirectory(directory);
            Process.Start(directory);
        }
        #endregion

        #region Drag/Drop Event Handlers
        private void urlTb_DragEnter(object sender, DragEventArgs e)
        {
            // Change the mouse icon depending if the user is trying to drop something legit
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

            // Only set the text of the textbox if it's a valid URL
            if (URLIsValid(url))
            {
                urlTb.Text = url;
            }
        }

        private void directoryTb_DragEnter(object sender, DragEventArgs e)
        {
            // Change the mouse icon depending if the user is trying to drop something legit
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

            // Parse the file list from the FileDrop object and set the textbox text
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
            // Password textbox should only be enabled if the "Require Password" checkbox is checked
            passwordTb.Enabled = encryptCb.Checked;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change the text of the Go button depending on the selected tab
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
        /// <summary>
        /// Function called to start processing
        /// </summary>
        private void startRun()
        {
            // Perform the correct action depending on the selected tab
            string arg;
            if (tabControl1.SelectedTab.Name == "saveTab")
            {
                arg = "save";
                goBtn.Text = "Saving Images";
            }
            else
            {
                arg = "decrypt";
                goBtn.Text = "Decrypting Images";
            }

            goBtn.LinkBehavior = LinkBehavior.NeverUnderline;
            goBtn.Links[0].Enabled = false;
            progressBar1.Show();
            // Start the background worker, passing in the process we want to run
            backgroundWorker.RunWorkerAsync(arg);
        }

        /// <summary>
        /// Function called when processing finishes/errors out
        /// </summary>
        private void finishRun()
        {
            // Reset GUI elements
            progressBar1.Hide();
            progressBar1.Value = 0;
            goBtn.Links[0].Enabled = true;
            goBtn.LinkBehavior = LinkBehavior.SystemDefault;

            if (tabControl1.SelectedTab.Name == "saveTab")
            {
                goBtn.Text = "Save Images";
            }
            else
            {
                goBtn.Text = "Decrypt";
            }
        }

        /// <summary>
        /// Main save function
        /// </summary>
        private void save()
        {
            try
            {
                string url = urlTb.Text;

                // Only proceed of the URL textbox contains a valid 4chan thread URL
                if (URLIsValid(url) && (!encryptCb.Checked || passwordTb.Text.Length > 0))
                {
                    HtmlAgilityPack.HtmlDocument doc = getTheadDocument(url);

                    // Only proceed if the thread is found (i.e. hasn't 404'ed, or the user passes in a URL that the regex says is valid but really doesn't exist)
                    if (threadIsFound(doc))
                    {
                        string fullDirectory, fileName, href = string.Empty;
                        HtmlNodeCollection nodes = getImageNodes(doc);
                        WebClient webClient = new WebClient();

                        // Instantiate variables used to report progress
                        int currentItemIndex = 1;
                        int totalItemsCount = nodes.Count;
                        int percentComplete;

                        // Iterate through each matching anchor tag. These tags contain the image name and the href. 
                        foreach (HtmlNode node in nodes)
                        {
                            try
                            {
                                // Report progress
                                percentComplete = (int)((float)currentItemIndex / (float)totalItemsCount * 100);
                                backgroundWorker.ReportProgress(percentComplete);

                                // Get href from node
                                href = string.Format("https:{0}", node.Attributes.Where(x => x.Name == "href").Select(x => x.Value).First());

                                if (encryptCb.Checked)
                                {
                                    // We are encrypting the downloaded files

                                    // Get the directory to save to and the file name
                                    fullDirectory = getFullEncryptedDirectory(url, passwordTb.Text, SALT);
                                    Directory.CreateDirectory(fullDirectory);
                                    fileName = EncryptFileString(node.InnerText, passwordTb.Text, SALT);

                                    // Make sure the filename doesn't already exist. Do what Windows does if it does
                                    int numFilesThatAlreadyHaveThisName = 0;
                                    while (File.Exists(Path.Combine(fullDirectory, fileName)))
                                    {
                                        numFilesThatAlreadyHaveThisName++;
                                        fileName = EncryptFileString(Path.GetFileNameWithoutExtension(node.InnerText) +
                                            " (" + numFilesThatAlreadyHaveThisName.ToString() + ")" +
                                            Path.GetExtension(node.InnerText), passwordTb.Text, SALT);
                                    }

                                    // Download a byte stream of the file, encrypt that stream, and write it to the path we determined earlier
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
                                    // We are NOT encrypting the downloaded files

                                    // Get the original directory and file name
                                    fullDirectory = getFullDirectory(url);
                                    Directory.CreateDirectory(fullDirectory);
                                    fileName = node.InnerText;

                                    // Make sure the filename doesn't already exist. Do what Windows does if it does
                                    int numFilesThatAlreadyHaveThisName = 0;
                                    while (File.Exists(Path.Combine(fullDirectory, fileName)))
                                    {
                                        numFilesThatAlreadyHaveThisName++;
                                        fileName = Path.GetFileNameWithoutExtension(node.InnerText) +
                                            " (" + numFilesThatAlreadyHaveThisName.ToString() + ")" +
                                            Path.GetExtension(node.InnerText);
                                    }

                                    // Download the file
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
                    MessageBox.Show("Make sure the URL is a valid 4chan thread URL, and that if you are requiring a password that it is at least one character.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving images\n\nTechnical error:\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Main decryption function
        /// </summary>
        private void decrypt()
        {
            try
            {
                // Decrypt the directory we will save to
                string encryptedFolderName = directoryTb.Text.Split('\\')[directoryTb.Text.Split('\\').Length - 1];
                string decryptedFolderName = DecryptFileString(encryptedFolderName, decryptPasswordTb.Text, SALT);
                string decryptedDirectory = directoryTb.Text.Replace(encryptedFolderName, decryptedFolderName);

                // Only continue if the user passed in a directory, not a file
                if (string.IsNullOrEmpty(Path.GetExtension(decryptedFolderName)))
                {
                    string[] files = Directory.GetFiles(directoryTb.Text);
                    Directory.CreateDirectory(decryptedDirectory);

                    // Instantiate variables used to report progress
                    int currentItemIndex = 1;
                    int totalItemsCount = files.Count();
                    int percentComplete;

                    foreach (string file in files)
                    {
                        try
                        {
                            // Report progress
                            percentComplete = (int)((float)currentItemIndex / (float)totalItemsCount * 100);
                            backgroundWorker.ReportProgress(percentComplete);

                            // Read the stream of bytes from the file and decrypt it
                            var decryptedBytes = DecryptBytes(File.ReadAllBytes(file), decryptPasswordTb.Text, SALT);
                            var encryptedFileName = file.Split('\\')[file.Split('\\').Length - 1];
                            var decryptedFileName = DecryptFileString(encryptedFileName, decryptPasswordTb.Text, SALT);

                            // Write the decrypted bytes to the decrypted directory and filename
                            // The extension of the file is preserved in the filename, so we can avoid the encoding detection headache
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
                    // It's OK to show the filename they tried to decrypt because they have already supplied a correct password at this point
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
        /// <summary>
        /// Validates if the clipboard contains text that is a valid 4chan thread URL
        /// </summary>
        /// <returns>A bool indicating whether or not the clipboard contains text that is a valid 4chan thread URL</returns>
        private bool clipboardTextIsThreadURL()
        {
            return Clipboard.ContainsText() && URLIsValid(Clipboard.GetText());
        }

        /// <summary>
        /// Validates if the string passed in is a valid 4chan thread URL
        /// </summary>
        /// <param name="url">The string to check</param>
        /// <returns>A bool indicating whether or not the string passed in is a valid 4chan thread URL</returns>
        private bool URLIsValid(string url)
        {
            return Regex.IsMatch(url, URLREGEX);
        }

        /// <summary>
        /// Validates if the HTML document passed in is an existant 4chan thread
        /// </summary>
        /// <param name="doc">The HTML document to check</param>
        /// <returns>A bool indicating whether or not the HTML document passed in is an existant 4chan thread</returns>
        private bool threadIsFound(HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode(TITLEXPATH).InnerText != NOTFOUNDTITLE;
        }
        #endregion

        #region Getter Functions
        /// <summary>
        /// Function to get the full directory to save images to, based on a 4chan thread url passed in.
        ///     Format is: BASEDIRECTORY/board/thread name (thread number)
        /// </summary>
        /// <param name="url">The url to build the directory from</param>
        /// <returns>A string containing the directory</returns>
        private string getFullDirectory(string url)
        {
            string[] urlParts = url.Replace(BASEURL, "").Split('/');
            string board = urlParts[0];
            string thread = string.Format("{0} ({1})", urlParts[urlParts.Length - 1], urlParts[urlParts.Length - 2]);
            return Path.Combine(Environment.ExpandEnvironmentVariables(BASEDIRECTORY), board, thread);
        }

        /// <summary>
        /// Function to get the full directory to save encrypted images to, based on a 4chan thread url passed in.
        ///     Format is: BASEDIRECTORY/ENCRYPTED SUBDIRECTORY/encrypted thread name and number
        /// </summary>
        /// <param name="url">The url to build the directory from</param>
        /// <param name="password">The password to use for encryption</param>
        /// <param name="salt">The salt to use for encryption</param>
        /// <returns>A string containing the directory</returns>
        private string getFullEncryptedDirectory(string url, string password, string salt)
        {
            string[] urlParts = url.Replace(BASEURL, "").Split('/');
            string board = Environment.ExpandEnvironmentVariables(ENCRYPTEDSUBDIRECTORY);
            string thread = EncryptFileString(string.Format("{0} ({1})", urlParts[urlParts.Length - 1], urlParts[urlParts.Length - 2]), password, salt);
            return Path.Combine(Environment.ExpandEnvironmentVariables(BASEDIRECTORY), board, thread);
        }

        /// <summary>
        /// Get an HTML document from a url passed in
        /// </summary>
        /// <param name="url">The url to get the document for</param>
        /// <returns>The HTML document</returns>
        private HtmlAgilityPack.HtmlDocument getTheadDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }

        /// <summary>
        /// Gets a collection of image anchor nodes from a 4chan thread document
        /// </summary>
        /// <param name="doc">An HTML document of a 4chan thread</param>
        /// <returns>An collection of HTML nodes of the image anchors</returns>
        private HtmlNodeCollection getImageNodes(HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.SelectNodes(IMAGEANCHORXPATH);
        }

        /// <summary>
        /// Populates the global variables from the config file
        ///     It is public so that the Options window can call this when it closes
        /// </summary>
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
        /// <summary>
        /// Encrypt an array of bytes
        /// Thanks to the top answer on http://stackoverflow.com/questions/4438691/simple-encryption-decryption-method-for-encrypting-an-image-file
        /// </summary>
        /// <param name="inputBytes">The bytes to encrypt</param>
        /// <param name="passPhrase">The password to use for encryption</param>
        /// <param name="saltValue">The salt to use for decryption</param>
        /// <returns>The encrypted bytes</returns>
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

        /// <summary>
        /// Decrypt an array of bytes
        /// Thanks to the top answer on http://stackoverflow.com/questions/4438691/simple-encryption-decryption-method-for-encrypting-an-image-file
        /// </summary>
        /// <param name="encryptedBytes">The encrypted bytes</param>
        /// <param name="passPhrase">The password to use for encryption</param>
        /// <param name="saltValue">The salt to use for decryption</param>
        /// <returns>The decrypted bytes</returns>
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

        /// <summary>
        /// Encrypt a string suitable for use in a directory or file name
        /// </summary>
        /// <param name="inputString">The string to encrypt</param>
        /// <param name="passPhrase">The password to use for encryption</param>
        /// <param name="saltValue">The salt to use for decryption</param>
        /// <returns>The encrypted string</returns>
        public string EncryptFileString(string inputString, string passPhrase, string saltValue)
        {
            // Use UTF8 to encrypt (instead of ASCII) because it only uses valid characters, 
            //     except for "/", which we replace with "-", which isn't used by UTF8 encryption, but is a valid file character
            var byteArray = Encoding.UTF8.GetBytes(inputString);
            var encryptedBytes = EncryptBytes(byteArray, passPhrase, saltValue);
            var encryptedString = Convert.ToBase64String(encryptedBytes).Replace("/", "-");

            return encryptedString;
        }

        /// <summary>
        /// Decrypt a string suitable for use in a directory or file name
        /// </summary>
        /// <param name="encryptedString">The string to decrypt</param>
        /// <param name="passPhrase">The password to use for encryption</param>
        /// <param name="saltValue">The salt to use for decryption</param>
        /// <returns>The decrypted string</returns>
        public string DecryptFileString(string encryptedString, string passPhrase, string saltValue)
        {
            // Replace back that "/" which we had to get rid of
            var byteArray = Convert.FromBase64String(encryptedString.Replace("-", "/"));
            var decryptedBytes = DecryptBytes(byteArray, passPhrase, saltValue);
            var decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            return new string(decryptedString.Where(x => !Path.GetInvalidFileNameChars().Contains(x)).ToArray());
        }
        #endregion
    }
}