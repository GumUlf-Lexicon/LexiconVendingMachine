namespace LexiconVendingMachine.Model.Products
{
	class SourCandy: Product
	{

		public SourCandy() : this(16) { }

		public SourCandy(int price)
		{
			Price = price;
			Description = "A bag of really sour candy.";
		}

		public override void Use()
		{
			System.Console.WriteLine("You open the bag and tastes one of the candies. Your face immediately makes an impression of a rasin.");
		}
	}
}
