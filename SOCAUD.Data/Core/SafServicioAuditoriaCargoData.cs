using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafServicioAuditoriaCargoData : IBaseRepository<SAF_SERAUDCARGO> { }


    public class SafServicioAuditoriaCargoData : BaseRepository<SAF_SERAUDCARGO>, ISafServicioAuditoriaCargoData
    {
        public SafServicioAuditoriaCargoData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }

}
