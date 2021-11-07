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
			vendingMachine.AddProduct(new Candy("A bag of really sour candy", 18, 200, Candy.Package.Bag, "You open the bag and tastes one of the candies.Your face immediately makes an impression of a rasin."));
			vendingMachine.AddProduct(new Candy("A candy cane", 10, 100));
			vendingMachine.AddProduct(new Beverage("Sparkling cloudberry drink", 33, 12, Beverage.Container.Can, "You open the drink and take a sip of it. You get an energy boost and runs around fast like a rabbit."));
			vendingMachine.AddProduct(new Tool("A swiss army knife", 159, "Red and silver", "You fold out the knife and cuts open a sealed bag of candy."));


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
