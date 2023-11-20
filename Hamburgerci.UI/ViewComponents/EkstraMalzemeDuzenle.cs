using Hamburgerci.Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
	public class EkstraMalzemeDuzenle : ViewComponent
	{
		public IViewComponentResult Invoke(EkstraMalzemeDTO model)
		{
			EkstraMalzemeDTO ekstraMalzeme = model;
			return View(ekstraMalzeme);
		}


	}
}
