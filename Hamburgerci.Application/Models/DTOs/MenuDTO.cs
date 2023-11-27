using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
	public class MenuDTO
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Lütfen menü adını giriniz.")]
		public string MenuAdi { get; set; }
		[Range(0, 1000, ErrorMessage = "Lütfen 0 ile 1000 arasında bir değer giriniz.")]
		public double MenuFiyati { get; set; }
	}
}
