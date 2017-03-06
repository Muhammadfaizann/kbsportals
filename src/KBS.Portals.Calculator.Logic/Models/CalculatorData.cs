using System;
using System.Collections.Generic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Util;

namespace KBS.Portals.Calculator.Logic.Models
{
    [DynamicRequire]
    public class CalculatorData
    {
        public CalculatorData()
        {
            Schedules = new List<Schedule>();
        }

        public Product Product { get; set; } // will dictate data elemt validation (e.g HP must have a PurFee)
        public CalculationType CalculationType { get; set; }


        public decimal FinanceAmount { get; set; }
        public double APR { get; set; }
        public double IRR { get; set; }
        public decimal Installment { get; set; }
        public int Term { get; set; }


        public decimal DocFee { get; set; } // Create a Documentation Fee Schedule Line
        public decimal PurchaseFee { get; set; } // Create a Purchase Fee Schedule Line
        public decimal Commission { get; set; }
        public int UpFrontNo { get; set; }
        public decimal UpFrontValue { get; set; }
        public decimal Ballon { get; set; } // Create a Ballon Payment Schedule Line
        public decimal Residual { get; set; } // Create a Residual Payment Schedule Line
        public DateTime StartDate { get; set; }
        public DateTime NextDate { get; set; }
        public IEnumerable<Schedule> Schedules { get; private set; } // Here for future useage by KBS to allow scheduels to be added

        public decimal Charges { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalSchedule { get; set; }

        
    }
}