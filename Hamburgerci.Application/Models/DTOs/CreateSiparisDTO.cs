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
		public Guid Id { get; set; }
		public MenuBoyutu MenuBoyutu { get; set; }
		public int SiparisAdeti { get; set; }
		public double ToplamTutar { get; }

		public ICollection<MenuVM> Menuler { get; set; }
		public ICollection<EkstraMalzemeVM> EkstraMalzemeler { get; set; }
	}
}
