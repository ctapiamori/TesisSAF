using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafSoaData : IBaseRepository<SAF_SOA> { }

    public class SafSoaData : BaseRepository<SAF_SOA>, ISafSoaData
    {
        public SafSoaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
