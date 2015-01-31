using System;
using NUnit.Framework;

namespace Frankentime.Domain.UnitTests.SysTimerTests
{
    [TestFixture]
    public class BasicTests
    {
        [SetUp]
        public void SetUp()
        {
            SysTime.Instance = null;
        }

        [Test]
        public void StaticCallInstantiatesItself()
        {
            Assert.IsTrue(DateTime.UtcNow.Subtract(SysTime.UtcNow) < TimeSpan.FromMilliseconds(500),
                Environment.NewLine + "SysTime not using DateTime correctly");

            Assert.AreEqual(typeof(SysTime), SysTime.Instance.GetType(), Environment.NewLine + "Incorrect SysTime instantiation");
        }
    }
}
