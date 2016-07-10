using SOCAUD.Business.Infraestructure;
using SOCAUD.Common.Enum;
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
    public interface ISafInvitacionLogic : IFacadeOperacionCRUD<SAF_INVITACION>
    {
        IEnumerable<TcINVITACION> ListarInvitacionesPublicacion(int? idPublicacion, int? idServicioAuditoria);
        IEnumerable<TcINVITACION> ListarInvitacionesPublicacion(int? idPublicacion, int? idServicioAuditoria, int idAuditor);
        IEnumerable<TcINVITACION> ListarInvitacionesPublicacionSoa(int? idPublicacion, int? idServicioAuditoria, int idSoa);
        TcACEPTARINVITACION AceptarInvitacion(int idInvitacion);
        TcINVITARAUDITORES InvitarAuditores(int idSoa, int idPublicacion, int idServicioAud, string idAuditorCargo);
        TcELIMINARFECHASASIGINVITACION EliminarFechasInvitacion(int idInvitacion, string fechasAgendadas);
        TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas);
    }

    public class SafInvitacionLogic : ISafInvitacionLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafInvitacionData _safInvitacionData;
        public SafInvitacionLogic()
        {
            this._uow = new UnitOfWork();
            this._safInvitacionData = new SafInvitacionData(_uow);
        }

        public SAF_INVITACION Registrar(SAF_INVITACION entidad)
        {
            return this._safInvitacionData.Add(entidad);
        }

        public SAF_INVITACION Actualizar(SAF_INVITACION entidad)
        {
            return this._safInvitacionData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try { this._safInvitacionData.Delete(id); return true; }
            catch (Exception) { return false; }
        }

        public SAF_INVITACION BuscarPorId(int id)
        {
            return this._safInvitacionData.GetById(id);
        }

        public IEnumerable<SAF_INVITACION> ListarTodos()
        {
            return this._safInvitacionData.GetAll();
        }

        public IEnumerable<TcINVITACION> ListarInvitacionesPublicacion(int? idPublicacion, int? idServicioAuditoria)
        {
            var invitaciones = this._safInvitacionData.ListarInvitacionesPublicacion(idPublicacion, idServicioAuditoria);
            return invitaciones;
        }

        public IEnumerable<TcINVITACION> ListarInvitacionesPublicacion(int? idPublicacion, int? idServicioAuditoria, int idAuditor)
        {
            var invitaciones = this._safInvitacionData.ListarInvitacionesPublicacion(idPublicacion, idServicioAuditoria);
            return invitaciones.Where(c => c.CODAUD == idAuditor && (c.ESTINV == (int)Estado.Invitacion.Enviada || c.ESTINV == (int)Estado.Invitacion.Cancelada || c.ESTINV == (int)Estado.Invitacion.Aceptado)).ToList(); 
        }

        public IEnumerable<TcINVITACION> ListarInvitacionesPublicacionSoa(int? idPublicacion, int? idServicioAuditoria, int idSoa)
        {
            var invitaciones = this._safInvitacionData.ListarInvitacionesPublicacion(idPublicacion, idServicioAuditoria);
            return invitaciones.Where(c => c.CODSOA == idSoa).ToList(); 
        }

        public TcACEPTARINVITACION AceptarInvitacion(int idInvitacion)
        {
            return this._safInvitacionData.AceptarInvitacion(idInvitacion);
        }


        public TcINVITARAUDITORES InvitarAuditores(int idSoa, int idPublicacion, int idServicioAud, string idAuditorCargo)
        {
            return this._safInvitacionData.InvitarAuditores(idSoa, idPublicacion, idServicioAud, idAuditorCargo);
        }


        public TcELIMINARFECHASASIGINVITACION EliminarFechasInvitacion(int idInvitacion, string fechasAgendadas)
        {
            return this._safInvitacionData.EliminarFechasInvitacion(idInvitacion, fechasAgendadas);
        }


        public TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas)
        {
            return this._safInvitacionData.RegistrarAgenda(idInvitacion, horas, fechas);
        }
    }
}
