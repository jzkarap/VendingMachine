using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Vending_Machine_Balance_Test()
        {
            CashCounter cashCounter = new CashCounter();

            decimal balance = cashCounter.Balance;

            Assert.AreEqual<decimal>(0, balance);

        }

        [TestMethod]
        public void Vending_Machine_Feed_Money()
        {
            CashCounter cashCounter = new CashCounter();

			cashCounter.Feed(5);

            Assert.AreEqual(5, cashCounter.Balance);
        }

        [TestMethod]
        public void Vending_Machine_Return_Change()
        {
            CashCounter cashCounter = new CashCounter();

            cashCounter.Feed(5);

            cashCounter.GetChange();

            Assert.AreEqual(0, cashCounter.Balance);
           
        }

        [TestMethod]
        public void Vending_Machine_Test_Charge()
        {
			CashCounter cashCounter = new CashCounter();

			cashCounter.Charge("Barf", "Test", (decimal)3.45);

			Assert.AreEqual((decimal)0, cashCounter.Balance);
        }

		/// <summary>
		/// Tests if item will be added to queue if purchased with sufficient funds
		/// </summary>
		[TestMethod]
		public void Purchase_With_Enough_Cash()
		{
			Vendor testVendor = new Vendor();

			CashCounter testCounter = new CashCounter();
			testCounter.Feed(10);

			Item testItem = new Item("Thing", (decimal)2.25, "Stuff");

			Stack<Item> testStack = new Stack<Item>();
			testStack.Push(testItem);
			testStack.Push(testItem);
			testStack.Push(testItem);

			Dictionary<string, Stack<Item>> testItems = new Dictionary<string, Stack<Item>>();
			testItems.Add("A1", testStack);

			Queue<Item> testPurchases = new Queue<Item>();

			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);

			Assert.AreEqual(testItem, testPurchases.Dequeue());
		}

		/// <summary>
		/// Tests if item will be added to queue if purchased with insufficient funds
		/// </summary>
		[TestMethod]
		public void Purchase_With_Too_Little_Cash()
		{
			Vendor testVendor = new Vendor();

			CashCounter testCounter = new CashCounter();
			testCounter.Feed(1);

			Item testItem = new Item("Thing", (decimal)2.25, "Stuff");

			Stack<Item> testStack = new Stack<Item>();
			testStack.Push(testItem);
			testStack.Push(testItem);
			testStack.Push(testItem);

			Dictionary<string, Stack<Item>> testItems = new Dictionary<string, Stack<Item>>();
			testItems.Add("A1", testStack);

			Queue<Item> testPurchases = new Queue<Item>();

			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);

			int amountOfItemsInTestList = 0;

			foreach (var kvp in testItems)
			{
				amountOfItemsInTestList = kvp.Value.Count;
			}

			Assert.AreEqual(3, amountOfItemsInTestList);
			Assert.AreEqual(0, testPurchases.Count);
		}

		/// <summary>
		/// Tests if item will be added to queue if purchased with insufficient funds
		/// </summary>
		[TestMethod]
		public void Purchase_With_No_Stock_Remaining()
		{
			Vendor testVendor = new Vendor();

			CashCounter testCounter = new CashCounter();
			testCounter.Feed(100);

			Item testItem = new Item("Thing", (decimal)2.25, "Stuff");

			Stack<Item> testStack = new Stack<Item>();
			testStack.Push(testItem);
			testStack.Push(testItem);
			testStack.Push(testItem);

			Dictionary<string, Stack<Item>> testItems = new Dictionary<string, Stack<Item>>();
			testItems.Add("A1", testStack);

			Queue<Item> testPurchases = new Queue<Item>();

			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);
			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);
			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);
			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);

			int amountOfItemsInTestList = 0;

			foreach (var kvp in testItems)
			{
				amountOfItemsInTestList = kvp.Value.Count;
			}

			Assert.AreEqual(3, testPurchases.Count);
			Assert.AreEqual(0, amountOfItemsInTestList);
		}

		/// <summary>
		/// Transaction occurs if balance is exact item cost
		/// </summary>
		[TestMethod]
		public void Purchase_With_Exact_Change()
		{
			Vendor testVendor = new Vendor();

			CashCounter testCounter = new CashCounter();
			testCounter.Feed((decimal)1.50);

			Item testItem = new Item("Thing", (decimal)1.50, "Stuff");

			Stack<Item> testStack = new Stack<Item>();
			testStack.Push(testItem);
			testStack.Push(testItem);
			testStack.Push(testItem);

			Dictionary<string, Stack<Item>> testItems = new Dictionary<string, Stack<Item>>();
			testItems.Add("A1", testStack);

			Queue<Item> testPurchases = new Queue<Item>();

			testVendor.Transaction(testCounter, testItems, "A1", testPurchases);


			int amountOfItemsInTestList = 0;

			foreach (var kvp in testItems)
			{
				amountOfItemsInTestList = kvp.Value.Count;
			}

			Assert.AreEqual(1, testPurchases.Count);
			Assert.AreEqual(2, amountOfItemsInTestList);
		}
	}
}
