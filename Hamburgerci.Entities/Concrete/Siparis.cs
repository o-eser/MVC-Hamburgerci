﻿using Hamburgerci.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburgerci.Entities.Concrete
{
    public class Siparis : BaseEntity
    {
        public MenuBoyutu MenuBoyutu { get; set; }
        public int SiparisAdeti { get; set; }
        public double ToplamTutar { get; }

        public ICollection<Menu> Menuler { get; set; }

        public ICollection<EkstraMalzeme> EkstraMalzemeler { get; set; }

    }
}
