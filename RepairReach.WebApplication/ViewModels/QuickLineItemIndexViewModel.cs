using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class QuickLineItemIndexViewModel
    {
        public int QuickLineItemId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}