using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
    public class SiparisEkle: ViewComponent
    {
        ISiparisService _siparisService;

        public SiparisEkle(ISiparisService siparisService)
        {
            _siparisService = siparisService;
        }

        public IViewComponentResult Invoke()
        {
            //CreateSiparisDTO createSiparis=await _siparisService.CreatePost();

            return View();
        }
    }
}
