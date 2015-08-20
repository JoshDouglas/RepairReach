using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class HowDidYouFindUsDeleteViewModel
    {
        [Required]
        public int HowDidYouFindUsId { get; set; }
        [Display(Name = "Customer Source")]
        [Required]
        public string Description { get; set; }
    }
}