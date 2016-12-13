using Microsoft.Reporting.WebForms;
using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Common.Helpers;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Helper;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafCronoEntidadLogic _cronoEntidadLogic;
        private readonly ISafBaseLogic _baseLogic;
        private readonly ISafGeneralLogic _safGeneralLogic;
        private readonly ISafServicioAuditoriaLogic _safServicioAuditoriaLogic;
        private readonly ISafServicioAuditoriaCargoLogic _safServicioAuditoriaCargoLogic;

        public BaseController()
        {
            this._cronogramaLogic = new SafCronogramaLogic();
            this._cronoEntidadLogic = new SafCronoEntidadLogic();
            this._baseLogic = new SafBaseLogic();
            this._safGeneralLogic = new SafGeneralLogic();
            this._safServicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            this._safServicioAuditoriaCargoLogic = new SafServicioAuditoriaCargoLogic();
        }

        // GET: Base
        public ActionResult Index()
        {
            var cronogramas = this._cronogramaLogic.ListarTodos();
            ViewBag.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() }).ToList();
            return View();
        }

        public ActionResult Registrar()
        {
            var cronogramas = this._cronogramaLogic.ListarTodos();

            var model = new BaseModel();
            model.FirmaPcaob = "N";
            model.FirmaInternacional = "N";
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() });
            model.Entidades = new List<SelectListItem>();
            model.EstadoBase = Estado.Bases.Elaboracion.GetHashCode();
            return View(model);
        }

        public ActionResult Editar(int id)
        {
            var _base = this._baseLogic.BuscarPorId(id);

            if (!_base.ESTBAS.GetValueOrDefault().Equals(Estado.Bases.Elaboracion.GetHashCode()))
                return RedirectToAction("View", new { id = id });

            var model = new BaseModel() { 
                Codigo = _base.CODBAS,
                Numero = _base.NUMBAS,
                FechaMaxPublicacion = WebHelper.GetShortDateString(_base.FECMAXCREPUBBAS),
                Descripcion = _base.DESBAS,
                TotalRetribucion = _base.TOTRETECOBAS.GetValueOrDefault(),
                TotalViaticos = _base.TOTVIABAS.GetValueOrDefault(),
                FirmaPcaob = _base.FIRPCAOBBAS,
                FirmaInternacional = _base.FIRINTBAS,
                Cronograma = _base.CODCRO.GetValueOrDefault(),
                CronoEntidad = _base.CODCROENT.GetValueOrDefault(),
                EstadoBase = _base.ESTBAS.GetValueOrDefault()
            };

            model.EstadoBaseDescripcion = this._safGeneralLogic.GetParametro(_base.ESTBAS.GetValueOrDefault()).NOMPAR;

            var cronogramas = this._cronogramaLogic.ListarTodos();
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString(), Selected = c.CODCRO.Equals(_base.CODCRO.GetValueOrDefault()) });

            var entidades = this._cronoEntidadLogic.ListarPorCronograma(_base.CODCRO.GetValueOrDefault());
            model.Entidades = entidades.Select(c => new SelectListItem() { Value = c.CODCROENT.ToString(), Text = c.DESCROENT, Selected = c.DESCROENT.Equals(_base.CODCROENT.GetValueOrDefault()) });
            return View(model);
        }

        [Route("Base/Editar/{idBase}/Servicio/{servicio}")]
        public ActionResult ServicioAuditoria(int idBase, int? servicio)
        {
            var model = new ServicioAuditoriaModel();
            var cargos = this._safGeneralLogic.ListarCargos();
            var _base = this._baseLogic.BuscarPorId(idBase);
            var entidad = _base.SAF_CRONOENTIDAD;
            var anioInicial = entidad.FECINICROENT.GetValueOrDefault().Year;
            var anioTermino = entidad.FECFINCROENT.GetValueOrDefault().Year;
            
            model.IdCodigoBase = idBase;
            model.NumeroBase = _base.NUMBAS;
            model.PeriodoBase = anioInicial == anioTermino ? anioInicial.ToString() : string.Format("{0} - {1}", anioInicial, anioTermino);
            model.EntidadBase = _base.DESBAS;
            model.RetribucionBase = _base.TOTRETECOBAS.GetValueOrDefault();

            model.Cargos = cargos.Select(c => new SelectListItem() { Value = c.CODCAR.ToString(), Text = c.NOMCAR });
            return View(model);
        }

        public JsonResult GrabarCargoEquipo(EquipoServicioAuditoriaModel model)
        {
            var cargo = new SAF_SERAUDCARGO()
            {
                CODSERAUD = model.IdServicioAuditoria,
                CODCAR = model.IdCargoServicioAuditoria,
                NUMMININTSERAUDCAR = model.CantidadIntegrantes,
                NUMMINHORPARSERAUDCAR = model.MinimoHoras
            };

            var experiencia = new SAF_SERAUDCAREXP()
            {
                NUMMINHORSERAUDCAREXP = model.MinimoHorasExperiencia
            };

            var capacitacion = new SAF_SERAUDCARCAP()
            {
                NUMMINHORSERAUDCAPCAP = model.MinimoHorasCapacitacion
            };

            try
            {
                this._safServicioAuditoriaCargoLogic.RegistrarCargoCompleto(cargo, experiencia, capacitacion);
                return Json(new MensajeRespuesta("Registro satisfactorio", true));
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }

        }

        public JsonResult ActualizarCargoEquipo(EquipoServicioAuditoriaModel model)
        {
            var cargo = new SAF_SERAUDCARGO()
            {
                CODSERAUDCAR = model.IdCargoServicioAuditoria,
                NUMMININTSERAUDCAR = model.CantidadIntegrantes,
                NUMMINHORPARSERAUDCAR = model.MinimoHoras
            };

            var experiencia = new SAF_SERAUDCAREXP()
            {
                CODSERAUDCAREXP = model.IdExperienciaServicioAuditoria,
                NUMMINHORSERAUDCAREXP = model.MinimoHorasExperiencia
            };

            var capacitacion = new SAF_SERAUDCARCAP()
            {
                CODSERAUDCARCAP = model.IdCapacitacionServicioAuditoria,
                NUMMINHORSERAUDCAPCAP = model.MinimoHorasCapacitacion
            };

            try
            {
                this._safServicioAuditoriaCargoLogic.ActualizarCargoCompleto(cargo, experiencia, capacitacion);
                return Json(new MensajeRespuesta("Actualización satisfactoria", true));
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
        }

        public JsonResult EliminarCargoEquipo(int id)
        {
            try
            {
                this._safServicioAuditoriaCargoLogic.EliminarCompleto(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }

        public JsonResult ListarServicios(int id)
        {
            //var 
            return Json(new { });
        }

        public JsonResult GrabarServicioAuditoria(ServicioAuditoriaModel servicio)
        {
            var servicioAuditoria = new SAF_SERVICIOAUDITORIA();
            servicioAuditoria.CODBAS = servicio.IdCodigoBase;
            servicioAuditoria.CODSERAUD = servicio.IdServicioAuditoria;
            servicioAuditoria.FECINISERAUD = Texto.GetDateTime(servicio.FechaInicio);
            servicioAuditoria.FECFINSERAUD = Texto.GetDateTime(servicio.FechaTermino);
            servicioAuditoria.RETECOSERAUD = servicio.RetribucionServicio;
            servicioAuditoria.VIASERAUD = servicio.ViaticosServicio;
            servicioAuditoria.IGVSERAUD = servicio.IgvServicio;

            try
            {
                var result = this._safServicioAuditoriaLogic.Registrar(servicioAuditoria);
                return Json(new MensajeRespuesta("Registro satisfactorio", true, result));
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
        }

        public JsonResult AgregarCargo(int idSerAud, int idCar, int canInt, int horPar)
        {
            //var cargoServicio = new SAF_SERAUDCARGO();
            var cargoServicio = this._safServicioAuditoriaCargoLogic.BuscarPorServicioCargo(idSerAud, idCar);
            if (cargoServicio == null) cargoServicio = new SAF_SERAUDCARGO();

            cargoServicio.CODSERAUD = idSerAud;
            cargoServicio.CODCAR = idCar;
            cargoServicio.NUMMININTSERAUDCAR = canInt;
            cargoServicio.NUMMINHORPARSERAUDCAR = horPar;

            try
            {
                var result = this._safServicioAuditoriaCargoLogic.Registrar(cargoServicio);
                return Json(new MensajeRespuesta("Registro satisfactorio", true, result));

            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
        }



        public ActionResult View(int id)
        {
            var _base = this._baseLogic.BuscarPorId(id);

            var model = new BaseModel()
            {
                Codigo = _base.CODBAS,
                Numero = _base.NUMBAS,
                FechaMaxPublicacion = WebHelper.GetShortDateString(_base.FECMAXCREPUBBAS),
                Descripcion = _base.DESBAS,
                TotalRetribucion = _base.TOTRETECOBAS.GetValueOrDefault(),
                TotalViaticos = _base.TOTVIABAS.GetValueOrDefault(),
                FirmaPcaob = _base.FIRPCAOBBAS,
                FirmaInternacional = _base.FIRINTBAS,
                Cronograma = _base.CODCRO.GetValueOrDefault(),
                CronoEntidad = _base.CODCROENT.GetValueOrDefault(),
                EstadoBase = _base.ESTBAS.GetValueOrDefault()
            };

            model.EstadoBaseDescripcion = this._safGeneralLogic.GetParametro(_base.ESTBAS.GetValueOrDefault()).NOMPAR;

            var cronogramas = this._cronogramaLogic.ListarTodos();
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString(), Selected = c.CODCRO.Equals(_base.CODCRO.GetValueOrDefault()) });

            var entidades = this._cronoEntidadLogic.ListarPorCronograma(_base.CODCRO.GetValueOrDefault());
            model.Entidades = entidades.Select(c => new SelectListItem() { Value = c.CODCROENT.ToString(), Text = c.DESCROENT, Selected = c.DESCROENT.Equals(_base.CODCROENT.GetValueOrDefault()) });
            return View(model);
        }

        public JsonResult ListarEntidadCronograma(int cronograma)
        {
            var entidades = this._cronoEntidadLogic.ListarPorCronograma(cronograma);
            return Json(entidades.Select(c => new SelectListItem() { Value = c.CODCROENT.ToString(), Text = c.DESCROENT }));
        }

        public JsonResult ListarBases(int cronograma)
        {
            var listado = this._baseLogic.BuscarPorCronograma(cronograma);
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

        public JsonResult SaveBase(BaseModel model)
        {
            var entidad = new SAF_BASE();
            try
            {
                if (!model.Codigo.Equals(0))
                    entidad = this._baseLogic.BuscarPorId(model.Codigo);

                entidad.FECMAXCREPUBBAS = WebHelper.GetDateTimeOrNull(model.FechaMaxPublicacion);
                entidad.TOTRETECOBAS = model.TotalRetribucion;
                entidad.TOTVIABAS = model.TotalViaticos;
                entidad.FIRPCAOBBAS = model.FirmaPcaob;
                entidad.FIRINTBAS = model.FirmaInternacional;
               
                if (model.Codigo.Equals(0))
                {
                    var numeroBase = string.Empty;

                    var correlativo = this._cronogramaLogic.NewCorrelativoBase(model.Cronograma);
                    var cronograma = this._cronogramaLogic.BuscarPorId(model.Cronograma);
                    numeroBase = cronograma.ANIOCRO.GetValueOrDefault().ToString() + "-" + correlativo.ToString("D5");


                    entidad.CODCROENT = model.CronoEntidad;
                    entidad.CODCRO = model.Cronograma;
                    entidad.DESBAS = this._cronoEntidadLogic.BuscarPorId(model.CronoEntidad).DESCROENT;
                    entidad.ESTBAS = Estado.Bases.Elaboracion.GetHashCode();
                    entidad.NUMBAS = numeroBase;
                    entidad.CORBAS = correlativo;
                    var result = this._baseLogic.Registrar(entidad);
                    return Json(new MensajeRespuesta("Registro satisfactorio", true, result));
                }
                else
                {
                    entidad.CODBAS = model.Codigo;
                    var result = this._baseLogic.Actualizar(entidad);
                    return Json(new MensajeRespuesta("Actualización satisfactoria", true, result));
                }
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
        }

        public JsonResult EliminarBase(int id)
        {
            try
            {
                this._baseLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }

        public ActionResult DescargarReporte(int id)
        {
            var file = ObtenerBaseRPT(id);
            return File(file, "application/pdf", "rptBase.pdf");
        }

        public Byte[] ObtenerBaseRPT(int id)
        {
            /* Carga de lista de datos */
            var baseRpt = this._baseLogic.BaseRpt(id);

            /* Creación de reporte */
            const string reportPath = "~/Reports/rptBase.rdlc";
            var localReport = new LocalReport { ReportPath = Server.MapPath(reportPath) };

            /* Seteando el datasource */
            var dtBase = new ReportDataSource("dtBase") { Value = baseRpt };

            localReport.DataSources.Add(dtBase);
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