using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface IVwSafPublicacionData : IBaseRepository<VW_SAF_PUBLICACION> { }

    public class VwSafPublicacionData : BaseRepository<VW_SAF_PUBLICACION>, IVwSafPublicacionData
    {
        public VwSafPublicacionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
