using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RepairReach.Core.Enum;

namespace RepairReach.WebApplication.ViewModels
{
    public class TeamEditViewModel
    {
        [Required]
        public int StaffId { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        [MaxLength(50)]
        public string DisplayName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Title")]
        public UserTitleEnum UserTitle { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Hourly Rate")]
        public decimal? HourlyRate { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage="Username must be numbers and letters only.")]
        public string Username { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}