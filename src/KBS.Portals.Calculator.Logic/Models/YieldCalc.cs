using System;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Logic.Models
{
    public class YieldCalc
    {
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public double Days { get; set; } //TODO Ask Gary about this variable
        public bool AffectYield { get; set; }
        public ScheduleType Type { get; set; }

        public YieldCalc (decimal amount, DateTime entryDate, bool affectYield, ScheduleType type)
        {
            Amount = amount;
            EntryDate = entryDate;
            AffectYield = affectYield;
            Type = type;
        }
    }
}
