using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Core.Model
{
    public class LineItem
    {
        public int LineItemId { get; set; }

        public int LineItemNumber { get; set; }
        
        public string Description { get; set; }
        
        public virtual Job Job { get; set; }

        public int JobId { get; set; }

        public string PartName { get; set; }

        public decimal? PartQty { get; set; }

        public decimal? PartEach { get; set; }

        public decimal? PartAmount
        {
            get
            {
                if (PartQty.HasValue == false || PartEach.HasValue == false) return null;

                return Math.Round(PartQty.Value * PartEach.Value, 2, MidpointRounding.ToEven);
            }
        }

        public decimal? PartCost { get; set; }

        public string PartNumber { get; set; }

        public string ServiceName { get; set; }

        public decimal? ServiceQty { get; set; }

        public decimal? ServiceEach { get; set; }

        public decimal? ServiceAmount
        {
            get
            {
                if (ServiceQty.HasValue == false || ServiceEach.HasValue == false) return null;

                return Math.Round(ServiceQty.Value * ServiceEach.Value, 2, MidpointRounding.ToEven);
            }
        }

        public decimal? ServiceCost { get; set; }

        //public decimal? LaborQty { get; set; }

        //public decimal? LaborEach { get; set; }

        //public decimal? LaborAmount
        //{
        //    get
        //    {
        //        if (LaborQty.HasValue == false || LaborEach.HasValue == false) return null;

        //        return Math.Round(LaborQty.Value * LaborEach.Value);
        //    }
        //}

        //public decimal? LaborCost { get; set; }

        public virtual Staff Technician { get; set; } //Technician

        public int StaffId { get; set; }

        public virtual TaxRate TaxRate { get; set; }

        public int TaxRateId { get; set; }

        public decimal TotalAmount
        {
            get
            {
                decimal partAmount = 0;
                decimal serviceAmount = 0;
                decimal laborAmount = 0;
                if (PartAmount.HasValue == true) partAmount = PartAmount.Value;
                if (ServiceAmount.HasValue == true) serviceAmount = ServiceAmount.Value;
                //if (LaborAmount.HasValue == true) laborAmount = LaborAmount.Value;

                decimal lineAmountBeforeTax = partAmount + serviceAmount;

                decimal taxRate = 0;

                if (TaxRate != null)
                {
                    taxRate = (TaxRate.Amount != 0 ? TaxRate.Amount / 100 : 0);
                }

                decimal taxAmount = Math.Round(lineAmountBeforeTax * taxRate, 2, MidpointRounding.ToEven);

                return lineAmountBeforeTax + taxAmount;
            }
        }

        public decimal PreTaxAmount
        {
            get
            {
                decimal partAmount = 0;
                decimal serviceAmount = 0;
                decimal laborAmount = 0;
                if (PartAmount.HasValue == true) partAmount = PartAmount.Value;
                if (ServiceAmount.HasValue == true) serviceAmount = ServiceAmount.Value;
                //if (LaborAmount.HasValue == true) laborAmount = LaborAmount.Value;

                decimal lineAmountBeforeTax = partAmount + serviceAmount;

                return lineAmountBeforeTax;
            }
        }

        public decimal TaxAmount
        {
            get
            {
                decimal partAmount = 0;
                decimal serviceAmount = 0;
                decimal laborAmount = 0;
                if (PartAmount.HasValue == true) partAmount = PartAmount.Value;
                if (ServiceAmount.HasValue == true) serviceAmount = ServiceAmount.Value;
                //if (LaborAmount.HasValue == true) laborAmount = LaborAmount.Value;

                decimal lineAmountBeforeTax = partAmount + serviceAmount;

                decimal taxRate = 0;

                if (TaxRate != null)
                {
                    taxRate = (TaxRate.Amount != 0 ? TaxRate.Amount / 100 : 0);
                }

                decimal taxAmount = Math.Round(lineAmountBeforeTax * taxRate, 2, MidpointRounding.ToEven);

                return taxAmount;
            }
        }
    }
}
