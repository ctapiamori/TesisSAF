﻿using Microsoft.Reporting.WebForms;
using ReportManagement;
using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Areas.Publicacion.Models;
using SOCAUD.Intranet.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Areas.Publicacion.Controllers
{


    public class ReporteConcursoPublico
    {

        public string NroConcurso { get; set; }
        public string FechaMaxConsultas { get; set; }
        public string FechaMaxResponderConsultas { get; set; }
        public string FechaMaxPropuestas { get; set; }
        public string ImageUrl { get; set; }
        public IList<string> Entidades { get; set; }

        public ReporteConcursoPublico()
        {

            this.Entidades = new List<string>();
            
        }
    }


    public class EvaluadorController : PdfViewController
    {
        //private readonly ConcursoPublicoMeritoAgente _agenteConcursoPublicoMerito;
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafPublicacionBaseLogic _publicacionBaseLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafBaseLogic _baseLogic;
        private readonly ISafGeneralLogic _safGeneralLogic;

        private readonly ISafWorkFlowLogic _safWorkFlowLogic;

        public EvaluadorController()
        {
            //this._agenteConcursoPublicoMerito = new ConcursoPublicoMeritoAgente();
            this._publicacionLogic = new SafPublicacionLogic();
            this._publicacionBaseLogic = new SafPublicacionBaseLogic();
            this._notificacionLogic = new SafNotificacionLogic();
            this._cronogramaLogic = new SafCronogramaLogic();
            this._baseLogic = new SafBaseLogic();
            this._safGeneralLogic = new SafGeneralLogic();
            this._safWorkFlowLogic = new SafWorkFlowLogic();
        }

        public ActionResult Bandeja()
        {
            return View();
        }

        public ActionResult Publicacion(int? idPub)
        {
            var model = new PublicacionViewModel();
            var cronogramas = this._cronogramaLogic.ListarTodos();
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() }).ToList();
            if(idPub.HasValue)
            {
                var publicacion = this._publicacionLogic.BuscarPorId(idPub.Value);

                if (!publicacion.ESTPUB.GetValueOrDefault().Equals(Estado.Publicacion.Elaboracion.GetHashCode()))
                    return RedirectToAction("View", new { id = idPub });

                model.CodigoPublicacion = publicacion.CODPUB;
                model.Cronograma = publicacion.CODCRO.GetValueOrDefault();
                //model.Base = publicacion.CODBAS.GetValueOrDefault();
                model.FechaMaximaCreacionConsulta = publicacion.FECMAXCONS.GetValueOrDefault().ToShortDateString();
                model.FechaMaximaPublicacionConcurso = publicacion.FECMAXCRECON.GetValueOrDefault().ToShortDateString();
                model.FechaMaximaResponderConsultas = publicacion.FECMAXRESCONS.GetValueOrDefault().ToShortDateString();
                model.FechaMaximaPresentacionPropuestas = publicacion.FECMAXPREPROP.GetValueOrDefault().ToShortDateString();
                model.estadoPublicacion = publicacion.ESTPUB.GetValueOrDefault();
                model.EstadoDescripcion = this._safGeneralLogic.GetParametro(publicacion.ESTPUB.GetValueOrDefault()).NOMPAR;

                var estadoWorkFlow = this._safWorkFlowLogic.MostrarWorkFlow(idPub.Value, "P");

                model.CodigoWorkFlow = estadoWorkFlow.Item1;
                model.FlgMostrarFlujoAprobacion = estadoWorkFlow.Item2;

                //var bases = this._baseLogic.BuscarPorCronograma(model.Cronograma);
                //model.Bases = bases.Select(c => new SelectListItem() { Value = c.CODBAS.ToString(), Text = c.DESBAS }).ToList();
            }
            else
            {
                model.estadoPublicacion = Estado.Publicacion.Elaboracion.GetHashCode();
            }
            
            return View(model);

        }

        public ActionResult View(int id)
        {
            var model = new PublicacionViewModel();
            var cronogramas = this._cronogramaLogic.ListarTodos();
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() }).ToList();

            var publicacion = this._publicacionLogic.BuscarPorId(id);

            model.CodigoPublicacion = publicacion.CODPUB;
            model.Cronograma = publicacion.CODCRO.GetValueOrDefault();
            model.FechaMaximaCreacionConsulta = publicacion.FECMAXCONS.GetValueOrDefault().ToShortDateString();
            model.FechaMaximaPublicacionConcurso = publicacion.FECMAXCRECON.GetValueOrDefault().ToShortDateString();
            model.FechaMaximaResponderConsultas = publicacion.FECMAXRESCONS.GetValueOrDefault().ToShortDateString();
            model.FechaMaximaPresentacionPropuestas = publicacion.FECMAXPREPROP.GetValueOrDefault().ToShortDateString();
            model.estadoPublicacion = publicacion.ESTPUB.GetValueOrDefault();
            model.EstadoDescripcion = this._safGeneralLogic.GetParametro(publicacion.ESTPUB.GetValueOrDefault()).NOMPAR;

            var estadoWorkFlow = this._safWorkFlowLogic.MostrarWorkFlow(id, "P");

            model.CodigoWorkFlow = estadoWorkFlow.Item1;
            model.FlgMostrarFlujoAprobacion = estadoWorkFlow.Item2;



            return View(model);

        }

        public JsonResult ListarBases(int publicacion)
        {
            var cronograma = this._publicacionLogic.BuscarPorId(publicacion).CODCRO.GetValueOrDefault();
            var bases = this._baseLogic.BuscarPorCronograma(cronograma);
            var estadoBaseAprobado = Estado.Bases.Aprobado.GetHashCode();
            bases = bases.Where(c => c.ESTBAS == estadoBaseAprobado);
            var basesPublicacion = this._publicacionBaseLogic.ListarPorPublicacion(publicacion);
            if (basesPublicacion.Any())
                bases = bases.Where(c => !basesPublicacion.Any(b=> b.CODBAS == c.CODBAS));

            var listBases = bases.Select(c => new string[] { 
                c.CODBAS.ToString(),
                c.DESBAS
            }).ToArray();

            var listBasesAsignadas = basesPublicacion.Select(c => new string[] { 
                c.CODPUBBAS.ToString(),
                c.DESPUBBAS
            }).ToArray();
 
            return Json(new { bases = listBases, asignadas = listBasesAsignadas });
        }

        public JsonResult ListarEntidades(int publicacion)
        {
            var basesPublicacion = this._publicacionBaseLogic.ListarPorPublicacion(publicacion);
            return Json(basesPublicacion.Select(c => new SelectListItem() { Value = c.CODPUBBAS.ToString(), Text = c.DESPUBBAS }).ToList());
        }

        public JsonResult GrabarPublicacion(PublicacionViewModel model)
        {
            try
            {
                SAF_PUBLICACION publicacion = new SAF_PUBLICACION()
                {
                    CODCRO = model.Cronograma,
                    ESTPUB = Estado.Publicacion.Elaboracion.GetHashCode()
                };

                if (model.CodigoPublicacion.HasValue)
                    publicacion = this._publicacionLogic.BuscarPorId(model.CodigoPublicacion.GetValueOrDefault());// this.modeloEntity.SAF_PUBLICACION.Where(c => c.CODPUB == model.CodigoPublicacion).FirstOrDefault();
                else
                {
                    var correlativo = this._cronogramaLogic.NewCorrelativoPublicacion(model.Cronograma);
                    var cronograma = this._cronogramaLogic.BuscarPorId(model.Cronograma);
                    publicacion.NUMPUB = cronograma.ANIOCRO.GetValueOrDefault().ToString() + "-" + correlativo.ToString("D4");
                }
                
                publicacion.FECMAXCRECON = WebHelper.GetDateTimeOrNull(model.FechaMaximaPublicacionConcurso);
                publicacion.FECMAXCONS = WebHelper.GetDateTimeOrNull(model.FechaMaximaCreacionConsulta);
                publicacion.FECMAXRESCONS = WebHelper.GetDateTimeOrNull(model.FechaMaximaResponderConsultas);
                publicacion.FECMAXPREPROP = WebHelper.GetDateTimeOrNull(model.FechaMaximaPresentacionPropuestas);

                if (model.CodigoPublicacion.HasValue)
                    this._publicacionLogic.Actualizar(publicacion);
                else
                    this._publicacionLogic.Registrar(publicacion);

                //var noti = new Helper.NotificacionAdmin();
                //string mensaje = string.Empty;
                //if (model.CodigoPublicacion.HasValue)
                //    mensaje = "Se realizaron los siguientes cambios en la publicacion, debe tener en cuenta que las fechas mostradas son los limites para realizar cada accion: <br /><br />";
                //else
                //    mensaje = "Se registro una publicacion, debe tener en cuenta que las fechas mostradas son los limites para realizar cada accion: <br /><br />";
                //mensaje = mensaje + "*) Fecha publicacion concurso : <strong>" + model.FechaMaximaPublicacionConcurso + "</strong><br/>";
                //mensaje = mensaje + "*) Fecha elaboracion consultas : <strong>" + model.FechaMaximaCreacionConsulta + "</strong><br/>";
                //mensaje = mensaje + "*) Fecha elaboracion absolucion de consultas : <strong>" + model.FechaMaximaResponderConsultas + "</strong><br/>";
                //mensaje = mensaje + "*) Fecha elaboracion de propuestas : <strong>" + model.FechaMaximaPresentacionPropuestas + "</strong><br/>";
                ////noti.grabarNotificacionTodosUsuarios(Notificacion.asuntoCambiosConcurso, mensaje);
                //this._notificacionLogic.GrabarNotificacionTodosUsuarios(Notificacion.asuntoCambiosConcurso, mensaje);

                //this.modeloEntity.SaveChanges();
                return Json(new MensajeRespuesta("Se grabo la publicacion satisfactoriamente", true, publicacion.CODPUB));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo grabar la publicacion", false));
            }
        }

        public JsonResult AsignarBases(string bases, int publicacion)
        {
            try
            {
                var _bases = bases.Split(',');
                foreach (var _base in _bases)
                {
                    var descripcion = this._baseLogic.BuscarPorId(int.Parse(_base)).DESBAS;
                    var basePublicacion = new SAF_PUBLICACIONBASE() { CODBAS = int.Parse(_base), CODPUB = publicacion, DESPUBBAS = descripcion };
                    this._publicacionBaseLogic.Registrar(basePublicacion);
                }

                return Json(new MensajeRespuesta("Se realizaron las asignaciones", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo asignar", false));
            }
        }

        public JsonResult DesasignarBases(string bases)
        {
            try
            {
                var _bases = bases.Split(',');
                foreach (var _base in _bases)
                {
                    this._publicacionBaseLogic.Eliminar(int.Parse(_base));
                }

                return Json(new MensajeRespuesta("Se realizaron las desasignaciones", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo desasignar", false));
            }
        }

        public JsonResult ListarPublicaciones()
        {
            var lista = this._publicacionLogic.ListarTodos();
            var data = lista.Select(c => new string[] {
                c.CODPUB.ToString(),
                c.NUMPUB,
                WebHelper.GetShortDateString(c.FECMAXCONS),
                WebHelper.GetShortDateString(c.FECMAXCRECON),
                WebHelper.GetShortDateString(c.FECMAXPREPROP),
                WebHelper.GetShortDateString(c.FECMAXRESCONS),
                c.ESTPUB == Estado.Publicacion.Elaboracion.GetHashCode() ? "Elaboracion" : (c.ESTPUB == Estado.Publicacion.Publicado.GetHashCode() ? "Publicado" : (c.ESTPUB == Estado.Publicacion.Aprobado.GetHashCode() ? "Aprobado" : "Pendiente Aprobación")),
                c.ESTPUB.GetValueOrDefault().ToString()
            }).ToArray();
            return Json(data);
        }

        public JsonResult PublicarPublicacion(int id)
        {
            try
            {
                var result = this._publicacionLogic.PublicarPublicacion(id); //this._agenteConcursoPublicoMerito.PublicarPublicacion(id);

                string mensaje = string.Empty;

                mensaje = "Se registro una publicacion, debe tener en cuenta que las fechas mostradas son los limites para realizar cada accion: <br /><br />";
                mensaje = mensaje + "* Fecha máxima para ELABORAR CONSULTAS (Debe enviar las preguntas para que la CGR pueda responder) : <strong>" + result.FECMAXCRECON + "</strong><br/>";
                mensaje = mensaje + "* Fecha máxima para publicar la ABSOLUCION de CONSULTAS : <strong>" + result.FECMAXRESCONS + "</strong><br/>";
                mensaje = mensaje + "* Fecha máxima para PRESENTACION de PROPUESTAS (Debe enviar las propuestas para que la CGR pueda evaluarlas) : <strong>" + result.FECMAXPREPROP + "</strong><br/>";
                this._notificacionLogic.GrabarNotificacionTodosUsuarios(Notificacion.asuntoPublicacionConcurso, mensaje);


                return Json(new MensajeRespuesta("Se genero la publicacion Satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Ocurrio un inconveniente al realizar los calculos de experiencia y capacitacion de los auditores", true));
            }
        }

        public JsonResult EliminarPublicacion(int id)
        {
            try
            {
                //var publicacion = this.modeloEntity.SAF_PUBLICACION.Where(c => c.CODPUB == id).FirstOrDefault();
                //publicacion.ESTREG = "0";
                //this.modeloEntity.SaveChanges();
                this._publicacionLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino el registro satisfactoriamente", true));
            }
            catch (Exception)
            {

                return Json(new MensajeRespuesta("No se pudo elimino el registro", false));
            }
        }

        //public ActionResult CreateReportPublicacion(int id)
        //{
        //    var model = new ReporteCronograma();
        //    var cronogramaRpt = this._cronogramaLogic.CronogramaRpt(id);
        //    var entidadesCronogramaRpt = this._cronoEntidadLogic.ListarEntidadesCronogramaRpt(id);
        //    FillImageUrl(model, "logo_contraloria.png");
        //    model.cronograma = cronogramaRpt.FirstOrDefault();
        //    model.listaEntidades = entidadesCronogramaRpt;
        //    return this.ViewPdf("", "CreateReporteCronograma", model);
        //}

        //private void FillImageUrl(ReporteCronograma model, string imageName)
        //{
        //    string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    model.ImageUrl = url + "Content/" + imageName;
        //}

        public class ReportePublicacion { 
            
        }

        public PartialViewResult VerResultadoCortePublicacion(int id)
        {
            var model = new PublicacionViewModel();
            model.CodigoPublicacion = id;
            return PartialView("_ResultadoCorte", model);
        }

        public JsonResult ListarResultadoCortePublicacion(int id)
        {
            var lista = this._publicacionLogic.VerResultadoCorte(id);// this._agenteConcursoPublicoMerito.VerResultadoCorte(id);
            var data = lista.Select(c => new string[] { 
                c.NOMCOMAUD,
                c.NOMCAR.ToString(),
                c.CAPAPUNT.GetValueOrDefault().ToString(),
                c.EXPPUNT.GetValueOrDefault().ToString(),
                c.TOTALPUNT.GetValueOrDefault().ToString()
            }).ToArray();
            return Json(data);
        }

        public ActionResult DescargarReporte(int id)
        {
            var file = ObtenerPublicacionRPT(id);
            return File(file, "application/pdf", "rptPublicacion.pdf");
        }

        public Byte[] ObtenerPublicacionRPT(int id)
        {
            /* Carga de lista de datos */
            var publicacionRpt = this._publicacionLogic.PublicacionRpt(id);
            var basesPublicacionRpt = this._publicacionBaseLogic.ListarBasesPublicacionRpt(id);

            /* Creación de reporte */
            const string reportPath = "~/Reports/rptPublicacion.rdlc";
            var localReport = new LocalReport { ReportPath = Server.MapPath(reportPath) };

            /* Seteando el datasource */
            var dtPublicacion = new ReportDataSource("dtPublicacion") { Value = publicacionRpt };
            var dtBasePublicacion = new ReportDataSource("dtBasePublicacion") { Value = basesPublicacionRpt };

            localReport.DataSources.Add(dtPublicacion);
            localReport.DataSources.Add(dtBasePublicacion);
            //localReport.SubreportProcessing += ReportePropuestaSubreportProcessingEventHandler;
            localReport.Refresh();

            //Configuración del reporte           

            string deviceInfoA4 = "<DeviceInfo>" +
                                         "  <OutputFormat>A4</OutputFormat>" +
                                         "  <PageWidth>21cm</PageWidth>" +
                                         "  <PageHeight>29.7cm</PageHeight>" +
                                         "  <MarginTop>1cm</MarginTop>" +
                                         "  <MarginLeft>1cm</MarginLeft>" +
                                         "  <MarginRight>1cm</MarginRight>" +
                                         "  <MarginBottom>1cm</MarginBottom>" +
                                         "</DeviceInfo>";
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            var file = localReport.Render("pdf", deviceInfoA4, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return file;
        }


        private void FillImageUrl(ReporteConcursoPublico model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }

        public ActionResult CreateReporteConcurso(int id)
        {
            var model = new ReporteConcursoPublico();
            FillImageUrl(model, "logo_contraloria.png");
            var concurso = _publicacionLogic.BuscarPorId(id);
            model.NroConcurso = concurso.NUMPUB;
            model.FechaMaxConsultas = concurso.FECMAXCONS.GetValueOrDefault().ToString("dd/MM/yyyy");
            model.FechaMaxResponderConsultas = concurso.FECMAXRESCONS.GetValueOrDefault().ToString("dd/MM/yyyy");
            model.FechaMaxPropuestas = concurso.FECMAXPREPROP.GetValueOrDefault().ToString("dd/MM/yyyy");


            var basesPublicacion = this._publicacionBaseLogic.ListarPorPublicacion(id);

            foreach (var item in basesPublicacion)
	        {
                model.Entidades.Add(item.DESPUBBAS);
	        }
            

            return this.ViewPdf("", "CreateReportPublicacion", model);
        }


    }
}