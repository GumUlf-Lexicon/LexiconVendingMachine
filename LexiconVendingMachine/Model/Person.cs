using LexiconVendingMachine.Data;
using System;

namespace LexiconVendingMachine.Model
{
	public class Person
	{
		// How much money does the person have
		private int _inWallet;
		public int InWallet
		{

			get => _inWallet;

			set
			{
				if(value < 0)
					throw new ArgumentException("The wallet can not contain a negative amout of money.");

				_inWallet = value;
			}


		}

		// What products does the person have
		private readonly Inventory _inventory = new Inventory();
		public Inventory Inventory { get => _inventory; }

		// A basic person with no money
		public Person() : this(0) { }

		// A person with money
		public Person(int money)
		{
			InWallet = money;
		}

		// Print out the availible funds in the wallet
		public void ShowWallet()
		{
			Console.WriteLine($"You have {InWallet} SEK in your wallet");
		}

		// Print out what products the person owns
		public void ShowInventory()
		{
			// Check if there are anything in the inventory, to not need create arrays
			// if it is empty, and print out appropirate message.
			if(Inventory.Size == 0)
			{
				Console.WriteLine("You do not have anything in your inventory!");
			}
			else
			{
				Product[] products = Inventory.GetAll();

				Console.WriteLine(" You have the following items: ");
				Console.WriteLine();

				foreach(Product product in products)
				{
					Console.WriteLine($"* {product.Description}");
				}
			}
		}

		// Make use of the items in your inventory
		public void UseItems()
		{

			// Check if the user has any items to use. If not, no need to do a lot of extra work
			if(Inventory.Size == 0)
			{
				Console.WriteLine("You do not have any items to use!");
				return;
			}

			// Get an array of all the users items and present them to the user
			Product[] items = Inventory.GetAll();

			Console.WriteLine(" Select from the following items: ");

			for(int i = 0; i < items.Length; i++)
			{
				Product product = items[i];
				Console.Write($"{i + 1,3}. {product.Description}");
				Console.WriteLine();
			}
			Console.WriteLine();

			// Find out what the person wants to do, use a specific item or
			// if they are done using items for now.
			bool validSelection = false;
			int selection = 0;
			while(!validSelection)
			{
				Console.WriteLine();
				Console.Write("Enter selection: ");

				bool validNumber = int.TryParse(Console.ReadLine(), out selection);
				Console.WriteLine();

				if(validNumber && selection > 0 && selection <= items.Length)
				{
					validSelection = true;
				}
				else if(selection > items.Length || selection < 0)
				{
					// Not a selction possible
					Console.WriteLine("That is not a valid selection!");
				}
				else
				{
					// !validNumber
					Console.WriteLine("You have to enter an integer number!");
				}
			}

			// Use the selected item
			items[selection - 1].Use();
			// TODO: Use up items, if applicable 

			Console.WriteLine();
			Console.WriteLine("Press any key to continue");
			_ = Console.ReadKey();
		}

		// Purchase items from the vending machine.
		public void UseVendingMachine(VendingMachine vendingMachine)
		{
			bool isDone = false;
			while(!isDone)
			{
				Console.Clear();
				Console.WriteLine();

				Console.WriteLine("What do you want to do?");
				Console.WriteLine();
				Console.WriteLine(" 1. See vending machine's products");
				Console.WriteLine(" 2. Buy product");
				Console.WriteLine(" 3. See funds in vending machine");
				Console.WriteLine(" 4. Add funds to the vending machine");
				Console.WriteLine(" 5. Receive remaining change");
				Console.WriteLine();
				Console.WriteLine(" 0. Done using vending machine");
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
					else if(selection < 0 || selection > 7)
					{
						Console.WriteLine("Invalid selection!");
					}
					else
					{
						validSelection = true;
					}
					Console.WriteLine();
				}


				Console.Clear();
				Console.WriteLine();

				switch(selection)
				{
					// See vending machine's products
					case 1:
						Console.WriteLine(vendingMachine.ShowAll());
						break;

					// Buy product
					case 2:
						// Try to make purchase of items availible
						if(vendingMachine.Purchase(out Product product))
						{
							// The person successfully bought an item. It is placed
							// in the persons inventory.
							Inventory.AddProduct(product);
						}
						else
						{
							Console.WriteLine("No product was bought!");
						}
						break;

					// See funds in vending machine
					case 3:
						Console.WriteLine($"Available funds in vending machine: {vendingMachine.AvailableFunds} SEK.");
						break;

					// Add funds to the vending machine
					case 4:
						InsertMoneyIntoVendingMachine(vendingMachine);
						break;

					// Receive remaining change
					case 5:
						int[] returnedChange = vendingMachine.EndTransaction();
						int totalChange = 0;
						int change;

						// Adding up and presenting the returned change
						Console.WriteLine();
						Console.Write("Returned change: ");

						for(int i = 0; i < returnedChange.Length - 1; i++)
						{
							change = returnedChange[i];
							totalChange += change;
							Console.Write($"{change} + ");
						}

						change = returnedChange[^1];
						totalChange += change;

						Console.WriteLine($"{change} = {totalChange} (SEK)");

						// Adding the money returned from the vending machine to the persons wallet.
						InWallet += totalChange;

						// Let the person know how much money they have after the
						// transactions with the vending machine.
						Console.WriteLine();
						Console.WriteLine($"You now have {InWallet} SEK in your wallet.");
						break;

					// Done using vending machine
					case 0:
						isDone = true;
						break;

					// ???
					default:
						break;
				}

				if(!isDone)
				{
					Console.WriteLine();
					Console.WriteLine("Press any key to continue");
					_ = Console.ReadKey();
				}
			}
		}

		// Insert money into the vending machine to be used
		// for purchases.
		private void InsertMoneyIntoVendingMachine(VendingMachine vendingMachine)
		{
			bool doneInsertingMoney = false;
			while(!doneInsertingMoney)
			{
				Console.Clear();

				// Let the person know how much money they have and how
				// much they have availible in the vending machine.
				Console.WriteLine();
				Console.WriteLine($"You have {InWallet} SEK in your wallet.");
				Console.WriteLine($"You have inserted {vendingMachine.AvailableFunds} SEK into the vending machine.");


				// Add money tho the vending machine
				Console.WriteLine();
				Console.WriteLine("Enter 0 to stop inserting money.");
				Console.Write("Enter amount to insert into vending machine: ");
				bool validNumber = int.TryParse(Console.ReadLine(), out int moneyToInsert);
				Console.WriteLine();

				if(validNumber && moneyToInsert > 0 && moneyToInsert <= InWallet)
				{
					// The funds are availble in wallet. Try to insert into
					// the vending machine.
					try
					{
						// Not intresed of the amout availible in the vending
						// machine at the moment.
						int availibleFunds = vendingMachine.InsertMoney(moneyToInsert);
						InWallet -= moneyToInsert;
						// The transfer succeeded

						Console.WriteLine($"You have {availibleFunds} SEK availible in the vending machine");
					}
					catch(ArgumentException argEx)
					{
						// The transfer failed. Probably due to wrong denomination of money.
						// Letting the person know, and they can try again.
						Console.WriteLine();
						Console.WriteLine("Money not accepted!");
						Console.WriteLine($"Error message: {argEx.Message}");
						Console.WriteLine();
					}
				}
				else if(validNumber && moneyToInsert == 0)
				{
					// The person does not want to insert more money into the vending machine.
					doneInsertingMoney = true;
				}
				else if(validNumber && moneyToInsert < 0)
				{
					// Person is trying to get money from the vending machine ... That is a no-no.
					Console.WriteLine("You can not insert a negative amout of money!");
				}
				else if(validNumber && moneyToInsert > InWallet)
				{
					// The person does not have the fund requested to insert into the
					// vending machine. 
					Console.WriteLine("You do not have that much money!");
				}
				else
				{
					// validNumber == false
					Console.WriteLine("You have to enter an integer number!");
				}

				if(!doneInsertingMoney)
				{
					Console.WriteLine();
					Console.WriteLine("Press any key to continue");
					_ = Console.ReadKey();
				}
			}

		}
	}
}
