using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafPropuestaEquipoDetalleData : IBaseRepository<SAF_PROPEQUIPODETALLE>
    {
        TcASIGNARFECHASPROPUESTA AsignarFechasPropuesta(int idEquipo, string fechas);
        TcELIMINARFECHASASIGPROP EliminarFechasAsignadas(int idPropuesta, string fechas);

        IEnumerable<TcDETALLEEQUIPOPORAUDITORIA> ListarEquipoAuditoria(int idAuditoria);
    }

    public class SafPropuestaEquipoDetalleData : BaseRepository<SAF_PROPEQUIPODETALLE>, ISafPropuestaEquipoDetalleData
    {
        private readonly IUnitOfWork _uow;

        public SafPropuestaEquipoDetalleData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }
        
        public TcASIGNARFECHASPROPUESTA AsignarFechasPropuesta(int idEquipo, string fechas)
        {
            return this._uow.DataContext().SP_SAF_ASIGNARFECHASPROPUESTA(idEquipo, fechas).FirstOrDefault();
        }


        public TcELIMINARFECHASASIGPROP EliminarFechasAsignadas(int idPropuesta, string fechas)
        {
            return this._uow.DataContext().SP_SAF_ELIMINARFECHASASIGPROP(idPropuesta, fechas).FirstOrDefault();
        }


        public IEnumerable<TcDETALLEEQUIPOPORAUDITORIA> ListarEquipoAuditoria(int idAuditoria)
        {
            return this._uow.DataContext().SP_SAF_DETALLEEQUIPOPORAUDITORIA(idAuditoria).ToList();
        }
    }
}
