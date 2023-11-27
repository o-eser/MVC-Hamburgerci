using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
	public class MenuDTO
	{
		public int Id { get; set; }
		public string MenuAdi { get; set; }
		public double MenuFiyati { get; set; }
	}
}
