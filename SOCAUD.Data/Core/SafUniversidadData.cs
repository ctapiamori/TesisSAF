using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOCAUD.Data.Core
{
    public interface ISafUniversidadData : IBaseRepository<SAF_UNIVERSIDAD> { }

    public class SafUniversidadData : BaseRepository<SAF_UNIVERSIDAD>, ISafUniversidadData
    {
        public SafUniversidadData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
