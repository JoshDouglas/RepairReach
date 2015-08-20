using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RepairReach.Core.Model;
using RepairReach.Import.BlueFolder;
using RepairReach.Import.Interface;
using RepairReach.Import.Model;

namespace RepairReach.BlueFolderImport
{
    public partial class MainForm : Form
    {
        private readonly ICustomImportService _blueFolderImportService;
        private BackgroundWorker _convertWorker;

        public MainForm()
        {
            InitializeComponent();
            _blueFolderImportService = new BlueFolderImportService();
            _convertWorker = new BackgroundWorker();

            _convertWorker.DoWork += new DoWorkEventHandler(_convertWorker_DoWork);
            _convertWorker.ProgressChanged += new ProgressChangedEventHandler
                    (_convertWorker_ProgressChanged);
            _convertWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (_convertWorker_RunWorkerCompleted);
            _convertWorker.WorkerReportsProgress = true;
            _convertWorker.WorkerSupportsCancellation = true;
        }

        private void btnFilesExist_Click(object sender, EventArgs e)
        {
            if (_blueFolderImportService.ImportFilesExist() == true)
            {
                MessageBox.Show("Import files exist.");
            }
            else
            {
                MessageBox.Show("Import files don't exist.");
            }
        }

        private void btnConvertCustomers_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<ImportCustomer> customers = _blueFolderImportService.ConvertCustomers();
            _blueFolderImportService.SaveCustomers(customers);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertStaf_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<Staff> staffs = _blueFolderImportService.ConvertStaffs();
            _blueFolderImportService.SaveStaffs(staffs);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertJobStatus_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<JobStatus> jobStatuses = _blueFolderImportService.ConvertJobStatuses();
            _blueFolderImportService.SaveJobStatuses(jobStatuses);
            this.Cursor = Cursors.Default;
        }

        private void btnConverTaxRates_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<TaxRate> taxRates = _blueFolderImportService.ConvertTaxRates();
            _blueFolderImportService.SaveTaxRates(taxRates);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertJobs_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<Job> jobs = _blueFolderImportService.ConvertJobs();
            _blueFolderImportService.SaveJobs(jobs);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertLineItems_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<LineItem> lineItems = _blueFolderImportService.ConvertLineItems();
            _blueFolderImportService.SaveLineItems(lineItems);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertJobNotes_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<JobNote> jobNotes = _blueFolderImportService.ConvertJobNotes();
            _blueFolderImportService.SaveJobNotes(jobNotes);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertQuickLines_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<QuickLineItem> quickLineItems = _blueFolderImportService.ConvertQuickLineItems();
            _blueFolderImportService.SaveQuickLineItems(quickLineItems);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertMarketing_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<HowDidYouFindUs> howDidYouFindUses = _blueFolderImportService.ConvertHowDidYouFindUses();
            _blueFolderImportService.SaveHowDidYouFindUses(howDidYouFindUses);
            this.Cursor = Cursors.Default;
        }

        private void btnConvertAll_Click(object sender, EventArgs e)
        {
            txtProgress.Clear();
            btnConvertAll.Enabled = false;
            btnStopConvert.Enabled = true;
            _convertWorker.RunWorkerAsync();
        }

        private void btnStopConvert_Click(object sender, EventArgs e)
        {
            if (_convertWorker.IsBusy) _convertWorker.CancelAsync();
        }

        private void _convertWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                txtProgress.Text += "Import was cancelled.";
            }
            else if (e.Error != null)
            {
                txtProgress.Text += "Error while performing import." + 
                    Environment.NewLine + e.Error.Message + 
                    Environment.NewLine + e.Error.StackTrace;
            }
            else
            {
                txtProgress.Text += "Import completed.";
            }

            btnConvertAll.Enabled = true;
            btnStopConvert.Enabled = false;
            pbConvert.Value = 0;
        }

        private void _convertWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbConvert.Value = e.ProgressPercentage;
            txtProgress.Text += e.UserState as string;
        }

        private void _convertWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime startTime;
            DateTime endTime;
            string progressMessage = string.Empty;

            startTime = DateTime.Now;
            progressMessage = "Import started at " + startTime.ToString() + Environment.NewLine;
            _convertWorker.ReportProgress(0, progressMessage);

            //do files exist
            progressMessage = "Checking if files exist..." + Environment.NewLine;
            _convertWorker.ReportProgress(0, progressMessage);
            if (_blueFolderImportService.ImportFilesExist() == true)
            {
                progressMessage = "Files exist, beginning import..." + Environment.NewLine;
                _convertWorker.ReportProgress(0, progressMessage);
            }
            else
            {
                progressMessage = "Files don't exist, exiting..." + Environment.NewLine;
                _convertWorker.ReportProgress(5, progressMessage);
                e.Cancel = true;
            }

            //database connection
            progressMessage = "Checking if database connection is good...";
            _convertWorker.ReportProgress(5, progressMessage);
            if (_blueFolderImportService.DatabaseIsConnected() == true)
            {
                progressMessage = "Connection is good, beginning import..." + Environment.NewLine;
                _convertWorker.ReportProgress(5, progressMessage);
            }
            else
            {
                progressMessage = "Connection is BAD, exiting..." + Environment.NewLine;
                _convertWorker.ReportProgress(5, progressMessage);
                e.Cancel = true;
            }

            //non-dependent imports
            progressMessage = "Importing marketing fields..." + Environment.NewLine;
            _convertWorker.ReportProgress(5, progressMessage);
            IEnumerable<HowDidYouFindUs> howDidYouFindUses = _blueFolderImportService.ConvertHowDidYouFindUses();
            _blueFolderImportService.SaveHowDidYouFindUses(howDidYouFindUses);
            progressMessage = "Imported " + howDidYouFindUses.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(10, progressMessage);

            progressMessage = "Importing quick line items..." + Environment.NewLine;
            _convertWorker.ReportProgress(10, progressMessage);
            IEnumerable<QuickLineItem> quickLineItems = _blueFolderImportService.ConvertQuickLineItems();
            _blueFolderImportService.SaveQuickLineItems(quickLineItems);
            progressMessage = "Imported " + quickLineItems.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(15, progressMessage);

            progressMessage = "Importing tax rates..." + Environment.NewLine;
            _convertWorker.ReportProgress(15, progressMessage);
            IEnumerable<TaxRate> taxRates = _blueFolderImportService.ConvertTaxRates();
            _blueFolderImportService.SaveTaxRates(taxRates);
            progressMessage = "Imported " + taxRates.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(20, progressMessage);

            _convertWorker.ReportProgress(20, progressMessage);
            progressMessage = "Importing job statuses..." + Environment.NewLine;
            IEnumerable<JobStatus> jobStatuses = _blueFolderImportService.ConvertJobStatuses();
            _blueFolderImportService.SaveJobStatuses(jobStatuses);
            progressMessage = "Imported " + jobStatuses.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(25, progressMessage);

            progressMessage = "Importing staff..." + Environment.NewLine;
            _convertWorker.ReportProgress(25, progressMessage);
            IEnumerable<Staff> staffs = _blueFolderImportService.ConvertStaffs();
            _blueFolderImportService.SaveStaffs(staffs);
            progressMessage = "Imported " + staffs.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(30, progressMessage);

            //main imports
            progressMessage = "Importing customers..." + Environment.NewLine;
            _convertWorker.ReportProgress(30, progressMessage);
            IEnumerable<ImportCustomer> customers = _blueFolderImportService.ConvertCustomers();
            _blueFolderImportService.SaveCustomers(customers);
            progressMessage = "Imported " + customers.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(50, progressMessage);

            progressMessage= "Importing jobs..." + Environment.NewLine;
            _convertWorker.ReportProgress(50, progressMessage);
            IEnumerable<Job> jobs = _blueFolderImportService.ConvertJobs();
            _blueFolderImportService.SaveJobs(jobs);
            progressMessage = "Imported " + jobs.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(75, progressMessage);

            progressMessage = "Importing line items..." + Environment.NewLine;
            _convertWorker.ReportProgress(75, progressMessage);
            IEnumerable<LineItem> lineItems = _blueFolderImportService.ConvertLineItems();
            _blueFolderImportService.SaveLineItems(lineItems);
            progressMessage = "Imported " + lineItems.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(85, progressMessage);

            progressMessage = "Importing job notes..." + Environment.NewLine;
            _convertWorker.ReportProgress(85, progressMessage);
            IEnumerable<JobNote> jobNotes = _blueFolderImportService.ConvertJobNotes();
            _blueFolderImportService.SaveJobNotes(jobNotes);
            progressMessage = "Imported " + lineItems.Count() + " records" + Environment.NewLine;
            _convertWorker.ReportProgress(95, progressMessage);

            endTime = DateTime.Now;
            progressMessage = "Import finished at " + endTime.ToString() + Environment.NewLine;
            _convertWorker.ReportProgress(95, progressMessage);

            var totalTimeElapsed = endTime.Subtract(startTime);

            progressMessage = "Total processing time: " + totalTimeElapsed.ToString() + Environment.NewLine;
            _convertWorker.ReportProgress(100, progressMessage);
        }
    }
}
