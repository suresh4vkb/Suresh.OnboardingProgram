using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestReference;

namespace UnitTest
{
    [TestClass]
    public class MyServiceTest
    {
        private MyService _myService;


        [TestInitialize]
        public void Init() => _myService = new MyService();

        [TestMethod]
        public void TestReturnA()
        {
            var actualValue = _myService.ReturnAorB(25);
            var expectedValue = MyService.ConstantA;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestReturnB()
        {
            var actualValue = _myService.ReturnAorB(64);
            var expectedValue = MyService.ConstantB;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), MyService.ArgumentNull)]
        public void TestReturnArgumentNullException() => _myService.ReturnAorB(null);

        [TestMethod]
        [ExpectedException(typeof(InvalidEnumArgumentException), MyService.InvalidArgument)]
        public void TestReturnInvalidArgument() => _myService.ReturnAorB(0);

        [TestCleanup]
        public void TestClean() => _myService = null;
    }
}
