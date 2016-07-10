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
    public interface ISafPropuestaEquipoLogic : IFacadeOperacionCRUD<SAF_PROPEQUIPO>
    {
        IEnumerable<TcEQUIPOAUDITORIA> ListarEquipoAuditoria(int idAuditoria);
        IEnumerable<TcEQUIPOAUDITORIARPT> ListarEquipoAuditoriaRpt(int idPropuesta);
    }

    public class SafPropuestaEquipoLogic : ISafPropuestaEquipoLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafPropuestaEquipoData _safPropuestaEquipoData;

        public SafPropuestaEquipoLogic()
        {
            this._uow = new UnitOfWork();
            this._safPropuestaEquipoData = new SafPropuestaEquipoData(_uow);
        }


        public SAF_PROPEQUIPO Registrar(SAF_PROPEQUIPO entidad)
        {
            return this._safPropuestaEquipoData.Add(entidad);
        }

        public SAF_PROPEQUIPO Actualizar(SAF_PROPEQUIPO entidad)
        {
            return this._safPropuestaEquipoData.Update(entidad);
        }

        public SAF_PROPEQUIPO BuscarPorId(int id)
        {
            return this._safPropuestaEquipoData.GetById(id);
        }

        public IEnumerable<SAF_PROPEQUIPO> ListarTodos()
        {
            return this._safPropuestaEquipoData.GetAll();
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safPropuestaEquipoData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TcEQUIPOAUDITORIA> ListarEquipoAuditoria(int idAuditoria)
        {
            return this._safPropuestaEquipoData.ListarEquipoAuditoria(idAuditoria);
        }


        public IEnumerable<TcEQUIPOAUDITORIARPT> ListarEquipoAuditoriaRpt(int idPropuesta)
        {
            return this._safPropuestaEquipoData.ListarEquipoAuditoriaRpt(idPropuesta);
        }
    }
}
