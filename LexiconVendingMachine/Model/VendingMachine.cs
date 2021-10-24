using LexiconVendingMachine.Data;
using System;

namespace LexiconVendingMachine.Model
{
	public class VendingMachine: IVending
	{
		// We only accept certain denominations
		private int[] _validDenominations = { 1, 5, 10, 20, 50, 100, 500, 1000 };

		// We need something to sell
		Inventory _inventory = new Inventory();

		// To keep track of how much money has been
		// inserted to the machine by the customer.
		private int _availableFunds = 0;

		public Inventory Inventory { get => _inventory; private set => _inventory = value; }

		public int AvailableFunds { get => _availableFunds; private set => _availableFunds = value; }


		// Create a standard vending machine
		public VendingMachine()
		{
		}

		// Create a vending machine that accepts the specified denominations
		public VendingMachine(int[] validDenominations)
		{
			_validDenominations = validDenominations;
		}


		// Prints out an indexed list with all the availible products
		public Product[] ShowAll()
		{
			Product[] products = Inventory.GetAll();

			Console.WriteLine("  The vending machine contains:");

			for(int i = 0; i < products.Length; i++)
			{
				Product product = products[i];

				Console.Write($"{i + 1,4}: ");
				product.Examine();
			}
			Console.WriteLine();

			return products;
		}

		// Add money to the vending machine to be able to buy products.
		// Returns how munch money is availible to buy for.
		public int InsertMoney(int amount)
		{
			// Only accept valid denominations
			if(!Array.Exists(_validDenominations, value => value == amount))
			{
				throw new ArgumentException("The inserted amount is not a valid denomination.");
			}

			AvailableFunds += amount;

			return AvailableFunds;
		}

		// Buy products from the vending machince.
		// Returns true if a product has been successfully bought
		// and the product is returned in product.
		// endShopping and needMoreMoney are set to false.
		//
		// Returns false if no product is bought.
		// product is set null. 
		// endShopping is set to true if the user is done with the shopping
		// needMoreMoney is set to true if the user want's to insert more
		// money into the vendingMachine
		// At most one of endShopping and needMoreMoney are set to true
		// at the same time.
		public bool Purchase(out Product product, out bool endShopping, out bool needMoreMoney)
		{
			// The product that the user bought
			product = null;

			// To know if the costumer is done shopping
			endShopping = false;

			// To know if the costumer wants to insert more money
			needMoreMoney = false;

			// Check if there is anything to buy, if not no need to continue shopping
			if(Inventory.Size == 0)
			{
				Console.WriteLine();
				Console.WriteLine("The Vending machine is empty!");
				Console.WriteLine();
				endShopping = true;
				return false;
			}


			// Listing all the available products
			Console.WriteLine();
			Product[] products = ShowAll();

			Console.WriteLine($"{"0",4}: Stop shopping");
			Console.WriteLine($"{"-1",4}: Insert more money");
			Console.WriteLine();
			Console.WriteLine($"Available funds: {AvailableFunds} SEK");


			// Get the costumers order. Retry until a valid selection is done
			int orderNo = int.MinValue;
			bool validSelection = false;

			while(!validSelection)
			{
				Console.Write("Enter rownumber for product: ");

				bool validInt = int.TryParse(Console.ReadLine(), out orderNo);
				if(!validInt)
				{
					Console.WriteLine("You must enter a number!");
				}
				else if(orderNo <= -2 || orderNo > products.Length)
				{
					Console.WriteLine("Invalid order number!");
				}
				else
				{
					validSelection = true;
				}
			}


			// Carry out the costumers order
			if(orderNo > 0)
			{
				product = products[orderNo - 1];

				if(product.Price <= AvailableFunds)
				{
					// The costumer can afford the item and it is bought

					AvailableFunds -= product.Price;
					Console.WriteLine($"You bought \"{product.Description}\".");
					return true;
				}
				else
				{
					// The costumer does not have enough money in the machine
					Console.WriteLine("You do not have enough availible funds!");
					product = null;
					return false;
				}
			}
			else if(orderNo == 0)
			{
				// The shopping is done
				endShopping = true;
				return false;
			}
			else if(orderNo == -1)
			{
				// The costumer wants to insert more money into the machine
				needMoreMoney = true;
				return false;
			}
			else
			{
				// Something has gone wrong
				Console.WriteLine("The machine broke down. Try again!");
				return false;
			}
		}

		// The remaining funds the costumer has available in the machine are returned.
		// Returns the amount returned
		public int EndTransaction()
		{
			// Takes out the remaining available funds
			int moneyLeft = AvailableFunds;
			AvailableFunds = 0;

			// Returns the funds to the user
			return moneyLeft;
		}

		// Adds new products to the machine
		public void AddProduct(Product productToAdd)
		{
			if(productToAdd is null)
				throw new ArgumentNullException("You have to supply the product to add!");

			Inventory.AddProduct(productToAdd);
		}

		// Removes old products from the machine
		// Returns true of the product was successfully removed,
		// and false if the product does not exist in the machine.
		public bool RemoveProduct(Product productToRemove)
		{
			if(productToRemove is null)
				throw new ArgumentNullException("You have to specify what product to remove!");

			return Inventory.RemoveProduct(productToRemove);
		}
	}
}
