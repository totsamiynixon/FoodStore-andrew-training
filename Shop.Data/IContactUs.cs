using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public interface IContactUs
    {
        void NewContactUs(ContactUs contact);
        IEnumerable<ContactUs> GetAll();
        //IEnumerable<Food> GetAll();
        //IEnumerable<Food> GetPreferred(int count);
        //IEnumerable<Food> GetFoodsByCategoryId(int categoryId);
        //IEnumerable<Food> GetFilteredFoods(int id, string searchQuery);
        //IEnumerable<Food> GetFilteredFoods(string searchQuery);
        //Food GetById(int id);
        //void NewFood(Food food);
        //void EditFood(Food food);
    }
}
