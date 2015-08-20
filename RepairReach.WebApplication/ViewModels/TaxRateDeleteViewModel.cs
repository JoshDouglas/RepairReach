using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class TaxRateDeleteViewModel
    {
        [Required]
        public int TaxRateId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
    }
}