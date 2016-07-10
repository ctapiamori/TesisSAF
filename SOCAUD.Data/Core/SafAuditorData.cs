using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafAuditorData : IBaseRepository<SAF_AUDITOR> {

        IEnumerable<TcAUDITORAPTOINVITAR> ListarAuditoresAptosInvitar(int idPublicacion, int idServicioAud);
        TcINVITARAUDITORES InvitarAuditores(int idSoa, int idPublicacion, int idServicioAud, string idAuditorCargo);
        IEnumerable<TcDISPONIBILIDADAUDITOR> ListarDisponibilidad(int idAuditor, int idSoa, string fechaInicio, string fechaTermino);
        TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas);
    }

    public class SafAuditorData : BaseRepository<SAF_AUDITOR>, ISafAuditorData
    {
        private readonly IUnitOfWork _uow;
        public SafAuditorData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<TcAUDITORAPTOINVITAR> ListarAuditoresAptosInvitar(int idPublicacion, int idServicioAud)
        {
            return this._uow.DataContext().SP_SAF_AUDITORAPTOINVITAR(idPublicacion, idServicioAud).ToList();
        }


        public TcINVITARAUDITORES InvitarAuditores(int idSoa, int idPublicacion, int idServicioAud, string idAuditorCargo)
        {
            return this._uow.DataContext().SP_SAF_INVITARAUDITORES(idSoa, idPublicacion, idServicioAud, idAuditorCargo).FirstOrDefault();
        }


        public IEnumerable<TcDISPONIBILIDADAUDITOR> ListarDisponibilidad(int idAuditor, int idSoa, string fechaInicio, string fechaTermino)
        {
            return this._uow.DataContext().SP_DISPONIBILIDADAUDITOR(idAuditor, idSoa, fechaInicio, fechaTermino).ToList();
        }


        public TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas)
        {
            return this._uow.DataContext().SP_SAF_AGENDAREGISTRAR(idInvitacion, horas, fechas).FirstOrDefault();
        }
    }
}
