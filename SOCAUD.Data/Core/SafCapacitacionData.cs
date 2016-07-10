using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCapacitacionData : IBaseRepository<SAF_CAPACITACION> { }


    public class SafCapacitacionData : BaseRepository<SAF_CAPACITACION>, ISafCapacitacionData
    {
        public SafCapacitacionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
