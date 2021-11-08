namespace LexiconVendingMachine.Model
{
	interface IVending
	{
		string ShowAll();

		int InsertMoney(int money);

		bool Purchase(out Product product);

		int[] EndTransaction();


	}
}
