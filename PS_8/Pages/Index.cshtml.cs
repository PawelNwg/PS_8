using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PS_8.DAL;
using PS_8.Models;

namespace PS_8.Pages
{
    public class IndexModel : PageModel
    {
        

        public List<Product> productList;

        IProductDB productDB;

        public IndexModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
       
        public void OnGet()
        {
            productList = productDB.List();

        }
       
    }
}
