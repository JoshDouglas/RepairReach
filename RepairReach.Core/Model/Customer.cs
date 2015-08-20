using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Enum;

namespace RepairReach.Core.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public CustomerDesignationEnum Designation { get; set; }

        public virtual HowDidYouFindUs HowDidYouFindUs { get; set; }

        public int? HowDidYouFindUsId { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public bool CollectPaymentOnSite { get; set; }

        public bool CallOnWay { get; set; }
        
        public bool PrefersTextMessaging { get; set; }

        public int? ImportedCustomerId { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(CompanyName) == false)
                {
                    return CompanyName;
                }
                else
                {
                    return FirstName + " " + LastName;
                }
            }
        }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
