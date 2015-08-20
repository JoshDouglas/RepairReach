using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class Appliance
    {
        public int ApplianceId { get; set; }

        public string Make { get; set; }

        public string Type { get; set; } //Enum of the appliance type (Washer/Dryer/etc)

        public string ModelNumber { get; set; }

        public string SerialNumber { get; set; }

        public string ProblemDescription { get; set; }

        public string DisplayName
        {
            get
            {
                return "[" + Type + "] " + Make + " " + ModelNumber;
            }
        }

        public virtual Job Job { get; set; }

        public int JobId { get; set; }
    }
}
