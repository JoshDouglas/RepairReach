using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class ActivityEvent
    {
        public int ActivityEventId { get; set; }
        public int JobId { get; set;}
        public DateTime EventTime { get; set; }
        public string Description { get; set; }
        public string CausedBy { get; set; }
        public virtual Job Job { get; set; }
    }
}
