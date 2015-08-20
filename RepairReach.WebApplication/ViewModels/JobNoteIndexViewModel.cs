using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobNoteIndexViewModel
    {
        public int JobNoteId { get; set; }
        public int JobId { get; set; }
        [Display(Name = "Note")]
        public string Note { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
    }
}