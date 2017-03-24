using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class BalRes : Calculator
    {
        internal BalRes(CalculatorData input) : base(input)
        {
        }

        internal override void CalculateImplementation()
        {
            var workDate = default(DateTime);
            var sNpv = Convert.ToDouble( Input.TotalCost);

            //Manually add empty BAL Schedule
            
            Input.AddSchedule(2, ScheduleType.BAL, 1, Input.Frequency, 0, Input.NextDate.AddMonths(Input.NoOfInstallments * (int)Input.Frequency));
            BuildChron();

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

            Boolean skipped = false;
//            int i = 1;
            foreach (var entry in YieldCalcChron)
            {
                if (skipped)
                {
                    if (entry.Value.AffectYield)
                    {
                        sNpv = sNpv + (sNpv * ((Input.IRR * entry.Value.Days) / (AccountDays * 100))) - Convert.ToDouble(entry.Value.Amount);
//                        workDate = entry.Value.EntryDate;
//                        i++;
//                        Debug.Write(sNpv.ToString());
                    }
                }
                skipped = true;
            }
            Input.Ballon = Convert.ToDecimal(Math.Round(sNpv,2));
            // Old system used to update the BAL schedule line as well but this is not really using scheule
            //do we need logic to distinguish BAL and RES?
        }
    }
}