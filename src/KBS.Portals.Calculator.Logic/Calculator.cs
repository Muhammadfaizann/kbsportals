using System;
using System.Collections.Generic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public abstract class Calculator : ICalculator
    {
        protected const int AccountDays = 365;

        protected SortedDictionary<SortKey, YieldCalc> YieldCalcChron;
           
        protected CalculatorData Input { get; set; }
        public List<string> ErrorMessages { get; set; }

        protected Calculator(CalculatorData input)
        {
            Input = input;
            ErrorMessages = new List<string>();

            YieldCalcChron = new SortedDictionary<SortKey, YieldCalc>();
        }

        public CalculatorData Calculate()
        {
            Input.Schedules = new List<Schedule>();
            if (Input.DocFee > 0)
            {
                Input.Schedules.Add(new Schedule(0, ScheduleType.DOC, 1, Input.Frequency, Input.DocFee,
                    Input.NextDate));
            }
            if (Input.NoOfInstallments > 0)
            {
                Input.Schedules.Add(new Schedule(1, ScheduleType.INS, Input.NoOfInstallments, Input.Frequency, Input.Installment, Input.NextDate));
            }
            if (Input.Ballon > 0)
            {
                Input.Schedules.Add(new Schedule(2, ScheduleType.BAL, 1, Input.Frequency, Input.Ballon,
                    Input.NextDate));
            }
            if (Input.Residual > 0)
            {
                Input.Schedules.Add(new Schedule(3, ScheduleType.RES, 1, Input.Frequency, Input.Residual,
                    Input.NextDate));
            }
            if (Input.PurchaseFee > 0)
            {
                Input.Schedules.Add(new Schedule(4, ScheduleType.RES, 1, Input.Frequency, Input.PurchaseFee,
                    Input.NextDate));
            }

            BuildChron();

            CalculateImplementation();

            return Input;
        }

        protected abstract void CalculateImplementation();

        public void Reload(CalculatorData input)
        {
            Input = input;
        }

        private void BuildChron()
        {
            var nextDate = default(DateTime);
            var serial = 0;

            //            NumOfInstalments = 0;
            SetTotals();
            YieldCalcChron.Clear();
            var amount = -(Input.TotalSchedule - Input.Charges);

            //TODO GAVIN We need a debug on old routine to see what should happen here, Only when I add the following override does calc work
            //first step I dont know if we need
            // need to confimr i got this right
            amount = -Input.FinanceAmount;

            YieldCalcChron.Add(new SortKey(Input.StartDate, serial), new YieldCalc(amount, Input.StartDate, true, ScheduleType.FIN));
            serial++;
            if (Input.Commission > 0)
            {
                YieldCalcChron.Add(new SortKey(Input.StartDate, serial), new YieldCalc(Input.Commission, Input.StartDate, false, ScheduleType.COM));
                serial++;
            }

            foreach (Schedule schedule in Input.Schedules)
            {
                if (schedule.Type.Equals(ScheduleType.FEE) || schedule.Type.Equals(ScheduleType.DOC) || schedule.Type.Equals(ScheduleType.PUR))
                {
                    YieldCalcChron.Add(new SortKey(Input.NextDate, serial), new YieldCalc(schedule.Amount, Input.NextDate, false, schedule.Type));
                    serial++;
                    //                    if (schedule.Type.Equals("FEE"))  { iMonths += schedule.Counts; };
                }
                //                if (schedule.Type.Equals("HOL")) { iMonths += schedule.Counts; };
                if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL))
                {
                    for (int i = 0; i <= schedule.Counts - 1; i++)
                    {
                        nextDate = schedule.NextDate.AddMonths(i * (int)schedule.Frequency);
                        YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, nextDate, true, schedule.Type));
                        serial++;
                    }
                    //                    iMonths += schedule.Counts;
                    //                        NumOfInstalments += schedule.Counts;
                }
                if (schedule.Type.Equals(ScheduleType.RES))
                {
                    // Always Collect Residual on last Nextdate from INS
                    YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, Input.NextDate, true, schedule.Type));
                    serial++;
                    //                    iMonths += schedule.Counts;
                }
            }
        }
        private void SetTotals()
        {
            decimal total = 0;
            //            decimal upFronts = 0;
            foreach (var schedule in Input.Schedules)
            {
                //if (schedule.Type.Equals(ScheduleType.UPF))
                //{
                //    upFronts += schedule.Amount * Convert.ToDecimal(schedule.Counts);
                //}
                if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL) || schedule.Type.Equals(ScheduleType.RES))
                {
                    total += schedule.Amount * Convert.ToDecimal(schedule.Counts);
                }
            }
            Input.TotalSchedule = total;
            Input.Charges = Input.TotalCost - total;
        }
    }
}