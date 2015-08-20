using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class TaxRateCreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Rate")]
        [Required]
        public decimal Amount { get; set; }
        [Display(Name = "Default Rate?")]
        [Required]
        public bool IsDefaultRate { get; set; }
    }
}