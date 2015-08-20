using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public decimal CostAmount { get; set; }
    }
}
