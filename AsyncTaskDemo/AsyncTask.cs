using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTaskDemo
{
    /// <summary>
    /// Demonstrates Appending the text to files Asynchronously and able to cancel the tasks in between. 
    /// </summary>
    public partial class AsyncTask : Form
    {
        #region Declarations

        private bool m_running = false;
        CancellationTokenSource m_cancelTokenSource = null;

        #endregion

        #region Constructor
        public AsyncTask()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Events
        /// <summary>Handles click on button</summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Empty event arguments</param>
        private async void BtnStart_Click(object sender, EventArgs e)
        {
            if (txtFolder.Text == string.Empty)
            {
                txtMessage.Text = "Please select the folder";
                return;
            }
            string[] files = Directory.GetFiles(txtFolder.Text);

            if (files.Length == 0)
            {
                txtMessage.Text = "No files found in the selected folder";
                return;
            }

            // Running flag will be true if button was already clicked, but will reset to false after completed
            if (!m_running)
            {
                // Update display to indicate we are running
                m_running = true;
                lblStatus.Text = "Working";
                BtnStart.Text = "Cancel";
                txtMessage.Text = string.Empty;
                progressBar.Visible = true;

                // Create progress and cancel objects
                Progress<int> prog = new Progress<int>(SetProgress);
                m_cancelTokenSource = new CancellationTokenSource();
                try
                {
                    // Launch the process. After launching, will "return" from this method.
                    await SlowProcess(prog, m_cancelTokenSource.Token);

                    // But, after complete processing will continue here
                    lblStatus.Text = "Done";
                }
                catch (OperationCanceledException)
                {
                    // If the operation was cancelled, the exception will be thrown as though it came from the await line
                    lblStatus.Text = "Canceled";
                }
                finally
                {
                    // Reset the UI
                    BtnStart.Text = "Start";
                    m_running = false;
                    m_cancelTokenSource = null;
                    progressBar.Value = 0;
                    progressBar.Visible = false;
                }
            }
            else
            {
                // User hit the Cancel button, so signal the cancel token and put a temporary message in the UI
                lblStatus.Text = "Waiting to cancel...";
                m_cancelTokenSource.Cancel();
            }
        }

        /// <summary>
        /// This event used to perform browse the folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowserFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            txtFolder.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Sets default folder path on Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncTask_Load(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Files");
            txtFolder.Text = folderBrowserDialog.SelectedPath;
        }

        #endregion

        #region Helper Methods

        /// <summary>Updates the progress display</summary>
        /// <param name="value">The new progress value</param>
        private void SetProgress(int value)
        {
            // Add 1 so that progress is "completed"
            int adjustedValue = value + 1;

            // Make sure value is in range
            adjustedValue = Math.Max(adjustedValue, progressBar.Minimum);
            adjustedValue = Math.Min(adjustedValue, progressBar.Maximum);

            progressBar.Value = adjustedValue;
        }

        /// <summary>Creates a Task that does some makework, but shows progress, and allows for cancel</summary>
        /// <param name="prog">Progress object that allows for reporting of current progress</param>
        /// <param name="ct">Cancellation token that allows code to determine if the user has attempted to cancel the operation</param>
        private Task SlowProcess(IProgress<int> prog, CancellationToken ct)
        {
            string path = txtFolder.Text;
            string[] files = Directory.GetFiles(path);

            //Perform time consuming operation by setting ParallelOptions.MaxDegreeOfParallelism attibute
            //This will make parallelism dependent on the number of CPU cores.

            var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 10 };
            Parallel.ForEach(files, options, file =>
            {
                using (StreamWriter w = File.AppendText(file))
                {
                    w.WriteLine("Content appened through parallelOptions");
                }
            });

            //Performing the time consuming operation through Asynchrous task
            return Task.Run(() =>
            {
                #region Progress Bar
                //for (int i = 0; i < 100; i++)
                //{
                //    // Check to see if the user cancelled. If so, throw an exception (which will be caught in the button_Click method).
                //    // Note: The debugger might stop and show an "OperationCanceledExcpetion was unhandled by user code" popup. You can
                //    //       ignore this (and choose not to show it again for this type of exception). The debugger simply cannot determine
                //    //       that there is actually a catch, since it is around the await call rather than the method call.
                //    //
                //    ct.ThrowIfCancellationRequested();

                //    // Update progress bar
                //    prog.Report(i);

                //    // Make work
                //    Thread.Sleep(200);
                //}
                #endregion

                int noOfFilesProcessed = 0;

                foreach (string file in files)
                {
                    using (StreamWriter w = File.AppendText(file))
                    {
                        w.WriteLine("Content appended through Async call");
                    }

                    noOfFilesProcessed++;

                    // Check to see if the user cancelled. If so, throw an exception (which will be caught in the button_Click method).
                    // Note: The debugger might stop and show an "OperationCanceledExcpetion was unhandled by user code" popup. You can
                    //       ignore this (and choose not to show it again for this type of exception). The debugger simply cannot determine
                    //       that there is actually a catch, since it is around the await call rather than the method call.
                    ct.ThrowIfCancellationRequested();

                    // Update the progress bar
                    prog.Report(CalculatePercentage(noOfFilesProcessed, files.Length));

                    Thread.Sleep(1000);
                }

            }, ct);

        }

        /// <summary>
        /// Calculates the Percentage of Progress bar based on the files processed/completed.
        /// </summary>
        /// <param name="currentFilePosition"></param>
        /// <param name="totalFileCount"></param>
        /// <returns></returns>
        private int CalculatePercentage(int currentFilePosition, int totalFileCount)
        {
            return (currentFilePosition / totalFileCount) * 100;
        }

        #endregion

    }
}
