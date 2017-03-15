using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class InstallmentApr : Calculator
    {
        internal InstallmentApr(CalculatorData input) : base(input) { }

        internal override void CalculateImplementation()
        {
            //var nextDate = default(DateTime);
            //double dtk;
            //var apr = Input.APR;
            //double interim;
            //double dSumOfPayments = 0;
            //double dModifyDocFee = 0;
            //Dim cLoanAmount As Decimal
            //nextDate = Input.NextDate;
            //interim = (nextDate - Input.StartDate).TotalDays;
            //dtk = Math.Round((interim / AccountDays),9);
            //var iFrequency = (int)Enum.Parse(typeof(Frequency), Enum.GetName(typeof(Frequency), Input.Frequency));
            //All above can be summarised on next 4 lines.


            DateTime nextDate = Input.NextDate;
            int iFrequency = (int)Input.Frequency;

            double interim = (nextDate - Input.StartDate).TotalDays,
                dtk = Math.Round(interim / AccountDays, 9),
                dSumOfPayments = 0,
                dModifyDocFee = 0;

            if (Input.DocFee > 0)
            {
                dModifyDocFee = Math.Round(Convert.ToDouble(Input.DocFee) / Math.Pow((1 + Input.APR / 100), dtk), 9);
            }
            
            //TODO Add logic for RES,BAL and PUR -- COMMISSION  AND UPFRONTS!!!!!

            for (var i = 1; i <= Input.NoOfInstallments; i++)
            {
                interim = (nextDate - Input.StartDate).TotalDays;
                dtk = Math.Round((interim / AccountDays), 9);

                dSumOfPayments = Math.Round(dSumOfPayments + 1 / Math.Pow((1 + Input.APR / 100), dtk), 9);
                
                nextDate = nextDate.AddMonths(iFrequency);
            }
            if (dSumOfPayments > 0)
            {
                //TODO GAVIN should we add commission to deal Cost to get get full calculation????
                var dealCost = Convert.ToDouble(Input.FinanceAmount) - Convert.ToDouble(Input.UpFrontValue);//GC + Convert.ToDouble(Input.Commission);
                Input.Installment = Math.Round(Convert.ToDecimal((dealCost - dModifyDocFee) / dSumOfPayments), 2);
                Input.TotalSchedule = Input.TotalSchedule + (Input.Installment * Input.NoOfInstallments);
            }
        }

        private void CalculateImplementationOldPreRefactor()
        {
            var nextDate = default(DateTime);
            double dtk;
            var apr = Input.APR;
            double interim;

            double dSumOfPayments = 0;
            var iFrequency = (int)Enum.Parse(typeof(Frequency), Enum.GetName(typeof(Frequency), Input.Frequency));
            double dModifyDocFee = 0;
            //Dim cLoanAmount As Decimal
            
            nextDate = Input.NextDate;
            interim = (nextDate - Input.StartDate).TotalDays;
            dtk = Math.Round((interim / AccountDays),9);

            if (Input.DocFee > 0 )
            {
                dModifyDocFee = Math.Round(Convert.ToDouble(Input.DocFee) / Math.Pow((1 + apr / 100), dtk),9);
            }

            nextDate = Input.NextDate;

            //TODO Add logic for RES,BAL and PUR -- COMMISSION  AND UPFRONTS!!!!!

            for (var i = 1; i <= Input.NoOfInstallments; i++)
            {
                interim = (nextDate - Input.StartDate).TotalDays;
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
                //        sCalcString = "INS" + Constants.vbTab + dSumOfPayments + Constants.vbTab + dTK + Constants.vbTab + Strings.Trim(Convert.ToString(FormatItem(Input.StartDate, "dd MMM yyyy"))) + Constants.vbTab + Strings.Trim(Convert.ToString(FormatItem(dNextDate, "dd MMM yyyy"))) + Constants.vbTab + Strings.Trim(Convert.ToString(interim));
                //        if (WriteCalcFile(sCalcString) == false)
                //            return;
                //    }
                nextDate = nextDate.AddMonths(iFrequency);
            }
            if (dSumOfPayments > 0)
            {
                //TODO GAVIN should we add commission to deal Cost to get get full calculation????
                var dealCost = Convert.ToDouble(Input.FinanceAmount) - Convert.ToDouble(Input.UpFrontValue);//GC + Convert.ToDouble(Input.Commission);
                Input.Installment = Math.Round(Convert.ToDecimal((dealCost - dModifyDocFee) / dSumOfPayments),2);
                Input.TotalSchedule = Input.TotalSchedule + (Input.Installment * Input.NoOfInstallments);
            }
            //if (bWriteCalc == true)
            //{
            //    sCalcString = "";

            //    if (WriteCalcFile(sCalcString) == false)
            //        return;

            //    sCalcString = "DealCost" + Constants.vbTab + "=" + Constants.vbTab + "Amount" + Constants.vbTab + "-" + Constants.vbTab + "ModifyDocFee";
            //    if (WriteCalcFile(sCalcString) == false)
            //        return;

            //    sCalcString = Strings.Trim(Convert.ToString(Deal_Cost)) + Constants.vbTab + "=" + Constants.vbTab + Strings.Trim(Convert.ToString(Input.Amount)) + Constants.vbTab + "-" + Constants.vbTab + Strings.Trim(Convert.ToString(dModifyDocFee));
            //    if (WriteCalcFile(sCalcString) == false)
            //        return;

            //    sCalcString = "Installment" + Constants.vbTab + "= ((" + Constants.vbTab + "Amount" + Constants.vbTab + "-" + Constants.vbTab + "dModifyDocFee" + Constants.vbTab + ") /" + Constants.vbTab + "dSumOfPayments" + Constants.vbTab + ")";
            //    if (WriteCalcFile(sCalcString) == false)
            //        return;

            //    sCalcString = Strings.Trim(Convert.ToString(Installment)) + Constants.vbTab + "= ((" + Constants.vbTab + Strings.Trim(Convert.ToString(Input.Amount)) + Constants.vbTab + "-" + Constants.vbTab + Strings.Trim(Convert.ToString(dModifyDocFee)) + Constants.vbTab + ") /" + Constants.vbTab + Strings.Trim(Convert.ToString(dSumOfPayments)) + Constants.vbTab + ")";
            //    if (WriteCalcFile(sCalcString) == false)
            //        return;
        }
    }
}