using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class VendorIndexViewModel
    {
        public int VendorId { get; set; }
        [Display(Name = "Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Phone")]
        public string CompanyPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                string street = Address1;
                if (string.IsNullOrEmpty(Address2) == false)
                {
                    street += " " + Address2;
                }
                return street + " " + City + ", " + State + " " + ZipCode;
            }
        }
        [Display(Name = "Contact")]
        public string Contact1Name { get; set; }
    }
}