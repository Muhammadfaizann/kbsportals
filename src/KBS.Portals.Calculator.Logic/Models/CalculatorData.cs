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
        public decimal UpFrontValue => UpFrontNo * Installment;
        public decimal Ballon { get; set; } // Create a Ballon Payment Schedule Line
        public decimal Residual { get; set; } // Create a Residual Payment Schedule Line
        public DateTime StartDate { get; set; }
        public DateTime NextDate { get; set; }
        public Frequency Frequency { get; set; }
        public List<Schedule> Schedules { get;  set; } // Here for future useage by KBS to allow scheduels to be added

        //        public decimal Charges { get; set; }

        public decimal TotalCost { get; set; }
        public decimal TotalSchedule { get; set; }

        public decimal Charges => TotalSchedule - TotalCost - Commission - DocFee - PurchaseFee;


        public int Term => Convert.ToInt32( NoOfInstallments * (int)Frequency);
        public decimal LoanOverPayment { get; set; } // Value return in term Calculation

        public List<KeyValuePair<string, string>> Summary
        {
            get
            {
                var results = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Finance Amount", FinanceAmount.ToString("C")),
                    new KeyValuePair<string, string>("APR", (APR / 100).ToString("p")),
                    new KeyValuePair<string, string>("IRR", (IRR / 100).ToString("p")),
                    new KeyValuePair<string, string>("Installment", Installment.ToString("C")),
                    new KeyValuePair<string, string>("No of Ins", NoOfInstallments.ToString("G")),
                    new KeyValuePair<string, string>("Start Date", StartDate.ToString("d")),
                    new KeyValuePair<string, string>("Next Date", NextDate.ToString("d")),
                    new KeyValuePair<string, string>("Term", Frequency.GetDescription())
                };


                if (UpFrontNo != 0)
                {
                    results.Add(new KeyValuePair<string, string>("No of Up Fronts", UpFrontNo.ToString("G")));
                    results.Add(new KeyValuePair<string, string>("Upfront Ins", UpFrontValue.ToString("C")));
                }

                if (DocFee != 0)
                {
                    results.Add(new KeyValuePair<string, string>("Doc Fee", DocFee.ToString("C")));
                }
                if (PurchaseFee != 0)
                {
                    results.Add(new KeyValuePair<string, string>("Purchase Fee", PurchaseFee.ToString("C")));
                }
                if (Commission != 0)
                {
                    results.Add(new KeyValuePair<string, string>("Commission", Commission.ToString("C")));
                }
                if (Ballon != 0)
                {
                    results.Add(new KeyValuePair<string, string>("Ballon", Ballon.ToString("C")));
                }
                if (Residual != 0)
                {
                    results.Add(new KeyValuePair<string, string>("Residual", Residual.ToString("C")));
                }
                if (Charges != 0)
                {
                    results.Add(new KeyValuePair<string, string>("Charges", Charges.ToString("C")));
                }
                if (TotalCost != 0)
                {
                    results.Add(new KeyValuePair<string, string>("TotalCost", TotalCost.ToString("C")));
                }
                if (TotalSchedule != 0)
                {
                    results.Add(new KeyValuePair<string, string>("TotalSchedule", TotalSchedule.ToString("C")));
                }
                if (LoanOverPayment != 0)
                {
                    results.Add(new KeyValuePair<string, string>("LoanOverPayment", LoanOverPayment.ToString("C")));
                }

                return results;
            }
        }

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