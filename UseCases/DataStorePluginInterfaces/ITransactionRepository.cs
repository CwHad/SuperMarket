using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
	public interface ITransactionRepository
	{
		void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty);
		IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date);
		IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate);
	}
}