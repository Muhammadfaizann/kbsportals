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
            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, new CalculatorData());

            Assert.AreEqual(calc.GetType(), typeof(APRInstallmentCalculator));
        }

        [Test]
        public void CalculateAPRInstallmentForSimpleInputs()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                Term = 60
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(100M, result.Installment );
        }
    }
}
