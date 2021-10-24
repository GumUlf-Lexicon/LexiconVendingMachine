using LexiconVendingMachine.Model;
using Xunit;

namespace LexiconVendingMachine.Tests.Model
{
	public class VendingMachineShould
	{
		[Fact]
		public void ShouldHaveNoProductsOnCreation()
		{
			// Arrange
			VendingMachine sut = new VendingMachine();

			// Act
			int inventorySize = sut.Inventory.Size;

			// Assert
			Assert.Equal(0, inventorySize);
		}

		[Fact]
		public void ShouldHaveNoMoneyOnCreation()
		{
			// Arrange
			VendingMachine sut = new VendingMachine();

			// Act
			int availableFunds = sut.AvailableFunds;

			// Assert
			Assert.Equal(0, availableFunds);
		}

	}
}
