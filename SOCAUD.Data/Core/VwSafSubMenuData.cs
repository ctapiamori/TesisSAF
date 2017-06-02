using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafSubMenuData : IBaseRepository<VW_SAF_SUBMENU> { }

    public class VwSafSubMenuData : BaseRepository<VW_SAF_SUBMENU>, IVwSafSubMenuData
    {
        public VwSafSubMenuData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
