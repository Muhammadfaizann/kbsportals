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
            var nextDate = Input.NextDate;
            var iFrequency = (int)Input.Frequency;

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
                dtk = Math.Round(interim / AccountDays, 9);

                dSumOfPayments = dSumOfPayments + 1 / Math.Pow((1 + Input.APR / 100), dtk); // dSumOfPayments + 1 / (1 + dAPR / 100) ^ dTK =		dSumOfPayments	0.993358473	Double
                
                dSumOfPayments = Math.Round(dSumOfPayments, 9);
                
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
    }
}