using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobNoteCreateViewModel
    {
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Note")]
        [Required]
        public string Note { get; set; }
    }
}