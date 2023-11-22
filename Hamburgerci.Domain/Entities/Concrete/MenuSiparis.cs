using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Abstract;
using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.Domain.Entities.Concrete
{
	public class MenuSiparis : BaseEntity
	{
        public int MenuId { get; set; }
		public Menu Menu { get; set; }

		public int SiparisId { get; set; }
		public Siparis Siparis { get; set; }

        public int Adet { get; set; }
    }
}
