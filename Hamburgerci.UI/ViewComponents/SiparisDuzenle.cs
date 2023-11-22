using Hamburgerci.Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
	public class SiparisDuzenle:ViewComponent
	{

		public IViewComponentResult Invoke(UpdateSiparisDTO model)
		{
			return View(model);
		}
	}
}
