using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RepairReach.Core.Enum;

namespace RepairReach.WebApplication.ViewModels
{
    public class TeamIndexViewModel
    {
        public int StaffId { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Title")]
        public UserTitleEnum UserTitle { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }
}