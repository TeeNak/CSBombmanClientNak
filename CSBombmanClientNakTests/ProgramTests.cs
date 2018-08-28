using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBombmanClientNak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanClientNak.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ExampleTest()
        {
            var s = Program.Example();
            Assert.AreEqual("aaa", s.Item1);
        }
    }
}