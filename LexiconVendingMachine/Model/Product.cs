using System;

namespace LexiconVendingMachine.Model
{
	public abstract class Product: IEquatable<Product>
	{
		// What is the product?
		private string _description;
		public string Description
		{
			get => _description;

			// Don't let the user change the descripion
			protected set
			{
				// There must be a description to know what the product is.
				if(string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("The product must have a description!");

				_description = value;
			}
		}
		
		// How much does the product cost
		private int _price;
		public int Price
		{
			get => _price;

			// Don't let the user change the price
			protected set
			{
				// We do not want to give away money
				if(value < 0)
					throw new ArgumentException("The product cannot have a negative price!");

				_price = value;
			}
		}

		// What happens when the product is used
		private string _usingAction;
		public string UsingAction
		{
			get => _usingAction;

			// Don't let the user change the using action
			protected set
			{
				// There must be a using action to know what happens when
				// the product is used.
				if(string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("The product must ha av using action!");
				}

				_usingAction = value;
			}
		}

		// Get a string with the description and price of the product
		public virtual string Examine()
		{
			return $"{Description,-50} {Price,5} SEK";
		}

		// Make use of the product.
		// Print out what's happening when using it
		public virtual void Use()
		{
			Console.WriteLine(UsingAction);
		}

		/*******************************************************************
		 *                   Implementation of IEquatable                  *
		 *******************************************************************/
		public override bool Equals(object obj)
		{
			//Check for null and compare run-time types.
			if((obj is null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}

			return Equals((Product)obj);
		}

		public virtual bool Equals(Product other)
		{
			return !(other is null)
				&& Description == other.Description
				&& Price == other.Price;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(_price, _description);
		}

		/*******************************************************************/


	}

}
