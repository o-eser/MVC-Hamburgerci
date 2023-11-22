using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Application.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Hamburgerci.UI.Controllers
{
	public class SiparisController : Controller
	{
		ISiparisService _siparisService;

		public SiparisController(ISiparisService siparisService)
		{
			this._siparisService = siparisService;
		}

		public async Task<IActionResult> Index(string searchText, int pageSize = 5, int page = 1)
		{
			SiparisListingVM vm = new SiparisListingVM();
			var siparisler = await _siparisService.GetAll();
			vm.Siparisler = siparisler.ToPagedList(pageNumber: page, pageSize: pageSize);

			if (!String.IsNullOrEmpty(searchText))
			{
				siparisler = await _siparisService.Search(searchText);
				vm.Siparisler = siparisler.ToPagedList(page, pageSize);
			}
			vm.CreateSiparis = await _siparisService.CreateSiparis();



			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> SiparisEkle(SiparisListingVM model)
		{
			await _siparisService.Create(model.CreateSiparis);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> SiparisSil(int id)
		{
			await _siparisService.Delete(id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> SiparisDuzenle(UpdateSiparisDTO model)
		{
			await _siparisService.Update(model);
			return RedirectToAction("Index");
		}
	}
}
