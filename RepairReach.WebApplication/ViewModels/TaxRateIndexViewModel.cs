using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class TaxRateIndexViewModel
    {
        [Required]
        public int TaxRateId { get; set; }
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