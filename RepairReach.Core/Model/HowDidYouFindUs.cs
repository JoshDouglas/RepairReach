using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class HowDidYouFindUs
    {
        public int HowDidYouFindUsId { get; set; }
        public string Description { get; set; }
        public int SequenceNumber { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
