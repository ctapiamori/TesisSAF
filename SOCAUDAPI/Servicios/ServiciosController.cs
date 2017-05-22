using SOCAUD.Business.Core;
using SOCAUD.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOCAUDAPI.Servicios
{
    [RoutePrefix("api")]
    public class ServiciosController : ApiController
    {

        private readonly ISeguridadLogic _seguridadLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafConsultaLogic _consultaLogic;

        public ServiciosController()
        {
            _seguridadLogic = new SeguridadLogic();
            _auditorLogic = new SafAuditorLogic();
            _soaLogic = new SafSoaLogic();
            _notificacionLogic = new SafNotificacionLogic();
            _publicacionLogic = new SafPublicacionLogic();
            _consultaLogic = new SafConsultaLogic();
        }


        public class UsuarioDTO {
            public int tipoUsuario { get; set; }
            public string usuario { get; set; }
            public string password { get; set; }
   
        }
        
        [HttpGet]
        [Route("iniciarSession")]
        public IHttpActionResult Acceder(string usuario, string password = "")
        {
            var usu = new UsuarioDTO() { usuario = usuario, password = password, tipoUsuario = 2 }; // SOA
            var result = this._seguridadLogic.AccederSistemaExtranet(usu.usuario, usu.password, usu.tipoUsuario);
            int CodigoResponsableLogin = 0;
            string NombreCompletoLogin = "";
            if (result.Exito)
            {
                if (usu.tipoUsuario == (int)Tipo.TipoUsuarioExtranet.Auditor)
                {
                    var auditor = _auditorLogic.GetAuditorByUsuario(usu.usuario);
                    CodigoResponsableLogin = auditor.CODAUD;
                    NombreCompletoLogin = string.Format("{0} {1}", auditor.NOMAUD, auditor.APEAUD);
                }
                else
                {
                    var soa = _soaLogic.InformacionPorUsuario(usu.usuario);
                    CodigoResponsableLogin = soa.CODSOA;
                    NombreCompletoLogin = soa.RAZSOCSOA;
                }
                return Ok(new { AccederSistemaExtranetResult = true });
                //Session["sessionUsuario"] = usuario;
                //Session["sessionTipoUsuario"] = tipoUsuario;
            }
            else {
                return Ok(new { AccederSistemaExtranetResult = false });
            } 
        }

        [HttpGet]
        [Route("ObtenerSoaPorUsuario")]
        public IHttpActionResult ObtenerSoaPorUsuario(string usuario)
        {
            try
            {
                var infoSOAPorUsuario = _soaLogic.InformacionPorUsuario(usuario);
                return Ok(new { ObtenerSoaPorUsuarioResult = new { CodigoSOA = infoSOAPorUsuario.CODSOA }});
            }
            catch (Exception)
            {
                return Ok(new { ObtenerSoaPorUsuarioResult = 0 });
            }
        }

        [HttpGet]
        [Route("GetNotificaciones")]
        public IHttpActionResult GetNotificaciones(string usuario)
        {
            try
            {
                var lista = _notificacionLogic.ListarNotificaciones(usuario);
                var result = (from c in lista select new {
                   CodigoNotificacion = c.CODNOT,
                   Asunto = c.ASUNOT
                });

                return Ok(new { ListarNotificacionesResult = result });
            }
            catch (Exception)
            {
                return Ok(new { ListarNotificacionesResult = new List<string>() });
            }
        }

        [HttpGet]
        [Route("GetNotificacion")]
        public IHttpActionResult GetNotificacion(int idNotificacion)
        {
            try
            {
                var notificacion = _notificacionLogic.BuscarPorId(idNotificacion);


                return Ok(new { GetNotificacionResult = new { Descripcion = notificacion.DESNOT } });
            }
            catch (Exception)
            {
                return Ok(new { GetNotificacionResult = new { Descripcion = "" } });
            }
        }

        [HttpGet]
        [Route("GetPublicaciones")]
        public IHttpActionResult GetPublicaciones()
        {
            try
            {
                var lista = _publicacionLogic.ListarPublicacion();

                var result = (from c in lista select new {
                    CodigoPublicacion = c.CODPUB,
                    DescripBase = c.DESBAS,
                    NumeroPublicacion = c.NUMPUB
                });


                return Ok(new { listarPublicacionResult = result  });
            }
            catch (Exception)
            {
                return Ok(new { listarPublicacionResult = new List<string>() });
            }
        }
        

        [HttpGet]
        [Route("GetConsultas")] 
        public IHttpActionResult listaConsulta(int idSoa, int idPub)
        {
            var listaConsulta = _consultaLogic.ListarConsultaPorPublicacionyUsuario(idSoa, idPub);
            var result = (from c in listaConsulta select new {
                CodigoConsulta = c.CODCON,
                DescripcionConsulta = c.DESCON,
                EstadoConsulta = c.ESTCON
            });
            return Ok(new { listarConsultaResult = result });
        }

        [HttpGet]
        [Route("DeleteConsulta")]
        public IHttpActionResult DeleteConsulta(int idCon)
        {
            try
            {
                _consultaLogic.DeleteConsulta(idCon);
                return Ok("Elimino");
            }
            catch (Exception)
            {
                return Ok("No elimino");
            }
        }

        [HttpGet]
        [Route("InsertConsulta")]
        public IHttpActionResult InsertConsulta(int idSoa, int idPub, string desCon) { 
            try
            {
                _consultaLogic.InsertConsulta(idSoa, idPub, desCon);
                return Ok("Agrego");
            }
            catch (Exception)
            {
                return Ok("Error al agregar");
            }
        }
        
        [HttpGet]
        [Route("SendConsulta")]
        public IHttpActionResult SendConsulta(int idCon)
        { 
            try
            {
                _consultaLogic.SendConsulta(idCon);
                return Ok("Envio");
            }
            catch (Exception)
            {
                return Ok("Error al enviar");
            }
        }
        
    }
}