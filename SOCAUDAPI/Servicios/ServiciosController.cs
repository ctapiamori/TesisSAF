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
        private readonly ISafPublicacionBaseLogic _publicacionYBasesLogic;
        private readonly ISafAbsolucionConsultaLogic _absolucionConsultaLogic;

     


        public ServiciosController()
        {
            _seguridadLogic = new SeguridadLogic();
            _auditorLogic = new SafAuditorLogic();
            _soaLogic = new SafSoaLogic();
            _notificacionLogic = new SafNotificacionLogic();
            _publicacionLogic = new SafPublicacionLogic();
            _consultaLogic = new SafConsultaLogic();
            _publicacionYBasesLogic = new SafPublicacionBaseLogic();
            _absolucionConsultaLogic = new SafAbsolucionConsultaLogic();
             
        }


        public class UsuarioDTO {
            public int tipoUsuario { get; set; }
            public string usuario { get; set; }
            public string password { get; set; }
   
        }
        
        [HttpGet]
        [Route("iniciarSession")]
        public IHttpActionResult Acceder(string tipo , string usuario, string password = "")
        {
            var usu = new UsuarioDTO();

            if (tipo == "S") { 
                usu.usuario = usuario;
                usu.password = password;
                usu.tipoUsuario = 2; // SOA
            }

            if (tipo == "A")
            {
                usu.usuario = usuario;
                usu.password = password;
                usu.tipoUsuario = 1; // SOA
            }

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
        [Route("GetAbsoluciones")]
        public IHttpActionResult GetAbsolucionConsulta()
        {
       
                var ListaAbsoluciones = _absolucionConsultaLogic.ListarAbsolucionConsultasCompleto();

                var result = (from c in ListaAbsoluciones
                              select new
                              {
                                  c.CODABSCON,
                                  c.NUMPUB,
                                  c.DESBAS,
                                  FECPUBABSOLUCION = c.FECPUBABSOLUCION.GetValueOrDefault().ToString("dd/MM/yyyy")
                              });
                return Ok(new { ObtenerAbsoluciones = result });
     
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
                var lista = _notificacionLogic.ListarNotificaciones(usuario).OrderByDescending(c=>c.FECREG);
                var result = (from c in lista select new {
                   CodigoNotificacion = c.CODNOT,
                   Asunto = c.ASUNOT,
                   EnviadoPor = c.USUEMI,
                   Fecha = c.FECREG.GetValueOrDefault().ToString("dd/MM/yyyy")
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


                return Ok(new { GetNotificacionResult = new { 
                    Descripcion = notificacion.DESNOT ,
                    Por = notificacion.USUEMI,
                    Hora = notificacion.FECREG.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm:ss")
                } 
                });
            }
            catch (Exception)
            {
                return Ok(new { GetNotificacionResult = new { Descripcion = "", Por = "", Hora = "" } });
            }
        }

        public class SelectList {
            public string Value { get; set; }
            public string Text { get; set; }
            public string FecMaxCrearConsulta { get; set; }
            public string FecMaxAbsolucion { get; set; }
            public string FecMaxPresentarPropuesta { get; set; }
        }
        [HttpGet]
        [Route("GetPublicaciones")]
        public IHttpActionResult GetPublicaciones()
        {
    
                var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();

                var listaPublicacion = (from c in publicaciones select new SelectList() {
                    Value = c.CODPUB.ToString(), 
                    Text = c.NUMPUB, 
                    FecMaxCrearConsulta = c.FECMAXCRECON.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    FecMaxAbsolucion = c.FECMAXRESCONS.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    FecMaxPresentarPropuesta = c.FECMAXPREPROP.GetValueOrDefault().ToString("dd/MM/yyyy"),
                }).ToList();
                var resultPrevio = listaPublicacion.GroupBy(c => new
                {
                    c.Value,
                    c.Text,
                    c.FecMaxAbsolucion,
                    c.FecMaxPresentarPropuesta,
                    c.FecMaxCrearConsulta
                }).OrderBy(g => g.Key.Value)
                .Select(g => new SelectList
                {
                    Text = g.Key.Text,
                    Value = g.Key.Value,
                    FecMaxAbsolucion  = g.Key.FecMaxAbsolucion,
                    FecMaxCrearConsulta = g.Key.FecMaxCrearConsulta,
                    FecMaxPresentarPropuesta = g.Key.FecMaxPresentarPropuesta
                });



                var result = (from c in resultPrevio
                              select new
                              {
                    CodigoPublicacion = c.Value,
                    NumeroPublicacion = c.Text,
                    FecMaxAbsolucion = c.FecMaxAbsolucion,
                    FecMaxCrearConsulta = c.FecMaxCrearConsulta,
                    FecMaxPresentarPropuesta = c.FecMaxPresentarPropuesta
                });


                return Ok(new { listarPublicacionResult = result  });
          
        }


        [HttpGet]
        [Route("GetBases")]
        public IHttpActionResult GetBases(int idPub)
        {
            try
            {
 
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var Bases = publicaciones.Where(c => c.CODPUB == idPub);

            var result = (from c in Bases
                          select new
                          {
                              CodigoBase = c.CODBAS.ToString(),
                              NombreEntidad = c.DESBAS
                          });
            return Ok(new { listarBasesResult = result });
            }
            catch (Exception)
            {
                return Ok(new { listarBasesResult = new List<string>() });
            }
        }

        [HttpGet]
        [Route("GetConsultas")] 
        public IHttpActionResult listaConsulta(int idSoa, int idPub, int idBase)
        {
            var listaConsulta = _consultaLogic.ListarConsultaPorPublicacion_Base_SOA(idSoa, idPub, idBase);
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
                var consulta = this._consultaLogic.buscarPorId(idCon);

                var listaAbso = this._absolucionConsultaLogic.ListarTodos().ToList();

                var existe = listaAbso.Where(c => c.CODPUB == consulta.CODPUB.GetValueOrDefault() && c.CODBASE == consulta.CODBAS.GetHashCode()).Any();
                if (existe)
                    return Ok(new { estado = false, mensaje = "Ya existe una absolución registrada para esta Publicación y Entidad, no puede continuar" });

                _consultaLogic.DeleteConsulta(idCon);
                return Ok(new { estado = true, mensaje = "Elimino la consulta satisfactoriamente" });
            }
            catch (Exception)
            {
                return Ok(new { estado = false, mensaje = "No se pudo eliminar la consulta" });
            }
        }

        [HttpGet]
        [Route("InsertConsulta")]
        public IHttpActionResult InsertConsulta(int idSoa, int idPub, int idBase, string desCon) { 
            try
            {

                var listaAbso = this._absolucionConsultaLogic.ListarTodos().ToList();

                var existe = listaAbso.Where(c => c.CODPUB == idPub && c.CODBASE == idBase).Any();
                if (existe)
                    return Ok(new { estado = false, mensaje = "Ya existe una absolución registrada para esta Publicación y Entidad, no puede continuar" });



                _consultaLogic.InsertConsulta(idSoa, idPub, idBase, desCon);
                return Ok(new { estado = true, mensaje = "Registro la consulta satisfactoriamente" });
            }
            catch (Exception)
            {
                return Ok(new { estado = false, mensaje = "No se pudo registrar la consulta" });
            }
        }
        
        [HttpGet]
        [Route("SendConsulta")]
        public IHttpActionResult SendConsulta(int idCon)
        { 
            try
            {
                var consulta = this._consultaLogic.buscarPorId(idCon);

                var listaAbso = this._absolucionConsultaLogic.ListarTodos().ToList();

                var existe = listaAbso.Where(c => c.CODPUB == consulta.CODPUB.GetValueOrDefault() && c.CODBASE == consulta.CODBAS.GetHashCode()).Any();
                if (existe)
                    return Ok(new { estado = false, mensaje = "Ya existe una absolución registrada para esta Publicación y Entidad, no puede continuar" });

                _consultaLogic.SendConsulta(idCon);
                return Ok(new { estado =  true, mensaje = "Se envio la consulta satisfactoriamente" } );
            }
            catch (Exception)
            {
                return Ok(new { estado = false, mensaje = "No se pudo enviar la consulta" });
            }
        }
        
    }
}