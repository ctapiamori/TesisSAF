using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafUsuarioData : IBaseRepository<VW_SAF_USUARIOS> { }
    public class VwSafUsuarioData : BaseRepository<VW_SAF_USUARIOS>, IVwSafUsuarioData
    {
        public VwSafUsuarioData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
