using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class QuickLineItem
    {
        public int QuickLineItemId { get; set; }

        public string Description { get; set; }

        public string PartName { get; set; }

        public decimal? PartQty { get; set; }

        public decimal? PartEach { get; set; }

        public decimal? PartAmount
        {
            get
            {
                if (PartQty.HasValue == false || PartEach.HasValue == false) return null;

                return Math.Round(PartQty.Value * PartEach.Value, 2, MidpointRounding.ToEven);
            }
        }

        public decimal? PartCost { get; set; }

        public string PartNumber { get; set; }

        public string ServiceName { get; set; }

        public decimal? ServiceQty { get; set; }

        public decimal? ServiceEach { get; set; }

        public decimal? ServiceAmount
        {
            get
            {
                if (ServiceQty.HasValue == false || ServiceEach.HasValue == false) return null;

                return Math.Round(ServiceQty.Value * ServiceEach.Value, 2, MidpointRounding.ToEven);
            }
        }

        public decimal? ServiceCost { get; set; }
    }
}
