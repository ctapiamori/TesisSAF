using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
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
    public class SolicitudAdminController : Controller
    {
        private readonly ISafSolicitudLogic _solicitudLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafGeneralLogic _generalLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;
        private readonly ISafSolCapacitacionLogic _solCapacitacionLogic;
        private readonly ISafSolExperienciaLogic _solExperienciaLogic;
        private readonly ISafWorkFlowLogic _safWorkflowLogic;

        public SolicitudAdminController()
        {
            _solicitudLogic = new SafSolicitudLogic();
            _soaLogic = new SafSoaLogic();
            _auditorLogic = new SafAuditorLogic();
            _generalLogic = new SafGeneralLogic();
            _notificacionLogic = new SafNotificacionLogic();
            _solCapacitacionLogic = new SafSolCapacitacionLogic();
            _solExperienciaLogic = new SafSolExperienciaLogic();
            this._safWorkflowLogic = new SafWorkFlowLogic();
        }

        // GET: SolicitudAdmin
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listarSolicitud()
        {
            var listado = this._solicitudLogic.ListarSolicitudesNoElaborados(); //modelEntity.VW_SAF_SOLICITUD.ToList().Where(c => c.ESTSOL != (int)Estado.Solicitud.Elaboracion);

            var data = listado.Select(c => new string[] { 
                c.CODSOL.ToString(),
                c.NUMSOL,
                c.NOMTIPSOL,
                c.NOMESTSOL,
                c.CODSOA.HasValue ? "S":"A"
            }).ToArray();

            return Json(data);
        }

        public ActionResult Flujo()
        {
            return View();
        }

        public JsonResult listarFlujos()
        {
            var listado = this._safWorkflowLogic.ListarTodos().ToList();

             

            var data = listado.Select(c => new string[] { 
                c.CODWORFLO.ToString(),
                c.DESTIPDOC,
                c.NOTWORFLO,
                WebHelper.GetShortDateString(c.FECREG),
                c.TIPDOC,
                c.ESTWORFLO.GetValueOrDefault().ToString(),
                c.CODDOC.GetValueOrDefault().ToString(),
                c.FLGNOTREP == "0" && c.TIPCARUSU.GetValueOrDefault().Equals(int.Parse(Session["tipoUsuario"].ToString())) ? "1" : "0",
                
            }).ToArray();

            return Json(data);
        }

        public ActionResult revisarSolicitudSOA(int id)
        {
            var model = new SoaModel();
            var infoSolicitud = this._solicitudLogic.BuscarPorId(id);// modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == id).FirstOrDefault();
            var infoSoa = this._soaLogic.BuscarPorId(infoSolicitud.CODSOA.GetValueOrDefault());// modelEntity.SAF_SOA.Where(c => c.CODSOA == infoSolicitud.CODSOA).FirstOrDefault();
            model.codSolicitud = infoSolicitud.CODSOL;
            model.numSolicitud = infoSolicitud.NUMSOL;
            model.codSoa = infoSoa.CODSOA;
            model.rucSoa = infoSoa.RUCSOA;
            model.razSocSoa = infoSoa.RAZSOCSOA;
            model.misSoa = infoSoa.MISSOA;
            model.visSoa = infoSoa.VISSOA;
            model.nomRepLegSoa = infoSoa.NOMREPLEGSOA;
            model.apeRepLegSoa = infoSoa.APEREPLEGSOA;
            model.corRepLegSoa = infoSoa.CORREPLEGSOA;
            model.telRepLegSoa = infoSoa.TELREPLEGSOA;
            model.celRepLegSoa = infoSoa.CELREPLEGSOA;
            model.numParSunSoa = infoSoa.NUMPARSUNARPSOA;
            model.codColConSoa = infoSoa.CODCOLCONSOA;
            model.firIntSoa = infoSoa.FIRINTSOA;
            model.firPcaobSoa = infoSoa.FIRPCAOBSOA;
            model.observacionSolicitud = infoSolicitud.OBSSOL;
            model.estadoSolicitud = infoSolicitud.ESTSOL.GetValueOrDefault();
            return View(model);
        }

        public ActionResult revisarSolicitudAuditor(int id)
        {
            var model = new AuditorModel();
            var infoSolicitud = this._solicitudLogic.BuscarPorId(id);// modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == id).FirstOrDefault();
            var infoAuditor = this._auditorLogic.BuscarPorId(infoSolicitud.CODAUD.GetValueOrDefault());// modelEntity.SAF_AUDITOR.Where(c => c.CODAUD == infoSolicitud.CODAUD).FirstOrDefault();
            model.codAud = infoAuditor.CODAUD;
            model.nomAud = infoAuditor.NOMAUD;
            model.apeComAud = infoAuditor.APEAUD;
            model.dniAud = infoAuditor.DNIAUD;
            model.fecNacAud = infoAuditor.FECNACAUD.GetValueOrDefault().ToShortDateString();
            model.sexAud = infoAuditor.SEXAUD;
            model.codDeparAud = infoAuditor.CODDEPAUD;
            model.codProvAud = infoAuditor.CODPROVAUD;
            model.codDisAud = infoAuditor.CODDISAUD;
            model.corAud = infoAuditor.CORAUD;
            model.telAud = infoAuditor.TELAUD;
            model.celAud = infoAuditor.CELAUD;
            model.codCerAud = infoAuditor.CODCERAUD;
            model.codigoSolicitud = infoSolicitud.CODSOL;
            model.codArchivo = infoAuditor.CODARC;
            model.nombreArchivo = infoAuditor.NOMBLABEL;
            model.estadoSolicitud = infoSolicitud.ESTSOL.GetValueOrDefault();
            var departamentos = this._generalLogic.ListarDepartamentos();
            var provincias = this._generalLogic.ListarProvincias();
            var distritos = this._generalLogic.ListarDistritos();
            model.cboDepartamento = (from c in departamentos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString(), Selected = (c.CODDEP == infoAuditor.CODDEPAUD) }).ToList();
            model.cboProvincia = (from c in provincias select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString(), Selected = (c.CODPROV == infoAuditor.CODPROVAUD) }).ToList();
            model.cboDistrito = (from c in distritos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString(), Selected = (c.CODDIS == infoAuditor.CODDISAUD) }).ToList();
            model.observacionSolicitud = infoSolicitud.OBSSOL;
            model.estadoSolicitud = infoSolicitud.ESTSOL.GetValueOrDefault();
            return View(model);
        }

        public JsonResult aceptarSolicitud(int id)
        {
            try
            {
                var infoSolicitud = this._solicitudLogic.BuscarPorId(id);// modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == id).FirstOrDefault();
                if (infoSolicitud.CODSOA.HasValue)
                {
                    infoSolicitud.ESTSOL = (int)Estado.Solicitud.Aprobado;
                    infoSolicitud.OBSSOL = "La solicitud de la Sociedad de Auditoria ha sido <strong>APROBADO</strong>. <br />Bienvenido al Sistema de Sociedades y Auditores!!!!";
                    //modelEntity.SaveChanges();
                    this._solicitudLogic.Actualizar(infoSolicitud);
                }
                else
                    actualizarResafAuditor(infoSolicitud);

                return Json(new MensajeRespuesta("Aprobo la solicitud satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo aprobar la solicitud", false));
            }
        }

        public ActionResult observarSolicitudSoa(SoaModel model)
        {
            var infoSolicitud = this._solicitudLogic.BuscarPorId(model.codSolicitud);// modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == model.codSolicitud).FirstOrDefault();


            //Helper.NotificacionAdmin noti = new Helper.NotificacionAdmin();
            //noti.grabarNotificacionSOA(infoSolicitud.CODSOA.Value, Notificacion.asuntoSolicitudObservada, Notificacion.bodySolicitudObservada);
            this._notificacionLogic.GrabarNotificacionSOA(infoSolicitud.CODSOA.Value, Notificacion.asuntoSolicitudObservada, Notificacion.bodySolicitudObservada);

            infoSolicitud.OBSSOL = model.observacionSolicitud;
            infoSolicitud.ESTSOL = (int)Estado.Solicitud.Observada;
            //modelEntity.SaveChanges();
            this._solicitudLogic.Actualizar(infoSolicitud);
            return RedirectToAction("Index", "SolicitudAdmin"); //View("revisarSolicitudSOA", infoSolicitud);           
        }

        public ActionResult observarSolicitudAuditor(AuditorModel model)
        {
            var infoSolicitud = this._solicitudLogic.BuscarPorId(model.codigoSolicitud);// modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == model.codigoSolicitud).FirstOrDefault();

            //Helper.NotificacionAdmin noti = new Helper.NotificacionAdmin();
            //noti.grabarNotificacionAuditor(infoSolicitud.CODAUD.Value, Notificacion.asuntoSolicitudObservada, Notificacion.bodySolicitudObservada);
            this._notificacionLogic.GrabarNotificacionAuditor(infoSolicitud.CODAUD.Value, Notificacion.asuntoSolicitudObservada, Notificacion.bodySolicitudObservada);

            infoSolicitud.OBSSOL = model.observacionSolicitud;
            infoSolicitud.ESTSOL = (int)Estado.Solicitud.Observada;
            //modelEntity.SaveChanges();
            this._solicitudLogic.Actualizar(infoSolicitud);
            return RedirectToAction("Index", "SolicitudAdmin"); //View("revisarSolicitudSOA", infoSolicitud);           
        }

        public void actualizarResafAuditor(SAF_SOLICITUD solicitud)
        {
            this._solicitudLogic.ActualizarResafAuditor(solicitud);
            //using (var scope = new TransactionScope())
            //{
            //    var infoSolicitud = modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == solicitud.CODSOL).FirstOrDefault();

            //    var capacSol = modelEntity.SAF_SOLCAPACITACION.Where(x => x.CODSOL == solicitud.CODSOL);
            //    var expSol = modelEntity.SAF_SOLEXPERIENCIA.Where(x => x.CODSOL == solicitud.CODSOL);
            //    var capacResaf = modelEntity.SAF_CAPACITACION.Where(x => x.CODAUD == solicitud.CODAUD);
            //    var expResaf = modelEntity.SAF_EXPERIENCIA.Where(x => x.CODAUD == solicitud.CODAUD);

            //    foreach (var item in capacResaf)
            //    {
            //        modelEntity.SAF_CAPACITACION.Remove(item);
            //        modelEntity.SaveChanges();
            //    }
            //    foreach (var item in expResaf)
            //    {
            //        modelEntity.SAF_EXPERIENCIA.Remove(item);
            //        modelEntity.SaveChanges();
            //    }

            //    foreach (var item in capacSol)
            //    {
            //        var resaf = new SAF_CAPACITACION
            //        {
            //            CODAUD = solicitud.CODAUD,
            //            DESCAP = item.DESSOLCAP,
            //            FECINICAP = item.FECINISOLCAP,
            //            FECFINCAP = item.FECFINSOLCAP,
            //            NUMHORCAP = item.NUMHORSOLCAP,
            //            FECREG = DateTime.Now,
            //            USUREG = "SYSTEM",
            //            ESTREG = "1",
            //            CODUNI = item.CODUNI,
            //            CODCAR = item.CODCAR,
            //            CODTIPCAPA = item.CODTIPCAPA,
            //            CODCATCAPA = item.CODCATCAPA,
            //            CODARC = item.CODARC,
            //            NOMBLABEL = item.NOMBLABEL
            //        };
            //        modelEntity.SAF_CAPACITACION.Add(resaf);
            //        modelEntity.SaveChanges();
            //    }
            //    foreach (var item in expSol)
            //    {
            //        var resaf = new SAF_EXPERIENCIA
            //        {
            //            CODAUD = solicitud.CODAUD,
            //            DESEXP = item.DESSOLEXP,
            //            FECINIEXP = item.FECINISOLEXP,
            //            FECFINEXP = item.FECFINSOLEXP,
            //            NUMHOREXP = item.NUMHORSOLEXP,
            //            FECREG = DateTime.Now,
            //            USUREG = "SYSTEM",
            //            ESTREG = "1",
            //            CODEMP = item.CODEMP,
            //            CODTIPEXP = item.CODTIPEXP,
            //            CODARC = item.CODARC,
            //            NOMBLABEL = item.NOMBLABEL
            //        };
            //        modelEntity.SAF_EXPERIENCIA.Add(resaf);
            //        modelEntity.SaveChanges();
            //    }

            //    infoSolicitud.ESTSOL = (int)Estado.Solicitud.Aprobado;
            //    infoSolicitud.OBSSOL = "La solicitud del Auditor ha sido <strong>APROBADO</strong>. <br />Bienvenido al Sistema de Sociedades y Auditores!!!!";
            //    modelEntity.SaveChanges();
            //    scope.Complete();
            //}

        }

        public JsonResult listarCapacitaciones(int id)
        {
            var especialidades = this._generalLogic.ListarCarreras();// modelEntity.SAF_CARRERA.ToList();
            var listado = this._solCapacitacionLogic.ListarPorSolicitud(id);// modelEntity.SAF_SOLCAPACITACION.Where(x => x.CODSOL == id).ToList();

            var data = listado.Select(c => new string[]{ 
                c.CODSOLCAP.ToString(),
                c.DESSOLCAP,
                especialidades.FirstOrDefault(x=>x.CODCAR == c.CODCAR).NOMCAR,
                c.FECINISOLCAP.HasValue ? c.FECINISOLCAP.Value.ToShortDateString():string.Empty,
                c.FECFINSOLCAP.HasValue ? c.FECFINSOLCAP.Value.ToShortDateString():string.Empty,                
                c.CODARC.HasValue ? c.CODARC.Value.ToString():string.Empty
            }).ToArray();

            return Json(data);
        }

        public JsonResult listarExperiencias(int id)
        {
            var empresas = this._generalLogic.ListarEmpresas();// modelEntity.SAF_EMPRESA.ToList();
            var listado = this._solExperienciaLogic.ListarPorSolicitud(id);// modelEntity.SAF_SOLEXPERIENCIA.Where(x => x.CODSOL == id).ToList();

            var data = listado.Select(c => new[] { 
                c.CODSOLEXP.ToString(),
                c.DESSOLEXP,
                empresas.FirstOrDefault(x=>x.CODEMP== c.CODEMP).RAZSOCEMP,
                c.FECINISOLEXP.HasValue ? c.FECINISOLEXP.Value.ToShortDateString():string.Empty,
                c.FECFINSOLEXP.HasValue ? c.FECFINSOLEXP.Value.ToShortDateString():string.Empty,                
                c.CODARC.HasValue ? c.CODARC.Value.ToString():string.Empty  
            }).ToArray();

            return Json(data);
        }
    }
}