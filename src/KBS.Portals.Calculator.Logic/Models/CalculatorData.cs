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
        public List<Schedule> Schedules { get; private set; } // Here for future useage by KBS to allow scheduels to be added

//        public decimal Charges { get; set; }
        public decimal Charges => TotalSchedule - TotalCost - Commission - DocFee - PurchaseFee;

        public decimal TotalCost { get; set; }
        public decimal TotalSchedule { get; set; }

        public int Term => Convert.ToInt32( NoOfInstallments * (int)Frequency);
        public decimal LoanOverPayment { get; set; } // Value return in term Calculation

        public string Summary
        {
            get
            {
                var boldMe = "<b>";
                var unBoldMe = "</b>";
                //Switch off for now until we can confirm summary can be html
                boldMe = "";
                unBoldMe = "";
                var summary = "Finance Amount".PadRight(15) + boldMe + FinanceAmount.ToString("C") + unBoldMe + "\n";
                summary += "APR".PadRight(15) + boldMe + (APR /100).ToString("p") + unBoldMe + "\n";
                summary += "IRR".PadRight(15) + boldMe + (IRR /100).ToString("p") + unBoldMe + "\n";
                summary += "Installment".PadRight(15) + boldMe + Installment.ToString("C") + unBoldMe + "\n";
                summary += "No of Ins".PadRight(15) + boldMe + NoOfInstallments.ToString("G") + unBoldMe + "\n";
                summary += "Start Date".PadRight(15) + boldMe + StartDate.ToString("d") + unBoldMe + "\n";
                summary += "Next Date".PadRight(15) + boldMe + NextDate.ToString("d") + unBoldMe + "\n";
                summary += "Frequency".PadRight(15) + boldMe + Frequency + unBoldMe + "\n";
                summary += "Term".PadRight(15) + boldMe + Term.ToString("G") + " Months" + unBoldMe + "\n";

                if (UpFrontNo != 0)
                {
                    summary += "No of Up Fronts".PadRight(15) + boldMe + UpFrontNo.ToString("G") + unBoldMe + "\n";
                    summary += "Upfront Ins".PadRight(15) + boldMe + UpFrontValue.ToString("C") + unBoldMe + "\n";
                }

                if (DocFee != 0)
                {
                    summary += "Doc Fee".PadRight(15) + boldMe + DocFee.ToString("C") + unBoldMe + "\n";
                }
                if (PurchaseFee != 0)
                {
                    summary += "Purchase Fee".PadRight(15) + boldMe + PurchaseFee.ToString("C") + unBoldMe + "\n";
                }
                if (Commission != 0)
                {
                    summary += "Commission".PadRight(15) + boldMe + Commission.ToString("C") + unBoldMe + "\n";
                }
                if (Ballon != 0)
                {
                    summary += "Ballon".PadRight(15) + boldMe + Ballon.ToString("C") + unBoldMe + "\n";
                }
                if (Residual != 0)
                {
                    summary += "Residual".PadRight(15) + boldMe + Residual.ToString("C") + unBoldMe + "\n";
                }
                if (Charges != 0)
                {
                    summary += "Charges".PadRight(15) + boldMe + Charges.ToString("C") + unBoldMe + "\n";
                }
                if (TotalCost != 0)
                {
                    summary += "TotalCost".PadRight(15) + boldMe + TotalCost.ToString("C") + unBoldMe + "\n";
                }
                if (TotalSchedule != 0)
                {
                    summary += "TotalSchedule".PadRight(15) + boldMe + TotalSchedule.ToString("C") + unBoldMe + "\n";
                }
                if (LoanOverPayment != 0)
                {
                    summary += "LoanOverPayment".PadRight(15) + boldMe + LoanOverPayment.ToString("C") + unBoldMe + "\n";
                }

                return summary;
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