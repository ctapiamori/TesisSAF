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
    public interface ISafNotificacionLogic : IFacadeOperacionCRUD<SAF_NOTIFICACION>
    {
        int ContadorNotificacionNoLeida(string usuario);
        IEnumerable<SAF_NOTIFICACION> ListarNotificaciones(string usuario);
        IEnumerable<SAF_NOTIFICACION> ListarNotificaciones(string usuario, string estado, string indicador);
        IEnumerable<SAF_NOTIFICACION> ListarNotificaciones(string bandeja, string usuario);
        IEnumerable<SAF_NOTIFICACION> ListarNotificacionesUsuario(string usuario);

        SAF_NOTIFICACION GetNotificacion(int idNotificacion);
        SAF_NOTIFICACION GetNotificacion(int idNotificacion, string usuario);
        void GrabarNotificacionAuditor(int idAuditor, string asunto, string body);
        void GrabarNotificacionSOA(int idSOA, string asunto, string body);
        void GrabarNotificacionTodosUsuarios(string asunto, string body);

    }

    public class SafNotificacionLogic : ISafNotificacionLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafNotificacionData _safNotificacionData;
        private readonly ISafAuditorData _safAuditorData;
        private readonly ISafSoaData _safSoaData;
        public SafNotificacionLogic()
        {
            this._uow = new UnitOfWork();
            this._safNotificacionData = new SafNotificacionData(_uow);
            this._safAuditorData = new SafAuditorData(_uow);
            this._safSoaData = new SafSoaData(_uow);
        }

        public SAF_NOTIFICACION Registrar(SAF_NOTIFICACION entidad)
        {
            var result = _safNotificacionData.Add(entidad);
            return result;
        }

        public SAF_NOTIFICACION Actualizar(SAF_NOTIFICACION entidad)
        {
            var result = _safNotificacionData.Update(entidad);
            return result;
        }

        public SAF_NOTIFICACION BuscarPorId(int id)
        {
            var result = _safNotificacionData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_NOTIFICACION> ListarTodos()
        {
            var result = _safNotificacionData.GetAll();
            return result;
        }

        public int ContadorNotificacionNoLeida(string usuario)
        {
            var result = this._safNotificacionData.GetMany(c => c.USUREC == usuario);
            return Convert.ToInt32(result.Count());
        }


        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<SAF_NOTIFICACION> ListarNotificaciones(string usuario)
        {
            var result = this._safNotificacionData.GetMany(c => c.USUREC == usuario && c.ESTNOT == "R" && c.INDNOT == "R");
            return result;
        }

        public IEnumerable<SAF_NOTIFICACION> ListarNotificaciones(string usuario, string estado, string indicador)
        {
            return this._safNotificacionData.GetMany(c => c.USUREC == usuario && c.ESTNOT == estado && c.INDNOT == indicador);
        }

        public SAF_NOTIFICACION GetNotificacion(int idNotificacion)
        {
            return this._safNotificacionData.GetById(idNotificacion);
        }

        public SAF_NOTIFICACION GetNotificacion(int idNotificacion, string usuario)
        {
            return this._safNotificacionData.GetMany(c => c.CODNOT.Equals(idNotificacion) && c.USUREC.Equals(usuario)).FirstOrDefault();
        }


        public IEnumerable<SAF_NOTIFICACION> ListarNotificaciones(string bandeja, string usuario)
        {
            return this._safNotificacionData.GetMany(c => c.ESTNOT == bandeja && c.USUREC == usuario);
        }


        public IEnumerable<SAF_NOTIFICACION> ListarNotificacionesUsuario(string usuario)
        {
            return this._safNotificacionData.GetMany(c => c.USUREC == usuario);
        }

        public void GrabarNotificacionAuditor(int idAuditor, string asunto, string body)
        {
            var infoAuditor = this._safAuditorData.GetById(idAuditor);
            var notificacion = new SAF_NOTIFICACION()
            {
                DESNOT = body,
                ASUNOT = asunto,
                FECREG = DateTime.Now,
                USUEMI = "SYSTEM",
                INDNOT = "R",
                ESTNOT = "R",
                USUREC = infoAuditor.NOMUSU,
                ESTREG = "1"
            };

            this.Registrar(notificacion);
        }

        public void GrabarNotificacionSOA(int idSOA, string asunto, string body)
        {
            var infoAuditor = this._safSoaData.GetById(idSOA);
            var notificacion = new SAF_NOTIFICACION()
            {
                DESNOT = body,
                ASUNOT = asunto,
                FECREG = DateTime.Now,
                USUEMI = "SYSTEM",
                INDNOT = "R",
                ESTNOT = "R",
                USUREC = infoAuditor.NOMUSU,
                ESTREG = "1"
            };

            this.Registrar(notificacion);
        }

        public void GrabarNotificacionTodosUsuarios(string asunto, string body)
        {
            var auditoresInfo = this._safAuditorData.GetAll();
            foreach (var item in auditoresInfo)
            {
                var notificacion = new SAF_NOTIFICACION()
                {
                    DESNOT = body,
                    ASUNOT = asunto,
                    FECREG = DateTime.Now,
                    INDNOT = "R",
                    ESTNOT = "R",
                    USUEMI = "SYSTEM",
                    USUREC = item.NOMUSU,
                    ESTREG = "1"
                };

                this.Registrar(notificacion);
            }


            var soasInfo = this._safSoaData.GetAll();
            foreach (var item in soasInfo)
            {
                var notificacion = new SAF_NOTIFICACION()
                {
                    DESNOT = body,
                    ASUNOT = asunto,
                    FECREG = DateTime.Now,
                    INDNOT = "R",
                    ESTNOT = "R",
                    USUEMI = "SYSTEM",
                    USUREC = item.NOMUSU,
                    ESTREG = "1"
                };

                this.Registrar(notificacion);
            }
        }
    }
}
