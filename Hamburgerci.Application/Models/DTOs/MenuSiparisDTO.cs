using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Application.Models.DTOs
{
	public class MenuSiparisDTO
	{
		public int MenuId { get; set; }
		public string? SiparisId { get; set; }
		public int MenuAdeti { get; set; }
        public string MenuAdi { get; set; }
    }
}
