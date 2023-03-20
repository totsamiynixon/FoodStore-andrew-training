using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Data.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Comment { get; set; }
        public string CreatedDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        // public bool IsMarried { get; set; }
    }
}
