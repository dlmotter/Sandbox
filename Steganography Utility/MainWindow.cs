using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Steganography_Utility
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            // Wire up async handler
            backgroundWorker.RunWorkerCompleted += backgroundWorker_Callback;

            // Wire up drag/drop handlers
            //   Encode tab
            containerImageTb.DragEnter += GenericTextBox_DragEnter;
            containerImageTb.DragDrop += GenericTextBox_DragDrop;

            hiddenFileTb.DragEnter += GenericTextBox_DragEnter;
            hiddenFileTb.DragDrop += GenericTextBox_DragDrop;

            resultImageTb.DragEnter += GenericTextBox_DragEnter;
            resultImageTb.DragDrop += GenericTextBox_DragDrop;

            //   Decode tab
            encodedImageTb.DragEnter += GenericTextBox_DragEnter;
            encodedImageTb.DragDrop += GenericTextBox_DragDrop;
        }

        #region Button clicks
        private void goBtn_Click(object sender, EventArgs e)
        {
            startRun();
        }

        private void GenericFileUpload_Click(object sender, EventArgs e)
        {
            //TODO: set openFileDialog filter

            openFileDialog.ShowDialog();

            Button buttonSender = (Button)sender;
            // Button and textbox must be named <name>Btn and <name>Tb, respectively
            TextBox textboxSender = (TextBox)Controls.Find(buttonSender.Name.Replace("Btn", "Tb"), true)[0];

            textboxSender.Text = openFileDialog.FileName;
        }

        private void GenericFileSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();

            Button buttonSender = (Button)sender;
            // Button and textbox must be named <name>Btn and <name>Tb, respectively
            TextBox textboxSender = (TextBox)Controls.Find(buttonSender.Name.Replace("Btn", "Tb"), true)[0];

            textboxSender.Text = saveFileDialog.FileName;
        }
        #endregion

        #region Drag/drop handlers
        private void GenericTextBox_DragEnter(object sender, DragEventArgs e)
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

        private void GenericTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            TextBox typedSender = (TextBox)sender;

            // If user dropped multiple files, only take the first one
            typedSender.Text = FileList[0];
        }
        #endregion

        #region Async operations
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // No try/catch in here
            // https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker.dowork.aspx
            /* "If the operation raises an exception that your code does not handle, the BackgroundWorker catches the exception 
                and passes it into the RunWorkerCompleted event handler, where it is exposed as the Error property of 
                System.ComponentModel.RunWorkerCompletedEventArgs. If you are running under the Visual Studio debugger, the debugger 
                will break at the point in the DoWork event handler where the unhandled exception was raised." */
            
            // In summary, the drawback is that it will be a little more annoying when debugging, but the plus side is that 
            // the RunWorkerCompletedEventArgs object in the callback function will have a meaningful Error property

            // Since there's only one background worker, check to see which function we are supposed to do
            if ((string)e.Argument == "encode")
            {
                Program.saveEncodedFile(containerImageTb.Text, hiddenFileTb.Text, resultImageTb.Text);
            }
            else if ((string)e.Argument == "decode")
            {
                Program.saveDecodedFile(encodedImageTb.Text);
            }
            else
            {
                throw new Exception(string.Format("Unknown operation: \"{0}\"", (string)e.Argument));
            }
        }

        private void backgroundWorker_Callback(object sender, RunWorkerCompletedEventArgs e)
        {
            finishRun(e.Error);
        }
        #endregion

        #region Action functions
        private void startRun()
        {
            goBtn.Enabled = false;

            // Perform the correct action depending on the selected tab
            string arg;
            if (MainTabControl.SelectedTab.Name == "encodeTabPage")
            {
                arg = "encode";
            }
            else
            {
                arg = "decode";
            }

            // Start the background worker, passing in the process we want to run
            backgroundWorker.RunWorkerAsync(arg);
        }

        private void finishRun(Exception ex)
        {
            if (ex == null)
            {
                if (openFolderCb.Checked)
                {
                    string directory = MainTabControl.SelectedTab.Name == "encodeTabPage" ? Path.GetDirectoryName(resultImageTb.Text) : Path.GetDirectoryName(encodedImageTb.Text);
                    Process.Start(directory);
                }
            }
            else
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            goBtn.Enabled = true;
        }
        #endregion
    }
}
