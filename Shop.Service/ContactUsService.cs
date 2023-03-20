using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Service
{
    public class ContactUsService : IContactUs
    {
        private readonly ApplicationDbContext _context;
        public ContactUsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void NewContactUs(ContactUs contact)
        {
           
            _context.Add(contact);
            _context.SaveChanges();
        }

        public IEnumerable<ContactUs> GetAll()
        {
            return _context.ContactUs;
        }


    }
}
