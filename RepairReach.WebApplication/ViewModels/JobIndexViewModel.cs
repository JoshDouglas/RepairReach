using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobIndexViewModel
    {
        public int JobId { get; set; }
        [Display(Name = "Job Number")]
        public int JobNumber { get; set; }
        [Display(Name = "Customer")]
        public string CustomerDisplayName { get; set; }
        [Display(Name = "Category")]
        public string CategoryDescription { get; set; }
        [Display(Name = "Status")]
        public string StatusDescription { get; set; }
        [Display(Name = "Team Member")]
        public string SalesRepDisplayName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        [Display(Name = "Phone")]
        public string ContactPhone1 { get; set; }
        [Display(Name = "Total")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                string street = Address1;
                if (string.IsNullOrEmpty(Address2) == false) street += " " + Address2;

                return street + " " + City + ", " + State + " " + Zipcode;
            }
        }
        [Display(Name = "Contact")]
        public string FullContactName
        {
            get { return ContactFirstName + " " + ContactLastName; }
        }
    }
}