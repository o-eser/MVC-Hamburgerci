using Hamburgerci.Services.Abstract;
using Hamburgerci.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.Controllers
{
    public class KullaniciPaneliController : Controller
    {
        private readonly ISiparisService _siparisService;

        public KullaniciPaneliController(ISiparisService siparisService)
        {
            _siparisService = siparisService;
        }

        public IActionResult Index()
        {

            SiparisVM vm1 =new SiparisVM();
            vm1.Siparisler = _siparisService.GetAll().ToList();
            return View(vm1);

        }

        public IActionResult SiparisEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SiparisEkleAsync(SiparisVM model)
        {
            if (ModelState.IsValid)
            {
                await _siparisService.CreateAsync(model.Siparis);
                return RedirectToAction("SiparisIndex");
            }
            return View(model);
        }

        public async Task<IActionResult> SiparisDuzenle(int id)
        {
            SiparisVM vm = new SiparisVM();
            vm.Siparis = await _siparisService.GetByIdAsync(id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult SiparisDuzenle(SiparisVM model)
        {
            if (ModelState.IsValid)
            {
                _siparisService.Update(model.Siparis);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> SiparisSil(int id)
        {
            await _siparisService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
