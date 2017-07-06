using Microsoft.Reporting.WebForms;
using ReportManagement;
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


    public class BaseController : PdfViewController
    {
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafCronoEntidadLogic _cronoEntidadLogic;
        private readonly ISafBaseLogic _baseLogic;
        private readonly ISafGeneralLogic _safGeneralLogic;
        private readonly ISafEntidadLogic _safEntidadLogic;

        private readonly ISafServicioAuditoriaLogic _safServicioAuditoriaLogic;
        private readonly ISafServicioAuditoriaCargoLogic _safServicioAuditoriaCargoLogic;

        private readonly ISafWorkFlowLogic _workFlowLogic;

        public BaseController()
        {
            this._cronogramaLogic = new SafCronogramaLogic();
            this._cronoEntidadLogic = new SafCronoEntidadLogic();
            this._baseLogic = new SafBaseLogic();
            this._safGeneralLogic = new SafGeneralLogic();
            this._safEntidadLogic = new SafEntidadLogic();
            this._safServicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            this._safServicioAuditoriaCargoLogic = new SafServicioAuditoriaCargoLogic();
            this._workFlowLogic = new SafWorkFlowLogic();
        }

        public JsonResult infoEntidadCronograma(int idCronoEntidad) {
            var infoEntidadCronograma = this._cronoEntidadLogic.BuscarPorId(idCronoEntidad);
            return Json(new { 
                FechaIni = infoEntidadCronograma.FECINICROENT.Value.ToString("dd/MM/yyyy"),
                FechaFin = infoEntidadCronograma.FECFINCROENT.Value.ToString("dd/MM/yyyy"),
            });
        }

        // GET: Base
        public ActionResult Index()
        {
            var cronogramas = this._cronogramaLogic.ListarTodos().Where(c=>c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); // Aprobada
            ViewBag.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() }).ToList();
            return View();
        }

        public ActionResult Registrar()
        {
            var cronogramas = this._cronogramaLogic.ListarTodos().Where(c => c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); // Aprobada
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


            var _croEntidad = this._cronoEntidadLogic.BuscarPorId(_base.CODCROENT.Value);

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
                EstadoBase = _base.ESTBAS.GetValueOrDefault(),
                TotalIgv = _base.TOTIGVBAS,
                FecIniAuditoriaCronograma = _croEntidad.FECINICROENT.Value.ToString("dd/MM/yyyy"),
                FecFinAuditoriaCronograma = _croEntidad.FECFINCROENT.Value.ToString("dd/MM/yyyy"),
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

            var _croEntidad = this._cronoEntidadLogic.BuscarPorId(_base.CODCROENT.Value);


            model.FechaInicioSegunCronograma = _croEntidad.FECINICROENT.Value.ToString("dd/MM/yyyy");
            model.FechaFinSegunCronograma = _croEntidad.FECFINCROENT.Value.ToString("dd/MM/yyyy");

            var entidad = _base.SAF_CRONOENTIDAD;
            var anioInicial = entidad.FECINICROENT.GetValueOrDefault().Year;
            var anioTermino = entidad.FECFINCROENT.GetValueOrDefault().Year;
            
            model.IdCodigoBase = idBase;
            model.NumeroBase = _base.NUMBAS;
            model.PeriodoBase = anioInicial == anioTermino ? anioInicial.ToString() : string.Format("{0} - {1}", anioInicial, anioTermino);
            model.EntidadBase = _base.DESBAS;
            model.RetribucionBase = _base.TOTRETECOBAS.GetValueOrDefault();

            model.Cargos = cargos.Select(c => new SelectListItem() { Value = c.CODCAR.ToString(), Text = c.NOMCAR });

            if (servicio.HasValue && servicio != 0) {
                model.IdServicioAuditoria = servicio.Value;
                var infoServicio = this._safServicioAuditoriaLogic.BuscarPorId(servicio.Value);
                model.FechaInicio = (infoServicio.FECINISERAUD.HasValue)?infoServicio.FECINISERAUD.Value.ToString("dd/MM/yyyy") : "";
                model.FechaTermino = (infoServicio.FECFINSERAUD.HasValue) ? infoServicio.FECFINSERAUD.Value.ToString("dd/MM/yyyy") : "";
                model.RetribucionServicio = (infoServicio.RETECOSERAUD.HasValue) ? infoServicio.RETECOSERAUD.Value : 0;
                model.IgvServicio = (infoServicio.IGVSERAUD.HasValue) ? infoServicio.IGVSERAUD.Value : 0;
                model.ViaticosServicio = (infoServicio.VIASERAUD.HasValue) ? infoServicio.VIASERAUD.Value : 0;
            }

            return View(model);
        }


        public JsonResult GrabarServicioAuditoria(ServicioAuditoriaModel model) {

            var infoBase = this._baseLogic.BuscarPorId(model.IdCodigoBase);
            var infoCroEntidad = this._cronoEntidadLogic.BuscarPorId(infoBase.CODCROENT.Value);
            var infoCrono = this._cronogramaLogic.BuscarPorId(infoBase.CODCRO.Value);
            var listaServiciosAuditoria = this._safServicioAuditoriaLogic.ListarTodos().Where(c => c.CODBAS == model.IdCodigoBase);

            var sumaRetribucion = listaServiciosAuditoria.Where(c=>c.CODSERAUD != model.IdServicioAuditoria).Sum(c => c.RETECOSERAUD);

            sumaRetribucion = sumaRetribucion + model.RetribucionServicio;

            if (sumaRetribucion > infoBase.TOTRETECOBAS) {
                return Json(new MensajeRespuesta("La suma de retribuciones economicas no puede ser mayor a la retribucion economica ingresada en las Bases del Concurso.", false));
            }

            if (model.IdServicioAuditoria == 0)
            {
                var nuevoServicio = new SAF_SERVICIOAUDITORIA();
                nuevoServicio.CODBAS = model.IdCodigoBase;
                nuevoServicio.FECFINSERAUD = Convert.ToDateTime(model.FechaTermino);
                nuevoServicio.FECINISERAUD = Convert.ToDateTime(model.FechaInicio);
                nuevoServicio.RETECOSERAUD = model.RetribucionServicio;
                nuevoServicio.VIASERAUD = model.ViaticosServicio;
                nuevoServicio.IGVSERAUD = model.IgvServicio;
                nuevoServicio.PERSERAUD = model.PeriodoBase.ToString();
                var servicioGrabado = this._safServicioAuditoriaLogic.Registrar(nuevoServicio);
                return Json(new MensajeRespuesta("Se registro el servicio de Auditoria satisfactoriamente", true, servicioGrabado.CODSERAUD));
            }
            else {
                var servicioExiste = this._safServicioAuditoriaLogic.BuscarPorId(model.IdServicioAuditoria);
                servicioExiste.FECFINSERAUD = Convert.ToDateTime(model.FechaTermino);
                servicioExiste.FECINISERAUD = Convert.ToDateTime(model.FechaInicio);
                servicioExiste.RETECOSERAUD = model.RetribucionServicio;
                servicioExiste.VIASERAUD = model.ViaticosServicio;
                servicioExiste.IGVSERAUD = model.IgvServicio;
                servicioExiste.PERSERAUD = model.PeriodoBase.ToString();
                this._safServicioAuditoriaLogic.Actualizar(servicioExiste);
                return Json(new MensajeRespuesta("Se actualizo el servicio de Auditoria satisfactoriamente", true, model.IdServicioAuditoria));
            }
        }

        public ActionResult ViewCargoEquipo(int IdServicio, int? idCargoServicio)
        {
            var model = new CargoEquipoServicioAuditoriaModel();
            var listaCargos = this._safGeneralLogic.ListarCargos();

            model.IdServicioAuditoria = IdServicio;
            model.IdCargoServicioAuditoria = idCargoServicio.HasValue? idCargoServicio.Value : 0;
            model.CargosServicioAuditoria = (from c in listaCargos select new SelectListItem() { Text = c.NOMCAR, Value = c.CODCAR.ToString() });
            return PartialView("_cargoequipo", model);
        }

        public JsonResult GrabarCargoEquipo(CargoEquipoServicioAuditoriaModel model)
        {
            var cargo = new SAF_SERAUDCARGO()
            {
                CODSERAUD = model.IdServicioAuditoria,
                CODCAR = model.IdCargoSeleted,
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
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", false));
            }

        }

        public JsonResult ActualizarCargoEquipo(CargoEquipoServicioAuditoriaModel model)
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

        public JsonResult ListarServicios(int idBase)
        {
            var listado = this._safServicioAuditoriaLogic.ServiciosPorBase(idBase);
            var data = listado.Select(c => new string[]{ 
                c.CODSERAUD.ToString(),
                c.PERSERAUD,
                (c.FECINISERAUD.HasValue)? c.FECINISERAUD.Value.ToString("dd/MM/yyyy") : "",
                (c.FECFINSERAUD.HasValue)? c.FECFINSERAUD.Value.ToString("dd/MM/yyyy") : "",
                (c.RETECOSERAUD.HasValue)?c.RETECOSERAUD.Value.ToString(): "0",
                (c.VIASERAUD.HasValue)?c.VIASERAUD.Value.ToString(): "0"
            }).ToArray();

            return Json(data);
        }

        //public JsonResult GrabarServicioAuditoria(ServicioAuditoriaModel servicio)
        //{
        //    var servicioAuditoria = new SAF_SERVICIOAUDITORIA();
        //    servicioAuditoria.CODBAS = servicio.IdCodigoBase;
        //    servicioAuditoria.CODSERAUD = servicio.IdServicioAuditoria;
        //    servicioAuditoria.FECINISERAUD = Texto.GetDateTime(servicio.FechaInicio);
        //    servicioAuditoria.FECFINSERAUD = Texto.GetDateTime(servicio.FechaTermino);
        //    servicioAuditoria.RETECOSERAUD = servicio.RetribucionServicio;
        //    servicioAuditoria.VIASERAUD = servicio.ViaticosServicio;
        //    servicioAuditoria.IGVSERAUD = servicio.IgvServicio;

        //    try
        //    {
        //        var result = this._safServicioAuditoriaLogic.Registrar(servicioAuditoria);
        //        return Json(new MensajeRespuesta("Registro satisfactorio", true, result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
        //    }
        //}

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

            var mostrarWorkFlow = _workFlowLogic.MostrarWorkFlow(id, "B");

            model.CodigoWorkFlow = mostrarWorkFlow.Item1;
            model.FlgMostrarFlujoAprobacion = mostrarWorkFlow.Item2;
            return View(model);
        }

        public JsonResult ListarEntidadCronograma(int cronograma)
        {
            var entidades = this._cronoEntidadLogic.ListarPorCronograma(cronograma);
            return Json(entidades.Select(c => new SelectListItem() { Value = c.CODCROENT.ToString(), Text = c.DESCROENT }));
        }

        public JsonResult ListarBases(int? cronograma)
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
                c.ESTBAS.GetValueOrDefault().Equals(Estado.Bases.Elaboracion.GetHashCode()) ? "En Elaboración" : (c.ESTBAS.GetValueOrDefault().Equals(Estado.Bases.PendienteAprobacion.GetHashCode())? "Pendiente de aprobación" : "Aprobado"),
                c.ESTBAS.GetValueOrDefault().Equals(Estado.Bases.Elaboracion.GetHashCode()) ? "1" : "0",
                c.ESTBAS.GetValueOrDefault().ToString()

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
                entidad.FIRPCAOBBAS = (model.FirmaPcaob == null)? "N" : model.FirmaPcaob;
                entidad.FIRINTBAS = (model.FirmaInternacional == null)? "N" : model.FirmaInternacional;
                entidad.TOTIGVBAS = model.TotalIgv;
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
            FillImageUrl(model, "logo_contraloria.png");
            return this.ViewPdf("", "CreateReporteBase", model);
        }


        private void FillImageUrl(ReporteBase model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
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


        public JsonResult ListarEquipoRequerido(int idServicio)
        {
            var listado = this._safServicioAuditoriaCargoLogic.ListarCargosPorServicioAuditoria(idServicio);
            var data = listado.Select(c => new string[]{ 
                c.CODSERAUDCAR.ToString(),
                c.NOMCAR,
                c.NUMMININTSERAUDCAR.GetValueOrDefault().ToString(),
                c.NUMMINHORPARSERAUDCAR.GetValueOrDefault().ToString(),
                c.NUMMINHORSERAUDCAPCAP.GetValueOrDefault().ToString(),
                c.NUMMINHORSERAUDCAREXP.GetValueOrDefault().ToString()
            }).ToArray();

            return Json(data);
        }

    }
}