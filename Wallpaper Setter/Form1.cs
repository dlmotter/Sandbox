﻿using Microsoft.Win32.TaskScheduler;
using System;
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
            try
            {
                parseConfig(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #region Event Handlers
        private void typeDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var categorySelected = typeDdl.Text == "Category";
            var searchSelected = typeDdl.Text == "Search";

            // "Category"-only fields
            categoryDdl.Visible = categorySelected;
            categoryLbl.Visible = categorySelected;
            filterDdl.Visible = categorySelected;
            filterLbl.Visible = categorySelected;

            // "Search"-only fields
            queryTb.Visible = searchSelected;
            queryLbl.Visible = searchSelected;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (typeDdl.Text == "Category" && (categoryDdl.Text.Trim().Length == 0 || filterDdl.Text.Trim().Length == 0))
            {
                MessageBox.Show("You must specify a category and filter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    parseConfig(true);
                    setScheduledEvents();
                    MessageBox.Show("Save successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void fileBtn_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(fileTb.Text);
            }
            catch (Exception)
            {
                saveFileDialog1.InitialDirectory = "%USERPROFILE%";
            }

            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName.Trim().Length > 0)
            {
                fileTb.Text = saveFileDialog1.FileName;
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var result =  MessageBox.Show("This will cancel automatic wallpaper changing.\nDo you wish to continue?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // User wants to delete the wallpaper setter task
            if (result == DialogResult.Yes)
            {
                using (TaskService taskService = new TaskService())
                {
                    if (taskService.FindTask("Wallpaper Setter") != null)
                    {
                        try
                        {
                            taskService.RootFolder.DeleteTask("Wallpaper Setter");
                            MessageBox.Show("Save successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
            }
        }

        private void linkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.thepaperwall.com");
        }
        #endregion

        private void parseConfig(bool saving)
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
                        case "query":
                            elem.Value = typeDdl.Text == "Search" ? queryTb.Text : "";
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
                        case "query":
                            queryTb.Text = elem.Value;
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
                        trigger.Repetition = new RepetitionPattern(new TimeSpan(1, 0, 0), TimeSpan.Zero);
                    }
                    else
                    {
                        trigger.Repetition = new RepetitionPattern(new TimeSpan(0, 1, 0), TimeSpan.Zero);
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
                newTask.Actions.Add(new ExecAction(utilPath, workingDirectory: Directory.GetCurrentDirectory()));
                newTask.Settings.Enabled = true;
                taskService.RootFolder.RegisterTaskDefinition("Wallpaper Setter", newTask);

                // Registration triggers get run automatically when created. But other types don't, so run manually if needed.
                if (newTask.Triggers[0].TriggerType != TaskTriggerType.Registration)
                {
                    taskService.RootFolder.Tasks["Wallpaper Setter"].Run();
                }
            }
        }
    }
}
