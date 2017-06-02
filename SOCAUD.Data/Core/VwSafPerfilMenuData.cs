using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafPerfilMenuData : IBaseRepository<VW_SAF_PERFILMENU> { }
    public class VwSafPerfilMenuData : BaseRepository<VW_SAF_PERFILMENU>, IVwSafPerfilMenuData
    {
        public VwSafPerfilMenuData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
