using System;

namespace LexiconVendingMachine.Model.Products
{
	class Candy: Product
	{

		// The weight of the candybags content in grams.
		private int _weightInG;
		public int WeightInG
		{
			get => _weightInG;

			// Only let the class change the weight
			private set
			{
				if(value <= 0)
				{
					throw new ArgumentException("The candy must weigh more than 0 grams!");
				}

				_weightInG = value;
			}
		}

		private Package _packaging;

		public Package Packaging
		{
			get => _packaging;

			private set => _packaging = value;
		}

		// Minumum constructor for the class
		public Candy(string description, int price, int weightInG) {
			WeightInG = weightInG;
			Description = description;
			Price = price;
			UsingAction = "You eat the candy! Yummy!";
			Packaging = Package.Unknown;
		}

		// Full constructor for the class
		public Candy(string description, int price, int weightInG, Package packaging, string usingAction) :
			this(description, price, weightInG)
		{
			UsingAction = usingAction;
			Packaging = packaging;
		}

		// Get a string with a description and price of the candybag
		public override string Examine()
		{
			return $"{Description + " " + WeightInG + " g",-50} {Price,5} SEK";
		}

		public enum Package
		{
			Unknown,
			None,
			Wrapped,
			Bag,
			Box,
			Other
		}
	}
}
