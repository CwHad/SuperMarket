﻿using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using CoreBusiness;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Policy = "Inventory")]
    public class CategoriesController : Controller
    {
		private readonly IViewCategoriesUseCase viewCategoriesUseCase;
		private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;
		private readonly IAddCategoryUseCase addCategoryUseCase;
		private readonly IEditCategoryUseCase editCategoryUseCase;
		private readonly IDeleteCategoryUseCase deleteCategoryUseCase;

		public CategoriesController(
            IViewCategoriesUseCase viewCategoriesUseCase, 
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
            IAddCategoryUseCase addCategoryUseCase,
			IEditCategoryUseCase editCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase
			)
        {
			this.viewCategoriesUseCase = viewCategoriesUseCase;
			this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
			this.addCategoryUseCase = addCategoryUseCase;
			this.editCategoryUseCase = editCategoryUseCase;
			this.deleteCategoryUseCase = deleteCategoryUseCase;
		}
        public IActionResult Index()
        {
            var categories = viewCategoriesUseCase.Execute();

			return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            // var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };
            var category = viewSelectedCategoryUseCase.Execute(id.HasValue ? id.Value : 0);
            return View(category);
        }

        [HttpPost]
		public IActionResult Edit(Category category)
		{
            if (ModelState.IsValid)
            {
				editCategoryUseCase.Execute(category.CategoryId, category);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Action = "edit";
			return View(category);
		}

        public IActionResult Add()
        {
			ViewBag.Action = "add";
			return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid) 
            {
                addCategoryUseCase.Execute(category);
                return RedirectToAction(nameof(Index));
            }

			ViewBag.Action = "add";
			return View(category);
        }

        public IActionResult Delete(int categoryId)
        {
			deleteCategoryUseCase.Execute(categoryId);
            return RedirectToAction(nameof(Index));
        }
	}
}
