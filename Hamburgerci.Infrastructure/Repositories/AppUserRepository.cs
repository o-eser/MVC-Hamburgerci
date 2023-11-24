using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Repositories.Context;

namespace Hamburgerci.Repositories.Concrete
{
	public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
	{
		public AppUserRepository(AppDbContext context) : base(context)
		{
		}
	}
}
