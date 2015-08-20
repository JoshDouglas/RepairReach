using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandrill;
using System.IO;
using System.Threading.Tasks;

namespace RepairReach.WebApplication.Mailer
{
    class MandrillMailer : ITransactionMailer
    {
        private MandrillApi GetApi()
        {
            return new MandrillApi("1Td6dl5BFNAo4fsCnBlVCA", false);
        }

        public async Task<bool> SendInvoiceEmailAsync(string customerEmail, string companyEmail, string companyName, string pdfFilePath)
        {
            try
            {
                EmailMessage message = new EmailMessage();
                message.from_email = companyEmail;
                message.from_name = companyName;
                message.text = "Attached is your invoice from " + companyName + ".";
                message.subject = "Invoice";
                message.to = new List<EmailAddress>()
                {
                    new EmailAddress(customerEmail)
                };

                IList<email_attachment> attachments = new List<email_attachment>();

                //file to baes 64
                var pdfBytes = File.ReadAllBytes(pdfFilePath);
                var pdfBase64 = Convert.ToBase64String(pdfBytes);

                var pdfAttachment = new email_attachment();
                pdfAttachment.content = pdfBase64;
                pdfAttachment.type = "application/pdf";
                pdfAttachment.name = Path.GetFileName(pdfFilePath);

                attachments.Add(pdfAttachment);
                message.attachments = attachments;

                MandrillApi api = GetApi();
                var results = await api.SendMessageAsync(message);

                foreach (var result in results)
                {
                    if (result.Status != EmailResultStatus.Sent && result.Status != EmailResultStatus.Queued && result.Status != EmailResultStatus.Scheduled) return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
