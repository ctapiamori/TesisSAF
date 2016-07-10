using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCarreraData : IBaseRepository<SAF_CARRERA> { }

    public class SafCarreraData : BaseRepository<SAF_CARRERA>, ISafCarreraData
    {
        public SafCarreraData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
