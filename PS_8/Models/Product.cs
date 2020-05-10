using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS_8.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nazwa produktu")]
        public string name { get; set; }
        [Display(Name = "Cena")]
        public decimal price { get; set; }

    }
}
