using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.ShoppingCart;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IFood _foodService;
        private readonly IShoppingCart _shoppingCartService;

        public ShoppingCartController(IFood foodService, IShoppingCart shoppingCartService)
        {
            _foodService = foodService;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index(bool isValidAmount = true, string returnUrl = "/")
        {
            _shoppingCartService.GetShoppingCartItems();

            var model = new ShoppingCartIndexModel
            {
                ShoppingCart = _shoppingCartService,                                             
                ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotal(),
                ReturnUrl = returnUrl
            };

            if (!isValidAmount)
            {
                ViewBag.InvalidAmountText = "*There were not enough items in stock to add*";
            }

            return View("Index", model);
        }

        [HttpGet]
        [Route("/ShoppingCart/Add/{id}/{returnUrl?}")]
        public IActionResult Add(int id, int? amount = 1, string returnUrl=null )
        {
            var food = _foodService.GetById(id);
            returnUrl = returnUrl.Replace("%2F", "/");
            bool isValidAmount = false;
            if (food != null)
            {
                isValidAmount = _shoppingCartService.AddToCart(food, amount.Value);
            }

            return Index(isValidAmount, returnUrl);
        }

        public IActionResult Remove(int foodId)
        {
            var food = _foodService.GetById(foodId);
            if (food != null)
            {
                _shoppingCartService.RemoveFromCart(food);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Back(string returnUrl="/")
        {
            return Redirect(returnUrl);
        }
    }
}