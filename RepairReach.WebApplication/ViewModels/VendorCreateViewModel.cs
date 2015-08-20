using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class VendorCreateViewModel
    {
        public int VendorId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Phone")]
        [Phone]
        public string CompanyPhone { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string CompanyEmail { get; set; }
        [Display(Name = "Address")]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string ZipCode { get; set; }
        [Display(Name = "1st Contact Name")]
        public string Contact1Name { get; set; }
        [Display(Name = "Title")]
        public string Contact1Title { get; set; }
        [Display(Name = "Phone")]
        public string Contact1Phone { get; set; }
        [Display(Name = "Email")]
        public string Contact1Email { get; set; }
        [Display(Name = "2nd Contact Name")]
        public string Contact2Name { get; set; }
        [Display(Name = "Title")]
        public string Contact2Title { get; set; }
        [Display(Name = "Phone")]
        public string Contact2Phone { get; set; }
        [Display(Name = "Email")]
        public string Contact2Email { get; set; }
    }
}