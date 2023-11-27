using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
	public class CreateSiparisDTO
	{
        public CreateSiparisDTO()
        {
            MenuSiparisler = new List<MenuSiparisDTO>();
			EkstraMalzemeSiparisler = new List<EkstraMalzemeSiparisDTO>();
			Menuler = new List<MenuVM>();
			EkstraMalzemeler = new List<EkstraMalzemeVM>();
        }
		public MenuBoyutu MenuBoyutu { get; set; }
		[Required(ErrorMessage = "Lütfen sipariş adetini giriniz.")]
		[Range(1, 1000, ErrorMessage = "Lütfen 1 ile 1000 arasında bir değer giriniz.")]
		public int SiparisAdeti { get; set; }
		public double ToplamTutar { get; }

        public List<MenuSiparisDTO> MenuSiparisler { get; set; } 
		public List<EkstraMalzemeSiparisDTO> EkstraMalzemeSiparisler { get; set; }

        public List<MenuVM> Menuler { get; set; }
		public List<EkstraMalzemeVM> EkstraMalzemeler { get; set; }
	}
}
