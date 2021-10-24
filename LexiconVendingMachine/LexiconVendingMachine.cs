/*******************************************
 *       Just for proof of concept         *
*******************************************/

using LexiconVendingMachine.Model;
using LexiconVendingMachine.Model.Products;
using System;

namespace LexiconVendingMachine
{
	class LexiconVendingMachine
	{
		static void Main(string[] args)
		{

			// A vending machine with some products is needed
			VendingMachine vendingMachine = new VendingMachine();
			vendingMachine.AddProduct(new SourCandy(15));
			vendingMachine.AddProduct(new SourCandy(15));
			vendingMachine.AddProduct(new SweetFizzyDrink(20));
			vendingMachine.AddProduct(new SwissArmyKnife(150));

			// A user is needed
			Person person = new Person(512);

			
			Console.WriteLine();
			person.UseVendingMachine(vendingMachine);
			
			Console.WriteLine();
			person.UseItems();

			// See what the user owns
			Console.Clear();
			person.ShowWallet();
			Console.WriteLine();
			person.ShowInventory();
			Console.WriteLine();



		}
	}
}
