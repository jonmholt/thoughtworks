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
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ItemFactoryTests
    {       
        public ItemFactoryTests()
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
        [ClassInitialize()]
        public static void Initialize(TestContext testContext) 
        { 
            


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

        /**
         * This test takes a single line standard entry and checks that a single object is returned
         */
        [TestMethod]
        public void SimpleParse()
        {            
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.singleItem)));

            //Assert that only a single item was returned
            Assert.AreEqual(1, list.Count);
        }
        
        /**
         * This takes a single line standard entry and checks that all the values are parsed correctly
         */
        [TestMethod]
        public void singleItemValueCheck()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.singleItem)));

            //Check that there is only one
            Assert.AreEqual(1, list.Count);

            //Get the item
            IItem item = list[0];

            //Asserts
            Assert.AreEqual(12.49, item.Price);
            Assert.IsInstanceOfType(item, typeof(Book));
            Assert.AreEqual("book", item.Description);
        }

        /**
         * This test takes a single line entry with a 2 count and checks that two objects are returned
         */
        [TestMethod]
        public void TwoOfSameItem()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.twoOfSameItem)));

            //Assert that 2 items are returned
            Assert.AreEqual(2, list.Count);
        }
        /**
         * This test takes a double line standard entry and checks that two objects are returned
         */
        [TestMethod]
        public void doubleItemCountCheck()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.doubleItem)));

            //Assert that 2 items are returned
            Assert.AreEqual(2, list.Count);
        }
        /**
         * This takes a single line standard entry and checks that all the values are parsed correctly
         */
        [TestMethod]
        public void doubleItemValueCheck()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.doubleItem)));

            //Check that there are two
            Assert.AreEqual(2, list.Count);

            //Get the item
            IItem item = list[1];

            //Asserts
            Assert.AreEqual(12.49, item.Price);
            Assert.IsInstanceOfType(item, typeof(Book));
            Assert.AreEqual("book", item.Description);
        }
        /**
         * This test takes a double line standard entry and checks that the two objects returned are equivalent
         */
        [TestMethod]
        public void doubleItemEquivalenceCheck()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.doubleItem)));

            //Assert that 2 items are returned
            Assert.AreEqual(2, list.Count);

            CompareItems(list[0], list[1]);
        }
        /**
         * Since the TestConstants.doubleItem and the twoOfSameItem are logically equivalent, this test checks that
         * two equivalent objects are returned
         */
        [TestMethod]
        public void TwoItemEquivalence()
        {
            //Get the two of same list
            List<IItem> list1 = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.twoOfSameItem)));

            //Assert that 2 items are returned
            Assert.AreEqual(2, list1.Count);

            //Get the double item List
            List<IItem> list2 = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.doubleItem)));

            //Assert that 2 items are returned
            Assert.AreEqual(2, list2.Count);

            //now for equivalence
            for (int i = 2 - 1; i >= 0; i--)
            {
                IItem item1 = list1[i];
                IItem item2 = list2[i];

                //Executes asserts on all the properties
                CompareItems(item1, item2);
            }
        }
        /**
         * This test parses input 1 and verifies the output values
         */
        [TestMethod]
        public void ValidateInput1()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.input1)));

            //Assert that 3 items are returned
            Assert.AreEqual(3, list.Count);

            foreach (IItem item in list)
            {
                if (item is Book)
                {
                    Assert.AreEqual(12.49, item.Price);
                    Assert.AreEqual(0, item.Tax);
                    Assert.AreEqual(12.49, item.ShelfPrice);
                    Assert.AreEqual("book", item.Description);
                }
                else if (item is Food)
                {
                    Assert.AreEqual(0.85, item.Price);
                    Assert.AreEqual(0, item.Tax);
                    Assert.AreEqual(0.85, item.ShelfPrice);
                    Assert.AreEqual("chocolate bar", item.Description);
                }
                else
                {
                    Assert.AreEqual(14.99, item.Price);
                    Assert.AreEqual(1.50, item.Tax);
                    Assert.AreEqual(16.49, item.ShelfPrice);
                    Assert.AreEqual("music CD", item.Description);
                }
            }
        }
        /**
         * This test parses input 2 and verifies the output values
         */
        [TestMethod]
        public void ValidateInput2()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.input2)));

            //Assert that 2 items are returned
            Assert.AreEqual(2, list.Count);

            foreach (IItem item in list)
            {
                if (item is Food)
                {
                    Assert.AreEqual(10.00, item.Price);
                    Assert.AreEqual(0.50, item.Tax);
                    Assert.AreEqual(10.50, item.ShelfPrice);
                    Assert.AreEqual("imported box of chocolates", item.Description);
                }
                else
                {
                    Assert.AreEqual(47.50, item.Price);
                    Assert.AreEqual(7.15, item.Tax);
                    Assert.AreEqual(54.65, item.ShelfPrice);
                    Assert.AreEqual("imported bottle of perfume", item.Description);
                }
            }
        }
        /**
         * This test parses input 3 and verifies the output values
         */
        [TestMethod]
        public void ValidateInput3()
        {
            //Get the list
            List<IItem> list = ItemFactory.Parse(new MemoryStream(Encoding.ASCII.GetBytes(TestConstants.input3)));

            //Assert that 4 items are returned
            Assert.AreEqual(4, list.Count);

            foreach (IItem item in list)
            {
                if (item is Medical)
                {
                    Assert.AreEqual(9.75, item.Price);
                    Assert.AreEqual(0, item.Tax);
                    Assert.AreEqual(9.75, item.ShelfPrice);
                    Assert.AreEqual("packet of headache pills", item.Description);
                }
                else if (item is Food)
                {
                    Assert.AreEqual(11.25, item.Price);
                    Assert.AreEqual(0.60, item.Tax);
                    Assert.AreEqual(11.85, item.ShelfPrice);
                    Assert.AreEqual("imported box of chocolates", item.Description);
                }
                else
                {
                    if (item.Description.Equals("bottle of perfume"))
                    {
                        Assert.AreEqual(18.99, item.Price);
                        Assert.AreEqual(1.90, item.Tax);
                        Assert.AreEqual(20.89, item.ShelfPrice);
                        Assert.AreEqual("bottle of perfume", item.Description);
                    }
                    else
                    {
                        Assert.AreEqual(27.99, item.Price);
                        Assert.AreEqual(4.20, item.Tax);
                        Assert.AreEqual(32.19, item.ShelfPrice);
                        Assert.AreEqual("imported bottle of perfume", item.Description);
                    }
                }
            }
        }

#region Helpers
        
        
        //Convenience methods
        private void CompareItems(IItem item1, IItem item2){
            Assert.AreEqual(item1.Description, item2.Description);
            Assert.AreEqual(item1.Price, item2.Price);
            Assert.AreEqual(item1.ShelfPrice, item2.ShelfPrice);
            Assert.IsInstanceOfType(item1, item2.GetType());
            Assert.AreEqual(item1.Tax, item2.Tax);
        }
#endregion

    }
}
