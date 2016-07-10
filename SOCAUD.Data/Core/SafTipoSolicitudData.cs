using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafTipoSolicitudData : IBaseRepository<SAF_TIPOSOLICITUD> { }

    public class SafTipoSolicitudData : BaseRepository<SAF_TIPOSOLICITUD>, ISafTipoSolicitudData
    {
        public SafTipoSolicitudData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
