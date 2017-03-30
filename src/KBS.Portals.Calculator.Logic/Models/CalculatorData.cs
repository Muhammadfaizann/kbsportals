using System;
using System.Collections.Generic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Util;

namespace KBS.Portals.Calculator.Logic.Models
{
    /// <summary>
    /// Model class for capturing settings required to perform a calculation. Acts as both an input and output model to the <see cref="KBS.Portals.Calculator.Logic.ICalculator"/>
    /// </summary>
    [DynamicRequire]
    public class CalculatorData
    {
        public CalculatorData()
        {
            Schedules = new List<Schedule>();
            Frequency = Frequency.Monthly;
        }

        public Product Product { get; set; } // will dictate data elemt validation (e.g HP must have a PurFee)
        public CalculationType CalculationType { get; set; }//TODO: This feels smelly putting the type on the poco when it's an external requirement. What happens if someone reloads an already instantiated calculator with the wrong calculator data type


        public decimal FinanceAmount { get; set; }
        public double APR { get; set; }
        public double IRR { get; set; }
        public decimal Installment { get; set; }
        public int NoOfInstallments { get; set; }


        public decimal DocFee { get; set; } // Create a Documentation Fee Schedule Line
        public decimal PurchaseFee { get; set; } // Create a Purchase Fee Schedule Line
        public decimal Commission { get; set; }
        public int UpFrontNo { get; set; }
        public decimal UpFrontValue { get; set; }
        public decimal Ballon { get; set; } // Create a Ballon Payment Schedule Line
        public decimal Residual { get; set; } // Create a Residual Payment Schedule Line
        public DateTime StartDate { get; set; }
        public DateTime NextDate { get; set; }
        public Frequency Frequency { get; set; }
        public List<Schedule> Schedules { get; private set; } // Here for future useage by KBS to allow scheduels to be added

        public decimal Charges { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalSchedule { get; set; }

        public int Term => Convert.ToInt32( NoOfInstallments * (int)Frequency);
        public decimal LoanOverPayment { get; set; } // Value return in term Calculation

        public void AddSchedule(int serial, ScheduleType type, int counts, Frequency frequency, decimal amount, DateTime nextDate)
        {
            AddSchedule(new Schedule(serial, type, counts, frequency,amount, nextDate));
        }

        public void AddSchedule(Schedule schedule)
        {
            if (Schedules == null)
            {
                Schedules = new List<Schedule>();
            }

            Schedules.Add(schedule);
        }

        
    }
}