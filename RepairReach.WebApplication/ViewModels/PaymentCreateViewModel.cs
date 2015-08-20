using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class PaymentCreateViewModel
    {
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Method")]
        [Required]
        public int PaymentMethodId { get; set; }
        [Display(Name = "Amount")]
        [Required]
        public decimal PaymentAmount { get; set; }
        [Display(Name = "Note")]
        public string Note { get; set; }
        [Display(Name = "Paid On")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }
    }
}