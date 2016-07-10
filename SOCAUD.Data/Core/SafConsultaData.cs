using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafConsultaData : IBaseRepository<SAF_CONSULTA> { }

    public class SafConsultaData : BaseRepository<SAF_CONSULTA>, ISafConsultaData
    {
        public SafConsultaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
