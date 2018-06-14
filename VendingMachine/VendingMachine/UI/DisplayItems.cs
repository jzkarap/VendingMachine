using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine.UI
{
    public class DisplayItems
    {
		public DisplayItems(Dictionary<string, Stack<Item>> slots, CashCounter currentCounter)
		{
			Console.Clear();

			foreach (var kvp in slots)
			{
				if (kvp.Value.Count > 0)
				{
					Console.WriteLine(kvp.Key + " " + kvp.Value.Peek().Name + " " + kvp.Value.Peek().Cost);
				}
				else
				{
					Console.WriteLine("SOLD OUT!!");
				}
			}

			Console.WriteLine("\nPress Enter to continue.\n");
			Console.ReadLine();
			Console.Clear();
			Menu main = new Menu(slots, currentCounter);
		}

	}
}
