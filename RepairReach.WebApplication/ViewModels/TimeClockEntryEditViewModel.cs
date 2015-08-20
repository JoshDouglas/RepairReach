using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class TimeClockEntryEditViewModel
    {
        public int StaffId { get; set; }
        public int TimeClockEntryId { get; set; }
        public DateTime DateWorked { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? TimeOut { get; set; }
        public bool SetTimeOut { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}