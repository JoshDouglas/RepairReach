using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairReach.WebApplication.ViewModels
{
    public class LineItemIndexViewModel
    {
        public int LineItemId { get; set; }
        public int JobId { get; set; }
        [Display(Name = "Line Number")]
        public int LineItemNumber { get; set; }
        [Display(Name = "Technician")]
        public string TechnicianDisplayName { get; set; }
        [Display(Name = "Tax")]
        public decimal TaxRateAmount { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Part Name")]
        public string PartName { get; set; }
        [Display(Name = "Quantity")]
        public decimal? PartQty { get; set; }
        [Display(Name = "Each")]
        public decimal? PartEach { get; set; }
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
        [Display(Name = "Cost")]
        public decimal? ServiceCost { get; set; }

        //calculated fields
        [Display(Name = "Part Amount")]
        public decimal? PartAmount
        {
            get
            {
                if (PartQty.HasValue == false || PartEach.HasValue == false) return null;

                return Math.Round(PartQty.Value * PartEach.Value, 2, MidpointRounding.ToEven);
            }
        }
        [Display(Name = "Service Amount")]
        public decimal? ServiceAmount
        {
            get
            {
                if (ServiceQty.HasValue == false || ServiceEach.HasValue == false) return null;

                return Math.Round(ServiceQty.Value * ServiceEach.Value, 2, MidpointRounding.ToEven);
            }
        }
        [Display(Name = "Total")]
        public decimal TotalAmount
        {
            get
            {
                decimal partAmount = 0;
                decimal serviceAmount = 0;
                if (PartAmount.HasValue == true) partAmount = PartAmount.Value;
                if (ServiceAmount.HasValue == true) serviceAmount = ServiceAmount.Value;

                decimal lineAmountBeforeTax = partAmount + serviceAmount;

                decimal taxRate = 0;

                if (TaxRateAmount != null)
                {
                    taxRate = (TaxRateAmount != 0 ? TaxRateAmount / 100 : 0);
                }

                decimal taxAmount = Math.Round(lineAmountBeforeTax * taxRate, 2, MidpointRounding.ToEven);

                return lineAmountBeforeTax + taxAmount;
            }
        }
        [Display(Name = "Pre-Tax")]
        public decimal PreTaxAmount
        {
            get
            {
                decimal partAmount = 0;
                decimal serviceAmount = 0;
                if (PartAmount.HasValue == true) partAmount = PartAmount.Value;
                if (ServiceAmount.HasValue == true) serviceAmount = ServiceAmount.Value;

                decimal lineAmountBeforeTax = partAmount + serviceAmount;

                return lineAmountBeforeTax;
            }
        }
        [Display(Name = "Tax")]
        public decimal TaxAmount
        {
            get
            {
                decimal partAmount = 0;
                decimal serviceAmount = 0;
                if (PartAmount.HasValue == true) partAmount = PartAmount.Value;
                if (ServiceAmount.HasValue == true) serviceAmount = ServiceAmount.Value;

                decimal lineAmountBeforeTax = partAmount + serviceAmount;

                decimal taxRate = 0;

                if (TaxRateAmount != null)
                {
                    taxRate = (TaxRateAmount != 0 ? TaxRateAmount / 100 : 0);
                }

                decimal taxAmount = Math.Round(lineAmountBeforeTax * taxRate, 2, MidpointRounding.ToEven);

                return taxAmount;
            }
        }
    }
}