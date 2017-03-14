using System;
using System.Collections.Generic;
using System.Text;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Logic.Models
{
    class YieldCalc
    {
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public double Days { get; set; }
        public Boolean AffectYield { get; set; }
        public ScheduleType Type { get; set; }

        public YieldCalc (decimal amount, DateTime entryDate, Boolean affectYield, ScheduleType type)
        {
            this.Amount = amount;
            this.EntryDate = entryDate;
            this.AffectYield = affectYield;
            this.Type = type;
        }
    }
}
