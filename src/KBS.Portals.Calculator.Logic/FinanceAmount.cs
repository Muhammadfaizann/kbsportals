using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class FinanceAmount : Calculator
    {
        internal FinanceAmount(CalculatorData input) : base(input)
        {
        }

        internal override void CalculateImplementation()
        {
            var workDate = default(DateTime);
            double sNpv = 0;

            foreach (var entry in YieldCalcChron)
            {
                if (entry.Value.AffectYield)
                {
                    workDate = entry.Value.EntryDate;
                    break;
                }
            }

            foreach (var entry in YieldCalcChron)
            {
                if (entry.Value.AffectYield)
                {
                    entry.Value.Days = (entry.Value.EntryDate - workDate).TotalDays;
                    workDate = entry.Value.EntryDate;
                }
            }


            foreach (var entry in YieldCalcChron.Reverse())
            {
                if (entry.Value.AffectYield)
                {
                    sNpv = (sNpv + Convert.ToDouble(entry.Value.Amount)) /
                           (1 + ((Input.IRR * entry.Value.Days) / (AccountDays * 100)));
                    sNpv = Math.Round(sNpv, 2);
                }
            }
            Input.FinanceAmount = Convert.ToDecimal(sNpv);
        }
    }
}