﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VendingMachine.Items;
using VendingMachine;
using System.IO;

namespace VendingMachine.VendingMachine
{
	public class CashCounter
	{
		int quarterCount;
		int dimeCount;
		int nickelCount;

		public CashCounter()
		{
			Balance = 0;
		}

		public decimal Balance { get; set; }

		public decimal Feed(decimal fedCash)
		{
			Balance += fedCash;
			TransactionRecorder("FEED MONEY:", fedCash);

			return Balance;
		}

		public Queue<Item> Charge(Dictionary<string, Stack<Item>> stock, string userInput, Queue<Item> purchases)
		{
			foreach (var kvp in stock)
			{
				if (stock.ContainsKey(userInput))
				{
					if (userInput.Equals(kvp.Key) &&
						kvp.Value.Count > 0 &&
						Balance > kvp.Value.Peek().Cost)
					{
						Thread.Sleep(800);
						Console.WriteLine($"That'll be {kvp.Value.Peek().Cost:c}!");
						Balance -= kvp.Value.Peek().Cost;
						Console.WriteLine();
						Thread.Sleep(1000);
						Console.WriteLine($"{kvp.Value.Peek().Name} at {kvp.Key} dispensed!");
						TransactionRecorder($"{kvp.Value.Peek().Name} {kvp.Key}", -(kvp.Value.Peek().Cost));
						Thread.Sleep(800);
						Console.WriteLine();
						purchases.Enqueue(kvp.Value.Pop());
					}
					else if (userInput.Equals(kvp.Key) &&
						kvp.Value.Count > 0 &&
						Balance < kvp.Value.Peek().Cost)
					{
						Thread.Sleep(800);
						Console.WriteLine("Insufficient funds to buy this item!");
						Console.WriteLine();
						break;
					}
					else if (userInput == kvp.Key)
					{
						Thread.Sleep(800);
						Console.WriteLine("This item is SOLD OUT!!");
						Console.WriteLine();
						break;
					}
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

		/// <summary>
		/// Calculates amount of change in quarters, dimes, and nickels 
		/// </summary>
		/// <returns>An array of the number of each type of coin</returns>
		public int[] GetChange()
		{
			decimal balanceBeforeChange = -(Balance);

			int[] arrayOfChange = new int[3];

			arrayOfChange[0] = (int)(Balance / (decimal).25);
			Balance  %= (decimal).25;
			arrayOfChange[1] = (int)(Balance / (decimal).10);
			Balance %= (decimal).10;
			arrayOfChange[2] = (int)(Balance / (decimal).05);
			Balance %= (decimal).05;

			TransactionRecorder("GIVE CHANGE:", balanceBeforeChange);

			return arrayOfChange;
		}

		/// <summary>
		/// Creates log entry for every change of state within machine
		/// </summary>
		private void TransactionRecorder(string eventType, decimal cashDelta)
		{
			using (StreamWriter recorder = new StreamWriter("log.txt", true))
			{
				recorder.WriteLine(DateTime.Now.ToString() + " "  + eventType.PadRight(25) + " " + $"{Balance - cashDelta:c}".PadRight(10) + " " + $"{Balance:c}");
			}
		}

	}
}
