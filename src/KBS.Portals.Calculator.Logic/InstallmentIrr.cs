using System;
using System.Collections.Generic;
using System.Linq;

using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class InstallmentIrr : Calculator
    {
        internal InstallmentIrr(CalculatorData input) : base(input) { }

        internal override void CalculateImplementation()
        {
            double sNpv = 0;
            var date = default(DateTime);
            var loopCount = 0;
            double lastDays = 0;
            var lastKey = new SortKey(default(DateTime), 0);


            double inc;
            double amount;

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

            amount = Math.Round(Convert.ToDouble(Input.FinanceAmount/(Input.NoOfInstallments + Input.UpFrontNo)), 4);
            inc = amount/2;
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

                if (Input.UpFrontNo > 0)
                {
                    if (sNpv > 0)
                    {
                        sNpv -= (amount*Input.UpFrontNo);
                    }
                    else
                    {
                        sNpv += (amount*Input.UpFrontNo);
                    }
                }

                foreach (var entry in YieldCalcChron)
                {
                    if (!entry.Key.Equals(lastKey)) // Skip first record for some reason - Gavin?
                    {
                        if (entry.Value.AffectYield)
                        {
                            sNpv += Math.Round(sNpv*Input.IRR*((entry.Value.Days - lastDays)/(AccountDays*100)), 4);
                            if (entry.Value.Amount.Equals(0) && entry.Value.Type.Equals(ScheduleType.INS))
                            {
                                sNpv += amount;
                            }
                            else
                            {
                                sNpv = sNpv + Convert.ToDouble(entry.Value.Amount);
                            }
                            lastDays = entry.Value.Days;
                        }
                    }
                }
                if (sNpv > 0.005 || sNpv < -0.005)
                {
                    if (sNpv > 0)
                    {
                        amount = amount - inc;
                    }
                    if (sNpv < 0)
                    {
                        amount = amount + inc;
                    }
                }
                if (amount <= 0)
                {
                    amount = 0.01;
                }
                inc = inc/2;
                loopCount++;

            } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

            if (loopCount > 9999)
            {
                Input.Installment = -9999;
            }
            else
            {
                Input.Installment = Math.Round(Convert.ToDecimal(amount), 2);
                if (Input.UpFrontNo > 0)
                {
                    Input.UpFrontValue = (Input.Installment* Input.UpFrontNo);
                }

                // Update Schedule with Instalment
                foreach (var schedule in Input.Schedules)
                {
                    if (schedule.Type.Equals(ScheduleType.INS))
                    {
                        schedule.Amount = Input.Installment;
                    }
                }
            }
        }
    }
}