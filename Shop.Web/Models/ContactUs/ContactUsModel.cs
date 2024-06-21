using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Shop.Web.Models.ContactUs
{
    public class ContactUsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email is incorrect")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]

        [Required]
        public string Email { get; set; }

        [Required]
        public string Comment { get; set; }

    }
}
