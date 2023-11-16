using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Concrete;
using Hamburgerci.Repositories.Abstract;
using Hamburgerci.Services.Abstract;

namespace Hamburgerci.Services.Concrete
{
    public class EkstraMalzemeManager : BaseManager<EkstraMalzeme>, IEkstraMalzemeService
    {
        public EkstraMalzemeManager(IEkstraMalzemeRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
