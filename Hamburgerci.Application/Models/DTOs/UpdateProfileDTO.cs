using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
    public class UpdateProfileDTO
    {
        //Todo: DataAnnotations
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public DataStatus DataStatus { get; set; }
    }
}
