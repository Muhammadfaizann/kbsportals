using System;
using System.Collections.Generic;
using System.Linq;

using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public class InstallmentIrr : Calculator
    {
        private SortedDictionary<SortKey, YieldCalc> YieldCalcChron = new SortedDictionary<SortKey, YieldCalc>();
        //        private int NumOfInstalments { get; set; }
        private const int AccountDays = 365;
        private CalculatorData _input;

        internal InstallmentIrr(CalculatorData input) : base(input)
        {
            _input = input;
        }

        public override CalculatorData Calculate()
        {
            _input.Schedules = new List<Schedule>();
            if (_input.DocFee > 0)
            {
                _input.Schedules.Add(new Schedule(0, ScheduleType.DOC, 1, _input.Frequency, _input.DocFee,
                    _input.NextDate));
            }
            if (_input.NoOfInstallments > 0)
            {
                _input.Schedules.Add(new Schedule(1, ScheduleType.INS, _input.NoOfInstallments, _input.Frequency, _input.Installment,_input.NextDate));
            }
            if (_input.Ballon > 0)
            {
                _input.Schedules.Add(new Schedule(2, ScheduleType.BAL, 1, _input.Frequency, _input.Ballon,
                    _input.NextDate));
            }
            if (_input.Residual > 0)
            {
                _input.Schedules.Add(new Schedule(3, ScheduleType.RES, 1, _input.Frequency, _input.Residual,
                    _input.NextDate));
            }
            if (_input.PurchaseFee > 0)
            {
                _input.Schedules.Add(new Schedule(4, ScheduleType.RES, 1, _input.Frequency, _input.PurchaseFee,
                    _input.NextDate));
            }

            BuildChron();
            CalculateIrrInstallment();

            return _input;

        }

        private void CalculateIrrInstallment()
        {
            double sNpv = 0;
            var date = default(DateTime);
            var loopCount = 0;
            double lastDays = 0;
            var lastKey = new SortKey(default(DateTime), 0);

            try
            {

                //no need to run swaps as Dictionary should be sorted

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

                amount = Math.Round(Convert.ToDouble(_input.FinanceAmount / (_input.NoOfInstallments + _input.UpFrontNo)), 4);
                inc = amount / 2;
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

                    if (_input.UpFrontNo > 0)
                    {
                        if (sNpv > 0)
                        {
                            sNpv -= (amount * _input.UpFrontNo);
                        }
                        else
                        {
                            sNpv += (amount * _input.UpFrontNo);
                        }
                    }

                    foreach (var entry in YieldCalcChron)
                    {
                        if (!entry.Key.Equals(lastKey)) // Skip first record for some reason - Gavin?
                        {
                            if (entry.Value.AffectYield)
                            {
                                sNpv += Math.Round(sNpv * _input.IRR * ((entry.Value.Days - lastDays) / (AccountDays * 100)), 4);
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
                        if (sNpv > 0) { amount = amount - inc; }
                        if (sNpv < 0) { amount = amount + inc; }
                    }
                    if (amount <= 0) { amount = 0.01; }
                    inc = inc / 2;
                    loopCount++;

                } while (!((sNpv <= 0.005 && sNpv >= -0.005) || loopCount > 9999));

                if (loopCount > 9999)
                {
                    _input.Installment = -9999;
                }
                else
                {
                    _input.Installment = Math.Round(Convert.ToDecimal(amount), 2);
                    if (_input.UpFrontNo > 0) { _input.UpFrontValue = (_input.Installment * _input.UpFrontNo); }

                    // Update Schedule with Instalment
                    foreach (var schedule in _input.Schedules)
                    {
                        if (schedule.Type.Equals(ScheduleType.INS))
                        {
                            schedule.Amount = _input.Installment;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // This is a common routine so should be in shared component Calculatir.cs?
        private void BuildChron()
        {
            var nextDate = default(DateTime);
            var serial = 0;

            //            NumOfInstalments = 0;

            try
            {
                SetTotals();
                YieldCalcChron.Clear();
                var amount = -((_input.TotalSchedule) - (_input.Charges));

                //TODO GAVIN We need a debug on old routine to see what should happen here, Only when I add the following override does calc work
                //first step I dont know if we need
                // need to confimr i got this right
                amount = - _input.FinanceAmount;

                YieldCalcChron.Add(new SortKey(_input.StartDate, serial), new YieldCalc(amount, _input.StartDate, true, ScheduleType.FIN));
                serial++;
                if (_input.Commission > 0)
                {
                    YieldCalcChron.Add(new SortKey(_input.StartDate, serial), new YieldCalc(_input.Commission, _input.StartDate, false, ScheduleType.COM));
                    serial++;
                }

                foreach (Schedule schedule in _input.Schedules)
                {
                    if (schedule.Type.Equals(ScheduleType.FEE) || schedule.Type.Equals(ScheduleType.DOC) || schedule.Type.Equals(ScheduleType.PUR))
                    {
                        YieldCalcChron.Add(new SortKey(_input.NextDate, serial), new YieldCalc(schedule.Amount, _input.NextDate, false, schedule.Type));
                        serial++;
                        //                    if (schedule.Type.Equals("FEE"))  { iMonths += schedule.Counts; };
                    }
                    //                if (schedule.Type.Equals("HOL")) { iMonths += schedule.Counts; };
                    if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL))
                    {
                        for (int i = 0; i <= schedule.Counts - 1; i++)
                        {
                            nextDate = schedule.NextDate.AddMonths(i * (int)schedule.Frequency);
                            YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, nextDate, true, schedule.Type));
                            serial++;
                        }
                        //                    iMonths += schedule.Counts;
                        //                        NumOfInstalments += schedule.Counts;
                    }
                    if (schedule.Type.Equals(ScheduleType.RES))
                    {
                        // Always Collect Residual on last Nextdate from INS
                        YieldCalcChron.Add(new SortKey(nextDate, serial), new YieldCalc(schedule.Amount, _input.NextDate, true, schedule.Type));
                        serial++;
                        //                    iMonths += schedule.Counts;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }
        private void SetTotals()
        {
            decimal total = 0;
            //            decimal upFronts = 0;

            foreach (var schedule in _input.Schedules)
            {
                //if (schedule.Type.Equals(ScheduleType.UPF))
                //{
                //    upFronts += schedule.Amount * Convert.ToDecimal(schedule.Counts);
                //}
                if (schedule.Type.Equals(ScheduleType.INS) || schedule.Type.Equals(ScheduleType.BAL) || schedule.Type.Equals(ScheduleType.RES))
                {
                    total += schedule.Amount * Convert.ToDecimal(schedule.Counts);
                }
            }
            _input.TotalSchedule = total;
            _input.Charges = _input.TotalCost - total;
        }


    }

}