using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafSolCapacitacionData : IBaseRepository<SAF_SOLCAPACITACION> { }

    public class SafSolCapacitacionData : BaseRepository<SAF_SOLCAPACITACION>, ISafSolCapacitacionData
    {
        public SafSolCapacitacionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
