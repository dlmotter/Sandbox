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
            Button buttonSender = (Button)sender;
            // Button and textbox must be named <name>Btn and <name>Tb, respectively
            TextBox textboxSender = (TextBox)Controls.Find(buttonSender.Name.Replace("Btn", "Tb"), true)[0];

            // Set the filter appropriately
            switch (buttonSender.Name)
            {
                case "containerImageBtn":
                    openFileDialog.Filter = Program._containerImageFilter;
                    break;
                case "hiddenFileBtn":
                    openFileDialog.Filter = "All Files|*.*";
                    break;
                case "encodedImageBtn":
                    openFileDialog.Filter = Program._resultImageFilter;
                    break;
            }

            // Only set the text if it is a valid type
            openFileDialog.ShowDialog();
            string lowerExtension = Path.GetExtension(openFileDialog.FileName).ToLower();
            if ((buttonSender.Name == "containerImageBtn" && Program._containerImageTypes.Contains(lowerExtension)) ||
                (buttonSender.Name == "hiddenFileBtn" && Program._fileTypeMapping.ContainsValue(lowerExtension)) ||
                (buttonSender.Name == "encodedImageBtn" && Program._resultImageTypes.Contains(lowerExtension)))
            {
                textboxSender.Text = openFileDialog.FileName;
            }
            else
            {
                MessageBox.Show(string.Format("The file type \"{0}\" is not supported.", lowerExtension), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenericFileSave_Click(object sender, EventArgs e)
        {
            Button buttonSender = (Button)sender;
            // Button and textbox must be named <name>Btn and <name>Tb, respectively
            TextBox textboxSender = (TextBox)Controls.Find(buttonSender.Name.Replace("Btn", "Tb"), true)[0];

            // Only set the text if it is a valid type
            saveFileDialog.ShowDialog();
            string lowerExtension = Path.GetExtension(saveFileDialog.FileName).ToLower();
            if (Program._resultImageTypes.Contains(lowerExtension))
            {
                textboxSender.Text = saveFileDialog.FileName;
            }
            else
            {
                MessageBox.Show(string.Format("The file type \"{0}\" is not supported.", lowerExtension), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Drag/drop handlers
        private void GenericTextBox_DragEnter(object sender, DragEventArgs e)
        {
            TextBox typedSender = (TextBox)sender;
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            string lowerExtension = Path.GetExtension(fileList[0]).ToLower();
            bool fileIsValid =
                (typedSender.Name == "containerImageTb" && Program._containerImageTypes.Contains(lowerExtension)) ||
                (typedSender.Name == "hiddenFileTb" && Program._fileTypeMapping.ContainsValue(lowerExtension)) ||
                (typedSender.Name == "resultImageTb" && Program._resultImageTypes.Contains(lowerExtension)) ||
                (typedSender.Name == "encodedImageTb" && Program._resultImageTypes.Contains(lowerExtension));
                
            // Change the mouse icon depending if the user is trying to drop something legit
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && fileIsValid)
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
            TextBox typedSender = (TextBox)sender;
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Validation is taken care of in the drag enter handler
            typedSender.Text = fileList[0];
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
