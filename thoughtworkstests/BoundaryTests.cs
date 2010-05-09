using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using thoughtworks;

namespace thoughtworkstests
{
    /// <summary>
    /// Summary description for BoundaryTests
    /// </summary>
    [TestClass]
    public class BoundaryTests
    {

        #region Setup
        
        public BoundaryTests()
        {
           
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
#endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"A gibberish binary file was unexpectedly allowed.")]
        public void BinaryInput()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.binary)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An incomplete line was unexpectedly allowed.")]
        public void TruncatedLine()
        {
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.singleItem.Substring(0,5))));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "A boat load of objects were unexpectedly allowed.")]
        public void UnrealisticItemNumber()
        {
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.lionsTigersAndBooksOhMy)));
            Assert.AreEqual(1000000, list.Count);
        }
        [TestMethod]        
        public void Count500()
        {
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.Test500)));
            Assert.AreEqual(500, list.Count);
        }

    }
}
