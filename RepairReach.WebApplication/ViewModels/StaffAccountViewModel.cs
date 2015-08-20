using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class StaffAccountViewModel
    {
        public Staff Staff { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}