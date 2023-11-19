using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;

namespace Hamburgerci.Application.Services.Abstract
{
	public interface IMenuService
	{
		Task Create(MenuDTO model);
		Task Update(MenuDTO model);
		Task Delete(int id);
		Task<MenuDTO> GetById(int id);
		Task<List<MenuDTO>?> GetAll();
		Task<List<MenuDTO>?> Search(string searchText);
	}
}
