using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class JobMapViewModel
    {
        public JobMapViewModel()
        {
            Job = new Core.Model.Job();
        }
        public Core.Model.Job Job { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }
    }
}