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

        #region Button clicks
        private void goBtn_Click(object sender, EventArgs e)
        {
            startRun();
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

            string s = "";

            // Parse the file list from the FileDrop object and set the textbox text
            foreach (string File in FileList)
            {
                s = s + " " + File;
            }

            TextBox typedSender = (TextBox)sender;
            typedSender.Text = s;
        }

        #endregion

        #region Async operations
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Since there's only one background worker, check to see which function we are supposed to do
            if ((string)e.Argument == "encode")
            {
                Program.saveEncodedImage(containerImageTb.Text, hiddenImageTb.Text, resultImageTb.Text);
            }
            else
            {
                Program.saveDecodedFile(encodedImageTb.Text, decodedImageTb.Text);
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
            string arg = MainTabControl.SelectedTab.Name == "encodeTabPage" ? "encode" : "decode";

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
