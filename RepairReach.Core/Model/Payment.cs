using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        //public string PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime DatePaid { get; set; }
        public string EnteredBy { get; set; }
        public string Note { get; set; }
        public virtual Job Job { get; set; }
        public int JobId { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
