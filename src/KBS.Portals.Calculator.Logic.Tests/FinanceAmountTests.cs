﻿using System;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class FinanceAmountTests
    {
        [Test]
        public void CanGetCalculatorFromFactory()
        {
            var calc = CalculatorFactory.Create(CalculationType.FinanceAmount, new CalculatorData());

            Assert.AreEqual(calc.GetType(), typeof(FinanceAmount));

        }

        [Test]
        public void CalculateFinanceAmountForSimpleInputs()
        {
            CalculatorData cd = new CalculatorData()
            {
//                FinanceAmount = 10000,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(207.53),
                IRR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

//            Assert.AreEqual(10000, result.FinanceAmount);
            Assert.AreEqual(9999.98, result.FinanceAmount);
        }
        [Test]
        public void CalculateFinanceAmountForQuarterly()
        {
            CalculatorData cd = new CalculatorData()
            {
                //                FinanceAmount = 10000,
                IRR = 3.069061,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Quarterly
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

            Assert.AreEqual(10000, result.FinanceAmount);
        }
        [Test]
        public void CalculateFinanceAmountForAnnual()
        {
            CalculatorData cd = new CalculatorData()
            {
                //                FinanceAmount = 10000,
                IRR = 9.000031,
                NoOfInstallments = 6,
                Installment = Convert.ToDecimal(2059.46),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Annual
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

            Assert.AreEqual(10000, result.FinanceAmount);
        }
        [Test]
        public void CalculateFinanceAmountWithDocFee()
        {
            CalculatorData cd = new CalculatorData()
            {
//                FinanceAmount = 10000,
                IRR = 9,
                NoOfInstallments = 60,
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                DocFee = 250
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

            Assert.AreEqual(9999.98, result.FinanceAmount);
        }

        [Test]
        public void CapitalFlow_1()
        {
            CalculatorData cd = new CalculatorData()
            {
//                FinanceAmount = 36900,
                IRR = 11.191345,
                NoOfInstallments = 24,
                UpFrontNo = 1,
                UpFrontValue = 15900,
                Commission = 650,
                DocFee = 0,
                Installment = Convert.ToDecimal(1006.80),
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

            Assert.AreEqual(36900, result.FinanceAmount);
        }

        [Test]
        public void CapitalFlow_3()
        {
            CalculatorData cd = new CalculatorData()
            {
//                FinanceAmount = 30750,
                IRR = 11.796204,
                NoOfInstallments = 24,
                UpFrontNo = 1,
                UpFrontValue = 16750,
                Commission = 350,
                DocFee = 0,
                Installment = Convert.ToDecimal(671.20),
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();
            Assert.AreEqual(30750, result.FinanceAmount);

        }

        [Test]
        public void CapitalFlow_4()
        {
            CalculatorData cd = new CalculatorData()
            {
//                FinanceAmount = 16250,
                IRR = 11.167938,
                UpFrontNo = 3,
                UpFrontValue = 518,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 33,
                Installment = Convert.ToDecimal(518),
                StartDate = Convert.ToDateTime("17 Oct 2016"),
                NextDate = Convert.ToDateTime("09 Nov 2016")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

//            Assert.AreEqual(16250, result.FinanceAmount);
            Assert.AreEqual(16250.03, result.FinanceAmount);
        }

        [Test]
        public void CapitalFlow_6()
        {
            CalculatorData cd = new CalculatorData()
            {
//                FinanceAmount = 68000,
                IRR = 11.000107,
                UpFrontNo = 1,
                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 250,
                NoOfInstallments = 30,
                Installment = Convert.ToDecimal(2343.3),
                StartDate = Convert.ToDateTime("10 Mar 2017"),
                NextDate = Convert.ToDateTime("10 Apr 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.FinanceAmount, cd);

            var result = calc.Calculate();

//            Assert.AreEqual(68000, result.FinanceAmount);
            Assert.AreEqual(68000.03, result.FinanceAmount);
        }
    }
}
