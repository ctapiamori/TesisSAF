using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafExperienciaData : IBaseRepository<SAF_EXPERIENCIA> { }


    public class SafExperienciaData : BaseRepository<SAF_EXPERIENCIA>, ISafExperienciaData
    {
        public SafExperienciaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
