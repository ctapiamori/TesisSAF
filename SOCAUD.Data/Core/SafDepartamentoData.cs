using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafDepartamentoData : IBaseRepository<SAF_DEPARTAMENTO> { }


    public class SafDepartamentoData : BaseRepository<SAF_DEPARTAMENTO>, ISafDepartamentoData
    {
        public SafDepartamentoData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
