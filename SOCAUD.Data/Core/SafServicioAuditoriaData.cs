using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafServicioAuditoriaData : IBaseRepository<SAF_SERVICIOAUDITORIA> { }


    public class SafServicioAuditoriaData : BaseRepository<SAF_SERVICIOAUDITORIA>, ISafServicioAuditoriaData
    {
        public SafServicioAuditoriaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
