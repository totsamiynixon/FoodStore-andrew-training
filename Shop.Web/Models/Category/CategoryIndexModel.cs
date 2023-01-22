using System.Collections.Generic;

namespace Shop.Web.Models.Category
{
    public class CategoryIndexModel
    {
        public IEnumerable<CategoryListingModel> CategoryList { get; set; }
    }
}
