using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class ApplianceIndexViewModel
    {
        public int ApplianceId { get; set; }
        public int JobId { get; set; }
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "What's Wrong?")]
        public string ProblemDescription { get; set; }
    }
}