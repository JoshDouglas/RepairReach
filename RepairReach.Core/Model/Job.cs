using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class Job
    {
        public int JobId { get; set; }
        
        public virtual Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public int JobNumber { get; set; }

        public virtual JobStatus JobStatus { get; set; }

        public int JobStatusId { get; set; }

        public virtual JobCategory JobCategory { get; set; }

        public int? JobCategoryId { get; set; }

        public virtual ICollection<JobNote> JobNotes { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public virtual Location Location { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public string ContactPhone1 { get; set; }

        public string ContactPhone2 { get; set; }

        public DateTime LastViewedTime { get; set; }

        public string LastViewedBy { get; set; }

        public DateTime JobCreated { get; set; }
        public DateTime? JobAuthorized { get; set; }
        public DateTime? JobScheduled { get; set; }
        public DateTime? JobStarted { get; set; }
        public DateTime? JobFinished { get; set; }
        public DateTime? JobClosed { get; set; }
        public DateTime? JobBilled { get; set; }
        public bool IsAuthorized { get; set; }
        //public bool IsClosed { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<ActivityEvent> ActivityEvents { get; set; }

        public virtual ICollection<Appliance> Appliances { get; set; } 

        public virtual Staff SalesRepresentative { get; set; } //Sales Rep

        public int StaffId { get; set; }

        public int? ImportedJobId { get; set; }

        public string FullAddress
        {
            get
            {
                string street = Address1;
                if (string.IsNullOrEmpty(Address2) == false)
                {
                    street += " " + Address2;
                }
                return street + " " + City + ", " + State + " " + Zipcode;
            }
        }

        public string FullContact
        {
            get
            {
                return ContactFirstName + " " + ContactLastName;
            }
        }

        public decimal GrandTotal
        {
            get
            {
                if (this.LineItems != null) return this.LineItems.Sum(lineItem => lineItem.TotalAmount);
                return 0;

            }
        }

        public decimal AmountPaid
        {
            get
            {
                if (this.Payments != null) return this.Payments.Sum(payment => payment.PaymentAmount);
                return 0;
            }
        }

        public decimal BalanceDue
        {
            get
            {
                return this.GrandTotal - this.AmountPaid;
            }
        }

        public string JobType
        {
            get
            {
                var jobType = "Estimate";
                if (JobAuthorized.HasValue) jobType = "WorkOrder";
                if (JobClosed.HasValue) jobType = "Invoice";
                return jobType;
            }
        }

        public string JobSubType
        {
            get
            {
                string jobSubType = "Not Authorized";
                if (JobAuthorized.HasValue && JobScheduled.HasValue == false) jobSubType = "Not Scheduled";
                if (JobAuthorized.HasValue && JobStarted.HasValue && JobClosed.HasValue == false) jobSubType = "Work Started";
                if (JobAuthorized.HasValue && JobFinished.HasValue && JobClosed.HasValue == false) jobSubType = "Work Finished";
                if (JobAuthorized.HasValue && JobClosed.HasValue && JobBilled.HasValue) jobSubType = "Billed";
                if (JobAuthorized.HasValue && JobClosed.HasValue && JobBilled.HasValue == false) jobSubType = "Not Billed";
                return jobSubType;
            }
        }

        public string Aging
        {
            get
            {
                if (JobClosed.HasValue)
                {
                    if (BalanceDue != 0)
                    {
                        var daysOver = DateTime.Today.Subtract(JobClosed.Value).Days;
                        if (daysOver < 30)
                        {
                            return "-30";
                        }
                        if (daysOver < 60)
                        {
                            return "+30";
                        }
                        if (daysOver < 90)
                        {
                            return "+60";
                        }
                        if (daysOver < 120)
                        {
                            return "+90";
                        }
                        if (daysOver >= 120)
                        {
                            return "+120";
                        }
                        return "";
                    }
                    else
                    {
                        return "Current";
                    }
                }
                else
                {
                    return "Not Completed";
                }
            }
        }

        public string JobCreatedDisplay
        {
            get { return JobCreated.ToString("d"); }
        }
        public string JobAuthorizedDisplay
        {
            get { return JobAuthorized.HasValue ? JobAuthorized.Value.ToString("d") : string.Empty; }
        }
        public string JobScheduledDisplay
        {
            get { return JobScheduled.HasValue ? JobScheduled.Value.ToString("d") : string.Empty; }
        }
        public string JobStartedDisplay
        {
            get { return JobStarted.HasValue ? JobStarted.Value.ToString("d") : string.Empty; }
        }
        public string JobFinishedDisplay
        {
            get { return JobFinished.HasValue ? JobFinished.Value.ToString("d") : string.Empty; }
        }
        public string JobClosedDisplay
        {
            get { return JobClosed.HasValue ? JobClosed.Value.ToString("d") : string.Empty; }
        }
        public string JobBilledDisplay
        {
            get { return JobBilled.HasValue ? JobBilled.Value.ToString("d") : string.Empty; }
        }        
    }
}
