using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafServAudCargoExperienciaData : IBaseRepository<SAF_SERAUDCAREXP> { }

    public class SafServAudCargoExperienciaData : BaseRepository<SAF_SERAUDCAREXP>, ISafServAudCargoExperienciaData
    {
        public SafServAudCargoExperienciaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
