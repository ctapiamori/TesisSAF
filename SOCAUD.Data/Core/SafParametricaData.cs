using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOCAUD.Data.Core
{
    public interface ISafParametricaData : IBaseRepository<SAF_PARAMETRICA> { }

    public class SafParametricaData : BaseRepository<SAF_PARAMETRICA>, ISafParametricaData
    {
        public SafParametricaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
