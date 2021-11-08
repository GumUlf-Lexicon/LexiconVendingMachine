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


			bool endProgram = false;

			while(!endProgram)
			{
				Console.Clear();
				Console.WriteLine();
				Console.WriteLine("What do you want to do?");

				Console.WriteLine();
				Console.WriteLine(" 1. See funds i wallet");
				Console.WriteLine(" 2. Use vending machine");
				Console.WriteLine(" 3. See your inventory ");
				Console.WriteLine(" 4. Use your products");
				Console.WriteLine();
				Console.WriteLine(" 0. End program");
				Console.WriteLine();


				// Get selection from user. Retry until a valid selection is made
				int selection = 0;
				bool validSelection = false;

				while(!validSelection)
				{
					Console.Write("Enter selection: ");

					bool validInt = int.TryParse(Console.ReadLine(), out selection);
					if(!validInt)
					{
						Console.WriteLine("You must enter a number!");
					}
					else if(selection < 0 || selection > 4)
					{
						Console.WriteLine("Invalid selection!");
					}
					else
					{
						validSelection = true;
					}
					Console.WriteLine();
				}

				bool waitForKey = true;
				switch(selection)
				{
					case 1:
						person.ShowWallet();
						break;

					case 2:
						person.UseVendingMachine(vendingMachine);
						waitForKey = false;
						break;

					case 3:
						person.ShowInventory();
						break;

					case 4:
						person.UseItems();
						break;

					case 0:
						endProgram = true;
						waitForKey = false;
						break;

					default:
						break;
				}

				if(waitForKey)
				{
					Console.WriteLine();
					Console.WriteLine("Press any key to continue");
					_ = Console.ReadKey();
				}
			}
		}
	}
}
