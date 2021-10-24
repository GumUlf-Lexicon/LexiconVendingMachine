using LexiconVendingMachine.Data;
using Xunit;


namespace LexiconVendingMachine.Tests.Data
{
	public class InventoryShould
	{
		[Fact]
		public void BeEmptyOnCreation()
		{
			// Act

			Inventory sut = new Inventory();

			// Assert

			Assert.Equal(0, sut.Size);
			Assert.Empty(sut.GetAll());


		}
	}
}
