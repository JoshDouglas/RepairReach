using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class DashboardStatusViewModel
    {
        public int Sequence { get; set; }
        public int JobCount { get; set; }
        public decimal JobPercentage { get; set; }
        public string Description { get; set; }
    }
}