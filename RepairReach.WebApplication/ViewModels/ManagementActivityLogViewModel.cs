using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class ManagementActivityLogViewModel
    {
        public int JobId { get; set; }
        [Display(Name = "Job")]
        public int JobNumber { get; set; }
        [Display(Name = "Time")]
        public DateTime EventTime { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "User")]
        public string CausedBy { get; set; }
    }
}