using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Enum;

namespace RepairReach.Core.Model
{
    public class Staff
    {
        public int StaffId { get; set; }

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserTitleEnum UserTitle { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public decimal? HourlyRate { get; set; }

        public bool IsActive { get; set; }

        public string Username { get; set; }

        public int? ImportedStaffId { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }

        public virtual ICollection<TimeClockEntry> TimeClockEntries { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public bool IsClockedIn
        {
            get
            {
                if (TimeClockEntries == null) return false;
                if (TimeClockEntries.Count == 0) return false;

                var lastTimeClockEntry = TimeClockEntries.Last();
                if (lastTimeClockEntry.TimeIn.HasValue && lastTimeClockEntry.TimeOut.HasValue == false) return true;
                return false;
            }
        }

        public bool IsClockedOut
        {
            get
            {
                if (TimeClockEntries == null) return true;
                if (TimeClockEntries.Count == 0) return true;

                var lastTimeClockEntry = TimeClockEntries.Last();
                if (lastTimeClockEntry.TimeOut.HasValue) return true;
                return false;
            }
        }

        //public Guid UserId { get; set; }

    }
}
