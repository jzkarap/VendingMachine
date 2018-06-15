using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VendingMachine.Items
{
    public class Item
    {

		public Item(string name, decimal cost, string type)
		{
			this.Name = name;
			this.Cost = cost;
			this.Type = type;
		}

		public string Name { get; }

		public string Type { get; }

		public decimal Cost { get; }

		public string Noise()
		{
			string noise = "";

			if (this.Type == "Chip")
			{
				noise = "Crunch, Crunch, Yum!\n";
			}

			if (this.Type == "Candy")
			{
				noise = "Munch, Munch, Yum!\n";
			}

			if (this.Type == "Gum")
			{
				noise = "Chew, Chew, Yum!\n";
			}

			if (this.Type == "Drink")
			{
				noise = "Glug, Glug, Yum!\n";
			}

			return noise;
		}
    }
}
