using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Hamburgerci.UI.ViewComponents
{
    public class SiparisEkle: ViewComponent
    {
        

		public SiparisEkle(ISiparisService siparisService)
        {
            
            
        }

        

        public IViewComponentResult Invoke()
        {
            
            return View();
        }
    }
}
