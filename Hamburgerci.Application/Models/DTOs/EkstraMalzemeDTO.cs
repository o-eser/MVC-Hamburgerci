using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
    public class EkstraMalzemeDTO
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public decimal Price { get; set; }
        public ParaBirimi ParaBirimi { get; set; }
    }
}
