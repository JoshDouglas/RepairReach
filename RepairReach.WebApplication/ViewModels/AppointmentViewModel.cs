using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public int JobId { get; set; }

        public virtual Core.Model.Job Job { get; set; }

        [Required]
        public int TechnicianStaffId { get; set; }

        public virtual Staff Technician { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string Note { get; set; }

        public bool IsCompleted { get; set; }
    }
}