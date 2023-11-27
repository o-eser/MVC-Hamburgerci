using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.VMs
{
	public class MenuVM
	{
		public int Id { get; set; }
		public string MenuAdi { get; set; }
		public double Fiyati { get; set; }
		[Range(1, 1000, ErrorMessage = "Lütfen 1 ile 1000 arasında bir değer giriniz.")]
		public int Adet { get; set; }
	}
}
