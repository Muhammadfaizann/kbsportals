using System;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Models
{
    public class CalculatorModel
    {
        public Product Product { get; set; }
        public CalculationType CalcType { get; set; }
        public decimal FinanceAmount { get; set; }
        public decimal Commission { get; set; }
        public int UpFrontNo { get; set; }
        public decimal UpFrontValue { get; set; }
        public decimal APR { get; set; }
        public decimal IRR { get; set; }
        public decimal DocFee { get; set; }
        public decimal PurFee { get; set; }
        public decimal Ballon { get; set; }
        public decimal Residual { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextDate { get; set; }
        public Frequency Frequency { get; set; }
        public decimal Installment { get; set; }
        public decimal Charges { get; set; }
        public int Term { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalSchedule { get; set; }
    }
}
