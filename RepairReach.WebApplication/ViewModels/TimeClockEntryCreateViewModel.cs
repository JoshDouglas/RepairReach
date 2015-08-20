using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class TimeClockEntryCreateViewModel
    {
        public int StaffId { get; set; }
        public DateTime DateWorked { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? TimeOut { get; set; }
        public bool SetTimeOut { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}