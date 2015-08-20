using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RepairReach.Core.Enum;

namespace RepairReach.WebApplication.ViewModels
{
    public class CustomerCreateViewModel
    {
        [Required]
        [Display(Name = "Designation")]
        public CustomerDesignationEnum Designation { get; set; }
        [Display(Name = "Customer Source")]
        public int? HowDidYouFindUsId { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Mobile Phone")]
        [Required]
        [Phone]
        public string Phone1 { get; set; }
        [Display(Name = "Home Phone")]
        [Phone]
        public string Phone2 { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
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
        [Display(Name = "Collect Payment on Site")]
        [Required]
        public bool CollectPaymentOnSite { get; set; }
        [Display(Name = "Call on Way")]
        [Required]
        public bool CallOnWay { get; set; }
        [Display(Name = "Prefers Text Messaging")]
        [Required]
        public bool PrefersTextMessaging { get; set; }
    }
}