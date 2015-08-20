using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class PaymentMethodDeleteViewModel
    {
        [Required]
        public int PaymentMethodId { get; set; }
        [Display(Name = "Method")]
        [Required]
        public string Description { get; set; }
    }
}