using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburgerci.Entities.Concrete;

namespace Hamburgerci.Entities.Abstract
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public Kullanici CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Kullanici ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Kullanici DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
