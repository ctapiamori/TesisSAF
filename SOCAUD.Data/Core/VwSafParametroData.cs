using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafParametroData : IBaseRepository<VW_SAF_PARAMETRICA> { }
    public class VwSafParametroData : BaseRepository<VW_SAF_PARAMETRICA>, IVwSafParametroData
    {
        public VwSafParametroData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
