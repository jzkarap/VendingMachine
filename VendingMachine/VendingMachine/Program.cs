using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			TransactionRecorder();
			CashCounter cashCounter = new CashCounter();

			// Creates a group of items to be purchased
			Stack<Item> purchases = new Stack<Item>();

			// Calls method to build machine
			GetItems();
			
			// Position/amount testing
			foreach (var kvp in GetItems())
			{
				Console.WriteLine(kvp.Key + " " + kvp.Value.Pop().Name);
			}
			// Transaction testing
			Console.WriteLine("Testing initial balance: " + cashCounter.Balance);
			cashCounter.Feed((decimal)5.05);
			cashCounter.Charge(GetItems()["A1"].Pop().Cost);
			Console.WriteLine($"Testing updated balance: {cashCounter.Balance:c}");
			Console.WriteLine(cashCounter.GetChange());
			Console.WriteLine($"{cashCounter.Balance:c}");

			TransactionRecorder();
			
		}

		/// <summary>
		/// Reads an inventory file and interprets for program
		/// </summary>
		/// <returns>The initial composition of machine + inventory</returns>
		static Dictionary<string, Stack<Item>> GetItems()
		{
			// Creates slots to hold a location & stacks of items
			Dictionary<string, Stack<Item>> slots = new Dictionary<string, Stack<Item>>();

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
						Item itemToVend = new Item(itemDetails[1], decimal.Parse(itemDetails[2]), itemDetails[3]);

						// Push itemToVend into stack stockPerSlot 5 times
						for (int i = 0; i < 5; i++)
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

		static void TransactionRecorder()
		{
			using (StreamWriter recorder = new StreamWriter("log.txt", true))
			{
				recorder.WriteLine(DateTime.Now.ToString());
			}
		}
	}
}
