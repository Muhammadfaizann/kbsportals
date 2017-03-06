using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Util;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class EnumTests
    {
        [Test]
        public void TestEnumberationWithCustomDisplayText()
        {
            var expected = "Documentation Fee";
            var actual = ScheduleType.DOC.GetDescription();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEnumberationWithoutCustomDisplayText()
        {
            var expected = "APRInstallment";
            var actual = CalculationType.APRInstallment.GetDescription();

            Assert.AreEqual(expected, actual);

        }

    }
}
