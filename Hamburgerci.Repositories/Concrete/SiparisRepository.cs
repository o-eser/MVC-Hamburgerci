using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Repositories.Concrete
{
    public class SiparisRepository : BaseRepository<Siparis>, ISiparisRepository
    {
        public SiparisRepository(AppDbContext context) : base(context)
        {
        }
    }
}
