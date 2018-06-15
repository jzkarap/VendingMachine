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
		public CashCounter()
		{
			Balance = 0;
		}

		public decimal Balance { get; private set; }

		public void Feed(decimal fedCash)
		{
			Balance += fedCash;
			TransactionRecorder("FEED MONEY:", fedCash);
		}

		public void Charge(string itemName, string itemPosition, decimal itemCost)
		{
			if (Balance - itemCost >= 0)
			{
				Balance -= itemCost;
				TransactionRecorder($"{itemName} {itemPosition}", -(itemCost));
			}
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
