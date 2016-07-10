using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface IVwSafSolicitudData : IBaseRepository<VW_SAF_SOLICITUD> { }

    public class VwSafSolicitudData : BaseRepository<VW_SAF_SOLICITUD>, IVwSafSolicitudData
    {
        public VwSafSolicitudData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
