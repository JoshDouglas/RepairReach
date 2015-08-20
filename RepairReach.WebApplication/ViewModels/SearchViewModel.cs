using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Core.Model.Job> Jobs { get; set; }
        public IEnumerable<Core.Model.Customer> Customers { get; set; }
        public int JobCount
        {
            get
            {
                if (Jobs != null)
                {
                    return Jobs.Count();
                }
                else
                {
                    return 0;
                }
            }
        }
        public int CustomerCount
        {
            get
            {
                if (Customers != null)
                {
                    return Customers.Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        public SearchViewModel()
        {
            SearchTerm = string.Empty;
            Jobs = new List<Core.Model.Job>();
            Customers = new List<Core.Model.Customer>();
        }
    }
}