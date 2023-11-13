using Hamburgerci.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Entities.Concrete
{
    public class Menu : BaseEntity
    {
        public string MenuAdi { get; set; }

        public double MenuFiyati { get; set; }

        public ParaBirimi ParaBirimi { get; set; }
    }
}
