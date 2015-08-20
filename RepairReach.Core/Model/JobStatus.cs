using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class JobStatus
    {
        public int JobStatusId { get; set; }

        public string Description { get; set; }

        public int SequenceNumber { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
