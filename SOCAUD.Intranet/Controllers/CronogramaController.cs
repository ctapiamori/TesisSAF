using Microsoft.Reporting.WebForms;
using ReportManagement;
using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Helper;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{

    public class ReporteCronograma
    {
        
        public TcSAFCRONOGRAMARPT cronograma { get; set; }
        public IEnumerable<TcSAFCRONOENTIDADCRONORPT> listaEntidades { get; set; }
        public string ImageUrl { get; set; }

        public ReporteCronograma()
        {
            
            this.cronograma = new TcSAFCRONOGRAMARPT();
            this.listaEntidades = new List<TcSAFCRONOENTIDADCRONORPT>();
        }
    }


    public class CronogramaController : PdfViewController
    {
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafEntidadLogic _entidadLogic;
        private readonly ISafCronoEntidadLogic _cronoEntidadLogic;
        private readonly ISafGeneralLogic _safGeneralLogic;

        private readonly ISafWorkFlowLogic _safWorkFlowLogic;

        public CronogramaController()
        {
            this._cronogramaLogic = new SafCronogramaLogic();
            this._entidadLogic = new SafEntidadLogic();
            this._cronoEntidadLogic = new SafCronoEntidadLogic();
            this._safGeneralLogic = new SafGeneralLogic();
            this._safWorkFlowLogic = new SafWorkFlowLogic();
        }

        // GET: Cronograma
        public ActionResult Index()
        {
            var anios = new List<SelectListItem>();
            //for (int i = Variables.ANIO_INICIAL; i < DateTime.Now.Year + 1; i++)
            //{
            //    anios.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            //}
            var cronogramas = this._cronogramaLogic.ListarTodos();

            foreach (var item in cronogramas)
            {
                anios.Add(new SelectListItem() { Value = item.ANIOCRO.Value.ToString(), Text = item.ANIOCRO.Value.ToString() });
            }
            ViewBag.Anios = anios;

            
            return View();
        }

        public ActionResult Registrar()
        {
            var model = new CronogramaModel();

            model.Anio = DateTime.Now.Year;

            return View(model);
        }

        public ActionResult Configurar(int id)
        {
            var cronograma = this._cronogramaLogic.BuscarPorId(id);
            var listaFlujoAprobacion = this._safWorkFlowLogic.ListarTodos();

            listaFlujoAprobacion = listaFlujoAprobacion.Where(c => c.TIPDOC == "C" && c.CODDOC == id).ToList();

            if (!cronograma.ESTCRO.GetValueOrDefault().Equals(Estado.Cronograma.Elaboracion.GetHashCode()))
                return RedirectToAction("View", new { id = id });

            var model = new CronogramaModel();

            model.Codigo = cronograma.CODCRO;
            model.Anio = cronograma.ANIOCRO.GetValueOrDefault();
            model.FechaPublicacion = WebHelper.GetShortDateString(cronograma.FECPUBCRO);
            model.FechaMaximaCreacionBase = WebHelper.GetShortDateString(cronograma.FECMAXCREBASCRO);
            model.EstadoCronograma = this._safGeneralLogic.GetParametro(cronograma.ESTCRO.GetValueOrDefault()).NOMPAR;

            var entidades = this._entidadLogic.ListarTodos();
            ViewBag.Entidades = entidades.Select(c => new SelectListItem() { Value = c.CODENT.ToString(), Text = c.RAZSOCENT });

            model.Entidad = new CronoEntidadModel()
            {
                Cronograma = cronograma.CODCRO
            };

            if (listaFlujoAprobacion.Count() >= 1)
            {
                var codigoTipoCargoUsuario = Convert.ToInt32(Session["tipoUsuario"]);
                var workFlowDelTipoDeUsuario = listaFlujoAprobacion.LastOrDefault();

                if (workFlowDelTipoDeUsuario.TIPCARUSU == codigoTipoCargoUsuario)
                {
                    model.CodigoWorkFlow = workFlowDelTipoDeUsuario.CODWORFLO;
                    model.FlgMostrarFlujoAprobacion = "S";
                }else
                    model.FlgMostrarFlujoAprobacion = "N";
            }
            else
            {
                model.FlgMostrarFlujoAprobacion = "S";
            }

            return View(model);
        }

        public ActionResult View(int id)
        {

            var listaFlujoAprobacion = this._safWorkFlowLogic.ListarWorkFlowCompleto();
            listaFlujoAprobacion = listaFlujoAprobacion.Where(c => c.CODTIPDOC == "C" && c.CODDOC == id).ToList();


            var cronograma = this._cronogramaLogic.BuscarPorId(id);
            var model = new CronogramaModel();

            model.Codigo = cronograma.CODCRO;
            model.Anio = cronograma.ANIOCRO.GetValueOrDefault();
            model.FechaPublicacion = WebHelper.GetShortDateString(cronograma.FECPUBCRO);
            model.FechaMaximaCreacionBase = WebHelper.GetShortDateString(cronograma.FECMAXCREBASCRO);
            model.EstadoCronograma = this._safGeneralLogic.GetParametro(cronograma.ESTCRO.GetValueOrDefault()).NOMPAR;

            var entidades = this._entidadLogic.ListarTodos();
            ViewBag.Entidades = entidades.Select(c => new SelectListItem() { Value = c.CODENT.ToString(), Text = c.RAZSOCENT });

            model.Entidad = new CronoEntidadModel()
            {
                Cronograma = cronograma.CODCRO
            };


            if (listaFlujoAprobacion.Count() >= 1) // SI ES EL PRIMER PASO
            {
                var codigoTipoCargoUsuario = Convert.ToInt32(Session["tipoUsuario"]);
                var workFlowDelTipoDeUsuario = listaFlujoAprobacion.LastOrDefault();

                if (workFlowDelTipoDeUsuario.TIPCARUSU == codigoTipoCargoUsuario) // SI ES PARA ESTE TIPO DE USUARIO
                {
                    if (workFlowDelTipoDeUsuario.CODESTDOC == Estado.Cronograma.Aprobado.GetHashCode()) // SI AUN ESTA EN PROCESO Y NO APROBADO
                    {
                        model.FlgMostrarFlujoAprobacion = "N";
                    }
                    else {
                        model.CodigoWorkFlow = workFlowDelTipoDeUsuario.CODWORFLO;
                        model.FlgMostrarFlujoAprobacion = "S";
                    }

                }
                else
                    model.FlgMostrarFlujoAprobacion = "N";
            }
            else
            {
                model.FlgMostrarFlujoAprobacion = "S";
            }

            return View(model);
        }

        public JsonResult SaveCronograma(CronogramaModel model)
        {
            var entidad = new SAF_CRONOGRAMA();

            if (Convert.ToDateTime(model.FechaPublicacion) >= Convert.ToDateTime(model.FechaMaximaCreacionBase))
                return Json(new MensajeRespuesta("La fecha máxima para aprobar cronograma debe ser menor a la fecha máxima para crear Bases.", false));

            if (model.Codigo == 0) { 
                var cronogramaExiste = this._cronogramaLogic.ListarPorAnio(model.Anio);

                if (cronogramaExiste.Count()>0)
                    return Json(new MensajeRespuesta("Ya existe un crongrama para el año ingresado.", false));
            }

            try
            {
                if (!model.Codigo.Equals(0))
                    entidad = this._cronogramaLogic.BuscarPorId(model.Codigo);

                entidad.ANIOCRO = model.Anio;
                entidad.FECPUBCRO = WebHelper.GetDateTimeOrNull(model.FechaPublicacion);
                entidad.FECMAXCREBASCRO = WebHelper.GetDateTimeOrNull(model.FechaMaximaCreacionBase);

                if (model.Codigo.Equals(0))
                {
                    entidad.ESTCRO = 1;
                    var result = this._cronogramaLogic.Registrar(entidad);
                    return Json(new MensajeRespuesta("Registro satisfactorio", true, result));
                }
                else
                {
                    entidad.CODCRO = model.Codigo;
                    var result = this._cronogramaLogic.Actualizar(entidad);
                    return Json(new MensajeRespuesta("Actualización satisfactoria", true));
                }
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
            
        }

        public JsonResult EliminarCronograma(int id)
        {
            try
            {
                this._cronogramaLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }

        public JsonResult ListarCronogramas(int? anio)
        {
            var listado = this._cronogramaLogic.ListarPorAnioCompleto(anio);
            var data = listado.Select(c => new string[]{ 
                c.CODCRO.ToString(),
                c.ANIOCRO.ToString(),
                WebHelper.GetShortDateString(c.FECPUBCRO),
                WebHelper.GetShortDateString(c.FECMAXCREBASCRO),
                c.NOMPAR,
                c.ESTCRO.GetValueOrDefault().Equals(Estado.Cronograma.Elaboracion.GetHashCode()) ? "1" : "0",
                c.ESTCRO.GetValueOrDefault().ToString()
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarEntidadCronograma(int cronograma)
        {
            var listado = this._cronoEntidadLogic.ListarPorCronograma(cronograma);
            var data = listado.Select(c => new string[]{ 
                c.CODCROENT.ToString(),
                c.DESCROENT,
                WebHelper.GetShortDateString(c.FECINICROENT),
                WebHelper.GetShortDateString(c.FECFINCROENT),
                c.CODCRO.ToString(),
                c.CODENT.ToString()
            }).ToArray();

            return Json(data);
        }

        public JsonResult SaveCronoEntidad(CronoEntidadModel model)
        {
            var entidad = new SAF_CRONOENTIDAD();
            try
            {
                if (Convert.ToDateTime(model.FechaInicio) > Convert.ToDateTime(model.FechaTermino)) {
                    return Json(new MensajeRespuesta("Fecha final debe ser mayor a la fecha de inicio", false));
                }

                entidad.CODCRO = model.Cronograma;
                entidad.CODENT = model.Entidad;
                entidad.DESCROENT = this._entidadLogic.BuscarPorId(model.Entidad).RAZSOCENT;
                entidad.FECINICROENT = WebHelper.GetDateTimeOrNull(model.FechaInicio);
                entidad.FECFINCROENT = WebHelper.GetDateTimeOrNull(model.FechaTermino);

                if (model.Codigo.Equals(0))
                {
                    var result = this._cronoEntidadLogic.Registrar(entidad);
                    return Json(new MensajeRespuesta("Se registro satisfactoriamente", true));
                }
                else
                {
                    entidad.CODCROENT = model.Codigo;
                    var result = this._cronoEntidadLogic.Actualizar(entidad);
                    return Json(new MensajeRespuesta("Se actualizo satisfactoriamente", true));
                }
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error));
            }
        }

        public JsonResult EliminarEntidad(int id)
        {
            try
            {
                this._cronoEntidadLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }

        public ActionResult DescargarReporte(int id)
        {
            var file = ObtenerCronogramaRPT(id);
            return File(file, "application/pdf", "rptCronograma.pdf");
        }

        public ActionResult CreateReporteCronograma(int id) {
            var model = new  ReporteCronograma();
            var cronogramaRpt = this._cronogramaLogic.CronogramaRpt(id);
            var entidadesCronogramaRpt = this._cronoEntidadLogic.ListarEntidadesCronogramaRpt(id);
             FillImageUrl(model, "logo_contraloria.png");
            model.cronograma = cronogramaRpt.FirstOrDefault();
            model.listaEntidades = entidadesCronogramaRpt;
            return this.ViewPdf("", "CreateReporteCronograma", model);
        }


    

        private void FillImageUrl(ReporteCronograma model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }



        public Byte[] ObtenerCronogramaRPT(int id)
        {
            /* Carga de lista de datos */
            var cronogramaRpt = this._cronogramaLogic.CronogramaRpt(id);
            var entidadesCronogramaRpt = this._cronoEntidadLogic.ListarEntidadesCronogramaRpt(id);

            /* Creación de reporte */
            const string reportPath = "~/Reports/rptCronograma.rdlc";
            var localReport = new LocalReport { ReportPath = Server.MapPath(reportPath) };

            /* Seteando el datasource */
            var dtCronograma = new ReportDataSource("dtCronograma") { Value = cronogramaRpt };
            var dtEntidadCronograma = new ReportDataSource("dtEntidadCronograma") { Value = entidadesCronogramaRpt };

            localReport.DataSources.Add(dtCronograma);
            localReport.DataSources.Add(dtEntidadCronograma);
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
    }
}