using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Items;
using VendingMachine.VendingMachine;

namespace VendingMachine.UI
{
	public class Menu
	{
		public Menu(Dictionary<string, Stack<Item>> slots, CashCounter cashCounter, Queue<Item> purchases)
		{
            while (true)
            {
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(Q) Quit");

                string userInput = Console.ReadLine();

                while (userInput != "1" &&
                       userInput != "2")
                {
                    Console.WriteLine("Please select option 1 or option 2!");
                    userInput = Console.ReadLine();
                }
                if (userInput == "1")
                {
                    DisplayItemsMenu items = new DisplayItemsMenu();
                    items.Run(slots, cashCounter, purchases);
                }
                if (userInput == "2")
                {
                    PurchaseMenu purchase = new PurchaseMenu(slots, cashCounter, purchases);
                }
                if (userInput == "Q")
                {
                    break;
                }
            }
		}

	}
}
