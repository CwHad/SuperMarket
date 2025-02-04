using CoreBusiness;

namespace UseCases.TransactionsUseCases
{
	public interface IGetTodayTransactionsUseCase
	{
		IEnumerable<Transaction> Execute(string cashierName);
	}
}