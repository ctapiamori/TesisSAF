using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafBaseEntregableData : IBaseRepository<SAF_BASEENTREGABLE> { }

    public class SafBaseEntregableData : BaseRepository<SAF_BASEENTREGABLE>, ISafBaseEntregableData
    {
        public SafBaseEntregableData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
