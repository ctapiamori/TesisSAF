using Newtonsoft.Json;
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
    public class SolicitudController : Controller
    {
        private readonly ISafSolicitudLogic _solicitudLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafGeneralLogic _generalLogic;
        private readonly ISafSolCapacitacionLogic _solCapacitacionLogic;
        private readonly ISafSolExperienciaLogic _solExperienciaLogic;

        public SolicitudController()
        {
            this._solicitudLogic = new SafSolicitudLogic();
            this._soaLogic = new SafSoaLogic();
            this._auditorLogic = new SafAuditorLogic();
            this._generalLogic = new SafGeneralLogic();
            this._solCapacitacionLogic = new SafSolCapacitacionLogic();
            this._solExperienciaLogic = new SafSolExperienciaLogic();
        }
        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listarSolicitud()
        {
           
            var listado = this._solicitudLogic.ListarSolicitudes();
            if (Convert.ToInt32(Session["sessionTipoUsuario"]) == (int)Tipo.TipoUsuarioExtranet.Auditor)
                listado = listado.Where(c => c.CODAUD == (int)Session["sessionCodigoResponsableLogin"]).ToList();
            else
                listado = listado.Where(c => c.CODSOA == (int)Session["sessionCodigoResponsableLogin"]).ToList();

            var data = listado.Select(c => new string[] { 
                c.CODSOL.ToString(),
                c.NUMSOL,
                c.NOMTIPSOL,
                c.NOMESTSOL,
                c.ESTSOL.GetValueOrDefault().ToString()
            }).ToArray();

            return Json(data);
        }

        public ActionResult editarSolicitudSOA(int id)
        {
            var model = new Models.SoaModel();
            var infoSolicitud = this._solicitudLogic.BuscarPorId(id); //modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == id).FirstOrDefault();
            var infoSoa = this._soaLogic.BuscarPorId(infoSolicitud.CODSOA.GetValueOrDefault()); //modelEntity.SAF_SOA.Where(c => c.CODSOA == infoSolicitud.CODSOA).FirstOrDefault();
            model.codSolicitud = infoSolicitud.CODSOL;
            model.numSolicitud = infoSolicitud.NUMSOL;
            model.codSoa = infoSoa.CODSOA;
            model.rucSoa = infoSoa.RUCSOA.Trim();
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
            model.observacionSolicitud = string.IsNullOrEmpty(infoSolicitud.OBSSOL) ? "No presenta observaciones" : infoSolicitud.OBSSOL;
            model.estadoSolicitud = infoSolicitud.ESTSOL.GetValueOrDefault();
            return View(model);
        }

        public ActionResult editarSolicitudAuditor(int id)
        {
            var model = new Models.AuditorModel();
            var infoSolicitud = this._solicitudLogic.BuscarPorId(id); //modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == id).FirstOrDefault();
            var infoAuditor = this._auditorLogic.BuscarPorId(infoSolicitud.CODAUD.GetValueOrDefault()); //modelEntity.SAF_AUDITOR.Where(c => c.CODAUD == infoSolicitud.CODAUD).FirstOrDefault();
            model.codAud = infoAuditor.CODAUD;
            model.nomAud = infoAuditor.NOMAUD;
            model.apeComAud = infoAuditor.APEAUD;
            model.dniAud = infoAuditor.DNIAUD.Trim();
            model.fecNacAud = infoAuditor.FECNACAUD.GetValueOrDefault().ToShortDateString();
            model.sexAud = infoAuditor.SEXAUD;

            model.codDeparAud = infoAuditor.CODDEPAUD;
            model.codProvAud = infoAuditor.CODPROVAUD;
            model.codProvAudSelect = infoAuditor.CODPROVAUD;
            model.codDisAud = infoAuditor.CODDISAUD;
            model.codDisAudSelect = infoAuditor.CODDISAUD;

            model.corAud = infoAuditor.CORAUD;
            model.telAud = infoAuditor.TELAUD;
            model.celAud = infoAuditor.CELAUD;
            model.codCerAud = infoAuditor.CODCERAUD;
            model.codigoSolicitud = infoSolicitud.CODSOL;
            model.codArchivo = infoAuditor.CODARC;
            model.nombreArchivo = infoAuditor.NOMBLABEL;
            model.estadoSolicitud = infoSolicitud.ESTSOL.GetValueOrDefault();
            model.observacionSolicitud = infoSolicitud.OBSSOL;
            var departamentos = this._generalLogic.ListarDepartamentos();
            model.cboDepartamento = (from c in departamentos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString(), Selected = (c.CODDEP == infoAuditor.CODDEPAUD) }).ToList();
            //model.cboDepartamento = (from c in modelEntity.SAF_DEPARTAMENTO.ToList().Where(c => c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString(), Selected = (c.CODDEP == infoAuditor.CODDEPAUD) }).ToList();

            //model.cboProvincia = (from c in modelEntity.SAF_PROVINCIA.ToList() select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString(), Selected = (c.CODPROV == infoAuditor.CODPROVAUD) }).ToList();
            //model.cboDistrito = (from c in modelEntity.SAF_DISTRITO.ToList() select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString(), Selected = (c.CODDIS == infoAuditor.CODDISAUD) }).ToList();
            return View(model);
        }

        public JsonResult CargarProvincia(int id)
        {
            //var lista = (from c in modelEntity.SAF_PROVINCIA.ToList().Where(c => c.CODDEP == id && c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            var provincias = this._generalLogic.ListarProvincias(id);
            var lista = (from c in provincias select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            return Json(lista);
        }

        public JsonResult CargarDistrito(int id)
        {
            //var lista = (from c in modelEntity.SAF_DISTRITO.ToList().Where(c => c.CODPROV == id && c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            var distritos = this._generalLogic.ListarDistritos(id);
            var lista = (from c in distritos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            return Json(lista);
        }


        [HttpPost]
        public string grabarRegistroSolicitudAuditor(AuditorModel model)
        {
            try
            {
                byte[] data = null;
                var filebe = new FileBe();
                if (model.archivoCertificadoAuditor != null)
                {
                    filebe.NarcCodigo = model.codArchivo;
                    filebe.CarcNombre = model.nombreArchivo;
                    filebe.FileData = model.archivoCertificadoAuditor;
                }

                var infoAuditor = this._auditorLogic.BuscarPorId(model.codAud);// modelEntity.SAF_AUDITOR.Where(c => c.CODAUD == model.codAud).FirstOrDefault();
                var id = Archivo.RegistrarArchivo(infoAuditor.CODARC, filebe);
                infoAuditor.CODDEPAUD = model.codDeparAud;
                infoAuditor.CODPROVAUD = model.codProvAud;
                infoAuditor.CODDISAUD = model.codDisAud;
                infoAuditor.ARCCERAUD = data;
                infoAuditor.CODCERAUD = model.codCerAud;
                infoAuditor.CODARC = id;
                infoAuditor.NOMBLABEL = model.nombreArchivo;
                infoAuditor.CORAUD = model.corAud;
                infoAuditor.CELAUD = model.celAud;
                infoAuditor.TELAUD = model.telAud;
                //modelEntity.SaveChanges();
                this._auditorLogic.Actualizar(infoAuditor);
                return JsonConvert.SerializeObject(new MensajeRespuesta("Se completo la informacion del Auditor Financiero", true));
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new MensajeRespuesta("No se pudo completar la informacion del Auditor Financiero", false));
                //return Json(new MensajeRespuesta("No se pudo completar la informacion del Auditor Financiero", false));
            }

        }

        public JsonResult grabarRegistroSolicitudSOA(SoaModel model)
        {
            try
            {
                var infoSOA = this._soaLogic.BuscarPorId(model.codSoa);// modelEntity.SAF_SOA.Where(c => c.CODSOA == model.codSoa).FirstOrDefault();
                infoSOA.NOMREPLEGSOA = model.nomRepLegSoa;
                infoSOA.APEREPLEGSOA = model.apeRepLegSoa;
                infoSOA.CORREPLEGSOA = model.corRepLegSoa;
                infoSOA.CELREPLEGSOA = model.celRepLegSoa;
                infoSOA.TELREPLEGSOA = model.telRepLegSoa;
                infoSOA.NUMPARSUNARPSOA = model.numParSunSoa;
                infoSOA.CODCOLCONSOA = model.codColConSoa;
                infoSOA.FIRINTSOA = model.firIntSoa;
                infoSOA.FIRPCAOBSOA = model.firPcaobSoa;
                //modelEntity.SaveChanges();
                this._soaLogic.Actualizar(infoSOA);
                return Json(new MensajeRespuesta("Se completo la informacion de la Sociedad Satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo completar la informacion de la Sociedad de Auditoria", false));
            }
        }


        public JsonResult enviarSolicitud(int idSol)
        {
            try
            {
                var infoSolicitud = this._solicitudLogic.BuscarPorId(idSol);// modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == idSol).FirstOrDefault();
                infoSolicitud.ESTSOL = (int)Estado.Solicitud.Enviada;
                //modelEntity.SaveChanges();
                this._solicitudLogic.Actualizar(infoSolicitud);
                return Json(new MensajeRespuesta("Se envio la solicitud satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo enviar la Solicitud de Inscripcion", false));
            }
        }

        public JsonResult crearNuevaSolicitud()
        {
            if (Convert.ToInt32(Session["sessionTipoUsuario"]) == (int)Tipo.TipoUsuarioExtranet.Auditor)
                return Json(crearSolicitudAuditor((int)Session["sessionCodigoResponsableLogin"]));

            return Json(crearSolicitudSoa((int)Session["sessionCodigoResponsableLogin"]));
        }

        public MensajeRespuesta crearSolicitudSoa(int codSoa)
        {
            var result = this._solicitudLogic.CrearSolicitudSoa(codSoa);// modelEntity.SP_SAF_CREARSOLICITUDSOA(codSoa).FirstOrDefault();
            return new MensajeRespuesta(result.MENSAJE, result.RESULTADO == 1, new { Codigo = result.CODIGO, Tipo = 1 });
        }

        public MensajeRespuesta crearSolicitudAuditor(int codAuditor)
        {
            var result = this._solicitudLogic.CrearSolicitudAuditor(codAuditor);// modelEntity.SP_SAF_CREARSOLICITUDAUDITOR(codAuditor).FirstOrDefault();
            return new MensajeRespuesta(result.MENSAJE, result.RESULTADO == 1, new { Codigo = result.CODIGO, Tipo = 2 });
        }

        #region Capacitacion
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

        public PartialViewResult nuevaCapacitacion(int id)
        {
            var model = new CapacitacionModel();
            model.codSol = id;
            var universidades = this._generalLogic.ListarUniversidades();
            var tipos = this._generalLogic.ListarParametricas(8);
            var categorias = this._generalLogic.ListarParametricas(10);
            var especialidades = this._generalLogic.ListarCarreras();
            model.Universidades = (from c in universidades select new SelectListItem() { Text = c.RAZSOCUNI, Value = c.CODUNI.ToString() }).ToList();
            model.Tipos = (from c in tipos select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR.ToString() }).ToList();
            model.Categorias = (from c in categorias select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR.ToString() }).ToList();
            model.Especialidades = (from c in especialidades select new SelectListItem() { Text = c.NOMCAR, Value = c.CODCAR.ToString() }).ToList();
            return PartialView("_capacitacion", model);
        }

        public PartialViewResult editarCapacitacion(int id)
        {
            var capacitacion = this._solCapacitacionLogic.BuscarPorId(id);// modelEntity.SAF_SOLCAPACITACION.FirstOrDefault(x => x.CODSOLCAP == id);
            var model = new CapacitacionModel();
            model.codSolCap = capacitacion.CODSOLCAP;
            model.desSolCap = capacitacion.DESSOLCAP;
            model.fechaInicioSolCap = string.Format("{0:dd/MM/yyyy}", capacitacion.FECINISOLCAP);
            model.fechaFinSolCap = string.Format("{0:dd/MM/yyyy}", capacitacion.FECFINSOLCAP);
            model.numHorasSolCap = capacitacion.NUMHORSOLCAP;
            model.codSol = capacitacion.CODSOL;
            model.codUni = capacitacion.CODUNI;
            model.codCar = capacitacion.CODCAR;
            model.codTipCapa = capacitacion.CODTIPCAPA;
            model.codCatCapa = capacitacion.CODCATCAPA;
            model.nombreArchivoCapa = capacitacion.NOMBLABEL;
            model.codArchivoCapa = capacitacion.CODARC;
            var universidades = this._generalLogic.ListarUniversidades();
            var tipos = this._generalLogic.ListarParametricas(8);
            var categorias = this._generalLogic.ListarParametricas(10);
            var especialidades = this._generalLogic.ListarCarreras();
            model.Universidades = (from c in universidades select new SelectListItem() { Text = c.RAZSOCUNI, Value = c.CODUNI.ToString(), Selected = (c.CODUNI == capacitacion.CODUNI) }).ToList();
            model.Tipos = (from c in tipos select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR.ToString(), Selected = (c.CODPAR == capacitacion.CODTIPCAPA) }).ToList();
            model.Categorias = (from c in categorias select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR.ToString(), Selected = (c.CODPAR == capacitacion.CODCATCAPA) }).ToList();
            model.Especialidades = (from c in especialidades select new SelectListItem() { Text = c.NOMCAR, Value = c.CODCAR.ToString(), Selected = (c.CODCAR == capacitacion.CODCAR) }).ToList();
            return PartialView("_capacitacion", model);
        }

        [HttpPost]
        public string guardarCapacitacion(CapacitacionModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                var capacitacion = new SAF_SOLCAPACITACION();
                var filebe = new FileBe();

                if (model.archivoCapaFile != null)
                {
                    filebe.NarcCodigo = model.codArchivoCapa;
                    filebe.CarcNombre = model.nombreArchivoCapa;
                    filebe.FileData = model.archivoCapaFile;
                }

                if (model.codSolCap.HasValue)
                    capacitacion = this._solCapacitacionLogic.BuscarPorId(model.codSolCap.Value); //modelEntity.SAF_SOLCAPACITACION.FirstOrDefault(x => x.CODSOLCAP == model.codSolCap);
                var id = Archivo.RegistrarArchivo(capacitacion.CODARC, filebe);

                capacitacion.DESSOLCAP = model.desSolCap;
                capacitacion.FECINISOLCAP = string.IsNullOrEmpty(model.fechaInicioSolCap) ? new DateTime?() : DateTime.Parse(model.fechaInicioSolCapYMD);
                capacitacion.FECFINSOLCAP = string.IsNullOrEmpty(model.fechaFinSolCap) ? new DateTime?() : DateTime.Parse(model.fechaFinSolCapYMD);
                capacitacion.NUMHORSOLCAP = model.numHorasSolCap;
                capacitacion.FECREG = DateTime.Now;
                capacitacion.USUREG = "SYSTEM";
                capacitacion.ESTREG = "1";
                capacitacion.CODSOL = model.codSol;
                capacitacion.CODUNI = model.codUni;
                capacitacion.CODCAR = model.codCar;
                capacitacion.CODTIPCAPA = model.codTipCapa;
                capacitacion.CODCATCAPA = model.codCatCapa;
                capacitacion.CODARC = id;
                capacitacion.NOMBLABEL = model.nombreArchivoCapa;
                //if (!model.codSolCap.HasValue)
                //    modelEntity.SAF_SOLCAPACITACION.Add(capacitacion);
                //modelEntity.SaveChanges();
                if (!model.codSolCap.HasValue)
                    this._solCapacitacionLogic.Registrar(capacitacion);
                else
                    this._solCapacitacionLogic.Actualizar(capacitacion);
                return JsonConvert.SerializeObject(new MensajeRespuesta("Se guardó la capacitación satisfactoriamente", true));
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new MensajeRespuesta("No se pudo guardar la capacitación", false));
            }

        }

        public JsonResult eliminarCapacitacion(int id)
        {
            try
            {
                //var capacitacion = modelEntity.SAF_SOLCAPACITACION.FirstOrDefault(x => x.CODSOLCAP == id);
                //modelEntity.SAF_SOLCAPACITACION.Remove(capacitacion);
                //modelEntity.SaveChanges();
                if (this._solCapacitacionLogic.Eliminar(id))
                    return Json(new MensajeRespuesta("Se eliminó la capacitación satisfactoriamente", true));
                else
                    return Json(new MensajeRespuesta("No se pudo eliminar la capacitación", false));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar la capacitación", false));
            }

        }
        #endregion

        #region Experiencia
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
        public PartialViewResult nuevaExperiencia(int id)
        {
            var model = new ExperienciaModel();
            model.codSol = id;

            var empresas = this._generalLogic.ListarEmpresas();
            var tipos = this._generalLogic.ListarParametricas(7);

            model.Empresas = (from c in empresas select new SelectListItem() { Text = c.RAZSOCEMP, Value = c.CODEMP.ToString() }).ToList();
            model.Tipos = (from c in tipos select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR.ToString() }).ToList();
            return PartialView("_experiencia", model);
        }

        public PartialViewResult editarExperiencia(int id)
        {
            var experiencia = this._solExperienciaLogic.BuscarPorId(id);// modelEntity.SAF_SOLEXPERIENCIA.FirstOrDefault(x => x.CODSOLEXP == id);
            var model = new ExperienciaModel();
            model.codSolExp = experiencia.CODSOLEXP;
            model.descSolExp = experiencia.DESSOLEXP;
            model.fechaInicioSolExp = string.Format("{0:dd/MM/yyyy}", experiencia.FECINISOLEXP);
            model.fechaFinSolExp = string.Format("{0:dd/MM/yyyy}", experiencia.FECFINSOLEXP);
            model.numHorasSolExp = experiencia.NUMHORSOLEXP;
            model.codSol = experiencia.CODSOL;
            model.codEmpresa = experiencia.CODEMP;
            model.codTipExp = experiencia.CODTIPEXP;
            model.nombreArchivoExp = experiencia.NOMBLABEL;
            model.codArchivoExp = experiencia.CODARC;

            var empresas = this._generalLogic.ListarEmpresas();
            var tipos = this._generalLogic.ListarParametricas(7);

            model.Empresas = (from c in empresas select new SelectListItem() { Text = c.RAZSOCEMP, Value = c.CODEMP.ToString() }).ToList();
            model.Tipos = (from c in tipos select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR.ToString() }).ToList();
            return PartialView("_experiencia", model);
        }

        [HttpPost]
        public string guardarExperiencia(ExperienciaModel model)
        {
            try
            {
                var experiencia = new SAF_SOLEXPERIENCIA();
                var filebe = new FileBe();

                if (model.archivoExpFile != null)
                {
                    filebe.NarcCodigo = model.codArchivoExp;
                    filebe.CarcNombre = model.nombreArchivoExp;
                    filebe.FileData = model.archivoExpFile;
                }

                if (model.codSolExp.HasValue)
                    experiencia = this._solExperienciaLogic.BuscarPorId(model.codSolExp.Value); //modelEntity.SAF_SOLEXPERIENCIA.FirstOrDefault(x => x.CODSOLEXP == model.codSolExp);
                var id = Archivo.RegistrarArchivo(experiencia.CODARC, filebe);

                experiencia.DESSOLEXP = model.descSolExp;
                experiencia.FECINISOLEXP = string.IsNullOrEmpty(model.fechaInicioSolExp) ? new DateTime?() : DateTime.Parse(model.fechaInicioSolExpYMD);
                experiencia.FECFINSOLEXP = string.IsNullOrEmpty(model.fechaFinSolExp) ? new DateTime?() : DateTime.Parse(model.fechaFinSolExpYMD);
                experiencia.NUMHORSOLEXP = model.numHorasSolExp;
                experiencia.FECREG = DateTime.Now;
                experiencia.USUREG = "SYSTEM";
                experiencia.ESTREG = "1";
                experiencia.CODSOL = model.codSol;
                experiencia.CODEMP = model.codEmpresa;
                experiencia.CODTIPEXP = model.codTipExp;
                experiencia.CODARC = id;
                experiencia.NOMBLABEL = model.nombreArchivoExp;
                //if (!model.codSolExp.HasValue)
                //    modelEntity.SAF_SOLEXPERIENCIA.Add(experiencia);
                //modelEntity.SaveChanges();
                if (!model.codSolExp.HasValue)
                    this._solExperienciaLogic.Registrar(experiencia);
                else
                    this._solExperienciaLogic.Actualizar(experiencia);
                return JsonConvert.SerializeObject(new MensajeRespuesta("Se guardó la experiencia satisfactoriamente", true));
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new MensajeRespuesta("No se pudo guardar la experiencia", false));
            }

        }

        public JsonResult eliminarExperiencia(int id)
        {
            try
            {
                //var experiencia = modelEntity.SAF_SOLEXPERIENCIA.FirstOrDefault(x => x.CODSOLEXP == id);
                //modelEntity.SAF_SOLEXPERIENCIA.Remove(experiencia);
                //modelEntity.SaveChanges();
                this._solExperienciaLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se eliminó la experiencia satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar la experiencia", false));
            }

        }
        #endregion
    }
}