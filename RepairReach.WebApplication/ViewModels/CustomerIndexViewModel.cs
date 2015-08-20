using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RepairReach.Core.Enum;

namespace RepairReach.WebApplication.ViewModels
{
    public class CustomerIndexViewModel
    {
        public int CustomerId { get; set; }
        [Display(Name = "Designation")]
        public CustomerDesignationEnum Designation { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone 1")]
        public string Phone1 { get; set; }
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string Zipcode { get; set; }


        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(CompanyName) == false) return CompanyName;

                return FirstName + " " + LastName;
            }
        }
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
    }
}