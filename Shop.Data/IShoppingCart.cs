using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public interface IShoppingCart
    {
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
        public bool AddToCart(Food food, int amount);
        public int RemoveFromCart(Food food);
        public IEnumerable<ShoppingCartItem> GetShoppingCartItems();
        public void ClearCart();
        public decimal GetShoppingCartTotal();
    }
}