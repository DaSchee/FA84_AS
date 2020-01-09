using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fahrkartenautomat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fahrkartenautomat.Tests
{
    [TestClass()]
    public class FahrkartenautomatTests
    {
        ConsoleReadProxy consoleReadProxy = new ConsoleReadProxy();
        [TestMethod()]
        public void EingabeTest()
        {
            Console.SetIn(consoleReadProxy);
            Assert.AreEqual(50, Fahrkartenautomat.Eingabe());
        }

        [TestMethod()]
        public void MünzeinwurfTest()
        {
            consoleReadProxy.readReturn = "50";
            Console.SetIn(consoleReadProxy);
            Assert.AreEqual(50, Fahrkartenautomat.Münzeinwurf(100.5, 20.5));
        }

        [TestMethod()]
        public void FahrscheinDruckenTest()
        {
            Fahrkartenautomat.FahrscheinDrucken();
        }

        [TestMethod()]
        public void RückgabeTest()
        {
            Assert.AreEqual(0, Fahrkartenautomat.Rückgabe(44.75));
        }
    }

    public class ConsoleReadProxy : TextReader
    {
        public string readReturn = "50";
        public override string ReadLine()
        {
            return readReturn;
        }
    }
}