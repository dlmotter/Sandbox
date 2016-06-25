using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Wallpaper_Setter
{
    public partial class Form1 : Form
    {
        private const string configPath = @"ws_config.xml";
        private const string utilPath = @"ws_util.exe";

        public Form1()
        {
            InitializeComponent();
            parseConfig(false);
        }

        #region Event Handlers
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

        private void fileBtn_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(fileTb.Text);
            }
            catch (Exception ex)
            {
                saveFileDialog1.InitialDirectory = "%USERPROFILE%";
            }

            saveFileDialog1.ShowDialog();
            fileTb.Text = saveFileDialog1.FileName;
        }
        #endregion

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
                            case "file":
                                elem.Value = fileTb.Text;
                                break;
                            case "keep":
                                elem.Value = keepFileDdl.Text;
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
                            case "file":
                                fileTb.Text = Environment.ExpandEnvironmentVariables(elem.Value);
                                break;
                            case "keep":
                                keepFileDdl.Text = elem.Value;
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

        private void setScheduledEvents()
        {
            using (TaskService taskService = new TaskService())
            {
                // Delete old task if it exists
                if (taskService.FindTask("Wallpaper Setter") != null)
                {
                    taskService.RootFolder.DeleteTask("Wallpaper Setter");
                }

                // Define the trigger
                Trigger trigger;
                if (frequencyDdl.Text.Equals("Hour") || frequencyDdl.Text.Equals("Minute"))
                {
                    // Run either every hour or every minute from registration
                    trigger = new RegistrationTrigger();
                    if (frequencyDdl.Text.Equals("Hour"))
                    {
                        trigger.SetRepetition(new TimeSpan(1, 0, 0), TimeSpan.Zero);
                    }
                    else
                    {
                        trigger.SetRepetition(new TimeSpan(0, 1, 0), TimeSpan.Zero);
                    }
                    
                }
                else
                {
                    // If they specify daily, or don't specify a frequency, run every day at 6 am
                    // TODO make that time configurable
                    DateTime now = DateTime.Now;
                    DateTime next = now.Date.AddHours(24 + 6);
                    next = next.AddDays((now - next).Days);

                    trigger = new DailyTrigger();
                    trigger.StartBoundary = next;
                }

                // Define the task
                TaskDefinition newTask = taskService.NewTask();
                newTask.RegistrationInfo.Description = "Wallpaper Setter";
                newTask.Triggers.Add(trigger);
                newTask.Actions.Add(new ExecAction(utilPath, workingDirectory: @"C:\Program Files\Wallpaper Setter"));
                newTask.Settings.Enabled = true;
                taskService.RootFolder.RegisterTaskDefinition("Wallpaper Setter", newTask);
            }
        }
    }
}
