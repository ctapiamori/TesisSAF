using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Common.Exceptions;
using SOCAUD.Data.Model;
using SOCAUD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class SolRegController : BaseController
    {
        private readonly ISafGeneralLogic _generalLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSolicitudLogic _solicitudLogic;
        public SolRegController()
        {
            _generalLogic = new SafGeneralLogic();
            _soaLogic = new SafSoaLogic();
            _auditorLogic = new SafAuditorLogic();
            _solicitudLogic = new SafSolicitudLogic();
        }

        // GET: SolReg
        public ActionResult Registro()
        {
            var model = new SolRegModel();
            var listaTipoEntidad = this._generalLogic.ListarRegistroTipoSolicitud();// this._agenteGestionSoaAuditor.listarRegistroTipoSolicitud();
            model.cboTipoSolicitud = (from c in listaTipoEntidad
                                      select new SelectListItem()
                                      {
                                          Text = c.NOMTIPSOL,
                                          Value = c.CODTIPSOL.ToString()
                                      });
            var departamentos = this._generalLogic.ListarDepartamentos();
            //model.auditor.cboDepartamento = (from c in modelEntity.SAF_DEPARTAMENTO.ToList().Where(c => c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString() }).ToList();
            model.auditor.cboDepartamento = (from c in departamentos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString() }).ToList();
            //model.auditor.cboProvincia = (from c in modelEntity.SAF_PROVINCIA.ToList() select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            //model.auditor.cboDistrito = (from c in modelEntity.SAF_DISTRITO.ToList() select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            return View(model);
        }

        public JsonResult CargarProvincia(int id)
        {
            var provincias = this._generalLogic.ListarProvincias(id);
            //var lista = (from c in modelEntity.SAF_PROVINCIA.ToList().Where(c => c.CODDEP == id && c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            var lista = (from c in provincias select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            return Json(lista);
        }

        public JsonResult CargarDistrito(int id)
        {
            var distritos = this._generalLogic.ListarDistritos(id);
            //var lista = (from c in modelEntity.SAF_DISTRITO.ToList().Where(c => c.CODPROV == id && c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            var lista = (from c in distritos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            return Json(lista);
        }

        public JsonResult GrabarSolicitudRegistro(SolRegModel model)
        {

            if (model.solicitud.codTipSol.GetValueOrDefault() == 1)
            { // SI ES SOA
                var existeUsuario = this._soaLogic.ExistUsuario(model.soa.nomUsu);// modelEntity.SAF_SOA.Where(c => c.NOMUSU.Equals(model.soa.nomUsu)).ToList().Any();
                if (existeUsuario)
                {
                    return Json(new MensajeRespuesta("El usuario que intenta registrar ya existe", false));
                }
            }
            else
            {
                var existeUsuario = this._auditorLogic.ExisteAuditor(model.auditor.nomUsu);// modelEntity.SAF_AUDITOR.Where(c => c.NOMUSU.Equals(model.auditor.nomUsu)).ToList().Any();
                if (existeUsuario)
                {
                    return Json(new MensajeRespuesta("El usuario que intenta registrar ya existe", false));
                }
            }

            var solicitud = new SAF_SOLICITUD() { 
                CODTIPSOL = model.solicitud.codTipSol,
                ESTSOL = (int)Estado.Solicitud.Elaboracion
            };


            //var entidad = new SolicitudInsActDTO();
            //entidad.Solicitud.CODTIPSOL = model.solicitud.codTipSol;
            //entidad.Solicitud.ESTSOL = (int)Estado.Solicitud.Elaboracion;
            //entidad.Soa.RAZSOCSOA = model.soa.razSocSoa;
            //entidad.Soa.RUCSOA = model.soa.rucSoa;
            //entidad.Soa.MISSOA = model.soa.misSoa;
            //entidad.Soa.VISSOA = model.soa.visSoa;
            //entidad.Soa.NOMUSU = model.soa.nomUsu;
            //entidad.Soa.PASUSU = model.soa.pasUsu;

            //entidad.Auditor.DNIAUD = model.auditor.dniAud;
            //entidad.Auditor.SEXAUD = model.auditor.sexAud;
            //entidad.Auditor.FECNACAUD = Convert.ToDateTime(model.auditor.fecNacAud);
            //entidad.Auditor.NOMAUD = model.auditor.nomAud;
            //entidad.Auditor.APEAUD = model.auditor.apeComAud;

            //entidad.Auditor.CODDEPAUD = model.auditor.codDeparAud;
            //entidad.Auditor.CODPROVAUD = model.auditor.codProvAud;
            //entidad.Auditor.CODDISAUD = model.auditor.codDisAud;
            //entidad.Auditor.CORAUD = model.auditor.corAud;
            //entidad.Auditor.TELAUD = model.auditor.telAud;
            //entidad.Auditor.CELAUD = model.auditor.celAud;
            //entidad.Auditor.NOMUSU = model.auditor.nomUsu;
            //entidad.Auditor.PASUSU = model.auditor.pasUsu;
            //var result = _agenteGestionSoaAuditor.GrabarSolicitudRegistro(entidad);
            //return Json(result);

            try
            {
                if (model.solicitud.codTipSol == (int)Tipo.TipoSolicitud.InscripcionSoa)
                {
                    var soa = new SAF_SOA()
                    {
                        RAZSOCSOA = model.soa.razSocSoa,
                        RUCSOA = model.soa.rucSoa,
                        MISSOA = model.soa.misSoa,
                        VISSOA = model.soa.visSoa,
                        NOMUSU = model.soa.nomUsu,
                        PASUSU = model.soa.pasUsu
                    };

                    this._solicitudLogic.GrabarSolicitudSoa(solicitud, soa);
                }
                else
                {

                    var auditor = new SAF_AUDITOR()
                    {
                        DNIAUD = model.auditor.dniAud,
                        SEXAUD = model.auditor.sexAud,
                        FECNACAUD = Convert.ToDateTime(model.auditor.fecNacAud),
                        NOMAUD = model.auditor.nomAud,
                        APEAUD = model.auditor.apeComAud,
                        CODDEPAUD = model.auditor.codDeparAud,
                        CODPROVAUD = model.auditor.codProvAud,
                        CODDISAUD = model.auditor.codDisAud,
                        CORAUD = model.auditor.corAud,
                        TELAUD = model.auditor.telAud,
                        CELAUD = model.auditor.celAud,
                        NOMUSU = model.auditor.nomUsu,
                        PASUSU = model.auditor.pasUsu
                    };

                    this._solicitudLogic.GrabarSolicitudAuditor(solicitud, auditor);

                }
                //var result = _servicesGestionSoaAuditorProxy.GrabarSolicitud(entidad);
                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (ExcepcionNegocio exn)
            {
                return Json(new MensajeRespuesta(exn.Message, false));
            }
            catch (Exception)
            {

                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
            }
        }
    }
}