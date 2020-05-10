using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS_8.DAL;
using PS_8.Models;

namespace PS_8
{
        public class EditModel : PageModel
        {
            IProductDB productDB;
            public EditModel(IProductDB _productDB)
            {
                productDB = _productDB;
            }

            [BindProperty]
            public Product product { get; set; }
            private XmlDocument doc = new XmlDocument();
            
            public void OnGet(int id)
            {
                product = productDB.Get(id);
            }
            
            public IActionResult OnPost(int id)
            {
                product.id = id;
                productDB.Update(product);
                return RedirectToPage("Index");
            }
          
        } 
}