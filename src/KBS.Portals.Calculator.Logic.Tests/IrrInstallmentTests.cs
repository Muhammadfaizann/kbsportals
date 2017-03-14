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
        public void CapitalFlow_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 36900,
                NoOfInstallments = 24,
                IRR = 11.191345,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontValue = 15900,
                Commission = 650,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1006.77, result.Installment);
        }
    }
}
