using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Contact1Name { get; set; }
        public string Contact1Title { get; set; }
        public string Contact1Phone { get; set; }
        public string Contact1Email { get; set; }
        public string Contact2Name { get; set; }
        public string Contact2Title { get; set; }
        public string Contact2Phone { get; set; }
        public string Contact2Email { get; set; }
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
    }
}
