using SOCAUD.Business.Infraestructure;
using SOCAUD.Data.Core;
using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Business.Core
{

    public interface ISafAuditoriaLogic : IFacadeOperacionCRUD<SAF_AUDITORIA>
    {
        IEnumerable<TcAUDITORIAS> ListarAuditoriasPorPropuesta(int idPropuesta);
    }

    public class SafAuditoriaLogic : ISafAuditoriaLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafAuditoriaData _safAuditoriaData;

        public SafAuditoriaLogic()
        {
            this._uow = new UnitOfWork();
            this._safAuditoriaData = new SafAuditoriaData(_uow);
        }


        public SAF_AUDITORIA Registrar(SAF_AUDITORIA entidad)
        {
            return this._safAuditoriaData.Add(entidad);
        }

        public SAF_AUDITORIA Actualizar(SAF_AUDITORIA entidad)
        {
            return this._safAuditoriaData.Update(entidad);
        }

        public SAF_AUDITORIA BuscarPorId(int id)
        {
            return this._safAuditoriaData.GetById(id);
        }

        public IEnumerable<SAF_AUDITORIA> ListarTodos()
        {
            return this._safAuditoriaData.GetAll();
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safAuditoriaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TcAUDITORIAS> ListarAuditoriasPorPropuesta(int idPropuesta)
        {
            return this._safAuditoriaData.ListarAuditoriasPorPropuesta(idPropuesta);
        }
    }
}
