using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fahrkartenautomat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrkartenautomat.Tests
{
    [TestClass()]
    public class FahrkartenautomatTests
    {
        [TestMethod()]
        public void EingabeTest()
        {
            Fahrkartenautomat.Eingabe();
        }

        [TestMethod()]
        public void MünzeinwurfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FahrscheinDruckenTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RückgabeTest()
        {
            Assert.Fail();
        }
    }
}