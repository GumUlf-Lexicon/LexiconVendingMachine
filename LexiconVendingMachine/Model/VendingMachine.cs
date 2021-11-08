using LexiconVendingMachine.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconVendingMachine.Model
{
	public class VendingMachine: Inventory, IVending
	{
		// We only accept certain denominations
		private readonly int[] _validDenominations;

		// To keep track of how much money has been
		// inserted to the machine by the customer.
		private int _availableFunds = 0;
		public int AvailableFunds { get => _availableFunds; private set => _availableFunds = value; }


		// Create a standard vending machine
		public VendingMachine() : this(new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 }) { }

		// Create a vending machine that accepts the specified denominations
		public VendingMachine(int[] validDenominations)
		{
			_validDenominations = validDenominations;
		}


		// Returns a string containing a  list with all the availible products
		public string ShowAll()
		{
			Product[] products = GetAll();

			StringBuilder returnString = new StringBuilder();
			returnString.AppendLine("  The vending machine contains:");
			Console.WriteLine();

			foreach(Product product in products)
			{
				returnString.AppendLine(product.Examine());
			}
			return returnString.ToString();
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
		//
		// Returns false if no product is bought.
		// product is set null. 
		public bool Purchase(out Product product)
		{
			// The product that the user bought
			product = null;

			// Check if there is anything to buy, if not no need to continue shopping
			if(Size == 0)
			{
				Console.WriteLine();
				Console.WriteLine("The Vending machine is empty!");
				Console.WriteLine();
				return false;
			}

			Product[] products = GetAll();
			// Listing all the available products
			Console.WriteLine();
			for(int row = 0; row < products.Length; row++)
			{
				Console.WriteLine($"{row + 1,4}: {products[row].Examine()}");
			}
			Console.WriteLine();
			Console.WriteLine($"Available funds: {AvailableFunds} SEK");
			Console.WriteLine();

			// Get the costumers order. Retry until a valid selection is done
			int orderNo = 0;
			bool validSelection = false;

			while(!validSelection)
			{
				Console.Write("Enter rownumber for product: ");

				bool validInt = int.TryParse(Console.ReadLine(), out orderNo);
				if(!validInt)
				{
					Console.WriteLine("You must enter a number!");
				}
				else if(orderNo <= 0 || orderNo > Size)
				{
					Console.WriteLine("Invalid order number!");
				}
				else
				{
					validSelection = true;
				}
				Console.WriteLine();
			}


			// Carry out the costumers order

			product = products[orderNo - 1];

			if(product.Price <= AvailableFunds)
			{
				// The costumer can afford the item and it is bought

				AvailableFunds -= product.Price;
				Console.WriteLine($"You bought \"{product.Description}\".");
				Console.WriteLine();
				return true;
			}
			else
			{
				// The costumer does not have enough money in the machine
				Console.WriteLine("You do not have enough availible funds!");
				Console.WriteLine();
				product = null;
				return false;
			}

		}

		// The remaining funds the costumer has available in the machine are returned.
		// Returns an array with change
		public int[] EndTransaction()
		{
			List<int> changeToReturn = new List<int>();

			// We need a to have the valid denominations sorted to be
			// able to go thrugh the denominations from the largest
			// to the smallest.
			Array.Sort(_validDenominations);


			// Creates the change to the user
			// Just add as high denomniation as possible each time. 
			// (If there are no denomninations of value 1, there might be
			// funds that can't be returned. Look in to that if needed.)
			int denomPos = _validDenominations.Length - 1;
			while(denomPos >= 0 && AvailableFunds > 0)
			{
				if(AvailableFunds >= _validDenominations[denomPos])
				{
					changeToReturn.Add(_validDenominations[denomPos]);
					AvailableFunds -= _validDenominations[denomPos];
				}
				else
				{
					denomPos--;
				}

			}

			// Returns the funds to the user
			return changeToReturn.ToArray();
		}
	}
}
