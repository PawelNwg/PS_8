using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS_8.Models
{
    public class SiteUser
    {
        [Display(Name = "Nazwa użytkownika")]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

}
