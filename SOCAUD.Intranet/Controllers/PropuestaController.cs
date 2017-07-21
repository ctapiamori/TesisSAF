using Microsoft.Reporting.WebForms;
using ReportManagement;
using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{

    public class ReportePropuesta
    {

        public string ImageUrl { get; set; }
        public string Publicacion { get; set; }
        public string SOA { get; set; }
        public string EntidadBase { get; set; }
        public decimal Retribucion { get; set; }
        public decimal IGVTotal { get; set; }
        public decimal RetribucionTotal { get; set; }

        public IList<AuditoriaPropuesta> ListaAuditoria { get; set; }
        public IList<EquipoPropuesta> ListaEquipo { get; set; }

        public ReportePropuesta()
        {
            this.ListaEquipo = new List<EquipoPropuesta>();
            this.ListaAuditoria = new List<AuditoriaPropuesta>();
        }


    }

    public class EquipoPropuesta
    {
        public string DNI { get; set; }
        public string NombreApellido { get; set; }
        public int Horas { get; set; }
    }

    public class AuditoriaPropuesta
    {
        public string Periodo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }

    public class PropuestaController : PdfViewController
    {
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafPropuestaLogic _propuestaLogic;
        private readonly ISafPropuestaEquipoLogic _propuestaEquipoLogic;

        private IEnumerable<TcEQUIPOAUDITORIARPT> equipoAuditoriaRpt;
        private readonly ISafAuditoriaLogic _auditoriaLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafBaseLogic _baseLogic;
        public PropuestaController()
        {
            _soaLogic = new SafSoaLogic();
            this._publicacionLogic = new SafPublicacionLogic();
            this._propuestaLogic = new SafPropuestaLogic();
            this._propuestaEquipoLogic = new SafPropuestaEquipoLogic();
            _baseLogic = new SafBaseLogic();
            _auditoriaLogic = new SafAuditoriaLogic();
        }

        // GET: Propuesta
        public ActionResult Index()
        {
            var publicaciones = this._publicacionLogic.ListarTodos();
            var listaPublicacionesPublicadas = publicaciones.Where(c => c.ESTPUB == 13).ToList(); // estado PUBLICADA
            var model = new PropuestaModel();
            model.cboPublicaciones = (from c in listaPublicacionesPublicadas select new SelectListItem() { Text = c.NUMPUB, Value = c.CODPUB.ToString() }).ToList();
            return View(model);
        }

        public JsonResult ListadoPropuestasCalificar(int? idPub)
        {
            var propuestas = this._propuestaLogic.ListadoPropuestasCalificar(idPub);// this.propues this.modelEntity.SP_SAF_PROPUESTAS().Where(c => (c.CODPUB == idPub || idPub == null) && (c.ESTPROP == (int)Estado.Propuesta.Enviada || c.ESTPROP == (int)Estado.Propuesta.Ganadora || c.ESTPROP == (int)Estado.Propuesta.Descalifica));
            var data = propuestas.Select(c => new string[] { 
                c.CODPRO.ToString(),
                string.Format("<strong>{0}</strong> - {1}", c.RUCSOA, c.RAZSOCSOA),
                c.NOMREPLEGSOA,
                c.CELREPLEGSOA,
                c.CORREPLEGSOA,
                c.DESBAS,
                c.RETRECO.ToString(),
                c.IGVTOTAL.ToString(),
                c.VALOR,
                c.PUNTAJETOTAL.GetValueOrDefault().ToString(),
                c.ESTPROP.GetValueOrDefault().ToString(),
                c.CODPUB.GetValueOrDefault().ToString()
            }).ToArray();
            return Json(data);
        }

        public JsonResult AsignarPropuestaComoGanadora(int idPropuesta, int idPublicacion)
        {
            try
            {
                var result = this._propuestaLogic.AsignarGanadorPropuesta(idPropuesta, idPublicacion);// this.modelEntity.SP_SAF_ASIGNARGANADORPROPUESTA(idPropuesta, idPublicacion).FirstOrDefault();
                if (result.RESULTADO.Equals(0))
                    return Json(new MensajeRespuesta(result.MENSAJE, false));
                else
                    return Json(new MensajeRespuesta(result.MENSAJE, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Ocurrio un error inesperado al asignar la propuesta", false));
            }

        }

        public ActionResult DescargarReportePropuesta(int id)
        {
            var file = ObtenerPropuestaRPT(id);
            return File(file, "application/pdf", "rptPropuesta.pdf");
        }

        public Byte[] ObtenerPropuestaRPT(int id)
        {
            /* Carga de lista de datos */
            var propuesta = this._propuestaLogic.ListarPropuestasRpt(id);// modelEntity.SP_SAF_PROPUESTA_RPT(id).ToList();
            equipoAuditoriaRpt = this._propuestaEquipoLogic.ListarEquipoAuditoriaRpt(id);// modelEntity.SP_SAF_EQUIPOAUDITORIA_RPT(id).ToList();

            /* Creación de reporte */
            const string reportPath = "~/Reports/rptPropuesta.rdlc";
            var localReport = new LocalReport { ReportPath = Server.MapPath(reportPath) };

            /* Seteando el datasource */
            var dtPropuesta = new ReportDataSource("dtPropuesta") { Value = propuesta };
            var dtEquipoAuditoria = new ReportDataSource("dtEquipoAuditoria") { Value = equipoAuditoriaRpt };

            localReport.DataSources.Add(dtPropuesta);
            localReport.DataSources.Add(dtEquipoAuditoria);
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

        private void FillImageUrlPROPUESTA(ReportePropuesta model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }

        public ActionResult CreateReportePropuesta(int id)
        {

            var propuestaReporte = new ReportePropuesta();
            var propuesta = this._propuestaLogic.BuscarPorId(id);

            var soainfo = this._soaLogic.BuscarPorId(propuesta.CODSOA.GetValueOrDefault());
            var publicacion = this._publicacionLogic.BuscarPorId(propuesta.CODPUB.GetValueOrDefault());
            var bases = this._baseLogic.BuscarPorId(propuesta.CODBAS.GetValueOrDefault());

            propuestaReporte.EntidadBase = bases.DESBAS;
            propuestaReporte.IGVTotal = propuesta.IGVTOTAL.GetValueOrDefault();
            propuestaReporte.Retribucion = propuesta.RETRECO.GetValueOrDefault();
            propuestaReporte.RetribucionTotal = propuesta.RETRECOTOTAL.GetValueOrDefault();
            propuestaReporte.Publicacion = publicacion.NUMPUB;
            propuestaReporte.SOA = soainfo.RAZSOCSOA;
            var auditorias = this._auditoriaLogic.ListarAuditoriasPorPropuesta(id);
            var equipo = this._propuestaLogic.ListarEquipoPropuesta(id);

            foreach (var item in auditorias)
            {
                propuestaReporte.ListaAuditoria.Add(new AuditoriaPropuesta()
                {
                    Periodo = item.PERAUD,
                    FechaFin = item.FECFINAUDITORIA.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    FechaInicio = item.FECINIAUDITORIA.GetValueOrDefault().ToString("dd/MM/yyyy")
                });
            }

            foreach (var item in equipo)
            {
                propuestaReporte.ListaEquipo.Add(new EquipoPropuesta()
                {
                    NombreApellido = item.NOMAUD + " " + item.APEAUD,
                    DNI = item.DNIAUD,
                    Horas = item.HORAS.GetValueOrDefault()
                });
            }
            FillImageUrlPROPUESTA(propuestaReporte, "logo_contraloria.png");
            return this.ViewPdf("", "CreateReportePropuesta", propuestaReporte);

        }
    }
}