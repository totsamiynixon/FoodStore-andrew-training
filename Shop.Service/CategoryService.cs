﻿using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void DeleteCategory(int id)
        {
            var currentCategory = GetById(id);
            _context.Remove(currentCategory);
            _context.SaveChanges();

        }
    }
}
