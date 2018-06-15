using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine.UI
{
	public class Menu
	{
		public Menu(Dictionary<string, Stack<Item>> slots, CashCounter currentCounter, Queue<Item> purchases, Vendor machine)
		{
			Console.WriteLine("(1) Display Vending Machine Items");
			Console.WriteLine("(2) Purchase");

			string userInput = Console.ReadLine();

			while (userInput != "1" &&
				   userInput != "2")
			{
				Console.WriteLine("Please select option 1 or option 2!");
				userInput = Console.ReadLine();
			}
			if (userInput == "1")
			{
				DisplayItems items = new DisplayItems(slots, currentCounter, purchases, machine);
			}
			if (userInput == "2")
			{
				PurchaseMenu purchase = new PurchaseMenu(slots, currentCounter, purchases, machine);
			}
		}

	}
}
