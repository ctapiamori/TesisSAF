using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface ISafTipoParametricaData : IBaseRepository<SAF_TIPOPARAMETRICA> { }
    public class SafTipoParametricaData : BaseRepository<SAF_TIPOPARAMETRICA>, ISafTipoParametricaData
    {
        public SafTipoParametricaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
