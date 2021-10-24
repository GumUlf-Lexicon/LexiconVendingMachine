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
			_inWallet = money;
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

				foreach(Product product in products)
				{
					Console.WriteLine(product.Description);
				}
			}

			Console.WriteLine();
		}

		// Make use of the items in your inventory
		public void UseItems()
		{
			// Main loop, keep going until the person is done using products
			bool doneUsingItems = false;
			while(!doneUsingItems)
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
				Console.WriteLine($"{0,3}. Done using items.");


				// Find out what the person wants to do, use a specific item or
				// if they are done using items for now.
				bool validSelection = false;
				while(!validSelection)
				{
					Console.WriteLine();
					Console.Write("Enter selection: ");

					bool validNumber = int.TryParse(Console.ReadLine(), out int selection);
					Console.WriteLine();

					if(validNumber && selection > 0 && selection <= items.Length)
					{
						// Use the selected item
						validSelection = true;
						items[selection - 1].Use();
						// TODO: Use up items, if applicable 
					}
					else if(validNumber && selection == 0)
					{
						// Done for now. 
						validSelection = true;
						doneUsingItems = true;
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
					Console.WriteLine();
				}
			}
		}

		// Purchase items from the vending machine.
		public void UseVendingMachine(VendingMachine vendingMachine)
		{
			// Present the products availible to buy
			vendingMachine.ShowAll();

			// There has to be money in the machine to be able to but thing
			InsertMoneyIntoVendingMachine(vendingMachine);

			// Buy things until the person is done
			Console.Clear();
			bool endTransaction = false;
			while(!endTransaction)
			{
				// Try to make purchase of items availible
				bool itemBought = vendingMachine.Purchase(out Product product, out endTransaction, out bool needMoreMoney);
				if(itemBought)
				{
					// The person successfully bought an item. It is placed
					// in the persons inventory.
					Inventory.AddProduct(product);
				}
				else if(needMoreMoney)
				{
					// Fill up the availible funds in the machine
					InsertMoneyIntoVendingMachine(vendingMachine);
				}
			}

			// Adding the money returned from the vending machine to the
			// persons wallet.
			InWallet += vendingMachine.EndTransaction();

			// Let the person know how much money they have after the
			// transactions with the vending machine.
			Console.WriteLine();
			Console.WriteLine($"You now have {_inWallet} SEK in your wallet.");
			Console.WriteLine();
		}

		// Insert money into the vending machine to be used
		// for purchases.
		private void InsertMoneyIntoVendingMachine(VendingMachine vendingMachine)
		{
			bool doneInsertingMoney = false;
			while(!doneInsertingMoney)
			{
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

				if(validNumber && moneyToInsert > 0 && moneyToInsert <= InWallet)
				{
					// The funds are availble in wallet. Try to insert into
					// the vending machine.
					try
					{
						// Not intresed of the amout availible in the vending
						// machine at the moment.
						_ = vendingMachine.InsertMoney(moneyToInsert);
						InWallet -= moneyToInsert;
						// The transfer succeeded
					}
					catch(ArgumentException argEx)
					{
						// The transfer failed. Probably due to wrong denomination of money.
						// Letting the person know, and they can try again.
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
				else if(validNumber && moneyToInsert > _inWallet)
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
				Console.WriteLine();
			}

		}
	}
}
