using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thoughtworks;
using System.IO;

namespace thoughtworkstests
{
    /// <summary>
    /// Summary description for ReceiptTests
    /// </summary>
    [TestClass]
    public class ReceiptTests
    {
        public ReceiptTests()
        {
            //
            // TODO: Add constructor logic here
            //
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SimpleReceipt()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.singleItem)));

            //Assert that only a single item was returned
            Assert.AreEqual(1, list.Count);

            //Ensure a string is returned
            string receipt = ReceiptFormatter.getReceipt(list);
            Assert.IsNotNull(receipt);
            Assert.AreNotEqual(receipt.Length, 0);
        }

        [TestMethod]
        public void SingleItemNoTaxCheck()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.singleItem)));

            //Assert that only a single item was returned
            Assert.AreEqual(1, list.Count);

            //Ensure a string is returned
            string receipt = ReceiptFormatter.getReceipt(list);

            //And it should be...
            string shouldBe = "1 book: $12.49\nSales Taxes: $0.00\nTotal: $12.49\n";
            Assert.AreEqual(shouldBe, receipt);
        }

        [TestMethod]
        public void SingleItemTaxCheck()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.singleItemWithTax)));

            //Assert that only a single item was returned
            Assert.AreEqual(1, list.Count);

            //Ensure a string is returned
            string receipt = ReceiptFormatter.getReceipt(list);

            //And it should be...
            string shouldBe = "1 imported bottle of perfume: $54.65\nSales Taxes: $7.15\nTotal: $54.65\n";
            Assert.AreEqual(shouldBe, receipt);
        }
    }
}
