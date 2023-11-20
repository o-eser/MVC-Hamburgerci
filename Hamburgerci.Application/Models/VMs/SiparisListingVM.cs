using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using X.PagedList;

namespace Hamburgerci.Application.Models.VMs
{
    public class SiparisListingVM
    {
        public SiparisListingVM()
        {
            
            Siparisler=new List<UpdateSiparisDTO>().ToPagedList(1, 1); ;
        }
        public IPagedList<UpdateSiparisDTO> Siparisler { get; set; }
    }
}
