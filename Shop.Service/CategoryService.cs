using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shop.Service
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

		public void EditCategory(Category category)
        {
            _context.Update(category);

            if (!category.IsVisible)
            {
                //var shoppingCartItemToDelete = _context.ShoppingCartItems.Where(
                //    item => category.Foods.Any(food => food.Id == item.FoodId));

                var list = _context.Foods.Where(food => food.CategoryId == category.Id);
                var DelList = _context.ShoppingCartItems.Where(item => list.Any(food => food.Id == item.FoodId));

                _context.ShoppingCartItems.RemoveRange(DelList);
            }
            
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return GetIncluding(); 
        }

        private IQueryable<Category> GetIncluding()
        {
            return _context.Categories.Include(c => c.Foods);
        }

        public Category GetById(int id)
        {
            return GetIncluding().FirstOrDefault(category => category.Id == id);
        }

        public void NewCategory(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetFilteredCategory(string searchQuery)
        {
            var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().Split(" ");
            if(queries == null)
            {
                return GetAll();
            }

            return GetAll().Where(item => queries.Any(query => (item.Name.ToLower().Contains(query.ToLower()))));
        }

        public void DeleteCategory(int id)
        {
            var currentCategory = GetById(id);
            _context.Remove(currentCategory);
            _context.SaveChanges();

        }
    }
}
