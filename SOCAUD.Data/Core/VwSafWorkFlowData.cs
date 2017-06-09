using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafWorkFlowData : IBaseRepository<VW_SAF_WORKFLOW> { }
    public class VwSafWorkFlowData : BaseRepository<VW_SAF_WORKFLOW>, IVwSafWorkFlowData
    {
        public VwSafWorkFlowData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
