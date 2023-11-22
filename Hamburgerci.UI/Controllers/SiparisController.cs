﻿using Hamburgerci.Application.Models.DTOs;
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
            var siparisler = new List<UpdateSiparisDTO>();
            vm.Siparisler = siparisler.ToPagedList(pageNumber: page, pageSize: pageSize);

            if (!String.IsNullOrEmpty(searchText))
            {
                siparisler = await _siparisService.Search(searchText);
                vm.Siparisler = siparisler.ToPagedList(page, pageSize);
            }
            vm.CreateSiparis= await _siparisService.CreateSiparis();

            

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SiparisEkle(SiparisListingVM model)
        {
            model.CreateSiparis.EkstraMalzemeSiparisler.AddRange(model.CreateSiparis.EkstraMalzemeler.
                Where(x => x.ParaBirimi != 0).
                Select(x => new EkstraMalzemeSiparisDTO { EkstraMalzemeId = x.Id }));
            return RedirectToAction("Index");
        }

       
    }
}
