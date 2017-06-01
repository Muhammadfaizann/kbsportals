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
            double sNpv = 0;

            var date = default(DateTime);
            var loopCount = 0;
            double lastDays = 0;
            var bFindUpfront = false;


            var lastKey = new SortKey(default(DateTime), 0);
            //no need to run swaps as Dictionary should be sorted

            double inc;
            double financeamount;
            //GC Removed second condition as Upfront Value is ALWAYS computed - RK confirm 28/4/17 upf is only ever NoOfIns
            if (Input.UpFrontNo > 0) //&& Input.UpFrontValue == 0)
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

            financeamount = -(Convert.ToDouble(Input.DocFee) + Convert.ToDouble(Input.Installment*Input.NoOfInstallments)+ Convert.ToDouble(Input.Installment*Input.UpFrontNo)+ Convert.ToDouble(Input.Ballon)+ Convert.ToDouble(Input.PurchaseFee)+ Convert.ToDouble(Input.Residual));
            inc = -financeamount / 2;
            do
            {
                var skipCount = 0;

                //look for the first yield affecting entry

                foreach (var entry in YieldCalcChron)
                {
                    skipCount++;
                    if (entry.Value.AffectYield)
                    {
                        if (entry.Value.Type == ScheduleType.FIN)
                        {
                            sNpv = Convert.ToDouble(financeamount);
                        }
                        else
                        {
                            sNpv = Convert.ToDouble(entry.Value.Amount);
                        }
                        lastDays = entry.Value.Days;
                        break;
                    }
                }
                double oldsNpv = sNpv;

                foreach (var entry in YieldCalcChron)
                {
                    if (skipCount <= 0) // Skip first record for some reason - yhe first is finance amount so already 
                    {
                        if (entry.Value.AffectYield)
                        {
                            sNpv += (sNpv * Input.IRR * ((entry.Value.Days - lastDays) / (AccountDays * 100)));
                            sNpv = sNpv + Convert.ToDouble(entry.Value.Amount);
                            lastDays = entry.Value.Days;
                        }

                    }
                    skipCount--;
                }
                if (sNpv > 0.005 || sNpv < -0.005)
                {
                    if (sNpv > 0)
                    {
                     financeamount = financeamount - inc;
                    }
                    if (sNpv < 0)
                    {
                        financeamount = financeamount + inc;
                    }
                }
                //if (financeamount >= 0)
                //{
                //    financeamount = -0.01;
                //}


                inc = inc / 2;

                loopCount++;
            } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

            if (loopCount > 9999)
            {
                Input.FinanceAmount = -9999;
            }
            else
            {
                Input.FinanceAmount = -(Math.Round(Convert.ToDecimal(financeamount), 2)) + Math.Round(Convert.ToDecimal(Input.Installment * Input.UpFrontNo),2) - Math.Round(Convert.ToDecimal(Input.Commission), 2);
            }
        }
    }
}