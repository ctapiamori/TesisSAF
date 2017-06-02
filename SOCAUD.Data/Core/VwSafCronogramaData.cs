using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafCronogramaData : IBaseRepository<VW_SAF_CRONOGRAMA> { }

    public class VwSafCronogramaData : BaseRepository<VW_SAF_CRONOGRAMA>, IVwSafCronogramaData
    {
        public VwSafCronogramaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
