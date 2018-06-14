using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine.UI
{
	public class PurchaseMenu
	{
		public PurchaseMenu(Dictionary<string, Stack<Item>> stock, CashCounter currentCounter)
		{
			Console.Clear();

			Console.WriteLine("(1) Feed Money");
			Console.WriteLine("(2) Select Product");
			Console.WriteLine("(3) Finish Transaction");
			Console.WriteLine($"Current money provided: {currentCounter.Balance:c}");

			string userInput = Console.ReadLine();

			while (userInput != "1" &&
					userInput != "2" &&
					userInput != "3")
			{
				Console.WriteLine("Please select option 1, 2, or 3!");
				userInput = Console.ReadLine();
			}

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

				currentCounter.Balance += decimal.Parse(userInput);
				PurchaseMenu purchase = new PurchaseMenu(stock, currentCounter);
			}

			if (userInput == "2")
			{
				Console.Clear();

				foreach (var kvp in stock)
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

				Console.WriteLine();

				Console.Write("Please select an item: ");

				userInput = Console.ReadLine();

				Console.WriteLine();

				foreach (var kvp in stock)
				{
					if (stock.ContainsKey(userInput))
					{
						if (userInput.Equals(kvp.Key) &&
							kvp.Value.Count > 0)
						{
							currentCounter.Charge(kvp.Value.Peek().Cost);
							Console.WriteLine($"Item {kvp.Key} dispensed!");
							kvp.Value.Pop();
						}
						else if (userInput == kvp.Key &&
							kvp.Value.Count == 0)
						{
							Console.WriteLine("This item is SOLD OUT!!");
							break;
						}
					}
					else
					{
						// ITEM POPPED HAS TO GO TO PURCHASED LIST -- FIGURE OUT HOW TO MOVE THS AROUND
						Console.WriteLine("Please select a valid selection!");
						break;
					}
				}

				Console.WriteLine("Press Enter to return to purchase menu!");
				Console.ReadLine();
				PurchaseMenu purchase = new PurchaseMenu(stock, currentCounter);
			}


		}
	}
}
