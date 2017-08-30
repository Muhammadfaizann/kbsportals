using System;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class IrrInstallmentTests
    {
        [Test]
        public void CanGetCalculatorFromFactory()
        {
            var calc = CalculatorFactory.Create(CalculationType.IRRInstallment, new CalculatorData());

            Assert.AreEqual(calc.GetType(), typeof(InstallmentIrr));
            
        }

        [Test]
        public void CalculateIrrInstallmentForSimpleInputs()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(207.53, result.Installment);
        }
        [Test]
        public void CalculateIrrInstallmentForUpfronts()
        {
            CalculatorData cd = new CalculatorData()
            {
                DocFee = 250,
                FinanceAmount = 10000,
                NoOfInstallments = 10,
                IRR = 5.192139,
                UpFrontNo = 2,
                StartDate = Convert.ToDateTime("28 Mar 2017"),
                NextDate = Convert.ToDateTime("28 Apr 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(850.00, result.Installment);
        }
        [Test]
        public void CalculateIrrInstallmentForCommission()
        {
            CalculatorData cd = new CalculatorData()
            {
                DocFee = 250,
                FinanceAmount = 10000,
                Commission = 200,
                NoOfInstallments = 10,
                IRR = 12.177734,
                UpFrontNo = 2,
                StartDate = Convert.ToDateTime("28 Mar 2017"),
                NextDate = Convert.ToDateTime("28 Apr 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(890.00, result.Installment);
        }


        [Test]
        public void CalculateIRRInstallmentForQuarterly()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Quarterly
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(300.85, result.Installment);
        }
        [Test]
        public void CalculateIRRInstallmentForAnnual()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 6,
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Annual
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(2059.46, result.Installment);
        }
        [Test]
        public void CalculateIRRWithDocFee()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                DocFee = 250
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(207.53, result.Installment);
        }

        [Test]
        public void CapitalFlowIRR_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 36900 - 15900,
                NoOfInstallments = 24,
                IRR = 11.191345,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 0,
//                UpFrontValue = 15900,
                Commission = 650,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1006.80, result.Installment);
        }

        [Test]
        public void CapitalFlowIRR_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 30750 - 16750,
                NoOfInstallments = 24,
                IRR = 11.796204,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 0,
//                UpFrontValue = 16750,
                Commission = 350,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(671.2, result.Installment);
        }

        [Test]
        public void CapitalFlow_4()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 16250,
                IRR = 11.167938,
                UpFrontNo = 3,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 33,
                //Installment = Convert.ToDecimal(518),
                StartDate = Convert.ToDateTime("17 Oct 2016"),
                NextDate = Convert.ToDateTime("09 Nov 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            //            Assert.AreEqual(16250, result.FinanceAmount);
            Assert.AreEqual(518, result.Installment);
        }

        [Test]
        public void CapitalFlow_6()
        {
            CalculatorData cd = new CalculatorData()
            {
                //Frig added for upfront Value
                FinanceAmount = 68000 - 6800,
                IRR = 11.000107,
                UpFrontNo = 0,
//                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 250,
                NoOfInstallments = 30,
                //Installment = Convert.ToDecimal(2343.3),
                StartDate = Convert.ToDateTime("10 Mar 2017"),
                NextDate = Convert.ToDateTime("10 Apr 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            //            Assert.AreEqual(68000, result.FinanceAmount);
            Assert.AreEqual(2343.3, result.Installment);
        }

        [Test]
        public void Fexco1()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 100000,
                NoOfInstallments = 33,
                IRR = 9,
                UpFrontNo = 3,
                StartDate = Convert.ToDateTime("27 Apr 2017"),
                NextDate = Convert.ToDateTime("27 May 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(3111.98, result.Installment);

            cd.IRR = 10;
            result = calc.Calculate();
            Assert.AreEqual(3150.21, result.Installment);
        }

        [Test]
        public void Fexco2()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 100000,
                NoOfInstallments = 59,
                IRR = 9,
                UpFrontNo = 1,
                StartDate = Convert.ToDateTime("27 Apr 2017"),
                NextDate = Convert.ToDateTime("27 May 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(2060.79, result.Installment);

            cd.IRR = 10;
            result = calc.Calculate();
            Assert.AreEqual(2107.61, result.Installment);
        }

        [Test]
        public void FAFIrrInstallment_M_33_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                UpFrontNo = 3,
                Commission = 0,
                DocFee = 0,
                IRR = 18.895447,
                NoOfInstallments = 33,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Installment);
        }

        [Test]
        public void FAFIrrInstallment_M_44_4()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                UpFrontNo = 4,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 44,
                IRR = 35.037177,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Installment);
        }

        [Test]
        public void FAFIrrInstallment_M_55_5()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                UpFrontNo = 5,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 55,
                IRR = 43.753098,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Installment);
        }
        // Half Yearly
        [Test]
        public void FAFIrrInstallment_H_5_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 5,
                IRR = 1.595337,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1700, result.Installment);
        }

        [Test]
        public void FAFIrrInstallment_H_7_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 7,
                IRR = 19.794189,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1700, result.Installment);

        }

        [Test]
        public void FAFIrrInstallment_H_9_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 9,
                IRR = 28.675659,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1700, result.Installment);
        }

        // Quarterly
        [Test]
        public void FAFIrrInstallment_Q_11_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 11,
                IRR = 13.979797,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1000, result.Installment);
        }

        [Test]
        public void FAFIrrInstallment_Q_15_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 15,
                IRR = 28.694214,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1000, result.Installment);
        }
        [Test]
        public void FAFIrrInstallment_Q_19_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 19,
                IRR = 35.60614,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1000, result.Installment);
        }

        [Test]
        public void FAFIrrInstallment_DivideByZero()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 0,
                IRR = 35.60614,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(-9999, result.Installment);
        }


        [Test]
        public void DOH_DOC_BAL()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 25000,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 150,
                NoOfInstallments = 48,
                Frequency = Frequency.Monthly,
                Ballon  = 2000,
                IRR = 9.5,
                StartDate = Convert.ToDateTime("04 Aug 2017"),
                NextDate = Convert.ToDateTime("04 Sep 2017")
            };
            cd.AddSchedule(0, ScheduleType.DOC, 150, Frequency.None, 150, Convert.ToDateTime("04 Sep 2017"));
            cd.AddSchedule(1, ScheduleType.INS, 48, Frequency.Monthly, 0, Convert.ToDateTime("04 Sep 2017"));
            cd.AddSchedule(2, ScheduleType.BAL, 1, Frequency.Monthly, Convert.ToDecimal(2000), Convert.ToDateTime("04 oct 2021"));
            cd.ManualSchedule = true;

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(594.02, result.Installment);
        }


    }
}
