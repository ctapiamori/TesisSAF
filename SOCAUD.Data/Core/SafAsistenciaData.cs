using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafAsistenciaData : IBaseRepository<SAF_ASISTENCIA> { }


    public class SafAsistenciaData : BaseRepository<SAF_ASISTENCIA>, ISafAsistenciaData
    {
        public SafAsistenciaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
