using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestReference;

namespace UnitTest
{
    [TestClass]
    public class MyClassTest
    {
        private MyClass _myClass;

        [TestInitialize]
        public void Init() => _myClass = new MyClass(new MyService());

        [TestMethod]
        public void TestReturnA()
        {
            var actualValue = _myClass.WhatItReturns(25);
            var expectedValue = MyService.ConstantA;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestReturnB()
        {
            var actualValue = _myClass.WhatItReturns(64);
            var expectedValue = MyService.ConstantB;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), MyService.ArgumentNull)]
        public void TestReturnArgumentNullException() => _myClass.WhatItReturns(null);

        [TestMethod]
        [ExpectedException(typeof(InvalidEnumArgumentException), MyService.InvalidArgument)]
        public void TestReturnInvalidArgument() => _myClass.WhatItReturns(0);

        [TestCleanup]
        public void TestClean() => _myClass = null;
    }
}
