using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PS_8.Models;

namespace PS_8
{
    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool ValidateUser(SiteUser user)
        {
            if ((user.userName == "admin") && (user.password == "abc"))
                return true;
            else
                return false;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.Name, user.userName)
            };
                           var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
               ClaimsPrincipal(claimsIdentity));
                return Redirect(returnUrl);
            }
            return Page();
        }
        public void OnGet()
        {

        }
    }
}