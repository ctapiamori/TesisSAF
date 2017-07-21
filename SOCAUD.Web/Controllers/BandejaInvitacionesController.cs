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

        private readonly ISafPublicacionBaseLogic _publicacionYBasesLogic;

        private readonly ISafBaseLogic _baseLogic;
        public BandejaInvitacionesController()
        {
            this._publicacionLogic = new SafPublicacionLogic();
            this._servicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            this._invitacionLogic = new SafInvitacionLogic();
            this._notificacionLogic = new SafNotificacionLogic();
            this._invitacionDetalleLogic = new SafInvitacionDetalleLogic();

            _publicacionYBasesLogic = new SafPublicacionBaseLogic();

            _baseLogic = new SafBaseLogic();
        }

        // GET: BandejaInvitaciones
        public ActionResult Index()
        {
            var model = new InvitacionModel();
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




        public JsonResult listarServicios(int idBase)
        {
            var serviciosAuditoria = this._servicioAuditoriaLogic.ServiciosPorBase(idBase);// modelEntity.SAF_SERVICIOAUDITORIA.ToList().Where(c => c.CODBAS == idBase && c.ESTREG == "1");
            List<SelectListItem> lista = new List<SelectListItem>();

            foreach (var item in serviciosAuditoria)
            {
                var fechaInicio = item.FECINISERAUD.HasValue ? item.FECINISERAUD.Value.ToString("dd/MM/yyyy") : "";
                var fechaFin = item.FECFINSERAUD.HasValue ? item.FECFINSERAUD.Value.ToString("dd/MM/yyyy") : "";
                lista.Add(new SelectListItem() { Value = item.CODSERAUD.ToString(), Text = string.Format("{0} ::: {1}-{2}", item.PERSERAUD, fechaInicio, fechaFin) });
            }
            var result = lista;
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

                var publicacion = this._publicacionLogic.BuscarPorId(invitacion.CODPUB.GetValueOrDefault());
                var ServAud = this._servicioAuditoriaLogic.BuscarPorId(invitacion.CODSERAUD.GetValueOrDefault());
                var baseinfo = this._baseLogic.BuscarPorId(ServAud.CODBAS.GetValueOrDefault());

                //var noti = new Helper.NotificacionAdmin();
                var mensaje = "El auditor <strong>" + Session["sessionNombreCompletoUsuario"].ToString() + "</strong> identificado con el DNI " + Session["sessionUsuario"].ToString() + " ACEPTO su invitación para la auditoria: <br/><br/>";
                mensaje = mensaje + "<strong>Publicación:</strong>" + publicacion.NUMPUB + "<br/>";
                mensaje = mensaje + "<strong>Entidad:</strong>" + baseinfo.DESBAS + "<br/>";
                mensaje = mensaje + "<strong>Periodo:</strong>" + ServAud.FECINISERAUD.GetValueOrDefault().ToString("dd/MM/yyyy") + " - " + ServAud.FECFINSERAUD.GetValueOrDefault().ToString("dd/MM/yyyy") + "<br/>";

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

                var publicacion = this._publicacionLogic.BuscarPorId(invitacion.CODPUB.GetValueOrDefault());
                var ServAud = this._servicioAuditoriaLogic.BuscarPorId(invitacion.CODSERAUD.GetValueOrDefault());
                var baseinfo = this._baseLogic.BuscarPorId(ServAud.CODBAS.GetValueOrDefault());


                //var noti = new Helper.NotificacionAdmin();
                var mensaje = "El auditor <strong>" + Session["sessionNombreCompletoUsuario"].ToString() + "</strong> identificado con el DNI " + Session["sessionUsuario"].ToString() + " CANCELO la invitacion para el concurso: <br/><br/>";
                mensaje = mensaje + "<strong>Publicación:</strong>" + publicacion.NUMPUB + "<br/>";
                mensaje = mensaje + "<strong>Entidad:</strong>" + baseinfo.DESBAS + "<br/>";
                mensaje = mensaje + "<strong>Periodo:</strong>" + ServAud.FECINISERAUD.GetValueOrDefault().ToString("dd/MM/yyyy") + " - " + ServAud.FECFINSERAUD.GetValueOrDefault().ToString("dd/MM/yyyy") + "<br/>";



                //noti.grabarNotificacionSOA((int)invitacion.CODSOA, Notificacion.asuntoInvitacionCancelado, mensaje);
                this._notificacionLogic.GrabarNotificacionSOA((int)invitacion.CODSOA, Notificacion.asuntoInvitacionCancelado, mensaje, Session["sessionNombreCompletoUsuario"].ToString());

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