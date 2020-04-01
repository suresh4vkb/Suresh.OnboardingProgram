using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestReference;

namespace UnitTest
{
    [TestClass]
    public class DivisionTest
    {
        private MyDivisionClass _myDivisionClass;

        [TestInitialize]
        public void DivisionTestInit() => _myDivisionClass = new MyDivisionClass();

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Divide by zero attempted")]
        public void TestDividedByZero() => _myDivisionClass.Divide(25, 0);

        [TestMethod]
        public void TestDivisionWithPositiveValues()
        {
            var actualValue = _myDivisionClass.Divide(25, 5);
            var expectedValue = 5;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestDivisionWithZero()
        {
            var actualValue = _myDivisionClass.Divide(0, 5);
            var expectedValue = 0;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestDivisionWithNegativeValues()
        {
            var actualValue = _myDivisionClass.Divide(-25, 5);
            var expectedValue = -5;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCleanup]
        public void TestClean() => _myDivisionClass = null;
    }
}
