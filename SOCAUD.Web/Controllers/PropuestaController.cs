using Newtonsoft.Json;
using ReportManagement;
using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Web.Helper;
using SOCAUD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
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

        public ReportePropuesta() {
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

        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafBaseLogic _baseLogic;
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafPropuestaLogic _propuestaLogic;
        private readonly ISafAuditoriaLogic _auditoriaLogic;
        private readonly ISafPropuestaEquipoLogic _propuestaEquipoLogic;
        private readonly ISafInvitacionDetalleLogic _invitacionDetalleLogic;
        private readonly ISafPropuestaEquipoDetalleLogic _propuestaEquipoDetalleLogic;
        private readonly ISafGeneralLogic _generalLogic;
        private readonly ISafPublicacionBaseLogic _publicacionYBasesLogic;
        private readonly ISafServicioAuditoriaLogic _servicioAuditoriaLogic;
        private readonly ISafServicioAuditoriaCargoLogic _safServicioAuditoriaCargoLogic;


        public PropuestaController()
        {
            _soaLogic = new SafSoaLogic();
            _publicacionLogic = new SafPublicacionLogic();
            _propuestaLogic = new SafPropuestaLogic();
            _auditoriaLogic = new SafAuditoriaLogic();
            _propuestaEquipoLogic = new SafPropuestaEquipoLogic();
            _invitacionDetalleLogic = new SafInvitacionDetalleLogic();
            _propuestaEquipoDetalleLogic = new SafPropuestaEquipoDetalleLogic();
            _generalLogic = new SafGeneralLogic();
            _publicacionYBasesLogic = new SafPublicacionBaseLogic();
            _servicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            _safServicioAuditoriaCargoLogic = new SafServicioAuditoriaCargoLogic();
            _baseLogic = new SafBaseLogic();
        }

        #region Creacion de Propuesto
        public ActionResult Index()
        {
            var model = new PropuestaModel();


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
            model.cboPublicaciones = result.ToList();
            return View(model);
        }


        public JsonResult listarBases(int idPub)
        {
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var Bases = publicaciones.Where(c => c.CODPUB == idPub);
            return Json(Bases);
        }


        public JsonResult CrearPropuesta(int idPub, int idBase)
        {
            try
            {
                var idSoa = (int)Session["sessionCodigoResponsableLogin"];
                var resut = this._propuestaLogic.CrearPropuesta(idPub,idBase, idSoa);// this.modelEntity.SP_SAF_CREARPROPUESTA(idPub, (int)Session["sessionCodigoResponsableLogin"]).FirstOrDefault();
                if (resut.RESULTADO.Equals(0))
                    return Json(new MensajeRespuesta(resut.MENSAJE, false));
                else
                    return Json(new MensajeRespuesta(resut.MENSAJE, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Se produjo un error no controlado al crear la propuesta", false));
            }
        }

        public JsonResult ListadoPropuestas(int? idPub, int? idBase)
        {
           
            var idSoa = (int)Session["sessionCodigoResponsableLogin"];
            var propuestas = this._propuestaLogic.ListarPropuestas(idPub, idBase, idSoa);// this.modelEntity.SP_SAF_PROPUESTAS().ToList().Where(c => c.CODPUB == idPub.GetValueOrDefault());
            //propuestas = propuestas.Where(c => c.CODSOA == (int)Session["sessionCodigoResponsableLogin"]).ToList();
            var data = propuestas.Select(c => new string[] { 
                c.CODPRO.ToString(),
                c.DESBAS,
                c.RETRECOTOTAL.ToString(),
                c.IGVTOTAL.ToString(),
                c.MONTVIATICO.ToString(),
                c.VALOR, // nombre estado prpuesta
                c.ESTPROP.GetValueOrDefault().ToString()
            }).ToArray();
            return Json(data);
        }

        public ActionResult VerPropuesta(int idPropuesta)
        {
            var propuesta = this._propuestaLogic.PropuestaPorId(idPropuesta);// this.modelEntity.SP_SAF_PROPUESTAS().ToList().Where(c => c.CODPRO == idPropuesta).FirstOrDefault();
            var model = new PropuestaModel();
            model.CODPRO = propuesta.CODPRO;
            model.codigoPropuestaSustento = propuesta.CODPRO;
            model.RAZSOCSOA = propuesta.RAZSOCSOA;
            model.RUCSOA = propuesta.RUCSOA;
            model.NOMREPLEGSOA = propuesta.NOMREPLEGSOA;
            model.CORREPLEGSOA = propuesta.CORREPLEGSOA;
            model.CELREPLEGSOA = propuesta.CELREPLEGSOA;
            model.TOTRETECOBASREQ = propuesta.TOTRETECOBASREQ;
            model.TOTIGVBASREQ = propuesta.TOTIGVBASREQ;
            model.TOTVIABASREQ = propuesta.TOTVIABASREQ;

            model.RETRECO = propuesta.RETRECO;
            model.RETRECOTOTAL = propuesta.RETRECOTOTAL;
            model.IGVTOTAL = propuesta.IGVTOTAL;
            model.MONTVIATICO = propuesta.MONTVIATICO;

            model.codArchivoFirmaInternacional = propuesta.CODARCFIRINT;
            model.codArchivoFirmaPCAOB = propuesta.CODARCFIRPCAOB;
            model.nombreArchivoFirmaInternacional = propuesta.NOMBLABELFIRINT;
            model.nombreArchivoFirmaPCAOB = propuesta.NOMBLABELFIRPCAOB;
            model.INDREQFIRINT = propuesta.INDREQFIRINT;
            model.INDREQFIRPCAOB = propuesta.INDREQFIRPCAOB;
            model.ESTPRO = propuesta.ESTPROP;
            return View(model);
        }

        public ActionResult LecturaPropuesta(int idPropuesta)
        {
            var propuesta = this._propuestaLogic.PropuestaPorId(idPropuesta);// this.modelEntity.SP_SAF_PROPUESTAS().ToList().Where(c => c.CODPRO == idPropuesta).FirstOrDefault();
            var model = new PropuestaModel();
            model.CODPRO = propuesta.CODPRO;
            model.codigoPropuestaSustento = propuesta.CODPRO;
            model.RAZSOCSOA = propuesta.RAZSOCSOA;
            model.RUCSOA = propuesta.RUCSOA;
            model.NOMREPLEGSOA = propuesta.NOMREPLEGSOA;
            model.CORREPLEGSOA = propuesta.CORREPLEGSOA;
            model.CELREPLEGSOA = propuesta.CELREPLEGSOA;
            model.TOTRETECOBASREQ = propuesta.TOTRETECOBASREQ;
            model.TOTIGVBASREQ = propuesta.TOTIGVBASREQ;
            model.TOTVIABASREQ = propuesta.TOTVIABASREQ;

            model.RETRECO = propuesta.RETRECO;
            model.RETRECOTOTAL = propuesta.RETRECOTOTAL;
            model.IGVTOTAL = propuesta.IGVTOTAL;
            model.MONTVIATICO = propuesta.MONTVIATICO;

            model.codArchivoFirmaInternacional = propuesta.CODARCFIRINT;
            model.codArchivoFirmaPCAOB = propuesta.CODARCFIRPCAOB;
            model.nombreArchivoFirmaInternacional = propuesta.NOMBLABELFIRINT;
            model.nombreArchivoFirmaPCAOB = propuesta.NOMBLABELFIRPCAOB;
            model.INDREQFIRINT = propuesta.INDREQFIRINT;
            model.INDREQFIRPCAOB = propuesta.INDREQFIRPCAOB;
            model.ESTPRO = propuesta.ESTPROP;
            return View(model);
        }

        public JsonResult GrabarRetribucionEconomica(int idProp, decimal retribucion, decimal igv, decimal totalretrib, decimal viatico)
        {

            try
            {
                var propuesta = this._propuestaLogic.BuscarPorId(idProp);// this.modelEntity.SAF_PROPUESTA.Where(c => c.CODPRO == idProp).FirstOrDefault();
                propuesta.RETRECO = retribucion;
                propuesta.IGVTOTAL = igv;
                propuesta.RETRECOTOTAL = totalretrib;
                propuesta.MONTVIATICO = viatico;
                //this.modelEntity.SaveChanges();
                this._propuestaLogic.Actualizar(propuesta);
                return Json(new MensajeRespuesta("Se registro la retribucion economica satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo registrar la retribucion economica", false));
            }
        }

        [HttpPost]
        public string guardarSustentoAdicional(PropuestaModel model)
        {
            try
            {
                var propuesta = this._propuestaLogic.BuscarPorId(model.codigoPropuestaSustento);// this.modelEntity.SAF_PROPUESTA.Where(c => c.CODPRO == model.codigoPropuestaSustento).FirstOrDefault();

                var filebeFirInter = new FileBe();

                if (model.archivoFirmaInternacional != null)
                {
                    filebeFirInter.NarcCodigo = model.codArchivoFirmaInternacional;
                    filebeFirInter.CarcNombre = model.nombreArchivoFirmaInternacional;
                    filebeFirInter.FileData = model.archivoFirmaInternacional;
                }

                var filebeFirPCAOB = new FileBe();

                if (model.archivoFirmaPCAOB != null)
                {
                    filebeFirPCAOB.NarcCodigo = model.codArchivoFirmaPCAOB;
                    filebeFirPCAOB.CarcNombre = model.nombreArchivoFirmaPCAOB;
                    filebeFirPCAOB.FileData = model.archivoFirmaPCAOB;
                }



                var idFirmaInter = Archivo.RegistrarArchivo(propuesta.CODARCFIRINT, filebeFirInter);
                var idFirmaPcaob = Archivo.RegistrarArchivo(propuesta.CODARCFIRINT, filebeFirPCAOB);

                propuesta.CODARCFIRINT = idFirmaInter;
                propuesta.CODARCFIRPCAOB = idFirmaPcaob;
                propuesta.NOMBLABELFIRINT = model.nombreArchivoFirmaInternacional;
                propuesta.NOMBLABELFIRPCAOB = model.nombreArchivoFirmaPCAOB;
                //this.modelEntity.SaveChanges();
                this._propuestaLogic.Actualizar(propuesta);

                //var id = Archivo.RegistrarArchivo(capacitacion.CODARC, filebe);

                return JsonConvert.SerializeObject(new MensajeRespuesta("Se guardó la sustentacion adicional satisfactoriamente", true));
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new MensajeRespuesta("No se pudo guardar la sustentacion adicional", false));
            }

        }

        public JsonResult ListarAuditorias(int idPropuesta)
        {
            var auditorias = this._auditoriaLogic.ListarAuditoriasPorPropuesta(idPropuesta);// this.modelEntity.SP_SAF_AUDITORIAS(idPropuesta).ToList();
            var data = auditorias.Select(c => new string[] { 
                c.CODAUDITORIA.ToString(),
                c.PERAUD,
                (c.FECINIAUDITORIA.HasValue)? c.FECINIAUDITORIA.Value.ToString("dd/MM/yyyy") : "",
                (c.FECFINAUDITORIA.HasValue)? c.FECFINAUDITORIA.Value.ToString("dd/MM/yyyy") : "",
                c.DESBAS
            }).ToArray();
            return Json(data);
        }

        public JsonResult VerEquipoAuditoria(int idAuditoria)
        {
            var equipoAuditoria = this._propuestaEquipoLogic.ListarEquipoAuditoria(idAuditoria);// this.modelEntity.SP_SAF_EQUIPOAUDITORIA(idAuditoria).ToList();
            var data = equipoAuditoria.Select(c => new string[] { 
                c.CODPROEQU.ToString(),
                c.PERAUD,
                c.DNIAUD,
                c.NOMCOMAUD,
                c.NOMCAR,
                c.CELAUD,
                c.CORAUD
            }).ToArray();
            return Json(data);
        }

        public PartialViewResult AsignarFechaEquipoPropuesta(int idPropuesta, int idEquipo)
        {
            var model = new EquipoAuditoriaModel();
            model.CODPROEQU = idEquipo;
            model.codigoPropuestaAsigFecha = idPropuesta;
            return PartialView("_AsignarFechaEquipo", model);
        }

        public PartialViewResult AsignarFechaEquipoPropuestaLec(int idPropuesta, int idEquipo)
        {
            var model = new EquipoAuditoriaModel();
            model.CODPROEQU = idEquipo;
            model.codigoPropuestaAsigFecha = idPropuesta;
            return PartialView("_AsignarFechaEquipoLec", model);
        }

        

        public JsonResult listadoFechasInvitadas(int idEquipo)
        {
            var fechasInvitadas = this._invitacionDetalleLogic.FechasInvitadas(idEquipo);// this.modelEntity.SP_SAF_FECHASINVITADAS(idEquipo).ToList();
            var data = fechasInvitadas.Select(c => new string[] { 
                c.FECINVDET.Value.ToString("dd/MM/yyyy")
            }).ToArray();
            return Json(data);
        }

        public JsonResult MostrarEquipoRequeridoServicioAuditoriaBase(int idAuditoria) { 
            var auditoriaProp = this._auditoriaLogic.BuscarPorId(idAuditoria);
            var servicioAuditoriaBase = _servicioAuditoriaLogic.BuscarPorId(auditoriaProp.CODSERAUD.GetValueOrDefault());

            var idServicio = servicioAuditoriaBase.CODSERAUD;

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

        public JsonResult listadoFechasAsignadas(int idEquipo)
        {
            var fechasAsignadas = this._propuestaEquipoDetalleLogic.ListarDetallePorEquipo(idEquipo);  //this.modelEntity.SAF_PROPEQUIPODETALLE.ToList().Where(c => c.CODPROEQU == idEquipo && c.ESTREG == "1").OrderBy(c => c.FECPROEQUIDET);
            var data = fechasAsignadas.Select(c => new string[] { 
                c.FECPROEQUIDET.Value.ToString("dd/MM/yyyy"),
                c.CODPROEQUDET.ToString()
            }).ToArray();
            return Json(data);
        }

        public JsonResult asignarFechasPropuesta(int idEquipo, string fechasAsgnar)
        {
            var result = this._propuestaEquipoDetalleLogic.AsignarFechasPropuesta(idEquipo, fechasAsgnar);// this.modelEntity.SP_SAF_ASIGNARFECHASPROPUESTA(idEquipo, fechasAsgnar).FirstOrDefault();
            if (result.RESULTADO.Equals(1))
            {
                return Json(new MensajeRespuesta(result.MENSAJE, true));
            }
            else
            {
                return Json(new MensajeRespuesta("No se pudo asignar las fechas", false));
            }
        }

        public JsonResult EliminarFechaAsignadas(int idPropuesta, string fechasAsignadas)
        {
            var result = this._propuestaEquipoDetalleLogic.EliminarFechasAsignadas(idPropuesta, fechasAsignadas);// this.modelEntity.SP_SAF_ELIMINARFECHASASIGPROP(idPropuesta, fechasAsignadas).FirstOrDefault();
            if (result.RESULTADO.Equals(1))
                return Json(new MensajeRespuesta(result.MENSAJE, true));
            else
                return Json(new MensajeRespuesta("No se pudo eliminar las fechas asignadas", false));
        }


        public JsonResult PresentarPropuesta(int idPropuesta)
        {
            try
            {
                var propuesta = this._propuestaLogic.BuscarPorId(idPropuesta);// this.modelEntity.SAF_PROPUESTA.Where(c => c.CODPRO == idPropuesta).FirstOrDefault();
                propuesta.ESTPROP = (int)Estado.Propuesta.Enviada;
                //this.modelEntity.SaveChanges();
                this._propuestaLogic.Actualizar(propuesta);
                return Json(new MensajeRespuesta("Se presento la propuesta satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo presentar la propuesta", false));
            }
        }

        public JsonResult EliminarPropuesta(int idProp) {
            try
            {
                this._propuestaLogic.Eliminar(idProp);
                return Json(new MensajeRespuesta("Elimino la propuesta satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar la propuesta", false));               
            }
        }

        #endregion

        #region Auditoria - Gantt
        public JsonResult ObtenerGanttAuditoria(int id)
        {
            var gantt = new List<Gantt>();
            var lista = this._propuestaEquipoDetalleLogic.ListarEquipoAuditoria(id);// modelEntity.SP_SAF_DETALLEEQUIPOPORAUDITORIA(id).ToList();
            if (lista.Any())
            {
                var listCar = lista.Select(x => new { NOMCAR = x.NOMCAR, NOMCOMAUD = x.NOMCOMAUD, CODPROEQU = x.CODPROEQU }).Distinct();
                var cargos = this._generalLogic.ListarCargos().ToList();// modelEntity.SAF_CARGO.ToList();
                cargos.ForEach(x => gantt.AddRange(listCar.Where(y => y.NOMCAR == x.NOMCAR).Select(y => new Gantt()
                {
                    name = string.Format("<span style='font-size: 70% !important;'>{0}</span>", x.NOMCAR.ToUpper()),
                    desc = string.Format("<span style='font-size: 65% !important;'>{0}</span>", y.NOMCOMAUD.ToUpper()),
                    values = lista.Where(z => z.NOMCAR == x.NOMCAR && z.CODPROEQU == y.CODPROEQU).Select(a => new GanttDet
                    {
                        from = a.FECPROEQUIDET.GetValueOrDefault().ToString("MM/dd/yyyy"),
                        to = a.FECPROEQUIDET.GetValueOrDefault().ToString("MM/dd/yyyy"),
                        label = "8 hrs",
                        customClass = "ganttGreen"
                    }).ToList()
                })));

            }
            else
            {
                gantt.Add(new Gantt()
                {
                    name = "",
                    desc = string.Format("<span style='font-size: 65% !important;'>{0}</span>", "NO POSEE REGISTROS"),
                    values = new List<GanttDet>()
                });
            }
            return Json(gantt.ToArray());
        }
        #endregion


        private void FillImageUrl(ReportePropuesta model, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.ImageUrl = url + "Content/" + imageName;
        }


        public ActionResult CreateReportePropuesta(int id) {

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
                propuestaReporte.ListaAuditoria.Add(new AuditoriaPropuesta() {
                    Periodo = item.PERAUD,
                    FechaFin = item.FECFINAUDITORIA.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    FechaInicio = item.FECINIAUDITORIA.GetValueOrDefault().ToString("dd/MM/yyyy")
                });
            }

            foreach (var item in equipo)
            {
                propuestaReporte.ListaEquipo.Add(new EquipoPropuesta() { 
                    NombreApellido = item.NOMAUD + " " + item.APEAUD,
                    DNI = item.DNIAUD,
                    Horas = item.HORAS.GetValueOrDefault()
                });
            }
            FillImageUrl(propuestaReporte, "logo_contraloria.png");
            return this.ViewPdf("", "CreateReportePropuesta", propuestaReporte);
            
        }
    }
}