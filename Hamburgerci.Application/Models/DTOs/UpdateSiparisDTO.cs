﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.VMs;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
	public class UpdateSiparisDTO
	{
        public UpdateSiparisDTO()
        {
            MenuSiparis = new List<MenuSiparisDTO>();
            EkstraMalzemeSiparis = new List<EkstraMalzemeSiparisDTO>(); 
        }
        public int Id { get; set; }
		public MenuBoyutu MenuBoyutu { get; set; }
		public int SiparisAdeti { get; set; }
		public double? ToplamTutar { get; set; }

		public List<MenuSiparisDTO> MenuSiparis { get; set; }
		public List<EkstraMalzemeSiparisDTO> EkstraMalzemeSiparis { get; set; }

		public List<MenuVM> Menuler { get; set; }
		public List<EkstraMalzemeVM> EkstraMalzemeler { get; set; }
	}
}
