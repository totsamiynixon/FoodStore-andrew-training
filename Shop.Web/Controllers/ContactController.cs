using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.ContactUs;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactUs _contactUs;
        public ContactController(IContactUs contactUs)
        {
            _contactUs = contactUs;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var comments = _contactUs.GetAll();
            var model = comments.Select(comment => new ContactModel
            {
                Id = comment.Id,
                FirstName = comment.FirstName,
                LastName = comment.LastName,
                Email = comment.Email,
                Comment = comment.Comment,
                CreatedDate = comment.CreatedDate
            });

            return View(model);
        }
    }
}