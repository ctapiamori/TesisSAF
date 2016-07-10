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

    public interface ISafPropuestaEquipoDetalleLogic : IFacadeOperacionCRUD<SAF_PROPEQUIPODETALLE>
    {
        IEnumerable<SAF_PROPEQUIPODETALLE> ListarDetallePorEquipo(int idEquipo);
        TcASIGNARFECHASPROPUESTA AsignarFechasPropuesta(int idEquipo, string fechas);
        TcELIMINARFECHASASIGPROP EliminarFechasAsignadas(int idPropuesta, string fechas);
        IEnumerable<TcDETALLEEQUIPOPORAUDITORIA> ListarEquipoAuditoria(int idAuditoria);
    }

    public class SafPropuestaEquipoDetalleLogic : ISafPropuestaEquipoDetalleLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafPropuestaEquipoDetalleData _safPropuestaEquipoDetalleData;

        public SafPropuestaEquipoDetalleLogic()
        {
            this._uow = new UnitOfWork();
            this._safPropuestaEquipoDetalleData = new SafPropuestaEquipoDetalleData(_uow);
        }


        public SAF_PROPEQUIPODETALLE Registrar(SAF_PROPEQUIPODETALLE entidad)
        {
            return this._safPropuestaEquipoDetalleData.Add(entidad);
        }

        public SAF_PROPEQUIPODETALLE Actualizar(SAF_PROPEQUIPODETALLE entidad)
        {
            return this._safPropuestaEquipoDetalleData.Update(entidad);
        }

        public SAF_PROPEQUIPODETALLE BuscarPorId(int id)
        {
            return this._safPropuestaEquipoDetalleData.GetById(id);
        }

        public IEnumerable<SAF_PROPEQUIPODETALLE> ListarTodos()
        {
            return this._safPropuestaEquipoDetalleData.GetAll();
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safPropuestaEquipoDetalleData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SAF_PROPEQUIPODETALLE> ListarDetallePorEquipo(int idEquipo)
        {
            return this._safPropuestaEquipoDetalleData.GetMany(c => c.CODPROEQU == idEquipo).ToList();
        }


        public TcASIGNARFECHASPROPUESTA AsignarFechasPropuesta(int idEquipo, string fechas)
        {
            return this._safPropuestaEquipoDetalleData.AsignarFechasPropuesta(idEquipo, fechas);
        }


        public TcELIMINARFECHASASIGPROP EliminarFechasAsignadas(int idPropuesta, string fechas)
        {
            return this._safPropuestaEquipoDetalleData.EliminarFechasAsignadas(idPropuesta, fechas);
        }


        public IEnumerable<TcDETALLEEQUIPOPORAUDITORIA> ListarEquipoAuditoria(int idAuditoria)
        {
            return this._safPropuestaEquipoDetalleData.ListarEquipoAuditoria(idAuditoria);
        }
    }
}
