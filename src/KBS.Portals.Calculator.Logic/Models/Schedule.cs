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
        public decimal Maintenance { get; set; } //TODO: Ask Gary... this doesn't appear to be used.
        public DateTime NextDate { get; set; }

        public Schedule(int serial, ScheduleType type, int counts, Frequency frequency, decimal amount, DateTime nextDate)
        {
            Serial = serial;
            Type = type;
            Counts = counts;
            Frequency = frequency;
            Amount = amount;
            Maintenance = 0;
            NextDate = nextDate;
        }


    }
}
