using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobCategoryDeleteViewModel
    {
        [Required]
        public int JobCategoryId { get; set; }
        [Display(Name = "Category")]
        [Required]
        public string Description { get; set; }
    }
}