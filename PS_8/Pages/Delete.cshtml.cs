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
    public class DeleteModel : PageModel
    {

        IProductDB productDB;
        public DeleteModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        public List<Product> productList;

        [BindProperty]
        public int id { get; set; }
       
        public IActionResult OnGet(int id)
        {
            productDB.Delete(id);
            return RedirectToPage("Index");
        }
    }
}