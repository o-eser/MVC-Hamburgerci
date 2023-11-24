using Hamburgerci.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Application.Models.DTOs
{
    public class AssignedRoleDTO
    {
        public IdentityRole<Guid> Role { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<Kullanici> HasRole { get; set; }
        public IEnumerable<Kullanici> HasNotRole { get; set; }

        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
