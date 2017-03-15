﻿using System;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]    
    public class APRInstallmentTests
    {
        [Test]
        public void CanGetCalculatorFromFactory()
        {
            var calc = CalculatorFactory.Create(CalculationType.APRInstallment, new CalculatorData());

            Assert.AreEqual(calc.GetType(), typeof(InstallmentApr));
        }

        [Test]
        public void CalculateAPRInstallmentForSimpleInputs()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                APR = 9,
                StartDate =  Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(205.83, result.Installment );
        }
        [Test]
        public void CalculateAPRInstallmentForQuarterly()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                APR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Quarterly
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(295.89, result.Installment);
        }
        [Test]
        public void CalculateAPRInstallmentForAnnual()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 6,
                APR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                Frequency = Frequency.Annual
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(2058.91, result.Installment);
        }
        [Test]
        public void CalculateAPRWithDocFee()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                APR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017"),
                DocFee =  250
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(200.72, result.Installment);
        }


        [Test]
        public void CapitalFlowAPR_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 36900,
                NoOfInstallments = 24,
                APR = 15.381418,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 1,
                UpFrontValue = 15900,
                Commission = 650,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1006.78, result.Installment);
        }

        [Test]
        public void CapitalFlowAPR_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 30750,
                NoOfInstallments = 24,
                APR = 15.381418,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 1,
                UpFrontValue = 16750,
                Commission = 350,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(671.19, result.Installment);
        }
    }
}
