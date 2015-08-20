using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobNoteEditViewModel
    {
        [Required]
        public int JobNoteId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Note")]
        [Required]
        public string Note { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}