using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Application.Models.DTOs
{
    public class LoginDTO
    {
        //Todo: DataAnnotations
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnURL { get; set; }
        //public bool RememberMe { get; set; }
    }
}
