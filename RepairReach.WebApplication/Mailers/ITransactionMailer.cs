using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.WebApplication.Mailer
{
    interface ITransactionMailer
    {
        Task<bool> SendInvoiceEmailAsync(string customerEmail, string companyEmail, string companyName, string pdfFilePath);
    }
}
