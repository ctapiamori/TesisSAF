using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class InvitacionAuditorController : BaseController
    {
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafServicioAuditoriaLogic _servicioAuditoriaLogic;
        private readonly ISafInvitacionLogic _invitacionLogic;
        private readonly ISafInvitacionDetalleLogic _invitacionDetalleLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafGeneralLogic _generalLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;

        private readonly ISafPublicacionBaseLogic _publicacionYBasesLogic;

        public InvitacionAuditorController()
        {
            _publicacionLogic = new SafPublicacionLogic();
            _servicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            _invitacionLogic = new SafInvitacionLogic();
            _auditorLogic = new SafAuditorLogic();
            _generalLogic = new SafGeneralLogic();
            _invitacionDetalleLogic = new SafInvitacionDetalleLogic();
            _notificacionLogic = new SafNotificacionLogic();
            _publicacionYBasesLogic = new SafPublicacionBaseLogic();
        }

        public ActionResult Index()
        {
            var model = new InvitacionModel();
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            model.cboPublicaciones = (from c in publicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();
            return View(model);
        }


        public JsonResult listarBases(int idPub)
        {
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var Bases = publicaciones.Where(c => c.CODPUB == idPub);
            return Json(Bases);
        }

        public JsonResult listarServicios(int idBase)
        {
            var serviciosAuditoria = this._servicioAuditoriaLogic.ServiciosPorBase(idBase);// modelEntity.SAF_SERVICIOAUDITORIA.ToList().Where(c => c.CODBAS == idBase && c.ESTREG == "1");
            List<SelectListItem> lista = new List<SelectListItem>();
           
            foreach (var item in serviciosAuditoria)
            {
                var fechaInicio = item.FECINISERAUD.HasValue? item.FECINISERAUD.Value.ToString("dd/MM/yyyy") :  "";
                var fechaFin = item.FECFINSERAUD.HasValue? item.FECFINSERAUD.Value.ToString("dd/MM/yyyy") :  "";
                lista.Add(new SelectListItem() { Value = item.CODSERAUD.ToString(), Text = string.Format("{0} ::: {1}-{2}", item.PERSERAUD, fechaInicio, fechaFin) });
	        }
            var result = lista;
            return Json(result);
        }

        public PartialViewResult BusquedaAuditores(int idPub, int idSerAud)
        {
            var model = new InvitacionModel();
            model.codigoPublicacionBusqueda = idPub;
            model.codigoServicioAudBusqueda = idSerAud;
            return PartialView("_BusquedaAuditores", model);
        }

        public PartialViewResult AgendaAuditor(int idInvitacion)
        {

            var invitacion = this._invitacionLogic.BuscarPorId(idInvitacion);// this.modelEntity.SAF_INVITACION.ToList().Where(c => c.CODINV == idInvitacion).FirstOrDefault();
            var auditor = this._auditorLogic.BuscarPorId(invitacion.CODAUD.GetValueOrDefault());// this.modelEntity.SAF_AUDITOR.ToList().Where(c => c.CODAUD == invitacion.CODAUD).FirstOrDefault();
            var cargo = this._generalLogic.GetCargo(invitacion.CODCAR.GetValueOrDefault());// this.modelEntity.SAF_CARGO.ToList().Where(c => c.CODCAR == invitacion.CODCAR).FirstOrDefault();
            var model = new InvitacionModel();
            model.codigoInvitacionAgenda = idInvitacion;
            model.codigoAuditorAgenda = auditor.CODAUD;
            model.nomCompletoAuditor = string.Format("{0} {1}", auditor.NOMAUD, auditor.APEAUD);
            model.cargoInvitacionAuditor = cargo.NOMCAR;
            model.numeroHorasLaboral = 8;
            return PartialView("_AgendaAuditor", model);
        }

        public JsonResult ListarMejorEquipo(int idPub, int idSerAud)
        {

            var listadoMejorEquipo = this._publicacionLogic.ListarMejorEquipoAuditoria(idPub, idSerAud);// this.modelEntity.SP_SAF_MEJOREQUIPO(idPub, idSerAud).ToList();
            var data = listadoMejorEquipo.Select(c => new string[] { 
                c.CODAUD.GetValueOrDefault().ToString(),
                c.NOMCOMAUD,
                c.NOMCAR,
                c.EXPPUNT.GetValueOrDefault().ToString(),
                c.CAPAPUNT.GetValueOrDefault().ToString(),
                c.TOTALPUNT.GetValueOrDefault().ToString(),
                c.DISPON.GetValueOrDefault().ToString(),
                c.CODCAR.GetValueOrDefault().ToString()
            });

            return Json(data);

        }

        public JsonResult ListarAuditoresAptos(int idPub, int idSerAud)
        {
            var listadoAuditoresAptos = this._auditorLogic.ListarAuditoresAptosInvitar(idPub, idSerAud);// this.modelEntity.SP_SAF_AUDITORAPTOINVITAR(idPub, idSerAud);
            var data = listadoAuditoresAptos.Select(c => new string[] { 
                c.CODAUD.GetValueOrDefault().ToString(),
                c.NOMCOMAUD,
                c.NOMCAR,
                c.EXPPUNT.GetValueOrDefault().ToString(),
                c.CAPAPUNT.GetValueOrDefault().ToString(),
                c.TOTALPUNT.GetValueOrDefault().ToString(),
                "0",
                c.CODCAR.GetValueOrDefault().ToString()
            });

            return Json(data);
        }

        public JsonResult InvitarAuditores(int idPub, int idSerAud, string strAudCargo)
        {
            try
            {
                var responsable = (int)Session["sessionCodigoResponsableLogin"];
                var resultadoInvitarAuditor = this._invitacionLogic.InvitarAuditores(responsable, idPub, idSerAud, strAudCargo);// this.modelEntity.SP_SAF_INVITARAUDITORES(((int)Session["sessionCodigoResponsableLogin"]), idPub, idSerAud, strAudCargo).FirstOrDefault();
                if (resultadoInvitarAuditor.RESULTADO.Equals(0))
                    return Json(new MensajeRespuesta(resultadoInvitarAuditor.MENSAJE, false));
                else
                    return Json(new MensajeRespuesta(resultadoInvitarAuditor.MENSAJE, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Ocurrio un error al invitar a los auditores", false));
            }
        }

        public JsonResult ListadoInvitacionesPorSOA(int? idPub, int? idSerAud)
        {
            var responsable = (int)Session["sessionCodigoResponsableLogin"];
            var listado = this._invitacionLogic.ListarInvitacionesPublicacionSoa(idPub, idSerAud, responsable);// this.modelEntity.SP_SAF_INVITACION(idPub, idSerAud).Where(c => c.CODSOA == ((int)Session["sessionCodigoResponsableLogin"])).ToList();
            var data = listado.Select(c => new string[] {
                c.CODINV.ToString(),
                c.NOMCOMAUD,
                c.DNIAUD,
                c.DESBAS,
                c.PERSERAUD,
                c.NOMCAR,
                c.FECACEPINV.HasValue ? c.FECACEPINV.Value.ToShortDateString() : "",
                c.VALOR,
                c.ESTINV.GetValueOrDefault().ToString()
            });

            return Json(data);
        }

        public JsonResult EnviarInvitacionAuditor(int id)
        {
            try
            {
                var invitacion = this._invitacionLogic.BuscarPorId(id);// this.modelEntity.SAF_INVITACION.ToList().Where(c => c.CODINV == id).FirstOrDefault();

                var detalleInvitacion = this._invitacionDetalleLogic.ListarPorInvitacion(id);// this.modelEntity.SAF_INVITACIONDETALLE.ToList().Where(c => c.CODINV == id);
                if (!detalleInvitacion.Any())
                {
                    return Json(new MensajeRespuesta("Debe asignar al menos una fecha", false));
                }
                
                //var noti = new Helper.NotificacionAdmin();
                var mensaje = "La sociedad <strong>" + Session["sessionNombreCompletoUsuario"].ToString() + "</strong> identificado con el RUC " + Session["sessionUsuario"].ToString() + " le ha enviado una invitacion para pertenecer a su equipo de Auditoria";
                //noti.grabarNotificacionAuditor((int)invitacion.CODAUD, Notificacion.asuntoInvitacion, mensaje);
                this._notificacionLogic.GrabarNotificacionAuditor(invitacion.CODAUD.GetValueOrDefault(), Notificacion.asuntoInvitacion, mensaje);


                invitacion.ESTINV = (int)Estado.Invitacion.Enviada;
                //this.modelEntity.SaveChanges();
                this._invitacionLogic.Actualizar(invitacion);
                return Json(new MensajeRespuesta("Se envio la invitacion satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo enviar la invitacion", false));
            }
        }

        public JsonResult EliminarInvitacion(int id)
        {
            try
            {
                //var invitacion = this.modelEntity.SAF_INVITACION.ToList().Where(c => c.CODINV == id).FirstOrDefault();
                //invitacion.ESTREG = "0";//Estado.Auditoria.Inactivo.ToString();
                //this.modelEntity.SaveChanges();
                this._invitacionLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino la invitacion satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar la invitacion", false));
            }
        }


        public JsonResult ListadoDisponibilidadAuditor(int idAuditor, string fechaInicio, string fechaFin)
        {
            var responsable = (int)Session["sessionCodigoResponsableLogin"];
            var listado = this._auditorLogic.ListarDisponibilidad(idAuditor, responsable, fechaInicio, fechaFin);// this.modelEntity.SP_DISPONIBILIDADAUDITOR(idAuditor, (int)Session["sessionCodigoResponsableLogin"], fechaInicio, fechaFin).ToList();

            var data = listado.Select(c => new string[]{
                c.FECDIL.HasValue? c.FECDIL.Value.ToString("dd/MM/yyyy") : ""
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListadoFechasAsig(int idInvitacion)
        {
            var listado = this._invitacionDetalleLogic.ListarPorInvitacion(idInvitacion).OrderBy(c => c.FECINVDET);// this.modelEntity.SAF_INVITACIONDETALLE.ToList().Where(c => c.CODINV == idInvitacion && c.ESTREG == "1").OrderBy(c => c.FECINVDET);
            var data = listado.Select(c => new string[]{
                c.FECINVDET.HasValue ? c.FECINVDET.Value.ToShortDateString() : "",
                c.CODINVDET.ToString()
            }).ToArray();
            return Json(data);
        }

        public JsonResult RegistrarFechasAgendaAuditor(int idInvitacion, string fechas)
        {
            try
            {
                var resultado = this._invitacionLogic.RegistrarAgenda(idInvitacion, 8, fechas);// this.modelEntity.SP_SAF_AGENDAREGISTRAR(idInvitacion, 8, fechas).FirstOrDefault(); // por defecto 8
                if (resultado.RESULTADO.Equals(1))
                    return Json(new MensajeRespuesta(resultado.MENSAJE, true));
                else
                    return Json(new MensajeRespuesta("No se pudo registrar las fechas", false));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo registrar las fechas", false));
            }
        }

        public JsonResult EliminarFechasAgenda(int idInvitacion, string fechasAgendaAuditor)
        {
            try
            {
                var resultado = this._invitacionLogic.EliminarFechasInvitacion(idInvitacion, fechasAgendaAuditor);// this.modelEntity.SP_SAF_ELIMINARFECHASASIGINVITACION(idInvitacion, fechasAgendaAuditor).FirstOrDefault();
                if (resultado.RESULTADO.Equals(1))
                    return Json(new MensajeRespuesta(resultado.MENSAJE, true));
                else
                    return Json(new MensajeRespuesta("No se puede eliminar las fechas", false));
            }
            catch (Exception)
            {

                return Json(new MensajeRespuesta("Se produjo un error al eliminar las fechas de la agenda", false));
            }
        }
    }
}