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
            Assert.AreEqual(12.271689, result.APR);
            Assert.AreEqual(118981.96, result.TotalSchedule);
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

        [Test]
        public void FAFRate_M_33_3()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 10000,
                UpFrontNo = 3,
                //                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 33,
                Installment = Convert.ToDecimal(350),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(18.895447, result.IRR);
            Assert.AreEqual(20.620883, result.APR);
        }

        [Test]
        public void FAFRate_M_44_4()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 10000,
                UpFrontNo = 4,
                //                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 44,
                Installment = Convert.ToDecimal(350),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(35.038177, result.IRR);
            Assert.AreEqual(41.250385, result.APR);
        }

        [Test]
        public void FAFRate_M_55_5()
        {
            CalculatorData cd = new CalculatorData()
            {
                // Added Frig to get upfront value in 
                FinanceAmount = 10000,
                UpFrontNo = 5,
                //                UpFrontValue = 6800,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 55,
                Installment = Convert.ToDecimal(350),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Jun 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(43.753098, result.IRR);
            Assert.AreEqual(53.686299, result.APR);
        }
// Half Yearly
        [Test]
        public void FAFRate_H_5_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 5,
                Installment = Convert.ToDecimal(1700),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(1.595337, result.IRR);
            Assert.AreEqual(1.607054, result.APR);
        }

        [Test]
        public void FAFRate_H_7_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 7,
                Installment = Convert.ToDecimal(1700),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(19.794189, result.IRR);
            Assert.AreEqual(21.692488, result.APR);
        }

        [Test]
        public void FAFRate_H_9_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.HalfYearly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 9,
                Installment = Convert.ToDecimal(1700),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Nov 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(28.675659, result.IRR);
            Assert.AreEqual(32.761486, result.APR);
        }

        // Quarterly
        [Test]
        public void FAFRate_Q_11_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 11,
                Installment = Convert.ToDecimal(1000),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(13.979797, result.IRR);
            Assert.AreEqual(14.911253, result.APR);
        }

        [Test]
        public void FAFRate_Q_15_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 15,
                Installment = Convert.ToDecimal(1000),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(28.694214, result.IRR);
            Assert.AreEqual(32.785546, result.APR);
        }
        [Test]
        public void FAFRate_Q_19_1()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 1,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 19,
                Installment = Convert.ToDecimal(1000),
                StartDate = Convert.ToDateTime("12 May 2017"),
                NextDate = Convert.ToDateTime("12 Aug 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(35.60614, result.IRR);
            Assert.AreEqual(42.031849, result.APR);
        }

        [Test]
        public void COGRate_Q()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 14172,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 21,
                Installment = Convert.ToDecimal(865),
                StartDate = Convert.ToDateTime("10 May 2017"),
                NextDate = Convert.ToDateTime("20 May 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(10.416077, result.IRR);
            Assert.AreEqual(10.928016, result.APR);
        }

        [Test]
        public void Rate_Bal()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Monthly,
                FinanceAmount = 10000,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 55,
                Ballon = Convert.ToDecimal(1022.20),
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(9.000000, result.IRR);
            Assert.AreEqual(9.380690, result.APR);
        }


        [Test]
        public void Rate_BalQ()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Quarterly,
                FinanceAmount = 10000,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 36,
                Ballon = Convert.ToDecimal(1000),
                Installment = Convert.ToDecimal(320),
                StartDate = Convert.ToDateTime("02 Jun 2017"),
                NextDate = Convert.ToDateTime("02 Jul 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(4.870010, result.IRR);
            Assert.AreEqual(4.980197, result.APR);
        }
        [Test]
        public void Rate_Res()
        {
            CalculatorData cd = new CalculatorData()
            {
                Frequency = Frequency.Monthly,
                FinanceAmount = 10000,
                UpFrontNo = 0,
                Commission = 0,
                DocFee = 0,
                NoOfInstallments = 55,
                Residual = Convert.ToDecimal(1022.20),
                Installment = Convert.ToDecimal(207.53),
                StartDate = Convert.ToDateTime("14 Feb 2017"),
                NextDate = Convert.ToDateTime("14 Mar 2017")
            };

            ICalculator calc = CalculatorFactory.Create(CalculationType.Rate, cd);

            var result = calc.Calculate();

            Assert.AreEqual(9.021454, result.IRR);
            Assert.AreEqual(9.403984, result.APR);
            Assert.AreEqual(12436.35, result.TotalSchedule);
        }


    }
}
