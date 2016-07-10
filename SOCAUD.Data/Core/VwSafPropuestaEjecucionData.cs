using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface IVwSafPropuestaEjecucionData : IBaseRepository<VW_SAF_PROPUESTAEJECUCION> { }

    public class VwSafPropuestaEjecucionData : BaseRepository<VW_SAF_PROPUESTAEJECUCION>, IVwSafPropuestaEjecucionData
    {
        public VwSafPropuestaEjecucionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
