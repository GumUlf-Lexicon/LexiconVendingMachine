namespace LexiconVendingMachine.Model.Products
{
	class SweetFizzyDrink: Product
	{

		public SweetFizzyDrink() : this(32) { }

		public SweetFizzyDrink(int price)
		{
			Price = price;
			Description = "A sweet sparkling drink tasting of cloudberries.";
		}

		public override void Use()
		{
			System.Console.WriteLine("You open the drink and take a sip of it. You get an energy boost and runs around fast like a rabbit.");
		}

	}
}
