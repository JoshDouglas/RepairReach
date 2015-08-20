using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels.Reports
{
    public class NonAuthorizedJobsViewModel
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool ShowAll { get; set; }
        public IEnumerable<Core.Model.Job> Jobs { get; set; }
    }
}