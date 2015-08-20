using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class PaymentIndexViewModel
    {
        public int PaymentId { get; set; }
        public int JobId { get; set; }
        [Display(Name = "Method")]
        public string PaymentMethod { get; set; }
        [Display(Name = "Amount")]
        public decimal PaymentAmount { get; set; }
        [Display(Name = "Note")]
        public string Note { get; set; }
        [Display(Name = "Paid On")]
        public DateTime DatePaid { get; set; }
        [Display(Name = "Entered By")]
        public string EnteredBy { get; set; }
    }
}