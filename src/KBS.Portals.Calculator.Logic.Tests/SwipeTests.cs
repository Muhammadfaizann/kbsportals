using System;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class SwipeTests
    {

        [Test]
        public void AprthenIrr()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                APR = 9,
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(205.83, result.Installment);

            cd.IRR = 9;
//            cd.Installment = 0;
            ICalculator calcIRR = CalculatorFactory.Create(CalculationType.IRRInstallment, cd);

            var result2 = calcIRR.Calculate();
            Assert.AreEqual(207.53, result2.Installment);
        }
    }
}
