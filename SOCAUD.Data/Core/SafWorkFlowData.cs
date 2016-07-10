using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafWorkFlowData : IBaseRepository<SAF_WORKFLOW>
    {

    }

    public class SafWorkFlowData : BaseRepository<SAF_WORKFLOW>, ISafWorkFlowData
    {
        public SafWorkFlowData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
