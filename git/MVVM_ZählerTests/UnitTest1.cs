using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zähler;

namespace MVVM_ZählerTests
{
    [TestClass]
    public class UnitTest1
    {
        Zählwerk zw = new Zählwerk(100);
        [TestMethod]
        public void TestMethod1()
        {
            zw.Zähle(12);
            Assert.AreEqual(112, zw.Zählerstand);
            zw.Zähle(30);
            Assert.AreEqual(sbyte.MaxValue, zw.Zählerstand);
            zw.Zähle(-50);
            Assert.AreEqual(77, zw.Zählerstand);
            zw.Zähle(-100);
            Assert.AreEqual(0, zw.Zählerstand);
        }
    }
}
