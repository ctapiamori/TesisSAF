using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class NotificacionController : BaseController
    {
        private readonly ISafNotificacionLogic _notificacionLogic;
        public NotificacionController()
        {
            this._notificacionLogic = new SafNotificacionLogic();
        }

        // GET: Notificacion
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ContarNoLeidas()
        {
            var usu = usuarioLogueado;
            return Json(new { exito = false, total = 0 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult mensaje()
        {
            //var agenteNotificacion = new NotificacionAgente();
            //return Json(agenteNotificacion.mensaje(Session["sessionUsuario"].ToString()));
            return null;
        }


        public ActionResult Bandeja()
        {
            return View();
        }

        public JsonResult ListarMensajes(string bandeja)
        {
            //IEnumerable<SAF_NOTIFICACION> mensajes = new List<SAF_NOTIFICACION>();
            var mensajes = this._notificacionLogic.ListarNotificaciones(bandeja, Session["sessionUsuario"].ToString());
            //mensajes = modelEntity.SAF_NOTIFICACION.ToList().Where(c => c.ESTNOT == bandeja && c.USUREC == Session["sessionUsuario"].ToString());
            mensajes = mensajes.OrderByDescending(c => c.FECREG);
            var data = mensajes.Select(c => new string[] { 
                c.CODNOT.ToString(),
                c.USUEMI,
                c.ASUNOT,
                c.FECREG.HasValue? c.FECREG.Value.ToString("dd/MM/yyyy HH:mm:ss"): "",
                //GetReciveNota(c.FECREG),
                c.INDNOT.Equals("R") ? "1" : "0",
                c.ESTNOT.Equals(TipoBandeja.BANDEJA_RECIBIDOS) ? "1" : "0"
            });

            return Json(data);
        }

        public JsonResult ContadorMensajesBandeja()
        {
            //IEnumerable<SAF_NOTIFICACION> mensajes = new List<SAF_NOTIFICACION>();
            //mensajes = modelEntity.SAF_NOTIFICACION.ToList().Where(c => c.USUREC == Session["sessionUsuario"].ToString());
            var mensajes = this._notificacionLogic.ListarNotificacionesUsuario(Session["sessionUsuario"].ToString());
            var cantidadNoLeidos = mensajes.Where(c => c.INDNOT == "R").Count();
            var cantidadEliminados = mensajes.Where(c => c.ESTNOT == "P").Count();
            return Json(new { cantNoLeidos = cantidadNoLeidos, cantEliminado = cantidadEliminados });
        }

        private string GetReciveNota(DateTime? fecha)
        {
            var time = (DateTime.Now - fecha.GetValueOrDefault());

            if (time.TotalMinutes < 60) return ((int)time.TotalMinutes).ToString() + " minuto(s)";
            if (time.TotalHours < 24) return ((int)time.TotalHours).ToString() + " hora(s)";
            return ((int)time.TotalDays).ToString() + " dia(s)";
        }

        public JsonResult LeerMensaje(int mensaje)
        {
            var data = this._notificacionLogic.GetNotificacion(mensaje, Session["sessionUsuario"].ToString());// modelEntity.SAF_NOTIFICACION.ToList().Where(c => c.CODNOT.Equals(mensaje) && c.USUREC.Equals(Session["sessionUsuario"])).FirstOrDefault();

            if (data.INDNOT.Equals("R"))
            {
                data.INDNOT = "L";

                //modelEntity.SaveChanges();
                this._notificacionLogic.Actualizar(data);
            }

            return Json(new { USUNOT = data.USUEMI, ASUNOT = data.ASUNOT, DESNOT = data.DESNOT, FECNOT = data.FECREG.GetValueOrDefault().ToString("dd/MM/yyyy hh:mm tt") });
        }

        public JsonResult EliminarMensaje(int mensaje)
        {
            try
            {
                var data = this._notificacionLogic.GetNotificacion(mensaje, Session["sessionUsuario"].ToString());// modelEntity.SAF_NOTIFICACION.ToList().Where(c => c.CODNOT.Equals(mensaje) && c.USUREC.Equals(Session["sessionUsuario"])).FirstOrDefault();

                if (data.ESTNOT.Equals(TipoBandeja.BANDEJA_RECIBIDOS))
                {
                    data.ESTNOT = TipoBandeja.BANDEJA_PAPELERA;

                    //modelEntity.SaveChanges();
                    this._notificacionLogic.Actualizar(data);

                    return Json(new MensajeRespuesta("Se proceso satisfactoriamente.", true));
                }

                return Json(new MensajeRespuesta("No se pudo eliminar la notificación.", false));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Se produjo un error al eliminar la notificación", false));
            }
        }
    }
}