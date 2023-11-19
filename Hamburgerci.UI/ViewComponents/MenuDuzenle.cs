using Hamburgerci.Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
	public class MenuDuzenle: ViewComponent
	{
		public IViewComponentResult Invoke(MenuDTO model)
		{
			MenuDTO menu = model;
			return View(menu);
		}
	}
}
