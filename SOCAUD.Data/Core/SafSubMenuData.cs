using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface ISafSubMenuData : IBaseRepository<SAF_SUBMENU> { }
    public class SafSubMenuData : BaseRepository<SAF_SUBMENU>, ISafSubMenuData
    {
        public SafSubMenuData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
