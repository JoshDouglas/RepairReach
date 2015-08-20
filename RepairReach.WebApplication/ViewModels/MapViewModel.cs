using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RepairReach.Core.Model;

namespace RepairReach.WebApplication.ViewModels
{
    public class MapViewModel
    {
        public MapViewModel()
        {
        }

        public IEnumerable<Core.Model.Appointment> Appointments { get; set; }

        [DataType(DataType.Date)]
        public DateTime ScheduledFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime ScheduledTo { get; set; }


    }
}