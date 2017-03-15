using System;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Models;
using NUnit.Framework;

namespace KBS.Portals.Calculator.Logic.Tests
{
    [TestFixture]
    public class SortKeyTests
    {
        [Test]
        public async Task TestThatSortKeysWithSameValuesAreEqual()
        {
            var sk1 = new SortKey(new DateTime(2017,1,1), 1);
            var sk1a = sk1;
            var sk2 = new SortKey(new DateTime(2017,1,1), 1);

            Assert.IsTrue(sk1.Equals(sk2));
            Assert.IsTrue(sk1 == sk2);
            Assert.IsTrue(sk1.Equals(sk1a));
            Assert.IsTrue(sk1 == sk1a);
            Assert.IsTrue(sk1.GetHashCode() == sk2.GetHashCode());
            Assert.IsTrue(sk1.GetHashCode() == sk1a.GetHashCode());
        }

        [Test]
        public async Task TestThatSortKeysWithDifferentDates()
        {
            var sk1 = new SortKey(new DateTime(2017, 1, 1), 1);
            var sk2 = new SortKey(new DateTime(2017, 1, 2), 1);

            Assert.IsFalse(sk1.Equals(sk2));
            Assert.IsTrue(sk1 != sk2);
        }

        [Test]
        public async Task TestThatSortKeysWithDifferentSerials()
        {
            var sk1 = new SortKey(new DateTime(2017, 1, 1), 1);
            var sk2 = new SortKey(new DateTime(2017, 1, 1), 2);

            Assert.IsFalse(sk1.Equals(sk2));
            Assert.IsTrue(sk1 != sk2);
        }

        [Test]
        public async Task TestThatSortKeysWithDifferentParameters()
        {
            var sk1 = new SortKey(new DateTime(2017, 1, 1), 1);
            var sk2 = new SortKey(new DateTime(2017, 1, 2), 2);

            Assert.IsFalse(sk1.Equals(sk2));
            Assert.IsTrue(sk1 != sk2);
        }
    }
}
