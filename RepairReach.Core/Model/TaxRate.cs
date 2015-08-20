using System.Collections.Generic;

namespace RepairReach.Core.Model
{
    public class TaxRate
    {
        public int TaxRateId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public bool IsDefaultRate { get; set; }

        public string DisplayName
        {
            get
            {
                return Name + " - " + Amount.ToString("N2");
            }
        }

        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
