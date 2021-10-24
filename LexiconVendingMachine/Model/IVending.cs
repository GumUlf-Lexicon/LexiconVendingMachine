namespace LexiconVendingMachine.Model
{
	interface IVending
	{
		Product[] ShowAll();

		int InsertMoney(int money);

		bool Purchase(out Product product, out bool endTransaction, out bool needMoreMoney);

		int EndTransaction();


	}
}
