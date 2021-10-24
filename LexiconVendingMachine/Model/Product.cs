using System;

namespace LexiconVendingMachine.Model
{
	public abstract class Product: IEquatable<Product>
	{
		private string _description;
		private int _price;

		// What is the product?
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

		// Print out the the description and price to console
		public virtual void Examine()
		{
			Console.WriteLine($"{Description,-50} {Price,5} SEK");
		}

		// Make use of the product.
		// Print out what's happening when using it
		public abstract void Use();

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
