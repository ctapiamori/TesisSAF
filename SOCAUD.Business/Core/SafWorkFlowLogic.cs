using SOCAUD.Business.Infraestructure;
using SOCAUD.Common.Enum;
using SOCAUD.Common.Exceptions;
using SOCAUD.Data.Core;
using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SOCAUD.Business.Core
{
    public interface ISafWorkFlowLogic : IFacadeOperacionCRUD<SAF_WORKFLOW>
    {

    }

    public class SafWorkFlowLogic : ISafWorkFlowLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafWorkFlowData _safWorkFlowData;
        public SafWorkFlowLogic()
        {
            this._uow = new UnitOfWork();
            this._safWorkFlowData = new SafWorkFlowData(this._uow);
        }

        public SAF_WORKFLOW Registrar(SAF_WORKFLOW entidad)
        {
            var result = _safWorkFlowData.Add(entidad);
            return result;
        }

        public SAF_WORKFLOW Actualizar(SAF_WORKFLOW entidad)
        {
            var result = _safWorkFlowData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public SAF_WORKFLOW BuscarPorId(int id)
        {
            var result = _safWorkFlowData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_WORKFLOW> ListarTodos()
        {
            var result = _safWorkFlowData.GetAll();
            return result;
        }
    }
}
