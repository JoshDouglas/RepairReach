using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using RepairReach.Data.Infrastructure.Identity;
using Microsoft.AspNet.Identity;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.Core.Enum;
using RepairReach.Core.Model;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository = null;
        private readonly IJobStatusRepository _jobStatusRepository = null;
        private readonly IAppointmentRepository _appointmentRepository = null;
        private readonly IActivityEventRepository _activityEventRepository = null;
        private readonly ICompanyRepository _companyRepository = null;

        public HomeController(IJobRepository jobRepository, IJobStatusRepository jobStatusRepository, IAppointmentRepository appointmentRepository,
            IActivityEventRepository activityEventRepository, ICompanyRepository companyRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            if (jobStatusRepository == null)
            {
                throw new ArgumentNullException("jobStatusRepository");
            }

            if (appointmentRepository == null)
            {
                throw new ArgumentNullException("appointmentRepository");
            }

            if (activityEventRepository == null)
            {
                throw new ArgumentNullException("activityEventRepository");
            }

            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }

            _jobRepository = jobRepository;
            _jobStatusRepository = jobStatusRepository;
            _appointmentRepository = appointmentRepository;
            _activityEventRepository = activityEventRepository;
            _companyRepository = companyRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard", "Home");
            //return View();
        }

        public async Task<ActionResult> Dashboard()
        {
            DashboardViewModel viewModel = await GetDashboardViewModel();
            var company = await _companyRepository.GetFirstAsync();

            @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            
            return View(viewModel);
        }

        private async Task<DashboardViewModel> GetDashboardViewModel()
        {
            DashboardViewModel viewModel = new DashboardViewModel();

            //make a status view model for each job status (except closed)
            var statuses = await _jobStatusRepository.GetAllAsync();
            var statusList = statuses.Where(s => s.Description.ToLower().Equals("closed") == false).OrderBy(s => s.SequenceNumber).ToList();

            var totalJobCount = await _jobRepository.GetCountAsync();
            viewModel.TotalJobCount = totalJobCount;

            foreach (var status in statusList)
            {
                var jobCount = await _jobRepository.GetCountForStatusAsync(status.Description);

                var statusViewModel = new DashboardStatusViewModel();
                statusViewModel.Sequence = status.SequenceNumber;
                statusViewModel.JobCount = jobCount;
                statusViewModel.Description = status.Description;
                if (totalJobCount != 0)
                    statusViewModel.JobPercentage = ((decimal) jobCount/(decimal) totalJobCount)*100;
                else
                    statusViewModel.JobPercentage = 0;

                viewModel.Statuses.Add(statusViewModel);
            }

            viewModel.Statuses = new List<DashboardStatusViewModel>(viewModel.Statuses.OrderBy(s => s.Sequence));

            //get appointments
            viewModel.AppointmentsToday = await _appointmentRepository.GetAllTodayAsync();
            viewModel.AppointmentsUpcoming = await _appointmentRepository.GetAllUpcomingAsync();
            viewModel.AppointmentsPastDue = await _appointmentRepository.GetAllPastDueAsync();
            viewModel.RecentActivityEvents = await _activityEventRepository.GetLast10Async();

            //job alerts
            viewModel.RescheduleNeedsApprovalJobAlerts = await _jobRepository.GetAllRescheduleNeedsApprovalAlertsAsync();
            viewModel.AwaitingPaymentJobAlerts = await _jobRepository.GetAllAwaitingPaymentAlertsAsync();
            viewModel.OnHoldJobAlerts = await _jobRepository.GetAllOnHoldAlertsAsync();

            //charts
            viewModel.DailyRevenueChartItems = await GetDailyChartItems();
            viewModel.MonthlyRevenueChartItems = await GetMonthlyChartItems();

            return viewModel;
        }

        private async Task<IList<DashboardRevenueChartViewModel>> GetDailyChartItems()
        {
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            IList<DashboardRevenueChartViewModel> dailyChartItems = new List<DashboardRevenueChartViewModel>();

            //get for last 7 days
            int sequence = 1;
            for (int i = 6; i >= 0; i--)
            {
                DashboardRevenueChartViewModel chartItem = new DashboardRevenueChartViewModel();

                DateTime closedDate = DateTime.UtcNow.AddDays(i * -1);
                DateTime localClosedDate = TimeZoneInfo.ConvertTimeFromUtc(closedDate, timeZoneInfo);

                var jobs = await _jobRepository.GetClosedOnDayAsync(localClosedDate);

                chartItem.Sequence = sequence;
                chartItem.Label = localClosedDate.ToString("M/d (ddd)");
                chartItem.Revenue = jobs.Sum(j => j.GrandTotal);
                dailyChartItems.Add(chartItem);

                sequence++;
            }

            return dailyChartItems;
        }

        private async Task<IList<DashboardRevenueChartViewModel>> GetMonthlyChartItems()
        {
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            IList<DashboardRevenueChartViewModel> monthlyChartItems = new List<DashboardRevenueChartViewModel>();

            //get for last 6 months
            int sequence = 1;
            for (int i = 5; i >= 0; i--)
            {
                DashboardRevenueChartViewModel chartItem = new DashboardRevenueChartViewModel();

                DateTime closedMonth = DateTime.UtcNow.AddMonths(i*-1);
                DateTime localClosedMonth = TimeZoneInfo.ConvertTimeFromUtc(closedMonth, timeZoneInfo);

                var jobs = await _jobRepository.GetClosedOnMonthYearAsync(localClosedMonth.Month, localClosedMonth.Year);

                chartItem.Sequence = sequence;
                chartItem.Label = localClosedMonth.ToString("MMMM");
                chartItem.Revenue = jobs.Sum(j => j.GrandTotal);
                monthlyChartItems.Add(chartItem);

                sequence++;
            }

            return monthlyChartItems;
        }
    }
}