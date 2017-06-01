using System;
using System.Diagnostics;
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
            Assert.AreEqual(10000, result.TotalCost);
            Assert.AreEqual(12349.8, result.TotalSchedule);
            Assert.AreEqual(2349.8, result.Charges);

        }
        [Test]
        public void CalculateAPRInstallmentForQuarterly()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                NoOfInstallments = 60,
                APR = 9.066625,
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
                APR = 9.367173,
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
                APR = 8.999852,
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
                // Added Frig to get upfront value in 
                FinanceAmount = 36900 - 15900,
                NoOfInstallments = 24,
                APR = 15.379017,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 0,
//                UpFrontValue = 15900,
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
                // Added Frig to get upfront value in 
                FinanceAmount = 30750 - 16750,
                NoOfInstallments = 24,
                APR = 15.379608,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 0,
//                UpFrontValue = 16750,
                Commission = 350,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(671.19, result.Installment);
        }


        [Test]
        public void APR_Upfront()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added upfront ( 24 instalments in total of which 3 are upfront gives 21)/
                FinanceAmount = 15000,
                NoOfInstallments = 21,
                APR = 9.905589,
                StartDate = Convert.ToDateTime("22 Nov 2016"),
                NextDate = Convert.ToDateTime("09 Dec 2016"),
                UpFrontNo = 3,
                //                UpFrontValue = 16750,
                Commission = 0,
                DocFee = 0
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(671.19, result.Installment);
        }


        [Test]
        public void FAFAprInstallment_M_33_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                UpFrontNo = 3,
                Commission = 0,
                DocFee = 0,
                APR = 20.620883,
                NoOfInstallments = 33,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Installment);
        }

        [Test]
        public void FAFAprInstallment_M_44_4()
        {
            CalculatorData cd = new CalculatorData()
            {
                FinanceAmount = 10000,
                UpFrontNo = 4,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 44,
                APR = 41.250385,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Installment);
        }

        [Test]
        public void FAFAprInstallment_M_55_5()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Monthly,
                FinanceAmount = 10000,
                UpFrontNo = 5,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 55,
                APR = 53.686299,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(350, result.Installment);
        }
        // Half Yearly
        [Test]
        public void FAFAprInstallment_H_5_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 5,
                APR = 1.607054,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1700, result.Installment);
        }

        [Test]
        public void FAFAprInstallment_H_7_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 7,
                APR = 21.692488,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1700, result.Installment);

        }

        [Test]
        public void FAFAprInstallment_H_9_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 9,
                APR = 32.761486,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1700, result.Installment);
        }

        // Quarterly
        [Test]
        public void FAFAprInstallment_Q_11_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 11,
                APR = 14.911253,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1000, result.Installment);
        }

        [Test]
        public void FAFAprInstallment_Q_15_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 15,
                APR = 32.785546,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1000, result.Installment);
        }
        [Test]
        public void FAFAprInstallment_Q_19_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 19,
                APR = 42.031849,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1000, result.Installment);
        }


        [Test]
        public void FAFAprInstallment_DivideByZero()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 0,
                APR = 42.031849,
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.APRInstallment, cd);

            var result = calc.Calculate();

            Assert.AreEqual(-9999, result.Installment);
        }




    }
}
