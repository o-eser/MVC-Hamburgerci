using System;
using System.Collections.Generic;
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
        }
		public MenuBoyutu MenuBoyutu { get; set; }
		public int SiparisAdeti { get; set; }
		public double ToplamTutar { get; }

        public List<MenuSiparisDTO> MenuSiparisler { get; set; } 
		public List<EkstraMalzemeSiparisDTO> EkstraMalzemeSiparisler { get; set; }

        public List<MenuVM> Menuler { get; set; }
		public List<EkstraMalzemeVM> EkstraMalzemeler { get; set; }
	}
}
