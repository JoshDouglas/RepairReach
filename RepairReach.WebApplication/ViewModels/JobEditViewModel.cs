using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobEditViewModel
    {
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Job Number")]
        [Required]
        public int JobNumber { get; set; }
        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }
        public string CustomerDisplayName { get; set; }
        [Display(Name = "Team Member")]
        [Required]
        public int StaffId { get; set; }
        [Display(Name = "Status")]
        [Required]
        public int JobStatusId { get; set; }
        [Display(Name = "Category")]
        public int? JobCategoryId { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        [Required]
        public string City { get; set; }
        [Display(Name = "State")]
        [Required]
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        [Required]
        public string Zipcode { get; set; }
        [Display(Name = "Contact First Name")]
        [Required]
        public string ContactFirstName { get; set; }
        [Display(Name = "Contact Last Name")]
        [Required]
        public string ContactLastName { get; set; }
        [Display(Name = "Mobile Phone")]
        [Required]
        [Phone]
        public string ContactPhone1 { get; set; }
        [Display(Name = "Home Phone")]
        [Phone]
        public string ContactPhone2 { get; set; }
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }
        [Display(Name = "Balance Due")]
        public decimal BalanceDue { get; set; }
        [Required]
        public DateTime JobCreated { get; set; }
        public DateTime? JobClosed { get; set; }
        public DateTime LastViewedTime { get; set; }
        public string LastViewedBy { get; set; }

        //relationships (view only)
        public IEnumerable<ApplianceIndexViewModel> Appliances { get; set; }
        public IEnumerable<AppointmentIndexViewModel> Appointments { get; set; }
        public IEnumerable<JobNoteIndexViewModel> JobNotes { get; set; }
        public IEnumerable<LineItemIndexViewModel> LineItems { get; set; }
        public IEnumerable<PaymentIndexViewModel> Payments { get; set; }
        public IEnumerable<JobEditActivityEventViewModel> ActivityEvents { get; set; } 
    }
}