using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafSolExperienciaData : IBaseRepository<SAF_SOLEXPERIENCIA> { }

    public class SafSolExperienciaData : BaseRepository<SAF_SOLEXPERIENCIA>, ISafSolExperienciaData
    {
        public SafSolExperienciaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
