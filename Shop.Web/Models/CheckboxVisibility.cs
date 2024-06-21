﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class CheckboxVisibility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; } = false;
    }
}
