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
            callingWindow = caller;
            Location = new System.Drawing.Point(caller.Location.X, caller.Location.Y);

            encryptedSubDirectoryTb.LostFocus += EncryptedSubDirectoryTb_LostFocus;
            baseDirectoryTb.LostFocus += BaseDirectoryTb_LostFocus;

            setTbValuesFromConfig();
        }

        private void BaseDirectoryTb_LostFocus(object sender, EventArgs e)
        {
            fullBaseDirectoryValueLbl.Text = getFullBaseDirectory();
            fullEncryptedDirectoryValueLbl.Text = getFullEncryptedPath();
        }

        private void EncryptedSubDirectoryTb_LostFocus(object sender, EventArgs e)
        {
            fullEncryptedDirectoryValueLbl.Text = getFullEncryptedPath();
        }

        private void defaultBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setTbValuesFromDefault();
        }

        private void submitBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setConfigValuesFromTbs();
            callingWindow.getGlobalVarsFromConfig();
            Close();
        }

        private void cancelBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void regenerateSaltLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult response = MessageBox.Show("Regenerating a new encryption salt will cause you to be unable to decrypt any images saved with the old salt.\n\nContinue?",
                "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (response.ToString() == "Yes")
            {
                setNewSalt();
            }
        }
        #endregion

        #region Getter/Setter Functions
        private string getFullEncryptedPath()
        {
            return Path.Combine(Environment.ExpandEnvironmentVariables(baseDirectoryTb.Text), Environment.ExpandEnvironmentVariables(encryptedSubDirectoryTb.Text));
        }

        private string getFullBaseDirectory()
        {
            return Environment.ExpandEnvironmentVariables(baseDirectoryTb.Text);
        }

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
