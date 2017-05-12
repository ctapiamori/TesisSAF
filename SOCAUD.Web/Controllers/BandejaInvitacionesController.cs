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
    public class BandejaInvitacionesController : BaseController
    {
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafServicioAuditoriaLogic _servicioAuditoriaLogic;
        private readonly ISafInvitacionLogic _invitacionLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;
        private readonly ISafInvitacionDetalleLogic _invitacionDetalleLogic;

        public BandejaInvitacionesController()
        {
            this._publicacionLogic = new SafPublicacionLogic();
            this._servicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            this._invitacionLogic = new SafInvitacionLogic();
            this._notificacionLogic = new SafNotificacionLogic();
            this._invitacionDetalleLogic = new SafInvitacionDetalleLogic();
        }

        // GET: BandejaInvitaciones
        public ActionResult Index()
        {
            var model = new InvitacionModel();
            var publicaciones = this._publicacionLogic.ListarTodos();// modelEntity.SAF_PUBLICACION.ToList().Where(c => c.ESTREG == "1");
            model.cboPublicaciones = (from c in publicaciones select new SelectListItem() { Value = string.Format("{0}-{1}", c.CODPUB, c.CODBAS), Text = c.NUMPUB }).ToList();
            return View(model);
        }

        public JsonResult listarServicios(int idBase)
        {
            var serviciosAuditoria = this._servicioAuditoriaLogic.ServiciosPorBase(idBase);// modelEntity.SAF_SERVICIOAUDITORIA.ToList().Where(c => c.CODBAS == idBase && c.ESTREG == "1");
            var result = (from c in serviciosAuditoria select new SelectListItem() { Text = c.PERSERAUD, Value = c.CODSERAUD.ToString() });
            return Json(result);
        }

        public JsonResult ListadoInvitaciones(string idPub, int? idSerAud)
        {

            int codigoPub = 0;
            if (idPub != null && idPub!="")
                codigoPub = Convert.ToInt32( idPub.Split('-')[0]);
            //var invitaciones = this.modelEntity.SP_SAF_INVITACION(idPub, idSerAud).ToList().Where(c => c.ESTINV == (int)Estado.Invitacion.Enviada || c.ESTINV == (int)Estado.Invitacion.Cancelada || c.ESTINV == (int)Estado.Invitacion.Aceptado);
            var invitaciones = this._invitacionLogic.ListarInvitacionesPublicacion((codigoPub == 0) ? (int?)null : codigoPub, idSerAud, (int)Session["sessionCodigoResponsableLogin"]);
            //invitaciones = invitaciones.Where(c => c.CODAUD == (int)Session["sessionCodigoResponsableLogin"]).ToList();
            var data = invitaciones.Select(c => new string[] {
                c.CODINV.ToString(),
                c.RUCSOA,
                c.RAZSOCSOA,
                c.DESBAS,
                c.PERSERAUD,
                c.VALOR,
                c.ESTINV.GetValueOrDefault().ToString()
            });
            return Json(data);
        }

        public JsonResult AceptarInvitacion(int id)
        {
            try
            {
                var invitacion = this._invitacionLogic.BuscarPorId(id);// this.modelEntity.SAF_INVITACION.Where(c => c.CODINV == id).FirstOrDefault();
                //var noti = new Helper.NotificacionAdmin();
                var mensaje = "El auditor <strong>" + Session["sessionNombreCompletoUsuario"].ToString() + "</strong> identificado con el DNI " + Session["sessionUsuario"].ToString() + " ACEPTO su invitacion";

                //this.modelEntity.SP_SAF_ACEPTARINVITACION(id);
                //this.modelEntity.SP_SAF_ACEPTARINVITACION(id);
                this._invitacionLogic.AceptarInvitacion(id);
                //noti.grabarNotificacionSOA((int)invitacion.CODSOA, Notificacion.asuntoInvitacionAceptada, mensaje);
                this._notificacionLogic.GrabarNotificacionSOA((int)invitacion.CODSOA, Notificacion.asuntoInvitacionAceptada, mensaje);

                //invitacion.ESTINV = (int)Estado.Invitacion.Aceptado;
                //invitacion.FECACEPINV = DateTime.Now;
                //modelEntity.SaveChanges();
                return Json(new MensajeRespuesta("Se acepto la invitacion satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo aceptar la invitacion", false));
            }
        }

        public JsonResult CancelarInvitacion(int id)
        {
            try
            {
                var invitacion = this._invitacionLogic.BuscarPorId(id);// this.modelEntity.SAF_INVITACION.Where(c => c.CODINV == id).FirstOrDefault();

                //var noti = new Helper.NotificacionAdmin();
                var mensaje = "El auditor <strong>" + Session["sessionNombreCompletoUsuario"].ToString() + "</strong> identificado con el DNI " + Session["sessionUsuario"].ToString() + " CANCELO su invitacion";
                //noti.grabarNotificacionSOA((int)invitacion.CODSOA, Notificacion.asuntoInvitacionCancelado, mensaje);
                this._notificacionLogic.GrabarNotificacionSOA((int)invitacion.CODSOA, Notificacion.asuntoInvitacionCancelado, mensaje);

                invitacion.ESTINV = (int)Estado.Invitacion.Cancelada;
                invitacion.INDCANINV = "A";
                //modelEntity.SaveChanges();
                this._invitacionLogic.Actualizar(invitacion);

                return Json(new MensajeRespuesta("Se cancelo la invitacion satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo cancelar la invitacion", false));
            }
        }

        public PartialViewResult VerDetalleInvitacion(int id)
        {
            var model = new InvitacionModel();
            model.CODINV = id;
            return PartialView("_DetalleInvitacion", model);
        }

        public JsonResult ListadoFechasAsig(int idInvitacion)
        {
            var listado = this._invitacionDetalleLogic.ListarPorInvitacion(idInvitacion).OrderBy(c => c.FECINVDET);// this.modelEntity.SAF_INVITACIONDETALLE.ToList().Where(c => c.CODINV == idInvitacion && c.ESTREG == "1").OrderBy(c => c.FECINVDET);
            var data = listado.Select(c => new string[]{
                c.FECINVDET.HasValue ? c.FECINVDET.Value.ToShortDateString() : ""
            }).ToArray();
            return Json(data);
        }
    }
}