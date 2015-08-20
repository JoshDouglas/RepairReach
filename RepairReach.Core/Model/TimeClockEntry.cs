using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
   public  class TimeClockEntry
    {
        public int TimeClockEntryId { get; set; }
        public int StaffId { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? DatePaid { get; set; }
        public virtual Staff Staff { get; set; }

       public string DateWorkedDisplay
       {
           get
           {
               if (TimeIn.HasValue) return TimeIn.Value.Date.ToString("d");

               return string.Empty;
           }
       }

       public string TimeInDisplay
       {
           get
           {
               if (TimeIn.HasValue) return TimeIn.Value.ToString("t");

               return string.Empty;
           }
       }

       public string TimeOutDisplay
       {
           get
           {
               if (TimeOut.HasValue) return TimeOut.Value.ToString("t");

               return string.Empty;
           }
       }

       public decimal? HoursWorked
       {
           get
           {
               if (TimeIn.HasValue == false || TimeOut.HasValue == false) return null;

               var totalHours = TimeOut.Value.Subtract(TimeIn.Value).TotalHours;
               return Convert.ToDecimal(totalHours);
           }
       }

       public decimal? AmountToPay
       {
           get
           {
               if (TimeIn.HasValue == false || TimeOut.HasValue == false || HourlyRate.HasValue == false || HoursWorked.HasValue == false) return null;

               return HourlyRate.Value*HoursWorked.Value;
           }
       }
    }
}
