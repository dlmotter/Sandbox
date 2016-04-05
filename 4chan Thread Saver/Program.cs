using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _4chan_Thread_Saver
{
    static class Program
    {
        public static class DefaultValues
        {
            public static string[] valueNames = new string[] { "baseDirectory", "encryptedSubDirectory", "baseUrl", "notFoundTitle", "titleXPath", "imageAnchorXPath", "urlRegEx" };
            public static string getValue(string param)
            {
                switch (param.ToLower())
                {
                    case "basedirectory":
                        return baseDirectory;
                    case "encryptedsubdirectory":
                        return encryptedSubDirectory;
                    case "baseurl":
                        return baseUrl;
                    case "notfoundtitle":
                        return notFoundTitle;
                    case "titlexpath":
                        return titleXPath;
                    case "imageanchorxpath":
                        return imageAnchorXPath;
                    case "urlregex":
                        return urlRegEx;
                    default:
                        return null;
                }
            }
            public static string baseDirectory = @"%userprofile%\Pictures\Saved 4chan Threads\";
            public static string encryptedSubDirectory = "Encrypted Threads";
            public static string baseUrl = "https://boards.4chan.org/";
            public static string notFoundTitle = "4chan - 404 Not Found";
            public static string titleXPath = "//title";
            public static string imageAnchorXPath = "//div[@class='fileText']/a";
            public static string urlRegEx = @"^https:\/\/boards\.4chan\.org\/[a-z]+\/thread\/[0-9]+\/[a-z0-9-]+$";
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                foreach (string valueName in DefaultValues.valueNames)
                {
                    if (string.IsNullOrWhiteSpace(configuration.AppSettings.Settings[valueName].Value))
                    {
                        configuration.AppSettings.Settings[valueName].Value = DefaultValues.getValue(valueName);
                    }
                }

                if (string.IsNullOrWhiteSpace(configuration.AppSettings.Settings["salt"].Value))
                {
                    configuration.AppSettings.Settings["salt"].Value = generateSalt();
                    configuration.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }

                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");

                Application.Run(new MainWindow());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid config file.\nAsk Daniel for a new one.\n\nTechnical Error:\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string generateSalt()
        {
            byte[] linkBytes = new byte[96];
            var rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(linkBytes);
            var text128 = Convert.ToBase64String(linkBytes);

            return text128;
        }
    }
}
