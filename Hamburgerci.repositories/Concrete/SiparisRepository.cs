using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;

namespace Hamburgerci.Repositories.Concrete
{
    public class SiparisRepository : BaseRepository<Siparis>, ISiparisRepository
    {
        public SiparisRepository(AppDbContext context) : base(context)
        {
        }
    }
}
