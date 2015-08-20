using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class CompanyCreateViewModel
    {
        public int CompanyId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Website")]
        [Url]
        public string Website { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        [Phone]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        [Phone]
        public string Fax { get; set; }
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
        [Display(Name = "Logo")]
        public string LogoPath { get; set; }
        [Display(Name = "Time Zone")]
        public string TimeZoneInfo { get; set; }
    }
}