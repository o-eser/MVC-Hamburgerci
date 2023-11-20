using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Application.Services.Abstract;
using Hamburgerci.Application.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Hamburgerci.Application.Models.DTOs;

namespace Hamburgerci.UI.Controllers
{
    public class EkstraMalzemeController : Controller
    {
        private readonly IEkstraMalzemeService _ekstraMalzemeService;

        public EkstraMalzemeController(IEkstraMalzemeService ekstraMalzemeService)
        {
            _ekstraMalzemeService = ekstraMalzemeService;
        }
        public async Task<IActionResult> Index(string searchText,int pageSize=5, int page=1)
        {
			EkstraMalzemeListingVM vm = new EkstraMalzemeListingVM();
            var ekstraMalzemeler = await _ekstraMalzemeService.GetAll();
            vm.EkstraMalzemeler = ekstraMalzemeler.ToPagedList(pageNumber: page, pageSize: pageSize);

            if(!String.IsNullOrEmpty(searchText) ) 
            {
                ekstraMalzemeler = await _ekstraMalzemeService.Search(searchText);
                vm.EkstraMalzemeler = ekstraMalzemeler.ToPagedList(page,pageSize);
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EkstraMalzemeEkle(EkstraMalzemeListingVM model)
        {
            if (ModelState.IsValid)
            {
                await _ekstraMalzemeService.Create(model.EkstraMalzeme);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EkstraMalzemeSil(int id)
        {
            await _ekstraMalzemeService.Delete(id);
            return RedirectToAction("Index");
        }

		[HttpPost]
        public async Task<IActionResult> EkstraMalzemeDuzenle(EkstraMalzemeDTO model)
        {
            if (ModelState.IsValid) 
            {
                await _ekstraMalzemeService.Update(model);
            }
            return RedirectToAction("Index");
        }







	}
}
