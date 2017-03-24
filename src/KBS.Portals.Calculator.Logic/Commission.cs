using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class Commission : Calculator
    {
        internal Commission(CalculatorData input) : base(input)
        {
        }

        internal override void CalculateImplementation()
        {

            var rate = Input.IRR;
            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, Input);

            var result = calc.Calculate();

            if (rate > result.IRR)
            {
                throw new Exception("Rate is already higher than that on the deal. Please change the details");
            }

            var charges = Convert.ToDouble( result.Charges);

            //code from If Not coCalcCharges(sYieldcalc, sRate, cCharges) Then
            var date = default(DateTime);
            var loopCount = 0;
            double lastDays = 0,
                sNpv = 0,
                inc;


            foreach (var entry in YieldCalcChron)
            {
                if (entry.Value.AffectYield)
                {
                    date = entry.Value.EntryDate;
                    break;
                }
            }

            foreach (var entry in YieldCalcChron)
            {
                entry.Value.Days = (entry.Value.EntryDate - date).TotalDays;
            }

            inc = charges / 2;
            var skipCount = 0;
            do
            {
                //look for the first yield affecting entry
                foreach (var entry in YieldCalcChron)
                {
                    skipCount++;
                    if (entry.Value.AffectYield)
                    {
                        sNpv = Convert.ToDouble(entry.Value.Amount) - charges;
                        lastDays = entry.Value.Days;
//                        lastKey = entry.Key;
                        break;
                    }
                }

                foreach (var entry in YieldCalcChron)
                {
                    if (skipCount <= 0) // Use skipCount to ingone non yield entries
                    {
                        if (entry.Value.AffectYield)
                        {
                            sNpv += (sNpv * rate * ((entry.Value.Days - lastDays) / (AccountDays * 100)));
                            sNpv = sNpv + Convert.ToDouble(entry.Value.Amount);
                            lastDays = entry.Value.Days;
                        }
                    }
                    skipCount--;
                }
                if (sNpv > 0.005 || sNpv < -0.005)
                {
                    if (sNpv < 0) { charges = charges - inc; }
                    if (sNpv > 0) { charges = charges + inc; }
                }
                inc = inc / 2;
                loopCount++;

            } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

            if (loopCount > 9999)
            { Input.IRR = -9999; }
            else
            { Input.IRR = Math.Round(rate, 6); }
            Input.Commission = Convert.ToDecimal( charges);

        }
    }
}