using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Shop.Web.Models
{
    public class FilterState
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
