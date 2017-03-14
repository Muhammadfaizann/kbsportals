using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public class InstallmentApr : Calculator
    {
        private SortedDictionary<SortKey, YieldCalc> YieldCalcChron = new SortedDictionary<SortKey, YieldCalc>();
        private const int AccountDays = 365;
        private CalculatorData _input;

        internal InstallmentApr(CalculatorData input) : base(input)
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

            //TODO I think we need to be adding the BAL,RES and PUR schedule lines but currently the caluclation does NOT take account of these!


            //TODO Gavin this next line was in call but never used to APR calc
            //BuildChron();
            CalculateAprInstallment();

            return _input;

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

                nextDate = _input.NextDate;


                //TODO Add logic for RES,BAL and PUR -- COMMISSION  AND UPFRONTS!!!!!

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
                    //TODO GAVIN should we add commission to deal Cost to get get full calculation????
                    var dealCost = Convert.ToDouble(_input.FinanceAmount) - Convert.ToDouble(_input.UpFrontValue);//GC + Convert.ToDouble(_input.Commission);
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