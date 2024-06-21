using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Web.DataMapper;
using Shop.Web.Models;
using System.Diagnostics;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFood _foodService;
        private readonly Mapper _mapper;

        public HomeController(IFood foodService)
        {
            _foodService = foodService;
            _mapper = new Mapper();
        }
        
        [Route("/")]
        public IActionResult Index()
        {
            var preferedFoods = _foodService.GetPreferred(10);
            var model = _mapper.FoodsToHomeIndexModel(preferedFoods);

            if (User.IsInRole("Admin"))
            {
                return View(model);
            };

                model.FoodsList = model.FoodsList.Where(food => food.IsVisible == true);
            
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery) || string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction("Index");
            }

            var searchedFoods = _foodService.GetFilteredFoods(searchQuery);
            var model = _mapper.FoodsToHomeIndexModel(searchedFoods);

            return View(model);
        }
    }
}
