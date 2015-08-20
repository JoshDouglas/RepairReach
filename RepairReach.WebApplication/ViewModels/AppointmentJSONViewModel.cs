using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class AppointmentJSONViewModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string resourceId { get; set; }
        public bool allDay { get; set; }
        public string backgroundColor { get; set; }
        public string address { get; set; }
        public string tooltipDescription { get; set; }
    }
}