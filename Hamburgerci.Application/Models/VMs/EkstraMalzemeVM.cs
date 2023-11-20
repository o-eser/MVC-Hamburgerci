using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.VMs
{
	public class EkstraMalzemeVM
	{
		public int Id { get; set; }
		public string Adi { get; set; }
		public double Fiyati { get; set; }
		public ParaBirimi ParaBirimi { get; set; }
	}
}
