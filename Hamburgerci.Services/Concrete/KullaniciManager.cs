using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;

namespace Hamburgerci.Services.Concrete
{
    public class KullaniciManager : BaseManager<Kullanici>
    {
        public KullaniciManager(IKullaniciRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
