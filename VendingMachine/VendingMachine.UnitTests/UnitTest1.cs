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

            decimal balance = cashCounter.Balance;

            decimal fedCashBalance = cashCounter.Feed(5);

            Assert.AreEqual(5, fedCashBalance);
        }

        [TestMethod]
        public void Vending_Machine_Return_Change()
        {
            CashCounter cashCounter = new CashCounter();

            decimal fedCash = cashCounter.Feed(5);

            cashCounter.GetChange();

            Assert.AreEqual(0, cashCounter.Balance);



           
        }
        [TestMethod]
        public void Vending_Machine_Test_Charge()
        {
            Dictionary<string, Stack<Item>> stock = GetItems();
            Dictionary<string, Stack<Item>> GetItems()
            {
                // Creates slots to hold a location & stacks of items
                Dictionary<string, Stack<Item>> slots = new Dictionary<string, Stack<Item>>();
                const int ProductName = 1;
                const int DefaultQuantity = 5;

                try
                {
                    using (StreamReader sr = new StreamReader("inventory.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            Stack<Item> stockForSlot = new Stack<Item>();
                            // Reads each line from inventory.txt
                            string line = sr.ReadLine();

                            // Splits lines at pipe symbol
                            string[] itemDetails = line.Split('|');

                            // Generates item
                            Item itemToVend = new Item(itemDetails[ProductName], decimal.Parse(itemDetails[2]), itemDetails[3]);

                            // Push itemToVend into stack stockPerSlot 5 times

                            for (int i = 0; i < DefaultQuantity; i++)
                            {
                                stockForSlot.Push(itemToVend);
                            }

                            // If slots (which represents a position in vending array)
                            // does not exist,
                            // add it
                            if (!slots.ContainsKey(itemDetails[0]))
                            {
                                // Attach current stock for slot to position in vending array
                                slots.Add(itemDetails[0], stockForSlot);
                            }
                        }

                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Looks like there is no inventory file available to stock your fake machine!");
                }

                return slots;
            }
            CashCounter cashCounter = new CashCounter();

            decimal fedCash = cashCounter.Feed(5);
            Queue<Item> purchases = new Queue<Item>();
            cashCounter.Charge(GetItems(), "A1", purchases);

            Assert.AreEqual((decimal)1.95, cashCounter.Balance);
        }
    }
}
