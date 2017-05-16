using System;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class RateTests
    {
        [Test]
        public void CanGetCalculatorFromFactory()
        {
            var calc = CalculatorFactory.Create(CalculationType.Rate, new CalculatorData());

            Assert.AreEqual(calc.GetType(), typeof(Rate));

        }

        [Test]
        public void CalculateRateForSimpleInputs()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(8.999924, result.IRR);
            Assert.AreEqual(9.380607, result.APR);
        }
        [Test]
        public void CalculateRateForQuarterly()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Quarterly
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(3.069061, result.IRR);
            Assert.AreEqual(3.112602, result.APR);
        }
        [Test]
        public void CalculateRateForAnnual()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 6,
                Installment = Convert.ToDecimal(2059.46),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Annual
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(9.000031, result.IRR);
            Assert.AreEqual(9.380723, result.APR);
        }
        [Test]
        public void CalculateRateWithPur()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 100000,
                DocFee = 250,
                PurchaseFee = 250,
                NoOfInstallments = 36,
                Installment = Convert.ToDecimal(3298.11),
                StartDate = Convert.ToDateTime("11 May 2017"),
                NextDate = Convert.ToDateTime("18 Jun 2017"),
                Frequency = Frequency.Monthly
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(11.341389, result.IRR);
           // Assert.AreEqual(12.271689, result.APR);
        }



        [Test]
        public void CalculateIRRWithDocFee()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                DocFee = 250
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(8.999924, result.IRR);
            Assert.AreEqual(10.560476, result.APR);
        }

        [Test]
        public void CapitalFlowIRR_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 36900 - 15900,
                NoOfInstallments = 24,
                UpFrontNo = 0,
//                UpFrontValue = 15900,
                Commission = 650,
                DocFee = 0,
                Installment = Convert.ToDecimal(1006.80),
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(11.191345, result.IRR);
            Assert.AreEqual(15.381418, result.APR);
        }

        [Test]
        public void CapitalFlowIRR_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 30750 - 16750,
                NoOfInstallments = 24,
                UpFrontNo = 0,
//                UpFrontValue = 16750,
                Commission = 350,
                DocFee = 0,
                Installment = Convert.ToDecimal(671.20),
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(11.796204, result.IRR);
            Assert.AreEqual(15.381418, result.APR);
        }
        [Test]
        public void CapitalFlowIRR_4()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 16250,
                UpFrontNo = 3,
//                UpFrontValue = 518,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 33,
                Installment = Convert.ToDecimal(518),
                StartDate = Convert.ToDateTime("17 Oct 2016"),
                NextDate = Convert.ToDateTime("09 Nov 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(11.167938, result.IRR);
            Assert.AreEqual(11.757695, result.APR);
        }
        [Test]
        public void CapitalFlowIRR_6()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 68000 - 6800,
                UpFrontNo = 0,
//                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 250,
                NoOfInstallments = 30,
                Installment = Convert.ToDecimal(2343.3),
                StartDate = Convert.ToDateTime("10 Mar 2017"),
                NextDate = Convert.ToDateTime("10 Apr 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(11.000107, result.IRR);
            Assert.AreEqual(11.938409, result.APR);
        }
    }
}
