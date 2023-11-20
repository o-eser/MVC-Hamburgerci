using Hamburgerci.Application.Models.DTOs;
using Hamburgerci.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Hamburgerci.Application.Models.VMs
{
    public class EkstraMalzemeListingVM
	{
        public EkstraMalzemeListingVM()
        {
            EkstraMalzeme=new EkstraMalzemeDTO();
            EkstraMalzemeler = new List<EkstraMalzemeDTO>().ToPagedList(1,1);
            
        }
        public IPagedList<EkstraMalzemeDTO> EkstraMalzemeler { get; set; }

        public EkstraMalzemeDTO EkstraMalzeme { get; set; }


    }
}
