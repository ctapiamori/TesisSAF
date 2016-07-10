using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafInvitacionData : IBaseRepository<SAF_INVITACION> {

        IEnumerable<TcINVITACION> ListarInvitacionesPublicacion(int? idPublicacion, int? idServicioAuditoria);
        TcINVITARAUDITORES InvitarAuditores(int idSoa, int idPublicacion, int idServicioAud, string idAuditorCargo);
        TcACEPTARINVITACION AceptarInvitacion(int idInvitacion);
        TcELIMINARFECHASASIGINVITACION EliminarFechasInvitacion(int idInvitacion, string fechasAgendadas);
        TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas);

    }

    public class SafInvitacionData : BaseRepository<SAF_INVITACION>, ISafInvitacionData
    {
        private readonly IUnitOfWork _uow;

        public SafInvitacionData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }


        public IEnumerable<TcINVITACION> ListarInvitacionesPublicacion(int? idPublicacion, int? idServicioAuditoria)
        {
            return this._uow.DataContext().SP_SAF_INVITACION(idPublicacion, idServicioAuditoria).ToList();
        }

        public TcINVITARAUDITORES InvitarAuditores(int idSoa, int idPublicacion, int idServicioAud, string idAuditorCargo)
        {
            return this._uow.DataContext().SP_SAF_INVITARAUDITORES(idSoa, idPublicacion, idServicioAud, idAuditorCargo).FirstOrDefault();
        }

        public TcACEPTARINVITACION AceptarInvitacion(int idInvitacion)
        {
            return this._uow.DataContext().SP_SAF_ACEPTARINVITACION(idInvitacion).First();
        }


        public TcELIMINARFECHASASIGINVITACION EliminarFechasInvitacion(int idInvitacion, string fechasAgendadas)
        {
            return this._uow.DataContext().SP_SAF_ELIMINARFECHASASIGINVITACION(idInvitacion, fechasAgendadas).FirstOrDefault();
        }


        public TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas)
        {
            return this._uow.DataContext().SP_SAF_AGENDAREGISTRAR(idInvitacion, horas, fechas).FirstOrDefault();
        }
    }
}
