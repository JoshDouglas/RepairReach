using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class AppointmentIndexViewModel
    {
        public int AppointmentId { get; set; }
        public int JobId { get; set; }
        [Display(Name = "Start")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Technician")]
        public string TechnicianDisplayName { get; set; }
        [Display(Name = "Note")]
        public string Note { get; set; }
    }
}