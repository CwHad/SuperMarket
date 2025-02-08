using Microsoft.AspNetCore.Mvc;
using UseCases.TransactionsUseCases;
using WebApp.ViewModels;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
	[Authorize]
	public class TransactionsController : Controller
	{
		private readonly ISearchTransactionsUseCase searchTransactionsUseCase;

		public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase)
        {
			this.searchTransactionsUseCase = searchTransactionsUseCase;
		}
        public IActionResult Index()
		{
			TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
			return View(transactionsViewModel);
		}

		public IActionResult Search(TransactionsViewModel transactionsViewModel)
		{
			var transactions = searchTransactionsUseCase.Execute(transactionsViewModel.CashierName??string.Empty,
				transactionsViewModel.StartDate,
				transactionsViewModel.EndDate);

			transactionsViewModel.Transactions = transactions;

			return View("Index", transactionsViewModel);
		}
	}
}
