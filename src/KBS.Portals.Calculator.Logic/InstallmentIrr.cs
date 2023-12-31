﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class InstallmentIrr : Calculator
    {
        internal InstallmentIrr(CalculatorData input) : base(input)
        {
        }

        internal override void CalculateImplementation()
        {
            double sNpv = 0;
            var date = default(DateTime);
            var loopCount = 0;
            double lastDays = 0;
            var bFindUpfront = false;


            
            //no need to run swaps as Dictionary should be sorted

            double inc;
            double amount;
            //GC Removed second condition as Upfront Value is ALWAYS computed - RK confirm 28/4/17 upf is only ever NoOfIns
            if (Input.UpFrontNo > 0 )//&& Input.UpFrontValue == 0)
            {
                bFindUpfront = true;
            }

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

            if (Input.NoOfInstallments > 0)
            {
                if (bFindUpfront)
                {
                    amount = Convert.ToDouble(Input.TotalCost / (Input.NoOfInstallments + Input.UpFrontNo));
                }
                else
                {
                    amount = Convert.ToDouble(Input.TotalCost / Input.NoOfInstallments);
                }
            }
            else
            {
                amount = 0; // Should fail to calculate and reyturn 0
            }
            inc = amount/2;
            do
            {
                //look for the first yield affecting entry
                double oldsNpv = 0;
                var skipCount = 0;
                foreach (var entry in YieldCalcChron)
                {
                    skipCount++;
                    if (entry.Value.AffectYield)
                    {
                        sNpv = Convert.ToDouble(entry.Value.Amount);
                        lastDays = entry.Value.Days;
                        break;
                    }
                }

                if (bFindUpfront)
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
                oldsNpv = sNpv;
                foreach (var entry in YieldCalcChron)
                {
                    if (skipCount<=0) // Skip first record for some reason - it is the finance amount and already primed
                    {
                        if (entry.Value.AffectYield)
                        {
                            sNpv += Math.Round(sNpv*Input.IRR*((entry.Value.Days - lastDays)/(AccountDays*100)), 4);

                            //On Swipe the installment is populated this cal so Amount never equals 0 this is only required for multiple INS lines ignore 0 check if its from app
                            if (Input.ManualSchedule && (entry.Value.Type == ScheduleType.INS || entry.Value.Type == ScheduleType.UPF))
                            {
                                sNpv += amount;
                            }
                            else 
                            if (entry.Value.Amount.Equals(0) && (entry.Value.Type == ScheduleType.INS || entry.Value.Type == ScheduleType.UPF))
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
                    skipCount--;
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

                if ((oldsNpv < 0 & sNpv > 0) || (oldsNpv > 0 & sNpv < 0))
                { inc = inc / 2; }
                oldsNpv = sNpv;

                loopCount++;

            } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

            if (loopCount > 9999)
            {
                Input.Installment = -9999;
            }
            else
            {
                Input.Installment = Math.Round(Convert.ToDecimal(amount), 2);

                // Update Schedule with Instalment
                foreach (var schedule in Input.Schedules)
                {
                    if (schedule.Type.Equals(ScheduleType.INS) && schedule.Amount.Equals(0))
                    {
                        schedule.Amount = Input.Installment;
                    }
                }
            }
        }
    }
}