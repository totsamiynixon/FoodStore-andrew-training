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
        ContactUs GetById(int id);
    }
}
