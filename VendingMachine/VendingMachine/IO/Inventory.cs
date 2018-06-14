using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VendingMachine.Items;

namespace VendingMachine.IO
{
	public class Inventory
	{
		public void GetItems()
		{
			int i;

			Stack<Item> stockPerSlot = new Stack<Item>();

			Dictionary<string, Stack<Item>> slots = new Dictionary<string, Stack<Item>>();

			using (StreamReader sr = new StreamReader("inventory.txt"))
			{
				while (!sr.EndOfStream)
				{
					// Reads each line from inventory.txt
					string line = sr.ReadLine();
					// Splits lines at pipe symbol
					string[] itemDetails = line.Split('|');

					for (i = 1; i <= 5; i++)
					{
						stockPerSlot.Push(new Item(itemDetails[1], decimal.Parse(itemDetails[2]), 
											itemDetails[3]));
						slots.Add(itemDetails[0], stockPerSlot);
					}

				}

				// Structure to work with: A1|Potato Crisps|3.05|Chip
			}

		}
	}
}