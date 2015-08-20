using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class LineItemEditViewModel
    {
        [Required]
        public int LineItemId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Display(Name = "Line Number")]
        [Required]
        public int LineItemNumber { get; set; }
        [Display(Name = "Technician")]
        [Required]
        public int StaffId { get; set; }
        [Display(Name = "Tax")]
        [Required]
        public int TaxRateId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Name")]
        public string PartName { get; set; }
        [Display(Name = "Quantity")]
        public decimal? PartQty { get; set; }
        [Display(Name = "Each")]
        public decimal? PartEach { get; set; }
        [Display(Name = "Cost")]
        public decimal? PartCost { get; set; }
        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }
        [Display(Name = "Name")]
        public string ServiceName { get; set; }
        [Display(Name = "Quantity")]
        public decimal? ServiceQty { get; set; }
        [Display(Name = "Each")]
        public decimal? ServiceEach { get; set; }
        [Display(Name = "Cost")]
        public decimal? ServiceCost { get; set; }
    }
}