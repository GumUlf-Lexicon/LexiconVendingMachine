using LexiconVendingMachine.Model;
using Xunit;

namespace LexiconVendingMachine.Tests.Model
{
	public class PersonShould
	{
		[Theory]
		[InlineData(10)]
		[InlineData(0)]
		[InlineData(9834923)]
		public void HaveMoneySpecifiedOnCreationInWallet(int moneyOnCreation)
		{
			// Arrange
			Person sut = new Person(moneyOnCreation);

			// Act
			int moneyInWallet = sut.InWallet;

			// Assert
			Assert.Equal(moneyOnCreation, moneyInWallet);

		}

	}
}
