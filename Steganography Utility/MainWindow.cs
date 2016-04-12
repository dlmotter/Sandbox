using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography_Utility
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            backgroundWorker.RunWorkerCompleted += backgroundWorker_Callback;
        }

        #region Button clicks
        private void goBtn_Click(object sender, EventArgs e)
        {
            startRun();
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
