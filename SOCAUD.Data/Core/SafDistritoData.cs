using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafDistritoData : IBaseRepository<SAF_DISTRITO> { }


    public class SafDistritoData : BaseRepository<SAF_DISTRITO>, ISafDistritoData
    {
        public SafDistritoData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
