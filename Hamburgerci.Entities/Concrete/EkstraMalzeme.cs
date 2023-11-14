using Hamburgerci.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Entities.Concrete
{
    public class EkstraMalzeme : BaseEntity
    {
        public string EkstraMalzemeAdi { get; set; }

        public double EkstraMalzemeFiyati { get; set; }

        public ParaBirimi ParaBirimi { get; set; }
    }
}
