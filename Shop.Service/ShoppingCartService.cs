using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Data.Models
{
    public class ShoppingCartService : IShoppingCart
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;

        public ShoppingCartService(ApplicationDbContext context, string userId)
        {
            _context = context;
            _userId = userId;
        }

        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        public bool AddToCart(Food food, int amount)
        {
            if (food.InStock == 0 || amount == 0)
            {
                return false;
            }

            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Food.Id == food.Id && s.ShoppingCartId == _userId);
            var isValidAmount = true;

            if (shoppingCartItem == null)
            {
                if (amount > food.InStock)
                {
                    isValidAmount = false;
                }
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = _userId,
                    Food = food,
                    Amount = Math.Min(food.InStock, amount)
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                if (food.InStock - shoppingCartItem.Amount - amount >= 0)
                {
                    shoppingCartItem.Amount += amount;
                }
                else
                {
                    shoppingCartItem.Amount += (food.InStock - shoppingCartItem.Amount);
                    isValidAmount = false;
                }
            }

            _context.SaveChanges();
            return isValidAmount;
        }

        public int RemoveFromCart(Food food)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Food.Id == food.Id && s.ShoppingCartId == _userId);
            int localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();

            return localAmount;
        }

        public IEnumerable<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == _userId)
                       .Include(s => s.Food));
        }

        public void ClearCart()
        {
            var cartItems = _context
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == _userId);

            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public void ClearCartItems()
        {
            var shoppingCartItemForDelete = _context
                .ShoppingCartItems
                .Where(item => item.Food.IsVisible == false);

            _context.ShoppingCartItems.Except(shoppingCartItemForDelete);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == _userId)
                .Select(c => c.Food.Price * c.Amount).ToList().Sum();
        }
    }
}
