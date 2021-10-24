namespace LexiconVendingMachine.Model.Products
{
	class SwissArmyKnife: Product
	{

		public SwissArmyKnife(): this(254) { }

		public SwissArmyKnife(int price)
		{
			Price = price;
			Description = "A swiss army knife.";
		}

		public override void Use()
		{
			System.Console.WriteLine("You fold out the knife and cuts open a sealed bag of candy.");
		}
	}

}
