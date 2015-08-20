using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class PaymentMethodCreateViewModel
    {
        [Display(Name = "Method")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Sequence")]
        [Required]
        public int SequenceNumber { get; set; }
    }
}