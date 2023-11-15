using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;

namespace Hamburgerci.Repositories.Concrete
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context)
        {
        }
    }
}
