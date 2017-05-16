using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public sealed class Term : Calculator
    {
        internal Term(CalculatorData input) : base(input)
        {
        }

        internal override void CalculateImplementation()
        {
            DateTime lastDate = Input.StartDate,
                workDate = Input.NextDate;
            var sNpv = Convert.ToDouble(Input.FinanceAmount - (Input.UpFrontNo * Input.Installment) + Input.Commission);
            var loopCount = 0;
            var docFee = Input.DocFee;

            foreach (var schedule in Input.Schedules)
            {
                if (schedule.Type.Equals(ScheduleType.INS) )
                {
                    if (schedule.NextDate == default(DateTime))
                    {
                        schedule.NextDate = Input.NextDate;
                    }
                    
                    do
                    {
                        loopCount++;                    
                        var days = ( workDate - lastDate).TotalDays;
                        sNpv = sNpv + (sNpv * ((Input.IRR * days) / (AccountDays * 100)) - Convert.ToDouble(schedule.Amount + docFee));
//                        sNpv = Math.Round(sNpv, 2);
                        lastDate = workDate;
                        workDate = schedule.NextDate.AddMonths(loopCount * (int)schedule.Frequency);
                        docFee = 0;
                        if (loopCount > 9999 ) { 
                            break ; 
                        }

                    } while (sNpv > (Convert.ToDouble(Input.Ballon) + 0.01)) ;
                }
                else
                {
                    if (!schedule.Type.Equals(ScheduleType.DOC)) { throw new Exception("Only Doc and Installment Schedule Types allowed for Term Calculation");}

                }
            }
            if (sNpv > (Convert.ToDouble(Input.Ballon) + 0.01) || loopCount > 9999) { throw new Exception("Unable to Calculate Term"); }
            Input.LoanOverPayment = Convert.ToDecimal(Math.Round(sNpv,2));
            Input.NoOfInstallments = loopCount;
        }
    }
}