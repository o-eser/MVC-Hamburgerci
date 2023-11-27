using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Application.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Hamburgerci.UI.Controllers
{
	[Authorize]
	public class SiparisController : Controller
	{
		private readonly ISiparisService _siparisService;

		public SiparisController(ISiparisService siparisService)
		{
			_siparisService = siparisService;
		}

		public async Task<IActionResult> Index( int pageSize = 5, int page = 1)
		{
			SiparisListingVM vm = new SiparisListingVM();
			var siparisler = await _siparisService.GetAll();
			vm.Siparisler = siparisler.ToPagedList(pageNumber: page, pageSize: pageSize);

			vm.CreateSiparis = await _siparisService.CreateSiparis();

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> SiparisEkle(SiparisListingVM model)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Lütfen gerekli alanları doldurunuz.");
				return RedirectToAction("Index",model);
			}
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
