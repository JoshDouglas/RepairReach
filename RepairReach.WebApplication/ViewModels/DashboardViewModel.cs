using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class DashboardViewModel
    {
        public List<DashboardStatusViewModel> Statuses { get; set; }

        public IEnumerable<Core.Model.Appointment> AppointmentsToday { get; set; }

        public IEnumerable<Core.Model.Appointment> AppointmentsUpcoming { get; set; }

        public IEnumerable<Core.Model.Appointment> AppointmentsPastDue { get; set; }

        public IEnumerable<ActivityEvent> RecentActivityEvents { get; set; }

        public IEnumerable<Core.Model.Job> RescheduleNeedsApprovalJobAlerts { get; set; }

        public IEnumerable<Core.Model.Job> AwaitingPaymentJobAlerts { get; set; }

        public IEnumerable<Core.Model.Job> OnHoldJobAlerts { get; set; }

        public IList<DashboardRevenueChartViewModel> DailyRevenueChartItems { get; set; }

        public IList<DashboardRevenueChartViewModel> MonthlyRevenueChartItems { get; set; }

        public int TotalJobCount { get; set; }

        public DashboardViewModel()
        {
            Statuses = new List<DashboardStatusViewModel>();
            DailyRevenueChartItems = new List<DashboardRevenueChartViewModel>();
            MonthlyRevenueChartItems = new List<DashboardRevenueChartViewModel>();
            TotalJobCount = 0;
        }
    }
}