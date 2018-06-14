using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Items
{
    public class Item
    {

		public Item(string name, decimal cost, string type)
		{
			this.Name = name;
			this.Cost = cost;
			this.Type = type;
			//this.Count = 5;
		}

		//public int Count { get; }

		public string Name { get; }

		public string Type { get; }

		public decimal Cost { get; }

		public string Noise()
		{
			string noise = "";

			if (this.Type == "Chip")
			{
				noise = "Crunch, Crunch, Yum!";
			}

			if (this.Type == "Candy")
			{
				noise = "Munch, Munch, Yum!";
			}

			if (this.Type == "Gum")
			{
				noise = "Chew, Chew, Yum!";
			}

			if (this.Type == "Drink")
			{
				noise = "Glug, Glug, Yum!";
			}

			return noise;
		}
    }
}
