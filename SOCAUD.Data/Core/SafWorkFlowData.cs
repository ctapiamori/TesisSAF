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
        IEnumerable<SP_SAF_WORKFLOW_POR_USU_Result> ListarWorkFlowCabeceraPorUsuario(string usu, string tipo, string fecini, string fecfin);
    }

    public class SafWorkFlowData : BaseRepository<SAF_WORKFLOW>, ISafWorkFlowData
    {
        private readonly IUnitOfWork _uow;

        public SafWorkFlowData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<SP_SAF_WORKFLOW_POR_USU_Result> ListarWorkFlowCabeceraPorUsuario(string usu, string tipo, string fecini, string fecfin)
        {
            return this._uow.DataContext().SP_SAF_WORKFLOW_POR_USU(usu, tipo, fecini, fecfin);
        }
    }
}
