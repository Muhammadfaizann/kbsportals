using System;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class CommissionTests
    {
        [Test]
        public void CanGetCalculatorFromFactory()
        {
            var calc = CalculatorFactory.Create(CalculationType.Commission, new CalculatorData());

            Assert.AreEqual(calc.GetType(), typeof(Commission));

        }

        [Test]
        public void CalculateCommissionForSimpleInputs()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(220),
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(600.86, result.Commission);
        }
        [Test]
        public void CalculateCommissionForManual()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(220),
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };
            cd.AddSchedule(1, ScheduleType.INS, 60, Frequency.Monthly, 220, Convert.ToDateTime("14 Mar 2017"));
            cd.ManualSchedule = true;

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(600.86, result.Commission);
        }

        [Test]
        public void CalculateCommissionForQuarterly()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                IRR = 3.069061,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(240.00),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Quarterly
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1564.59, result.Commission);
        }

        [Test]
        public void CalculateCommissionForAnnual()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                IRR = 9.000031,
                NoOfInstallments = 6,
                Installment = Convert.ToDecimal(2100),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Annual
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(196.85, result.Commission);
        }
        [Test]
        public void CalculateCommissionWithDocFee()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                IRR = 9,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(220),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                DocFee = 250
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(600.86, result.Commission);
        }

        [Test]
        public void CapitalFlow_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 36900 - 15900, // 21,000
                IRR = 11.191345,
                NoOfInstallments = 22,
                UpFrontNo = 0,
//                UpFrontValue = 15900,
                //Commission = 650,
                DocFee = 0,
                Installment = Convert.ToDecimal(1088.53),
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(650.02, result.Commission);
        }

        [Test]
        public void CapitalFlow_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 30750 - 16750, // 14,000
                IRR = 11.796204,
                NoOfInstallments = 22,
                UpFrontNo = 0,
//                UpFrontValue = 16750,
                //Commission = 350,
                DocFee = 0,
                Installment = Convert.ToDecimal(725.35),
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Commission);

        }

        [Test]
        public void CapitalFlow_4()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 16250 - (3 * 518), // 14,696
                IRR = 11.167938,
                UpFrontNo = 0,
//                UpFrontValue = 518,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 30,
                Installment = Convert.ToDecimal(600),
                StartDate = Convert.ToDateTime("17 Oct 2016"),
                NextDate = Convert.ToDateTime("09 Nov 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(984.59, result.Commission);
        }

        [Test]
        public void CapitalFlow_6()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 68000 - 6800,  //61,200
                IRR = 11.000107,
                UpFrontNo = 0,
//                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 250,
                NoOfInstallments = 30,
                Installment = Convert.ToDecimal(2400),
                StartDate = Convert.ToDateTime("10 Mar 2017"),
                NextDate = Convert.ToDateTime("10 Apr 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Commission, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1480.84, result.Commission);
        }
    }
}
