using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;

namespace Hamburgerci.Repositories.Concrete
{
    public class EkstraMalzemeRepository : BaseRepository<EkstraMalzeme>, IEkstraMalzemeRepository
    {
        public EkstraMalzemeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
