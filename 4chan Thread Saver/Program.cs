using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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

                // Loop through each setting...
                foreach (string valueName in DefaultValues.valueNames)
                {
                    // And if it isn't present (likely this is the first run of the app)...
                    if (string.IsNullOrWhiteSpace(configuration.AppSettings.Settings[valueName].Value))
                    {
                        // Set it to the default
                        configuration.AppSettings.Settings[valueName].Value = DefaultValues.getValue(valueName);
                    }
                }

                // Generate a new encryption salt if none is set (likely this is the first run of the app)...
                if (string.IsNullOrWhiteSpace(configuration.AppSettings.Settings["salt"].Value))
                {
                    configuration.AppSettings.Settings["salt"].Value = generateSalt();
                }

                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");

                // Launch main window
                Application.Run(new MainWindow());
            }
            catch (Exception ex)
            {
                // There was an error parsing the config file
                var response = MessageBox.Show("Invalid or missing configuration file detected.\nWould you like a new one to be generated?", "Invalid Configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response.ToString() == "Yes")
                {
                    try
                    {
                        generateConfigFile();
                        Main();
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Unable to create a new configuration file.\n\nTechnical Error:\n" + ex2.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Method to generate a secure, 128 character long encryption salt
        /// </summary>
        /// <returns>A 128 character long string suitable to be used as an encryption salt</returns>
        public static string generateSalt()
        {
            byte[] linkBytes = new byte[96];
            // Use the RNGCryptoServiceProvider object because the normal Random namespace isn't good enough for security applications
            var rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(linkBytes);
            var text128 = Convert.ToBase64String(linkBytes);

            return text128;
        }

        /// <summary>
        /// Generate a config file using default values
        /// </summary>
        public static void generateConfigFile()
        {
            string fileName = string.Format("{0}.config", AppDomain.CurrentDomain.FriendlyName);

            StringBuilder docText = new StringBuilder();
            docText.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            docText.AppendLine("<!-- Autogenerated -->");
            docText.AppendLine("<configuration>");
            docText.AppendLine("  <startup>");
            docText.AppendLine("    <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.6.1\" />");
            docText.AppendLine("  </startup>");
            docText.AppendLine("  <appSettings>");
            foreach (string valueName in DefaultValues.valueNames)
            {
                docText.AppendLine(string.Format("    <add key=\"{0}\" value=\"{1}\" />", valueName, DefaultValues.getValue(valueName)));
            }
            docText.AppendLine(string.Format("    <add key=\"salt\" value=\"{0}\" />", generateSalt()));
            docText.AppendLine("  </appSettings>");
            docText.AppendLine("</configuration>");

            File.WriteAllText(fileName, docText.ToString());
        }
    }
}
