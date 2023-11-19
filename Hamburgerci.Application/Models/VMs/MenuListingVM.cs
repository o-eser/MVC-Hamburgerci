using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Application.Models.DTOs;
using X.PagedList;

namespace Hamburgerci.Application.Models.VMs
{
	public class MenuListingVM
	{
        public MenuListingVM()
        {
            Menu=new MenuDTO();
            Menuler=new List<MenuDTO>().ToPagedList(1,1);
        }
        public IPagedList<MenuDTO> Menuler { get; set; }
        public MenuDTO Menu { get; set; }
    }
}
