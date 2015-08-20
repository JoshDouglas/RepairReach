using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobStatusDeleteViewModel
    {
        [Required]
        public int JobStatusId { get; set; }
        [Display(Name = "Status")]
        [Required]
        public string Description { get; set; }
    }
}