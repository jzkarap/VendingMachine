using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VendingMachine.Items;
using VendingMachine.IO;

namespace VendingMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			GetItems();

			Stack<Item> purchases = new Stack<Item>();

			GetItems();

			foreach (var kvp in GetItems())
			{
				Console.WriteLine(kvp.Key + " " + kvp.Value.Pop().Name);
			}
		
	
			Dictionary<string, Stack<Item>> GetItems()
			{
				int i;
				int lineCount = 0;

				Dictionary<string, Stack<Item>> slots = new Dictionary<string, Stack<Item>>();

				using (StreamReader sr = new StreamReader("inventory.txt"))
				{
					while (!sr.EndOfStream)
					{
						Stack<Item> stockPerSlot = new Stack<Item>();
						// Reads each line from inventory.txt
						string line = sr.ReadLine();
						lineCount++;
						// Splits lines at pipe symbol
						string[] itemDetails = line.Split('|');

						// Generates item
						Item itemToVend = new Item(itemDetails[1], decimal.Parse(itemDetails[2]), itemDetails[3]);

						// Push itemToVend into stack stockPerSlot 5 times
						for (i = 0; i < 5; i++)
						{
							stockPerSlot.Push(itemToVend);
						}

						// If slots (which represents a position in vending array)
						// does not exist,
						// add it
						if (!slots.ContainsKey(itemDetails[0]))
						{
							// Attach current stock for slot to position in vending array
							slots.Add(itemDetails[0], stockPerSlot);
						}

					}

				}
				return slots;
			}
		}
	}
}
