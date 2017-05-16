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
    }
}
