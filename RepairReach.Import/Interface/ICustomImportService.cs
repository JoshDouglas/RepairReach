using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;
using RepairReach.Import.Model;

namespace RepairReach.Import.Interface
{
    public interface ICustomImportService
    {
        bool ImportFilesExist();
        IEnumerable<ImportCustomer> ConvertCustomers();
        void SaveCustomers(IEnumerable<ImportCustomer> customers);
        IEnumerable<Job> ConvertJobs();
        void SaveJobs(IEnumerable<Job> jobs);
        IEnumerable<LineItem> ConvertLineItems();
        void SaveLineItems(IEnumerable<LineItem> lineItems);
        IEnumerable<Staff> ConvertStaffs();
        void SaveStaffs(IEnumerable<Staff> staffs);
        IEnumerable<JobNote> ConvertJobNotes();
        void SaveJobNotes(IEnumerable<JobNote> jobNotes);
        IEnumerable<TaxRate> ConvertTaxRates();
        void SaveTaxRates(IEnumerable<TaxRate> taxRates);
        IEnumerable<JobStatus> ConvertJobStatuses();
        void SaveJobStatuses(IEnumerable<JobStatus> jobStatuses);
        IEnumerable<QuickLineItem> ConvertQuickLineItems();
        void SaveQuickLineItems(IEnumerable<QuickLineItem> quickLineItems);
        IEnumerable<HowDidYouFindUs> ConvertHowDidYouFindUses();
        void SaveHowDidYouFindUses(IEnumerable<HowDidYouFindUs> howDidYouFindUses);
        void SaveContext();
        bool DatabaseIsConnected();
    }
}
