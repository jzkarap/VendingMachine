using System;
using System.Collections.Generic;
using System.Text;

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

		public decimal Feed(decimal fedCash)
		{
			Balance += fedCash;

			return Balance;
		}

		public decimal Charge(decimal costOfItem)
		{
			if (Balance - costOfItem > 0)
			{
				Balance -= costOfItem;
			}
			else
			{
				Console.WriteLine("Your balance isn't high enough to complete purchase.");
			}
			return Balance;
		}

		public string GetChange()
		{
			quarter = (int)(Balance / (decimal).25);
			Balance  %= (decimal).25;
			dime = (int)(Balance / (decimal).10);
			Balance %= (decimal).10;
			nickel = (int)(Balance / (decimal).05);
			Balance %= (decimal).05;

			int[] arrayOfChange = { quarter, dime, nickel };

			return $"Testing change stuff: {arrayOfChange[0]} quarters, {arrayOfChange[1]} dimes, {arrayOfChange[2]} nickels";
		}

	}
}
