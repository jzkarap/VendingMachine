using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VendingMachine.Items;
using VendingMachine.UI;
using VendingMachine.VendingMachine;

namespace VendingMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, Stack<Item>> stock = GetItems();

			CashCounter cashCounter = new CashCounter();

			// Creates a group of items to be purchased
			Stack<Item> purchases = new Stack<Item>();

			Menu main = new Menu(stock, cashCounter);
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

	}
}
