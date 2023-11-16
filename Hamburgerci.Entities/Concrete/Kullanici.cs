using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Enum;
using Microsoft.AspNetCore.Identity;

namespace Hamburgerci.Entities.Concrete
{
    public class Kullanici : IdentityUser<int>, IBaseEntity
    {
        public Kullanici()
        {
            CreatedDate = DateTime.Now;
            DataStatus = DataStatus.Inserted;
        }
        public string FirstName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus DataStatus { get; set; }

        public ICollection<Siparis> Siparisler { get; set; }

    }
}
