using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.Domain.Entities.Concrete
{
	public class EkstraMalzemeSiparis : BaseEntity
	{
        public int EkstraMalzemeId { get; set; }
		public EkstraMalzeme EkstraMalzeme { get; set; }

        public int SiparisId { get; set; }
		public Siparis Siparis { get; set; }

		public int Adet { get; set; }
    }
}
