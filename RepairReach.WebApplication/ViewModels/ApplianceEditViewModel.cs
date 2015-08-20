using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class ApplianceEditViewModel
    {
        [Required]
        public int ApplianceId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Type")]
        [Required]
        public string Type { get; set; }
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "What's Wrong?")]
        public string ProblemDescription { get; set; }
    }
}