using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Antlr.Runtime.Tree;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class ApplianceViewModel
    {
        public Core.Model.Appliance Appliance { get; set; }

        public ApplianceViewModel()
        {
            Appliance = new Core.Model.Appliance();
        }
    }
}