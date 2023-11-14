using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositoriess.Abstract;
using Hamburgerci.Repositoriess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repositoriess.Concrete
{
    public class SiparisRepository : BaseRepository<Siparis>, ISiparisRepository
    {
        public SiparisRepository(AppDbContext context) : base(context)
        {
        }
    }
}
