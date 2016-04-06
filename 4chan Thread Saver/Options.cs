using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace _4chan_Thread_Saver
{
    public partial class Options : Form
    {
        #region Global Variables
        private MainWindow callingWindow;
        #endregion

        #region System Functions
        public Options(MainWindow caller)
        {
            InitializeComponent();
            // Make this window pop up in the same place as the calling window
            callingWindow = caller;
            Location = new System.Drawing.Point(caller.Location.X, caller.Location.Y);

            // Wire up textbox lose focus handlers
            encryptedSubDirectoryTb.LostFocus += EncryptedSubDirectoryTb_LostFocus;
            baseDirectoryTb.LostFocus += BaseDirectoryTb_LostFocus;

            // Populate the textbox values
            setTbValuesFromConfig();
        }

        private void BaseDirectoryTb_LostFocus(object sender, EventArgs e)
        {
            // When the textbox loses focues, set our directory labels so the user can see the real directory
            //     (i.e. it has the environment variables expanded)
            fullBaseDirectoryValueLbl.Text = getFullBaseDirectory();
            fullEncryptedDirectoryValueLbl.Text = getFullEncryptedPath();
        }

        private void EncryptedSubDirectoryTb_LostFocus(object sender, EventArgs e)
        {
            // When the textbox loses focues, set our directory label so the user can see the real directory
            //     (i.e. it has the environment variables expanded and the base directory prepended)
            fullEncryptedDirectoryValueLbl.Text = getFullEncryptedPath();
        }

        private void defaultBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Populate the textboxes with default values, but don't save them
            setTbValuesFromDefault();
        }

        private void submitBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Save the values, set the global vars on the parent window, and close
            setConfigValuesFromTbs();
            callingWindow.getGlobalVarsFromConfig();
            Close();
        }

        private void cancelBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Close without saving
            Close();
        }

        private void regenerateSaltLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Warn the user of the consequences of regenerating an encryption salt
            DialogResult response = MessageBox.Show("Regenerating a new encryption salt will cause you to be unable to decrypt any images saved with the old salt.\n\nContinue?",
                "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (response.ToString() == "Yes")
            {
                setNewSalt();
            }
        }
        #endregion

        #region Getter/Setter Functions
        /// <summary>
        /// Gets the full encrypted path
        /// </summary>
        /// <returns>The full encrypted path</returns>
        private string getFullEncryptedPath()
        {
            // Combine expanded base directory and encrypted sub directory
            return Path.Combine(Environment.ExpandEnvironmentVariables(baseDirectoryTb.Text), Environment.ExpandEnvironmentVariables(encryptedSubDirectoryTb.Text));
        }

        /// <summary>
        /// Get the full base directory
        /// </summary>
        /// <returns>The full base directory</returns>
        private string getFullBaseDirectory()
        {
            // Expand base directory
            return Environment.ExpandEnvironmentVariables(baseDirectoryTb.Text);
        }

        /// <summary>
        /// Set the values of the textboxes based on the config file
        /// </summary>
        private void setTbValuesFromConfig()
        {
            baseDirectoryTb.Text = ConfigurationManager.AppSettings.Get("baseDirectory");
            fullBaseDirectoryValueLbl.Text = getFullBaseDirectory();
            encryptedSubDirectoryTb.Text = ConfigurationManager.AppSettings.Get("encryptedSubDirectory");
            fullEncryptedDirectoryValueLbl.Text = getFullEncryptedPath();
            baseUrlTb.Text = ConfigurationManager.AppSettings.Get("baseUrl");
            notFoundTitleTb.Text = ConfigurationManager.AppSettings.Get("notFoundTitle");
            titleXPathTb.Text = ConfigurationManager.AppSettings.Get("titleXPath");
            imageAnchorXPathTb.Text = ConfigurationManager.AppSettings.Get("imageAnchorXPath");
            urlRegExTb.Text = ConfigurationManager.AppSettings.Get("urlRegEx");
        }

        /// <summary>
        /// Set the values of the textboxes based on the default values
        /// </summary>
        private void setTbValuesFromDefault()
        {
            baseDirectoryTb.Text = Program.DefaultValues.baseDirectory;
            fullBaseDirectoryValueLbl.Text = getFullBaseDirectory();
            encryptedSubDirectoryTb.Text = Program.DefaultValues.encryptedSubDirectory;
            fullEncryptedDirectoryValueLbl.Text = getFullEncryptedPath();
            baseUrlTb.Text = Program.DefaultValues.baseUrl;
            notFoundTitleTb.Text = Program.DefaultValues.notFoundTitle;
            titleXPathTb.Text = Program.DefaultValues.titleXPath;
            imageAnchorXPathTb.Text = Program.DefaultValues.imageAnchorXPath;
            urlRegExTb.Text = Program.DefaultValues.urlRegEx;
        }

        /// <summary>
        /// Set the values of the config file based on the textboxes
        /// </summary>
        private void setConfigValuesFromTbs()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["baseDirectory"].Value = baseDirectoryTb.Text;
            configuration.AppSettings.Settings["encryptedSubDirectory"].Value = encryptedSubDirectoryTb.Text;
            configuration.AppSettings.Settings["baseUrl"].Value = baseUrlTb.Text;
            configuration.AppSettings.Settings["notFoundTitle"].Value = notFoundTitleTb.Text;
            configuration.AppSettings.Settings["titleXPath"].Value = titleXPathTb.Text;
            configuration.AppSettings.Settings["imageAnchorXPath"].Value = imageAnchorXPathTb.Text;
            configuration.AppSettings.Settings["urlRegEx"].Value = urlRegExTb.Text;

            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Generate and save a new encryption salt to the config file
        /// </summary>
        private void setNewSalt()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["salt"].Value = Program.generateSalt();
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }
}
