using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafNotificacionData : IBaseRepository<SAF_NOTIFICACION> { }

    public class SafNotificacionData : BaseRepository<SAF_NOTIFICACION>, ISafNotificacionData
    {
        public SafNotificacionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
