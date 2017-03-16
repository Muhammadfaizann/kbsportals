using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class Rate : Calculator
    {
        internal Rate(CalculatorData input) : base(input)
        {
        }

        internal override void CalculateImplementation()
        {
            var date = default(DateTime);
            var loopCount = 0;
            double lastDays = 0,
                sNpv = 0,
                rate = 256,
                inc = 128;
 
            var lastKey = new SortKey(default(DateTime), 0);
            //no need to run swaps as Dictionary should be sorted


            foreach (var entry in YieldCalcChron)
            {
                if (entry.Value.AffectYield)
                {
                    sNpv = Convert.ToDouble(entry.Value.Amount);
                    date = entry.Value.EntryDate;
                    break;
                }
            }

            foreach (var entry in YieldCalcChron)
            {
                entry.Value.Days = (entry.Value.EntryDate - date).TotalDays;
            }


            do
            {
                //look for the first yield affecting entry
                foreach (var entry in YieldCalcChron)
                {
                    if (entry.Value.AffectYield)
                    {
                        sNpv = Convert.ToDouble(entry.Value.Amount);
                        lastDays = entry.Value.Days;
                        lastKey = entry.Key;
                        break;
                    }
                }

                foreach (var entry in YieldCalcChron)
                {
                    if (!entry.Key.Equals(lastKey)) // Skip first record for some reason - Gavin?
                    {
                        if (entry.Value.AffectYield)
                        {
                            sNpv += (sNpv * rate * ((entry.Value.Days - lastDays) / (AccountDays * 100)));
                            sNpv = sNpv + Convert.ToDouble(entry.Value.Amount);
                            lastDays = entry.Value.Days;
                        }
                    }
                }
                if (sNpv > 0.005 || sNpv < -0.005)
                {
                    if (sNpv < 0){rate = rate - inc;}
                    if (sNpv > 0){rate = rate + inc;}
                }
                if (rate <= 0){rate = 0.01;}
                inc = inc / 2;
                loopCount++;

            } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

            if (loopCount > 9999)
            {Input.IRR = -9999;}
            else
            {Input.IRR = Math.Round(rate,6);}

            //repeate fro APR i.e. not just yeild affecting
            rate = 256;
            inc = 128;
            loopCount = 0;
            do
            {

                //look for the first yield affecting entry
                foreach (var entry in YieldCalcChron)
                {
                        sNpv = Convert.ToDouble(entry.Value.Amount);
                        lastDays = entry.Value.Days;
                        lastKey = entry.Key;
                        break;
                }

                foreach (var entry in YieldCalcChron)
                {
                    if (!entry.Key.Equals(lastKey)) // Skip first record for some reason - Gavin?
                    {
                        sNpv += (sNpv * rate * ((entry.Value.Days - lastDays) / (AccountDays * 100)));
                        sNpv = sNpv + Convert.ToDouble(entry.Value.Amount);
                        lastDays = entry.Value.Days;
                    }
                }
                if (sNpv > 0.005 || sNpv < -0.005)
                {
                    if (sNpv < 0) { rate = rate - inc; }
                    if (sNpv > 0) { rate = rate + inc; }
                }
                if (rate <= 0) { rate = 0.01; }
                inc = inc / 2;
                loopCount++;

            } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

            if (loopCount > 9999)
            {
                Input.APR = -9999;
            }
            else
            {
                inc = 1 + (rate /1200);
                rate = 1;
                for (var i = 1; i <= 12; i++)
                {
                    rate = rate * inc;
                }
                Input.APR = Math.Round(((rate - 1) * 100),6);

            }
        }
    }
}