using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOCAUD.Data.Core
{

    public interface ISafPefilMenuData : IBaseRepository<SAF_PERFIL_MENU> { }

    public class SafPefilMenuData : BaseRepository<SAF_PERFIL_MENU>, ISafPefilMenuData
    {
        public SafPefilMenuData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
