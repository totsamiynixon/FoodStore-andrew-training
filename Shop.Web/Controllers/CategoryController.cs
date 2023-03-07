﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Web.Models.Category;
using Shop.Web.Models.Food;
using Shop.Web.DataMapper;
using System.Linq;
using Shop.Web.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Shop.Web.Controllers
{
    public class CategoryController : Controller
	{
		private readonly ICategory _categoryService;
		private readonly IFood _foodService;
        private readonly Mapper _mapper;

		public CategoryController(ICategory categoryService, IFood foodService)
		{
			_categoryService = categoryService;
			_foodService = foodService;
            _mapper = new Mapper();
		}

		public IActionResult Index()
		{
			var categories = _categoryService.GetAll();
			var model = _mapper.CategoriesToCategoryIndexModel(categories);

			return View(model);
		}

		public IActionResult Topic(int id, string searchQuery, SortState sortOrder)
		{
			var category = _categoryService.GetById(id);
			var foods = _foodService.GetFilteredFoods(id, searchQuery);

			var foodListings = foods.Select(food => new FoodListingModel
			{
				Id = food.Id,
				Name = food.Name,
				InStock = food.InStock,
				Price = food.Price,
				ShortDescription = food.ShortDescription,
				Category = _mapper.FoodToCategoryListing(food),
				ImageUrl = food.ImageUrl,
                IsVisible = food.IsVisible
			});

			if(!User.IsInRole("Admin")) 
			{
                foodListings = foodListings.Where(x => x.IsVisible == true);
            }

			var model = new CategoryTopicModel
			{
				Category = _mapper.CategoryToCategoryListing(category),
				Foods = foodListings
			};

			if (sortOrder == SortState.PriceAsc)
			{
                model.Foods = model.Foods.OrderBy(food => food.Price);
                return View(model);
            }

            if (sortOrder == SortState.PriceDesc)
            {
                model.Foods = model.Foods.OrderByDescending(food => food.Price);
                return View(model);
            }
			if(sortOrder == SortState.None)
			{
				return View(model);
			}

            return View(model);
		}

		[HttpPost]
		public IActionResult Topic(int id, FilterState priceValue)
		{
			var category = _categoryService.GetById(id);
			var foods = _foodService.GetFoodsByCategoryId(id);

            // var form = Request.Form;
            // Попробовать реализовать через Component               to do 

            var minPrice = priceValue.MinPrice;
			var maxPrice = priceValue.MaxPrice;
            
            var foodListings = foods.Select(food => new FoodListingModel
			{
				Id = food.Id,
				Name = food.Name,
				InStock = food.InStock,
				Price = food.Price,
				ShortDescription = food.ShortDescription,
				Category = _mapper.FoodToCategoryListing(food),
				ImageUrl = food.ImageUrl
			});

			var model = new CategoryTopicModel
			{
				Category = _mapper.CategoryToCategoryListing(category),
				Foods = foodListings
			};

			if(Convert.ToBoolean(minPrice))
			{
                model.Foods = model.Foods.Where(x => x.Price <= minPrice);
            }
			if (Convert.ToBoolean(maxPrice))
			{
                model.Foods = model.Foods.Where(x => x.Price >= maxPrice);
            }
			

			return View(model);
		}

		public IActionResult Search(int id, string searchQuery)
		{
			return RedirectToAction("Topic", new { id, searchQuery });
		}

		[Authorize(Roles = "Admin")]
		public IActionResult New()
		{
			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Create Category";
			return View("CreateEdit");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult New(CategoryListingModel model)
		{
			if (ModelState.IsValid)
			{
				var category = _mapper.CategoryListingToModel(model);
				_categoryService.NewCategory(category);
				return RedirectToAction("Topic", new { id = category.Id, searchQuery = "" });
			}

			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Create Category";

			return View("CreateEdit", model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Edit(int id)
		{
            ViewBag.ActionText = "change";
            ViewBag.Action = "Edit";
            ViewBag.CancelAction = "Topic";
            ViewBag.SubmitText = "Save Changes";
            ViewBag.RouteId=id;

			var category = _categoryService.GetById(id);
			if (category != null)
			{
				var model = _mapper.CategoryToCategoryListing(category);
				return View("CreateEdit" ,model);
			}

			return View("CreateEdit");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(CategoryListingModel model)
		{
			if (ModelState.IsValid)
			{
				var category = _mapper.CategoryListingToModel(model);
				_categoryService.EditCategory(category);
				return RedirectToAction("Topic", new { id = category.Id, searchQuery = "" });
			}

            ViewBag.ActionText = "change";
            ViewBag.Action = "Edit";
            ViewBag.CancelAction = "Topic";
            ViewBag.SubmitText = "Save Changes";
            ViewBag.RouteId=model.Id;

			return View("CreateEdit",model);
		}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }
		[HttpPost]
		public IActionResult Search(string searchQuery)
		{
			if(string.IsNullOrWhiteSpace(searchQuery) || string.IsNullOrEmpty(searchQuery))
			{
				return RedirectToAction("Index");
			}

			var searchCategory = _categoryService.GetFilteredCategory(searchQuery);
			var model = _mapper.CategoriesToCategoryIndexModel(searchCategory);

			return View(model);
		}
    }
}
