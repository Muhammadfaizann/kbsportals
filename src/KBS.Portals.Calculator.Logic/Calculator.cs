using System;
using System.Collections.Generic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="KBS.Portals.Calculator.Logic.ICalculator" />
    public abstract class Calculator : ICalculator
    {
        protected const int AccountDays = 365;

        protected SortedDictionary<SortKey, YieldCalc> YieldCalcChron;
        protected CalculatorData Input;

        public List<string> ErrorMessages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        protected Calculator(CalculatorData input)
        {
            Input = input;

            ErrorMessages = new List<string>();

            YieldCalcChron = new SortedDictionary<SortKey, YieldCalc>();
        }

        public CalculatorData Calculate()
        {
            var i = 0;
            Input.Schedules.Clear();
            //Clear other Variables to be calculated
            if (Input.CalculationType == CalculationType.APRInstallment)
            {
                Input.Installment = 0;
                Input.IRR = 0;
            }
            if (Input.CalculationType == CalculationType.IRRInstallment)
            {
                Input.Installment = 0;
                Input.APR = 0;
            }

            if (Input.CalculationType == CalculationType.Rate)
            {
                Input.APR = 0;
                Input.IRR = 0;
            }

            if (Input.CalculationType == CalculationType.FinanceAmount)
            {
                Input.APR = 0;
                Input.FinanceAmount = 0;
            }

            if (Input.CalculationType == CalculationType.BalRes)
            {
                Input.APR = 0;
                Input.Ballon = 0;
                Input.Residual = 0;
            }

            if (Input.CalculationType == CalculationType.Commission)
            {
                Input.APR = 0;
                Input.Commission = 0;
            }


            if (Input.DocFee > 0)
            {
                Input.AddSchedule(i, ScheduleType.DOC, 1, Input.Frequency, Input.DocFee, Input.NextDate);
                i++;
            }
            if (Input.NoOfInstallments > 0 || Input.Installment > 0)
            {
                Input.AddSchedule(i, ScheduleType.INS, Input.NoOfInstallments, Input.Frequency, Input.Installment, Input.NextDate);
                i++;
            }
            if (Input.Ballon > 0)
            {
                Input.AddSchedule(i, ScheduleType.BAL, 1, Input.Frequency, Input.Ballon,Input.NextDate);
                i++;
            }
            if (Input.Residual > 0)
            {
                Input.AddSchedule(i, ScheduleType.RES, 1, Input.Frequency, Input.Residual, Input.NextDate);
                i++;
            }
            if (Input.PurchaseFee > 0)
            {
                Input.AddSchedule(i,ScheduleType.PUR, 1, Input.Frequency, Input.PurchaseFee, Input.NextDate);
            }

          BuildChron();

            CalculateImplementation();
            SetTotals();
            return Input;
        }

        internal abstract void CalculateImplementation();

        public void Reload(CalculatorData input)
        {
            Input = input;
        }

        protected void BuildChron()
        {
            var nextDate = default(DateTime);
            var serial = 0;

            //            NumOfInstalments = 0;

            try
            {
                SetTotals();
                YieldCalcChron.Clear();
               //var amount = -((Input.TotalSchedule) - (Input.Charges));

                //GC Copied thuis from below Input.Charges = Input.TotalSchedule - Input.TotalCost;
                //GC Should be but fails tests                var amount = -(Input.TotalCost);
                //var amount = -((Input.TotalSchedule) - (Input.TotalSchedule - Input.TotalCost));
                var amount = -( Input.TotalCost);



                YieldCalcChron.Add(new SortKey(Input.StartDate, serial), new YieldCalc(amount, Input.StartDate, true, ScheduleType.FIN));
                serial++;
                if (Input.Commission > 0)
                {
                    YieldCalcChron.Add(new SortKey(Input.StartDate, serial), new YieldCalc(Input.Commission, Input.StartDate, false, ScheduleType.COM));
                    serial++;
                }

                foreach (Schedule schedule in Input.Schedules)

                {
                    if (schedule.NextDate == default(DateTime))
                    {

                            schedule.NextDate = nextDate;
                    }

                    if (schedule.Type.Equals(ScheduleType.FEE) || schedule.Type.Equals(ScheduleType.DOC) )
                    {
                        YieldCalcChron.Add(new SortKey(Input.NextDate, serial), new YieldCalc(schedule.Amount, Input.NextDate, false, schedule.Type));
                        serial++;
                        //                    if (schedule.Type.Equals("FEE"))  { iMonths += schedule.Counts; };
                    }
                    //                if (schedule.Type.Equals("HOL")) { iMonths += schedule.Counts; };



                    if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL))
                    {
                        if (schedule.Type.Equals(ScheduleType.BAL))
                        {
                            //Need to increment the next date for a one off balloon
                            nextDate = nextDate.AddMonths((int) schedule.Frequency);
                            schedule.NextDate =  nextDate;
                            
                        }
                        for (int i = 0; i <= schedule.Counts - 1; i++)
                        {

                            nextDate = schedule.NextDate.AddMonths(i * (int)schedule.Frequency);
                            YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, nextDate,  true, schedule.Type));
                            serial++;
                        }
                        //                    iMonths += schedule.Counts;
                        //                        NumOfInstalments += schedule.Counts;
                    }

                    if (schedule.Type.Equals(ScheduleType.PUR))
                    {
                        // Always Collect Purchase fee on last Nextdate from INS
                        YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, nextDate, false, schedule.Type));
                        serial++;
                        //                    if (schedule.Type.Equals("FEE"))  { iMonths += schedule.Counts; };
                    }
                    if (schedule.Type.Equals(ScheduleType.RES))
                    {
                        // Always Collect Residual on last Nextdate from INS
                        //schedule.NextDate.AddMonths(i * (int)schedule.Frequency)
                        YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, nextDate, true, schedule.Type));
                        serial++;
                        //                    iMonths += schedule.Counts;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
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
                if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL) || schedule.Type.Equals(ScheduleType.RES) || schedule.Type.Equals(ScheduleType.PUR))
                {
                    total += schedule.Amount * Convert.ToDecimal(schedule.Counts);
                }
            }
            Input.TotalSchedule = total;
            Input.TotalCost = Input.FinanceAmount + Input.Commission - (Input.UpFrontValue);
            //Input.Charges = Input.TotalSchedule - Input.TotalCost;
        }
    }
}