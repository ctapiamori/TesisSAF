using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafBaseData : IBaseRepository<SAF_BASE> { }

    public class SafBaseData : BaseRepository<SAF_BASE>, ISafBaseData
    {
        public SafBaseData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
