using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class AppointmentEditViewModel
    {
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Start")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Start")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }
        [Display(Name = "End")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "End")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
        [Display(Name = "Technician")]
        [Required]
        public int TechnicianStaffId { get; set; }
        [Display(Name = "Note")]
        public string Note { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string Created { get; set; }
        [Display(Name = "Call On Way")]
        [Required]
        public bool CallOnWay { get; set; }
        [Display(Name = "Text On Way")]
        [Required]
        public bool TextOnWay { get; set; }

        //for calendar
        public IList<TeamIndexViewModel> Technicians { get; set; }
        //for map
        public MapViewModel Map { get; set; }
    }
}