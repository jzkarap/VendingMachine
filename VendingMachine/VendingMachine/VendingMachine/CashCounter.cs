using System;
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
		int quarter;
		int dime;
		int nickel;

		public CashCounter()
		{
			Balance = 0;
		}

		public decimal Balance { get; set; }

		public decimal Feed(decimal fedCash, CashCounter currentCounter)
		{
			Balance += fedCash;
			TransactionRecorder("FEED MONEY:", fedCash, currentCounter);

			return Balance;
		}

		public Queue<Item> Charge(Dictionary<string, Stack<Item>> stock, string userInput, CashCounter currentCounter)
		{
			Queue<Item> purchases = new Queue<Item>();

			foreach (var kvp in stock)
			{
				if (stock.ContainsKey(userInput))
				{
					if (userInput.Equals(kvp.Key) &&
						kvp.Value.Count > 0 &&
						currentCounter.Balance > kvp.Value.Peek().Cost)
					{
						Thread.Sleep(800);
						Console.WriteLine($"That'll be {kvp.Value.Peek().Cost:c}!");
						currentCounter.Balance -= kvp.Value.Peek().Cost;
						Console.WriteLine();
						Thread.Sleep(1000);
						Console.WriteLine($"{kvp.Value.Peek().Name} at {kvp.Key} dispensed!");
						TransactionRecorder($"{kvp.Value.Peek().Name} {kvp.Key}", -(kvp.Value.Peek().Cost), currentCounter);
						Thread.Sleep(800);
						Console.WriteLine();
						purchases.Enqueue(kvp.Value.Pop());
					}
					else if (userInput.Equals(kvp.Key) &&
						kvp.Value.Count > 0 &&
						currentCounter.Balance < kvp.Value.Peek().Cost)
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

		public string GetChange(CashCounter currentCounter)
		{
			decimal balanceBeforeChange = -(currentCounter.Balance);

			quarter = (int)(Balance / (decimal).25);
			Balance  %= (decimal).25;
			dime = (int)(Balance / (decimal).10);
			Balance %= (decimal).10;
			nickel = (int)(Balance / (decimal).05);
			Balance %= (decimal).05;

			int[] arrayOfChange = { quarter, dime, nickel };

			TransactionRecorder("GIVE CHANGE:", balanceBeforeChange, currentCounter);

			return $"Change returned: {arrayOfChange[0]} quarters, {arrayOfChange[1]} dimes, {arrayOfChange[2]} nickels";
		}


		/// <summary>
		/// Creates log entry for every change of state within machine
		/// </summary>
		public void TransactionRecorder(string eventType, decimal cashDelta, CashCounter currentCounter)
		{
			using (StreamWriter recorder = new StreamWriter("log.txt", true))
			{
				recorder.WriteLine(DateTime.Now.ToString() + " " + eventType + "\t" + $"{currentCounter.Balance - cashDelta:c}" + "\t" + $"{currentCounter.Balance:c}");
			}
		}

	}
}
