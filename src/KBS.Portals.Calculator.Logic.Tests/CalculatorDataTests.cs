using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class CalculatorDataTests
    {
        [Test]
        [TestCase(true, CalculationType.APRInstallment,  100, 100, .1)]
        [TestCase(false, CalculationType.APRInstallment, 100, 100, 0)]
        [TestCase(false, CalculationType.APRInstallment, 100, 0, .1)]
        [TestCase(false, CalculationType.APRInstallment, 100, 0, 0)]
        [TestCase(false, CalculationType.APRInstallment, 0, 100, .1)]
        [TestCase(false, CalculationType.APRInstallment, 0, 100, 0)]
        [TestCase(false, CalculationType.APRInstallment, 0, 0, .1)]
        [TestCase(false, CalculationType.APRInstallment, 0, 0, 0)]
        [TestCase(true, CalculationType.IRRInstallment, 100, 100, .1)]
        [TestCase(false, CalculationType.IRRInstallment, 100, 100, 0)]
        [TestCase(false, CalculationType.IRRInstallment, 100, 0, .1)]
        [TestCase(false, CalculationType.IRRInstallment, 100, 0, 0)]
        [TestCase(false, CalculationType.IRRInstallment, 0, 100, .1)]
        [TestCase(false, CalculationType.IRRInstallment, 0, 100, 0)]
        [TestCase(false, CalculationType.IRRInstallment, 0, 0, .1)]
        [TestCase(false, CalculationType.IRRInstallment, 0, 0, 0)]
        public void CheckCalculatorDataIsValidForAPRInstallment(bool expected, CalculationType ct, decimal fa, int t, double rate)
        {
            CalculatorData cd = new CalculatorData()
            {
                CalculationType = ct,
                FinanceAmount = fa,
                NoOfInstallments = t,
                APR = rate,
                IRR = rate 
            };

            var context = new ValidationContext(cd, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(cd, context, results);

            Assert.AreEqual(expected, isValid);
        }
    }
}
