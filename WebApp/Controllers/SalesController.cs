using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using WebApp.ViewModels;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
	[Authorize(Policy = "Cashiers")]
	public class SalesController : Controller
	{
		private readonly IViewCategoriesUseCase viewCategoriesUseCase;
		private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
		private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;
		private readonly ISellProductUseCase sellProductUseCase;


		public SalesController(
			IViewCategoriesUseCase viewCategoriesUseCase,
			IViewSelectedProductUseCase viewSelectedProductUseCase,
			IViewProductsInCategoryUseCase viewProductsInCategoryUseCase,
			ISellProductUseCase sellProductUseCase
			)
        {
			this.viewCategoriesUseCase = viewCategoriesUseCase;
			this.viewSelectedProductUseCase = viewSelectedProductUseCase;
			this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;
			this.sellProductUseCase = sellProductUseCase;
		}
        public IActionResult Index()
		{
			var salesViewModel = new SalesViewModel
			{
				Categories = viewCategoriesUseCase.Execute()
			};
			return View(salesViewModel);
		}

		public IActionResult SellProductPartial(int productId)
		{
			var product = viewSelectedProductUseCase.Execute(productId);
			return PartialView("_SellProduct", product);
		}
		
		public IActionResult Sell(SalesViewModel salesViewModel)
		{
			if(ModelState.IsValid)
			{
				// Sell the prodcut 
				//var prod = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
				
				sellProductUseCase.Execute(
					"Cashier1",
					salesViewModel.SelectedProductId,
						
					//prod.Price.HasValue ? prod.Price.Value : 0,
					//prod.Quantity.HasValue ? prod.Quantity.Value : 0,
					salesViewModel.QuantityToSell
				);
				//prod.Quantity -= salesViewModel.QuantityToSell;
				//ProductsRepository.UpdateProduct(salesViewModel.SelectedProductId, prod);
			}

			var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
			salesViewModel.SelectedCategoryId = product?.CategoryId ?? 0;
			salesViewModel.Categories = viewCategoriesUseCase.Execute();
			return View("Index", salesViewModel);
		}

		public IActionResult ProductsByCategoryPartial(int categoryId)
		{
			var products = viewProductsInCategoryUseCase.Execute(categoryId);
			return PartialView("_Products", products);
		}

	}
} 
