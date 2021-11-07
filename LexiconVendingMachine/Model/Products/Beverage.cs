using System;

namespace LexiconVendingMachine.Model.Products
{
	class Beverage: Product
	{
		// How much beverage is there (in centiliter)?
		private int _volumeInCl;
		public int VolumeInCl
		{
			get => _volumeInCl;

			private set
			{
				if(value <= 0)
				{
					throw new ArgumentException("There must be at least something to drink!");
				}
				_volumeInCl = value;
			}
		}

		// How is the beverage contained?
		private Container _packaging;
		public Container Packaging
		{
			get => _packaging;

			private set => _packaging = value;
		}

		// Minumal Beverage constructor
		public Beverage(string description, int price, int volume)
		{
			Description = description;
			Price = price;
			UsingAction = "You drink the drink!";
			VolumeInCl = volume;
			Packaging = Container.Unkown;
		}

		// Full Beverage constuctor
		public Beverage(string description, int price, int volume, Container packaging, string usingAction) :
			this(description, price, volume)
		{
			UsingAction = usingAction;
			Packaging = packaging;
		}

		// Return a string that describes the beverage and its price.
		public override string Examine()
		{
			return $"{$"{Description} {VolumeInCl} cl",-50} {Price,5} SEK";
		}

		// Possible ways to contain the Beverage
		public enum Container
		{
			Unkown,
			GlassBottle,
			PlasticBottle,
			Can,
			Carton,
			BagInBox,
			PlasticBag,
			PaperCup,
			CeramicCup,
			DrinkingGlass,
			Keg,
			Other
		}

	}

}
