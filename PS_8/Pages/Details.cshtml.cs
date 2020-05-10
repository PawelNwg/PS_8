using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS_8.DAL;
using PS_8.Models;

namespace PS_8
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }
        IProductDB productDB;
        public DetailsModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }

        public void OnGet(int id)
        {
            product = productDB.Get(id);
        }
    }
}