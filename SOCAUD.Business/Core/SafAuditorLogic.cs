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
    public interface ISafAuditorLogic : IFacadeOperacionCRUD<SAF_AUDITOR>
    {
        bool AccederAuditor(string usuario, string password);
        SAF_AUDITOR GetAuditorByUsuario(string usuario);
        bool ExisteAuditor(string usuario);
        IEnumerable<TcAUDITORAPTOINVITAR> ListarAuditoresAptosInvitar(int idPublicacion, int idServicioAud);
        IEnumerable<TcDISPONIBILIDADAUDITOR> ListarDisponibilidad(int idAuditor, int idSoa, string fechaInicio, string fechaTermino);

        TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas);
    }

    public class SafAuditorLogic : ISafAuditorLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafAuditorData _safAuditorData;
        public SafAuditorLogic()
        {
            this._uow = new UnitOfWork();
            this._safAuditorData = new SafAuditorData(_uow);
        }

        public SAF_AUDITOR Registrar(SAF_AUDITOR entidad)
        {

            var result = _safAuditorData.Add(entidad);
            return (SAF_AUDITOR)result;
        }

        public SAF_AUDITOR Actualizar(SAF_AUDITOR entidad)
        {
            var result = _safAuditorData.Update(entidad);
            return (SAF_AUDITOR)result;
        }

        public SAF_AUDITOR BuscarPorId(int id)
        {
            var result = _safAuditorData.GetById(id);
            return (SAF_AUDITOR)result;
        }

        public IEnumerable<SAF_AUDITOR> ListarTodos()
        {
            var result = _safAuditorData.GetAll();
            return result;
        }

        public bool AccederAuditor(string usuario, string password)
        {
            var result = _safAuditorData.GetMany(c => c.NOMUSU == usuario && c.PASUSU == password).Any();
            return result;
        }


        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }


        public SAF_AUDITOR GetAuditorByUsuario(string usuario)
        {
            return _safAuditorData.Get(c => c.NOMUSU == usuario);
        }

        public bool ExisteAuditor(string usuario)
        {
            return _safAuditorData.GetMany(c => c.NOMUSU == usuario).Any();
        }


        public IEnumerable<TcAUDITORAPTOINVITAR> ListarAuditoresAptosInvitar(int idPublicacion, int idServicioAud)
        {
            return this._safAuditorData.ListarAuditoresAptosInvitar(idPublicacion, idServicioAud);
        }


        public IEnumerable<TcDISPONIBILIDADAUDITOR> ListarDisponibilidad(int idAuditor, int idSoa, string fechaInicio, string fechaTermino)
        {
            return this._safAuditorData.ListarDisponibilidad(idAuditor, idSoa, fechaInicio, fechaTermino);
        }


        public TcAGENDAREGISTRAR RegistrarAgenda(int idInvitacion, int horas, string fechas)
        {
            return this._safAuditorData.RegistrarAgenda(idInvitacion, horas, fechas);
        }
    }
}
