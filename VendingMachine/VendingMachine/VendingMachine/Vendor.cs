using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VendingMachine.Items;

namespace VendingMachine.VendingMachine
{
    public class Vendor
    {

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
            //foreach (var kvp in stock)
            //{
            //	itemPosition = kvp.Key;
            //	itemQuantity = kvp.Value.Count;
            

            if (stock.ContainsKey(userInput) &&
                stock[userInput].Count > 0)
            {
                decimal itemCost = stock[userInput].Peek().Cost;
                string itemName = stock[userInput].Peek().Name;

                if (currentCounter.Balance >= itemCost)
                {
                    Thread.Sleep(800);
                    Console.WriteLine($"That'll be {itemCost:c}!");
                    currentCounter.Charge(itemName, userInput, itemCost);
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    Console.WriteLine($"{itemName} at {userInput} dispensed!");
                    Thread.Sleep(800);
                    Console.WriteLine();
                    // Can't figure out how to refer to kvp.Value.Pop() without removing the item from the stack...
                    // Is it possible?
                    Item item = stock[userInput].Pop();
                    purchases.Enqueue(item);
                }
                else if (currentCounter.Balance < itemCost)
                {
                    Thread.Sleep(800);
                    Console.WriteLine("Insufficient funds to buy this item!");
                    Console.WriteLine();
                    //break;
                }
            }
            else if (stock[userInput].Count == 0)
            {
                Thread.Sleep(800);
                Console.WriteLine("This item is SOLD OUT!!");
                Console.WriteLine();
                //break;
            }

            else
            {
                Console.WriteLine("Invalid selection!");
                Console.WriteLine();
                //break;
            }
            //}

            return purchases;
        }
    }
}
