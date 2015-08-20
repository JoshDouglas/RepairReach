using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Enum;

namespace RepairReach.Core.Model
{
    public class JobNote
    {
        public int JobNoteId { get; set; }

        public virtual Job Job { get; set; }

        public int JobId { get; set; }

        public string Note { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
