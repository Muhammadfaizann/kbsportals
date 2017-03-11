using System;
using System.Collections.Generic;
using System.Text;

namespace KBS.Portals.Calculator.Logic.Models
{
    class YieldCalc
    {
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public double Days { get; set; }
        public Boolean AffectYield { get; set; }
        public String Type { get; set; }

        public YieldCalc (decimal amount, DateTime entryDate, Boolean affectYield,String type)
        {
            this.Amount = amount;
            this.EntryDate = entryDate;
            this.AffectYield = affectYield;
            this.Type = type;
        }
    }
}
