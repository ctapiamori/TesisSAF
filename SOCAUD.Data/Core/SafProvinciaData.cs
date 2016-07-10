using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafProvinciaData : IBaseRepository<SAF_PROVINCIA> { }


    public class SafProvinciaData : BaseRepository<SAF_PROVINCIA>, ISafProvinciaData
    {
        public SafProvinciaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
