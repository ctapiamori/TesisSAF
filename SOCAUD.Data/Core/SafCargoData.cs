using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCargoData : IBaseRepository<SAF_CARGO> { }

    public class SafCargoData : BaseRepository<SAF_CARGO>, ISafCargoData
    {
        public SafCargoData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
