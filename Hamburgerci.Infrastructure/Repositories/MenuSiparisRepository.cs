using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Domain.Entities.Concrete;
using Hamburgerci.Domain.Repositories;
using Hamburgerci.Repositories.Concrete;
using Hamburgerci.Repositories.Context;

namespace Hamburgerci.Infrastructure.Repositories
{
	public class MenuSiparisRepository : BaseRepository<MenuSiparis>, IMenuSiparisRepository
	{
		public MenuSiparisRepository(AppDbContext context) : base(context)
		{
		}
	}
}
