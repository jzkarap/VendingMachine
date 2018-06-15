using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VendingMachine.Items;

namespace VendingMachine.VendingMachine
{
	public class Vendor
	{
		string itemName;
		string itemPosition;
		int itemQuantity;
		decimal itemCost;

		/// <summary>
		/// Takes user selection, checks against current balance, dispenses item if conditions are accepted
		/// </summary>
		/// <param name="currentCounter">Current cash</param>
		/// <param name="stock">Current stock of machine</param>
		/// <param name="userInput">Item user has selected</param>
		/// <param name="purchases">The queue where purchases are stored</param>
		/// <returns>Queue of purchases</returns>
		public Queue<Item> Transaction(CashCounter currentCounter, Dictionary<string, Stack<Item>> stock, string userInput, Queue<Item> purchases)
		{
			foreach (var kvp in stock)
			{
				itemPosition = kvp.Key;
				itemQuantity = kvp.Value.Count;

				if (stock.ContainsKey(userInput) &&
					itemQuantity > 0)
				{
					itemCost = kvp.Value.Peek().Cost;
					itemName = kvp.Value.Peek().Name;

					if (userInput.Equals(itemPosition) &&
						currentCounter.Balance >= itemCost)
					{
						Thread.Sleep(800);
						Console.WriteLine($"That'll be {itemCost:c}!");
						currentCounter.Charge(itemName, itemPosition, itemCost);
						Console.WriteLine();
						Thread.Sleep(1000);
						Console.WriteLine($"{itemName} at {itemPosition} dispensed!");
						Thread.Sleep(800);
						Console.WriteLine();
						// Can't figure out how to refer to kvp.Value.Pop() without removing the item from the stack...
						// Is it possible?
						purchases.Enqueue(kvp.Value.Pop());
					}
					else if (userInput.Equals(itemPosition) &&
						currentCounter.Balance < itemCost)
					{
						Thread.Sleep(800);
						Console.WriteLine("Insufficient funds to buy this item!");
						Console.WriteLine();
						break;
					}
				}

				else if (userInput == itemPosition &&
						itemQuantity < 0)
				{
					Thread.Sleep(800);
					Console.WriteLine("This item is SOLD OUT!!");
					Console.WriteLine();
					break;
				}

				else
				{
					Console.WriteLine("Invalid selection!");
					Console.WriteLine();
					break;
				}
			}

			return purchases;
		}
	}
}
