using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOCAUD.Data.Core
{

    public interface ISafMenuData : IBaseRepository<SAF_MENU> { }
    public class SafMenuData : BaseRepository<SAF_MENU>, ISafMenuData
    {
        public SafMenuData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
