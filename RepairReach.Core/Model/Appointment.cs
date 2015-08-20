using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int JobId { get; set; }

        public virtual Job Job { get; set; }

        public int TechnicianStaffId { get; set; }

        public virtual Staff Technician { get; set; }

        public string CreatedBy { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime Created { get; set; }

        public string Note { get; set; }

        public bool CallOnWay { get; set; }

        public bool TextOnWay { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CompletedTime { get; set; }
    }
}
