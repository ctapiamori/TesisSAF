using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOCAUD.Data.Model;
using ReportManagement;
namespace SOCAUD.Intranet.Controllers
{

    public class ReporteAbsolucionConsulta
    {

        public string NumeroPublicacion { get; set; }
        public string NombreEntidad { get; set; }
        public string ImageUrl { get; set; }
        public IList<ReporteConsultaRespuestaItem> ListaConsultas { get; set; }
        public ReporteAbsolucionConsulta()
        {
            this.ListaConsultas = new List<ReporteConsultaRespuestaItem>();
        }
    }

    public class ReporteConsultaRespuestaItem {
        public string Soa { get; set; }
        public string Consulta { get; set; }
        public string Respuesta { get; set; }

        public ReporteConsultaRespuestaItem() { }
    }

    public class ConsultaController : PdfViewController
    {

        private readonly ISafBaseLogic _baseLogic;
        private readonly ISafConsultaLogic _consultaLogic;
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafPublicacionBaseLogic _publicacionYBasesLogic;
        private readonly ISafAbsolucionConsultaLogic _absolucionConsultaLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;
        public ConsultaController()
        {
            _baseLogic = new SafBaseLogic();
            _publicacionLogic = new SafPublicacionLogic();
            _consultaLogic = new SafConsultaLogic();
            _publicacionYBasesLogic = new SafPublicacionBaseLogic();
            this._notificacionLogic = new SafNotificacionLogic();
            _absolucionConsultaLogic = new SafAbsolucionConsultaLogic();
        }

        public ActionResult Index() {

            var model = new ConsultaModel();

            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();

            var listaPublicacion = (from c in publicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();
            var result = listaPublicacion.GroupBy(c => new
            {
                c.Value,
                c.Text
            }).OrderBy(g => g.Key.Value)
            .Select(g => new SelectListItem
            {
                Text = g.Key.Text,
                Value = g.Key.Value
            });


            model.lista = result.ToList();
            return View(model);
        }


        public JsonResult listarBases(int idPub)
        {
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var Bases = publicaciones.Where(c => c.CODPUB == idPub);
            return Json(Bases);
        }

        public JsonResult ListarConsultas(int? idPublicacion, int? idBase) {
            var lista = _consultaLogic.ListarConsultaPorPublicacion_Base(idPublicacion, idBase);

            var estadoEnviado = Estado.ConsultasPublicacion.Enviado.GetHashCode();
            var estadoRespondido = Estado.ConsultasPublicacion.Respondida.GetHashCode();

            lista = lista.Where(c => c.ESTCON == estadoEnviado || c.ESTCON == estadoRespondido);
            var data = lista.Select(c => new string[]{ 
                c.CODCON.ToString(),
                c.NUMPUB,
                c.DESBAS,
                c.RAZSOCSOA,
                c.DESCON.ToString(),
                c.RESCON,
                c.ESTCON.Value.ToString(),
            }).ToArray();


            return Json(data);
        }

        public PartialViewResult ResponderConsulta(int idConsulta) {

            var consulta = _consultaLogic.ObtenerConsultaCompleta(idConsulta);
            var consultaEntidad = new ConsultaEntidadModel();
            consultaEntidad.CodigoConsulta = consulta.CODCON;
            consultaEntidad.Publicacion = consulta.NUMPUB;
            consultaEntidad.DesBase = consulta.DESBAS;
            consultaEntidad.SOA = consulta.RAZSOCSOA;
            consultaEntidad.DescripcionConsulta = consulta.DESCON;
            consultaEntidad.RespuestaConsulta = consulta.RESCON;
            consultaEntidad.EstadoDes = consulta.ESTDES;
            consultaEntidad.idBase = consulta.CODBAS.GetHashCode();
            consultaEntidad.IdPublicacion = consulta.CODPUB.GetHashCode();
            return PartialView("_ResponderConsulta", consultaEntidad);
        }



        public JsonResult GrabarRespuesta(ConsultaEntidadModel model) {
            try
            {

                var listaAbso = this._absolucionConsultaLogic.ListarTodos().ToList();

                var existe = listaAbso.Where(c => c.CODPUB == model.IdPublicacion && c.CODBASE == model.idBase).Any();
                if (existe)
                    return Json(new MensajeRespuesta("Ya existe una absolución registrada para esta Publicación y Entidad, no puede continuar", false));


                _consultaLogic.GrabarRespuesta(model.CodigoConsulta, model.RespuestaConsulta);
                 return Json(new MensajeRespuesta("Se grabo la respuesta satisfactoria", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo grabar la respuesta", false));
            }

            
        }


        public PartialViewResult AbsolucionConsulta()
        {
            var model = new AbsolucionConsultasModel();

            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();

            var listaPublicacion = (from c in publicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();
            var result = listaPublicacion.GroupBy(c => new
            {
                c.Value,
                c.Text
            }).OrderBy(g => g.Key.Value)
            .Select(g => new SelectListItem
            {
                Text = g.Key.Text,
                Value = g.Key.Value
            });
            model.lista = result;
            model.Hora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            return PartialView("_AbsolucionConsultas", model);
        }

        public JsonResult GrabarAbsolucionConsulta(int idPub, int idBase)
        {
            try
            {
                var listaAbso = this._absolucionConsultaLogic.ListarTodos().ToList();

                var existe = listaAbso.Where(c => c.CODPUB == idPub && c.CODBASE == idBase).Any();
                if (existe)
                    return Json(new MensajeRespuesta("Ya existe una absolución registrada para esta Publicación y Entidad", false));

                var absolucion = this._absolucionConsultaLogic.Registrar(new SAF_ABSOLUCION_CONSULTA() {
                    CODBASE = idBase,
                    CODPUB = idPub,
                    FECPUBABSOLUCION = DateTime.Now,
                });

                var infoPublicacion = this._publicacionLogic.BuscarPorId(idPub);
                var infoBase = this._baseLogic.BuscarPorId(idBase);

                string mensaje = string.Empty;

                mensaje = "Se ha publicado el documento de ABSOLUCION de CONSULTAS, para el concurso: <br /><br />";
                mensaje = mensaje + "Publicación: <strong>" + infoPublicacion.NUMPUB + "</strong><br/>";
                mensaje = mensaje + "Entidad: <strong>" + infoBase.DESBAS + "</strong><br/>";

                this._notificacionLogic.GrabarNotificacionTodosUsuarios(Notificacion.asuntoAbsolucionConsulta, mensaje);



                return Json(new MensajeRespuesta("Se grabo la absolución de consultas satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo grabar la absolución de consultas", false));
            }
        }


        public ActionResult CreateReporteAbsolucion(int id)
        {
            var model = new ReporteAbsolucionConsulta();
            var datosAbsolucion = this._absolucionConsultaLogic.BuscarPorId(id);
            var datosPublicacion = this._publicacionLogic.BuscarPorId(datosAbsolucion.CODPUB.GetValueOrDefault());
            var datosBase = this._baseLogic.BuscarPorId(datosAbsolucion.CODBASE.GetValueOrDefault());
            model.NombreEntidad = datosBase.DESBAS;
            FillImageUrl(model, "logo_contraloria.png");

            var lista = _consultaLogic.ListarConsultaPorPublicacion_Base(datosAbsolucion.CODPUB.GetValueOrDefault(), datosAbsolucion.CODBASE.GetValueOrDefault());

            var estadoRespondido = Estado.ConsultasPublicacion.Respondida.GetHashCode();

            lista = lista.Where(c => c.ESTCON == estadoRespondido);

            model.NombreEntidad = datosBase.DESBAS;
            model.NumeroPublicacion = datosPublicacion.NUMPUB;

            foreach (var item in lista)
	        {
                model.ListaConsultas.Add(new ReporteConsultaRespuestaItem() { 
                    Consulta = item.DESCON,
                    Respuesta = item.RESCON,
                    Soa = item.RAZSOCSOA
                });
	        }
            
            return this.ViewPdf("", "CreateReporteAbsolucion", model);
        }

        private void FillImageUrl(ReporteAbsolucionConsulta model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }

    }
}