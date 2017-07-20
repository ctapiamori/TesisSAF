using ReportManagement;
using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Data.Model;
using SOCAUD.Web.Helper;
using SOCAUD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{

    #region ABSOLUCION
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

    public class ReporteConsultaRespuestaItem
    {
        public string Soa { get; set; }
        public string Consulta { get; set; }
        public string Respuesta { get; set; }

        public ReporteConsultaRespuestaItem() { }
    }

    #endregion

    #region BASES

    public class ReporteBase
    {
        public TcSAFBASERPT bases { get; set; }
        public string RUCEntidad { get; set; }
        public string RazonSocial { get; set; }
        public string MisionEntidad { get; set; }
        public string VisionEntidad { get; set; }
        public string BaseLegalEntidad { get; set; }
        public string ActividadPrincipalEntidad { get; set; }
        public string DomicilioLegalEntidad { get; set; }
        public string PaginaWebEntidad { get; set; }

        public string ImageUrl { get; set; }
        public IEnumerable<TcSAFCRONOENTIDADCRONORPT> listaEntidades { get; set; }
        public ReporteBase()
        {
            this.bases = new TcSAFBASERPT();
            this.listaEntidades = new List<TcSAFCRONOENTIDADCRONORPT>();
        }
    }


    #endregion

    #region CONCURSO PUBLICO

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

    #endregion

    public class ConsultasPublicaController : PdfViewController
    {

        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafBaseLogic _baseLogic;

        private readonly ISafAbsolucionConsultaLogic _absolucionConsultaLogic;
        private readonly ISafConsultaLogic _consultaLogic;

        private readonly ISafCronoEntidadLogic _cronoEntidadLogic;
        private readonly ISafEntidadLogic _safEntidadLogic;

        private readonly ISafPublicacionBaseLogic _publicacionBaseLogic;
        public ConsultasPublicaController()
        {
            this._publicacionLogic = new SafPublicacionLogic();
            this._cronogramaLogic = new SafCronogramaLogic();
            this._absolucionConsultaLogic = new SafAbsolucionConsultaLogic();
            this._baseLogic = new SafBaseLogic();
            this._consultaLogic = new SafConsultaLogic();
            this._cronoEntidadLogic = new SafCronoEntidadLogic();
            this._safEntidadLogic = new SafEntidadLogic();
            this._publicacionBaseLogic = new SafPublicacionBaseLogic();
        }

        public ActionResult ConsultaConcurso()
        {
            var model = new ConsultaConcurso();
            var cronogramas = this._cronogramaLogic.ListarTodos().Where(c=>c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); // Aprobada
            model.cboCronograma = (from c in cronogramas select new SelectListItem() { Text = c.ANIOCRO.GetValueOrDefault().ToString(), Value = c.CODCRO.ToString() });
            return View(model);
        }

        public ActionResult Cronograma()
        {
            return View();
        }

        public ActionResult Bases()
        {
            var model = new ConsultaBases();
            var cronogramas = this._cronogramaLogic.ListarTodos().Where(c => c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); // Aprobada
            model.cboCronograma = (from c in cronogramas select new SelectListItem() { Text = c.ANIOCRO.GetValueOrDefault().ToString(), Value = c.CODCRO.ToString() });
            return View(model);
        }

        public ActionResult Absolucion()
        {
            return View();
        }


        public JsonResult ListarAbsolucionConsulta() {
            var listado = _absolucionConsultaLogic.ListarAbsolucionConsultasCompleto();
            var data = listado.Select(c => new string[]{ 
                c.CODABSCON.ToString(),
                c.NUMPUB,
                c.DESBAS,
                c.FECPUBABSOLUCION.HasValue? "": c.FECPUBABSOLUCION.Value.ToString("dd/MM/yyyy HH:mm:ss")
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarPublicaciones(int? idCronograma, string numeroPublicacion)
        {
            var lista = this._publicacionLogic.ListarTodos().ToList();

            if (idCronograma.HasValue && string.IsNullOrEmpty(numeroPublicacion))
                lista = lista.Where(c => c.CODCRO == idCronograma.Value).ToList();
            if (idCronograma.HasValue && !string.IsNullOrEmpty(numeroPublicacion))
                lista = lista.Where(c => c.CODCRO == idCronograma.Value && c.NUMPUB.Contains(numeroPublicacion)).ToList();
            if (!idCronograma.HasValue && !string.IsNullOrEmpty(numeroPublicacion))
                lista = lista.Where(c => c.NUMPUB.Contains(numeroPublicacion)).ToList();

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

        public JsonResult ListarCronogramas()
        {
            var listado = this._cronogramaLogic.ListarPorAnioCompleto(null).Where(c => c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); 
            var data = listado.Select(c => new string[]{ 
                c.CODCRO.ToString(),
                c.ANIOCRO.ToString(),
                WebHelper.GetShortDateString(c.FECPUBCRO),
                WebHelper.GetShortDateString(c.FECMAXCREBASCRO),
                c.NOMPAR
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarBases(int? cronograma)
        {
            var listado = this._baseLogic.BuscarPorCronograma(cronograma).Where(c=>c.ESTBAS == Estado.Bases.Aprobado.GetHashCode());
            var data = listado.Select(c => new string[]{ 
                c.CODBAS.ToString(),
                c.NUMBAS,
                WebHelper.GetShortDateString(c.FECMAXCREPUBBAS),
                c.DESBAS,
                c.TOTRETECOBAS.GetValueOrDefault().ToString(),
                c.TOTVIABAS.GetValueOrDefault().ToString(),
                WebHelper.GetBooleanString(c.FIRPCAOBBAS),
                WebHelper.GetBooleanString(c.FIRINTBAS),
                c.ESTBAS.GetValueOrDefault().Equals(Estado.Bases.Elaboracion.GetHashCode()) ? "1" : "0"

            }).ToArray();

            return Json(data);
        }


        public ActionResult CreateReporteAbsolucion(int id)
        {
            var model = new ReporteAbsolucionConsulta();
            var datosAbsolucion = this._absolucionConsultaLogic.BuscarPorId(id);
            var datosPublicacion = this._publicacionLogic.BuscarPorId(datosAbsolucion.CODPUB.GetValueOrDefault());
            var datosBase = this._baseLogic.BuscarPorId(datosAbsolucion.CODBASE.GetValueOrDefault());
            model.NombreEntidad = datosBase.DESBAS;
            FillImageUrlCONSULTA(model, "logo_contraloria.png");

            var lista = _consultaLogic.ListarConsultaPorPublicacion_Base(datosAbsolucion.CODPUB.GetValueOrDefault(), datosAbsolucion.CODBASE.GetValueOrDefault());

            var estadoRespondido = Estado.ConsultasPublicacion.Respondida.GetHashCode();

            lista = lista.Where(c => c.ESTCON == estadoRespondido);

            model.NombreEntidad = datosBase.DESBAS;
            model.NumeroPublicacion = datosPublicacion.NUMPUB;

            foreach (var item in lista)
            {
                model.ListaConsultas.Add(new ReporteConsultaRespuestaItem()
                {
                    Consulta = item.DESCON,
                    Respuesta = item.RESCON,
                    Soa = item.RAZSOCSOA
                });
            }

            return this.ViewPdf("", "CreateReporteAbsolucion", model);
        }

        private void FillImageUrlCONSULTA(ReporteAbsolucionConsulta model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }


        public ActionResult CreateReporteBase(int id)
        {
            var model = new ReporteBase();
            var baseRpt = this._baseLogic.BaseRpt(id);
            var infoBase = this._baseLogic.BuscarPorId(id);
            var infoCronoEntidad = this._cronoEntidadLogic.BuscarPorId(infoBase.CODCROENT.Value);
            var infoEntidad = this._safEntidadLogic.BuscarPorId(infoCronoEntidad.CODENT.Value);
            var entidadesCronogramaRpt = this._cronoEntidadLogic.ListarEntidadesCronogramaRpt(infoBase.CODCRO.Value);

            model.bases = baseRpt.FirstOrDefault();
            model.listaEntidades = entidadesCronogramaRpt;
            model.RazonSocial = infoEntidad.RAZSOCENT;
            model.RUCEntidad = infoEntidad.RUCENT;
            model.PaginaWebEntidad = string.IsNullOrEmpty(infoEntidad.PAGWEBENT) ? "NO HAY INFORMACION" : infoEntidad.PAGWEBENT;
            model.BaseLegalEntidad = string.IsNullOrEmpty(infoEntidad.BASLEGENT) ? "NO HAY INFORMACION" : infoEntidad.BASLEGENT;
            model.VisionEntidad = string.IsNullOrEmpty(infoEntidad.VISENT) ? "NO HAY INFORMACION" : infoEntidad.VISENT;
            model.MisionEntidad = string.IsNullOrEmpty(infoEntidad.MISENT) ? "NO HAY INFORMACION" : infoEntidad.MISENT;
            model.ActividadPrincipalEntidad = string.IsNullOrEmpty(infoEntidad.ACTPRIENT) ? "NO HAY INFORMACION" : infoEntidad.ACTPRIENT;
            model.DomicilioLegalEntidad = string.IsNullOrEmpty(infoEntidad.DOMLEGENT) ? "NO HAY INFORMACION" : infoEntidad.DOMLEGENT;
            FillImageUrlBASE(model, "logo_contraloria.png");
            return this.ViewPdf("", "CreateReporteBase", model);
        }


        private void FillImageUrlBASE(ReporteBase model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }


        public ActionResult CreateReporteConcurso(int id)
        {
            var model = new ReporteConcursoPublico();
            FillImageUrlCONCURSO(model, "logo_contraloria.png");
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

        private void FillImageUrlCONCURSO(ReporteConcursoPublico model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }
    }
}