using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public class APRInstallmentCalculator : Calculator
    {
        private SortedDictionary<SortKey, YieldCalc> YieldCalcChron = new SortedDictionary<SortKey, YieldCalc>();
//        private int NumOfInstalments { get; set; }
        private int AccountDays = 365;
        private CalculatorData _input;

        internal APRInstallmentCalculator(CalculatorData input) : base(input)
        {
            _input = input;
        }

        public override CalculatorData Calculate()
        {
            _input.Schedules = new List<Schedule>();
            if (_input.DocFee > 0)
            {
                _input.Schedules.Add(new Schedule(0, ScheduleType.DOC, 1, _input.Frequency, _input.DocFee, _input.NextDate));
            }
            if (_input.NoOfInstallments > 0)
            {
                _input.Schedules.Add(new Schedule(1, ScheduleType.INS, _input.NoOfInstallments, _input.Frequency, 0, _input.NextDate));
            }

            //TODO Gavin this next line was in call but never used to APR calc
            //BuildChron();
            CalculateAprInstallment();

            return _input;

        }

        // This is a common routine so should be in shared component Calculatir.cs?
        private void BuildChron()
        {
            var nextDate = default(DateTime);
            var serial = 0;

//            NumOfInstalments = 0;

            try
            {
                SetTotals();
                YieldCalcChron.Clear();
                var amount = -((_input.TotalSchedule) - (_input.Charges));
                YieldCalcChron.Add(new SortKey(_input.StartDate, serial), new YieldCalc(amount, _input.StartDate, true, ""));
                serial++;
                if (_input.Commission > 0)
                {
                    YieldCalcChron.Add(new SortKey(_input.StartDate, serial), new YieldCalc(_input.Commission, _input.StartDate, false, "COM"));
                    serial++;
                }

                foreach (Schedule schedule in _input.Schedules)
                {
                    if (schedule.Type.Equals(ScheduleType.FEE) || schedule.Type.Equals(ScheduleType.DOC) || schedule.Type.Equals(ScheduleType.PUR))
                    {
                        YieldCalcChron.Add(new SortKey(_input.NextDate, serial), new YieldCalc(schedule.Amount, _input.NextDate, false, schedule.Type.ToString()));
                        serial++;
                        //                    if (schedule.Type.Equals("FEE"))  { iMonths += schedule.Counts; };
                    }
                    //                if (schedule.Type.Equals("HOL")) { iMonths += schedule.Counts; };
                    if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL))
                    {
                        for (int i = 0; i <= schedule.Counts - 1; i++)
                        {
                            nextDate = schedule.NextDate.AddMonths(i * (int)schedule.Frequency);
                            YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, nextDate, true, schedule.Type.ToString()));
                            serial++;
                        }
                        //                    iMonths += schedule.Counts;
//                        NumOfInstalments += schedule.Counts;
                    }
                    if (schedule.Type.Equals(ScheduleType.RES))
                    {
                        // Always Collect Residual on last Nextdate from INS
                        YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, _input.NextDate, true, schedule.Type.ToString()));
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

            foreach (var schedule in _input.Schedules)
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
            _input.TotalSchedule = total;
            _input.Charges = _input.TotalCost - total;
        }

        private void CalculateAprInstallment ()
        {
            try
            {
                var nextDate = default(DateTime);
                double dtk;
                var apr = _input.APR;
                double interim;

                double dSumOfPayments = 0;
                var iFrequency = (int)Enum.Parse(typeof(Frequency), Enum.GetName(typeof(Frequency), _input.Frequency));
                double dModifyDocFee = 0;
                //Dim cLoanAmount As Decimal


                nextDate = _input.NextDate;
                interim = (nextDate - _input.StartDate).TotalDays;
                dtk = Math.Round((interim / AccountDays),9);

                if (_input.DocFee > 0 )
                {
                    dModifyDocFee = Math.Round(Convert.ToDouble(_input.DocFee) / Math.Pow((1 + apr / 100), dtk),9);
                }

                //TODO GAVIN PLEASE Check this Logic it does not look right to me
                //this was an AddMonth check but it only seemed to be set for CalcRental which is what this is and only used here??
                //dNextDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, _input.FirstInstallment);
                nextDate = _input.StartDate.AddMonths(1);


                //TODO Add logic for RES,BAL and PUR

                for (var i = 1; i <= _input.NoOfInstallments; i++)
                {
                    interim = (nextDate - _input.StartDate).TotalDays;
                    dtk = Math.Round((interim / AccountDays), 9);
                    dSumOfPayments = Math.Round(dSumOfPayments + 1 / Math.Pow((1 + apr / 100), dtk),9);

                    //if (bWriteCalc == true)
                    //    {
                    //        if (i == 1)
                    //        {
                    //            sCalcString = "";
                    //            if (WriteCalcFile(sCalcString) == false)
                    //                return;
                    //            sCalcString = "Type" + Constants.vbTab + "SumPayments" + Constants.vbTab + "dTK" + Constants.vbTab + "Next Date" + Constants.vbTab + "Start Date " + Constants.vbTab + "Interim";
                    //            if (WriteCalcFile(sCalcString) == false)
                    //                return;
                    //        }
                    //        sCalcString = "INS" + Constants.vbTab + dSumOfPayments + Constants.vbTab + dTK + Constants.vbTab + Strings.Trim(Convert.ToString(FormatItem(_input.StartDate, "dd MMM yyyy"))) + Constants.vbTab + Strings.Trim(Convert.ToString(FormatItem(dNextDate, "dd MMM yyyy"))) + Constants.vbTab + Strings.Trim(Convert.ToString(interim));
                    //        if (WriteCalcFile(sCalcString) == false)
                    //            return;
                    //    }
                    nextDate = nextDate.AddMonths(iFrequency);
                }
                if (dSumOfPayments > 0)
                {
                    var dealCost = Convert.ToDouble(_input.FinanceAmount) - Convert.ToDouble(_input.UpFrontValue);
                    _input.Installment = Math.Round(Convert.ToDecimal((dealCost - dModifyDocFee) / dSumOfPayments),2);
                    _input.TotalSchedule = _input.TotalSchedule + (_input.Installment * _input.NoOfInstallments);
                }
                //if (bWriteCalc == true)
                //{
                //    sCalcString = "";

                //    if (WriteCalcFile(sCalcString) == false)
                //        return;

                //    sCalcString = "DealCost" + Constants.vbTab + "=" + Constants.vbTab + "Amount" + Constants.vbTab + "-" + Constants.vbTab + "ModifyDocFee";
                //    if (WriteCalcFile(sCalcString) == false)
                //        return;

                //    sCalcString = Strings.Trim(Convert.ToString(Deal_Cost)) + Constants.vbTab + "=" + Constants.vbTab + Strings.Trim(Convert.ToString(_input.Amount)) + Constants.vbTab + "-" + Constants.vbTab + Strings.Trim(Convert.ToString(dModifyDocFee));
                //    if (WriteCalcFile(sCalcString) == false)
                //        return;

                //    sCalcString = "Installment" + Constants.vbTab + "= ((" + Constants.vbTab + "Amount" + Constants.vbTab + "-" + Constants.vbTab + "dModifyDocFee" + Constants.vbTab + ") /" + Constants.vbTab + "dSumOfPayments" + Constants.vbTab + ")";
                //    if (WriteCalcFile(sCalcString) == false)
                //        return;

                //    sCalcString = Strings.Trim(Convert.ToString(Installment)) + Constants.vbTab + "= ((" + Constants.vbTab + Strings.Trim(Convert.ToString(_input.Amount)) + Constants.vbTab + "-" + Constants.vbTab + Strings.Trim(Convert.ToString(dModifyDocFee)) + Constants.vbTab + ") /" + Constants.vbTab + Strings.Trim(Convert.ToString(dSumOfPayments)) + Constants.vbTab + ")";
                //    if (WriteCalcFile(sCalcString) == false)
                //        return;

            }
            //do stuff
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}