using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class QuickLineItemDeleteViewModel
    {
        public int QuickLineItemId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}