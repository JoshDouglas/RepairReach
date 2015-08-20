using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class DashboardRevenueChartViewModel
    {
        public int Sequence { get; set; }
        public string Label { get; set; }
        public decimal Revenue { get; set; }
    }
}