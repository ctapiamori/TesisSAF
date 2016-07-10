using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafArchivoData : IBaseRepository<SAF_ARCHIVO> { }


    public class SafArchivoData : BaseRepository<SAF_ARCHIVO>, ISafArchivoData
    {
        public SafArchivoData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
