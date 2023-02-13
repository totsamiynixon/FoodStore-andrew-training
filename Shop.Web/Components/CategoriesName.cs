using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Web.Models.Category;
using System.Linq;

namespace Shop.Web.Components
{
    public class CategoriesName: ViewComponent
    {
        private readonly ICategory _categoryService;

        public CategoriesName(ICategory categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke ()
        {
            var categories = _categoryService.GetAll().
               Select(category => new CategoryDropdownModel
               {
                   Name = category.Name,
                   Id = category.Id,
               });

            ViewBag.Categories = new CategoryDropdownIndexModel
            {
                CategoryListName = categories
            };

            return View();
        }
    }
}
