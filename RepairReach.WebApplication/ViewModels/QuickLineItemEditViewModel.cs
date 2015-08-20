using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class QuickLineItemEditViewModel
    {
        [Required]
        public int QuickLineItemId { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Part Name")]
        public string PartName { get; set; }
        [Display(Name = "Quantity")]
        public decimal? PartQty { get; set; }
        [Display(Name = "Each")]
        public decimal? PartEach { get; set; }
        [Display(Name = "Amount")]
        public decimal? PartAmount
        {
            get
            {
                if (PartQty.HasValue == false || PartEach.HasValue == false) return null;

                return Math.Round(PartQty.Value * PartEach.Value, 2, MidpointRounding.ToEven);
            }
        }
        [Display(Name = "Cost")]
        public decimal? PartCost { get; set; }
        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }
        [Display(Name = "Quantity")]
        public decimal? ServiceQty { get; set; }
        [Display(Name = "Each")]
        public decimal? ServiceEach { get; set; }
        [Display(Name = "Amount")]
        public decimal? ServiceAmount
        {
            get
            {
                if (ServiceQty.HasValue == false || ServiceEach.HasValue == false) return null;

                return Math.Round(ServiceQty.Value * ServiceEach.Value, 2, MidpointRounding.ToEven);
            }
        }
        [Display(Name = "Cost")]
        public decimal? ServiceCost { get; set; }
    }
}