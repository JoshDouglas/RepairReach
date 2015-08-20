using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using RepairReach.Core.Enum;
using RepairReach.Core.Model;

namespace RepairReach.Import.Model
{
    public class ImportCustomer
    {
        public Customer Customer { get; set; }
        public ImportCustomer()
        {
            Customer = new Customer();
            Customer.Designation = CustomerDesignationEnum.Individual;
            Customer.CompanyName = string.Empty;
            Customer.FirstName = string.Empty;
            Customer.LastName = string.Empty;
            Customer.Phone1 = string.Empty;
            Customer.Phone2 = string.Empty;
            Customer.Email = string.Empty;
            Customer.Address1 = string.Empty;
            Customer.City = string.Empty;
            Customer.State = string.Empty;
            Customer.Zipcode = string.Empty;
        }
    }
}
