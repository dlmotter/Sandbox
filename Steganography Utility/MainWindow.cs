using System;
using System.ComponentModel;
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

            hiddenImageTb.DragEnter += GenericTextBox_DragEnter;
            hiddenImageTb.DragDrop += GenericTextBox_DragDrop;

            resultImageTb.DragEnter += GenericTextBox_DragEnter;
            resultImageTb.DragDrop += GenericTextBox_DragDrop;

            //   Decode tab
            encodedImageTb.DragEnter += GenericTextBox_DragEnter;
            encodedImageTb.DragDrop += GenericTextBox_DragDrop;

            decodedImageTb.DragEnter += GenericTextBox_DragEnter;
            decodedImageTb.DragDrop += GenericTextBox_DragDrop;
        }

        private void hiddenTextRb_CheckedChanged(object sender, EventArgs e)
        {
            hiddenTextTb.Enabled = hiddenTextRb.Checked;
            hiddenImageBtn.Enabled = !hiddenTextRb.Checked;
            hiddenImageTb.Enabled = !hiddenTextRb.Checked;
        }

        #region Button clicks
        private void goBtn_Click(object sender, EventArgs e)
        {
            startRun();
        }

        private void GenericFileUpload_Click(object sender, EventArgs e)
        {
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
            try {
                // Since there's only one background worker, check to see which function we are supposed to do
                if ((string)e.Argument == "encodeImage")
                {
                    Program.saveEncodedImage(containerImageTb.Text, hiddenImageTb.Text, resultImageTb.Text);
                }
                else if ((string)e.Argument == "encodeText")
                {
                    Program.saveEncodedText(containerImageTb.Text, hiddenTextTb.Text, resultImageTb.Text);
                }
                else if ((string)e.Argument == "decode")
                {
                    Program.saveDecodedFile(encodedImageTb.Text, decodedImageTb.Text);
                }
                else
                {
                    throw new Exception(string.Format("Unknown operation: \"{0}\"", (string)e.Argument));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker_Callback(object sender, RunWorkerCompletedEventArgs e)
        {
            finishRun();
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
                if (hiddenImageRb.Checked)
                {
                    arg = "encodeImage";
                }
                else
                {
                    arg = "encodeText";
                }
            }
            else
            {
                arg = "decode";
            }

            // Start the background worker, passing in the process we want to run
            backgroundWorker.RunWorkerAsync(arg);
        }

        private void finishRun()
        {
            goBtn.Enabled = true;
        }
        #endregion
    }
}
