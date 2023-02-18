using Shop.Data;

namespace Shop.Web.Models.ShoppingCart
{
    public class ShoppingCartIndexModel
    {
        public IShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public string ReturnUrl { get; set; }
    }
}
