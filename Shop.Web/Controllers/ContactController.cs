using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Web.DataMapper;
using Shop.Data.Models;
using Shop.Web.Models.ContactUs;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactUs _contactUs;
        private readonly Mapper _mapper;

        public ContactController(IContactUs contactUs)
        {
            _contactUs = contactUs;
            _mapper = new Mapper();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ContactList()
        {
            var comments = _contactUs.GetAll();
            var model = _mapper.ContactUsToConatctModel(comments);

            return View(model);
        }       

        [HttpPost]
        public IActionResult ContactForm(ContactUs contactModel)
        {
            _contactUs.NewContactUs(contactModel);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [Route("Contact/DetailComment/{id}")]
        public IActionResult DetailComment(int id)
        {
            var comment = _contactUs.GetById(id);
            var model = _mapper.ContactUsToContactModel(comment);

            return View(model);
        }
    }
}