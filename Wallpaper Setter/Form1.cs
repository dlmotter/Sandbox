using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Wallpaper_Setter
{
    public partial class Form1 : Form
    {
        // TODO unhardcode this
        private const string configPath = @"C:\GitHubRepos\Sandbox\Wallpaper Setter\ws_config.xml";

        public Form1()
        {
            InitializeComponent();
            parseConfig(false);
        }

        private void typeDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryDdl.Enabled = typeDdl.Text == "Category";
            filterDdl.Enabled = typeDdl.Text == "Category";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (typeDdl.Text == "Category" && (categoryDdl.Text.Trim().Length == 0 || filterDdl.Text.Trim().Length == 0))
            {
                MessageBox.Show("You must specify a category and filter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                parseConfig(true);
                setScheduledEvents();

                MessageBox.Show("Save successful!");
            }
        }

        /// <summary>
        /// Read of write from/to the config file
        /// </summary>
        /// <param name="saving">Whether or not to save the form or read from it</param>
        private void parseConfig(bool saving)
        {
            try
            {
                XDocument config = XDocument.Load(configPath);

                foreach (XElement elem in config.Root.Elements())
                {
                    if (saving)
                    {
                        switch (elem.Name.LocalName)
                        {
                            case "type":
                                elem.Value = typeDdl.Text;
                                break;
                            case "category":
                                elem.Value = typeDdl.Text == "Category" ? categoryDdl.Text : "";
                                break;
                            case "filter":
                                elem.Value = typeDdl.Text == "Category" ? filterDdl.Text : "";
                                break;
                            case "frequency":
                                elem.Value = frequencyDdl.Text;
                                break;
                        }
                    }
                    else
                    {
                        switch (elem.Name.LocalName)
                        {
                            case "type":
                                typeDdl.Text = elem.Value.Trim().Length == 0 ? "Wallpaper of the Day" : elem.Value;
                                break;
                            case "category":
                                categoryDdl.Text = elem.Value;
                                break;
                            case "filter":
                                filterDdl.Text = elem.Value;
                                break;
                            case "frequency":
                                frequencyDdl.Text = elem.Value.Trim().Length == 0 ? "Daily" : elem.Value;
                                break;
                        }
                    }
                }

                if (saving)
                {
                    config.Save(configPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Delete any outdated scheduled Windows events and save the new one if necessary
        /// </summary>
        private void setScheduledEvents()
        {
            //TODO implement
        }
    }
}
