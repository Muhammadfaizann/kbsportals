using System;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Logic.Models
{
    public class Schedule
    {
        public int Serial { get; set; }
        public ScheduleType Type { get; set; }
        public int Counts { get; set; }
        public Frequency Frequency { get; set; }
        public decimal Amount { get; set; }
        public decimal Maintenance { get; set; }
        public DateTime NextDate { get; set; }

        public Schedule(int serial,ScheduleType type, int counts, Frequency frequency, Decimal amount, DateTime nextDate)
        {
            this.Serial = serial;
            this.Type = type;
            this.Counts = counts;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Maintenance = 0;
            this.NextDate = nextDate;
        }


    }
}
