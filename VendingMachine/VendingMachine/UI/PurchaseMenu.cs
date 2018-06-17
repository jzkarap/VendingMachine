using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine.UI
{
	public class PurchaseMenu
	{
		Vendor machine = new Vendor();


		public PurchaseMenu(Dictionary<string, Stack<Item>> stock, CashCounter cashCounter, Queue<Item> purchases)
		{
			Console.Clear();

			Console.WriteLine("(1) Feed Money");
			Console.WriteLine("(2) Select Product");
			Console.WriteLine("(3) Finish Transaction");
			Console.WriteLine($"Current money provided: {cashCounter.Balance:c}");

			string userInput = Console.ReadLine();

			// Prompts user for valid menu selection
			while (userInput != "1" &&
					userInput != "2" &&
					userInput != "3")
			{
				Console.WriteLine("Please select option 1, 2, or 3!");
				userInput = Console.ReadLine();
			}

			// Prompts user to enter whole dollar amount $1-10
			// Does not allow invalid input
			if (userInput == "1")
			{
				Console.WriteLine();
				Console.Write("Please enter a whole dollar amount $1, $2, $5, or $10: ");
				userInput = Console.ReadLine();

				while (userInput != "1" &&
					userInput != "2" &&
					userInput != "5" &&
					userInput != "10")
				{
					Console.WriteLine("Please select a valid amount!");
					userInput = Console.ReadLine();
				}

				cashCounter.Feed(decimal.Parse(userInput));

				PurchaseMenu purchase = new PurchaseMenu(stock, cashCounter, purchases);
			}

			// Generates list of current stock,
			// Gets user selection,
			// Calls CashCounter.Charge function
			if (userInput == "2")
			{
				Console.Clear();

				foreach (var kvp in stock)
				{
					if (kvp.Value.Count > 0)
					{
						Console.WriteLine($"{kvp.Key} {kvp.Value.Peek().Name}".PadRight(25) + $"{kvp.Value.Peek().Cost:c}");
					}
					else
					{
						Console.WriteLine("SOLD OUT!!");
					}
				}

				Console.WriteLine();

				Console.Write("Please select an item: ");

				userInput = Console.ReadLine();

				Console.WriteLine();

				purchases = machine.Transaction(cashCounter, stock, userInput, purchases);

				// Could make this prompt user for valid input instead of kicking them back to Purchase menu--
				// But directions state to return to Purchase menu...
				// ... How closely are we meant to follow the script?
				Console.WriteLine("Press Enter to return to purchase menu!");

				Console.ReadLine();

				PurchaseMenu purchase = new PurchaseMenu(stock, cashCounter, purchases);
			}

			if (userInput == "3")
			{
				int[] arrayOfChange = cashCounter.GetChange();

				int quarterCount = arrayOfChange[0];
				int dimeCount = arrayOfChange[1];
				int nickelCount = arrayOfChange[2];

				Console.WriteLine($"Change returned: {quarterCount} quarters, {dimeCount} dimes, {nickelCount} nickels");
				Console.WriteLine();
				Console.Write(".");
				Thread.Sleep(300);
				Console.Write(".");
				Thread.Sleep(300);
				Console.Write(".");
				Thread.Sleep(300);
				Console.Write(".");
				Thread.Sleep(300);
				Console.Clear();

				while (purchases.Count > 0)
				{
					Item currentItem = purchases.Dequeue();
					Console.WriteLine(currentItem.Noise());
					Thread.Sleep(800);
				}

				Console.WriteLine("Thanks for shopping with ComplicatedVending!");
				Thread.Sleep(800);
				Console.WriteLine();
				Menu main = new Menu(stock, cashCounter, purchases);
			}

		}
	}
}
