using SOCAUD.Business.Core;
using SOCAUD.Common.Helpers;
using SOCAUD.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class GeneralController : BaseController
    {
        private readonly ISafNotificacionLogic _notificacionLogic;
        public GeneralController()
        {
            this._notificacionLogic = new SafNotificacionLogic();
        }

        // GET: General
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DescargarArchivo(long id)
        {
            var archivo = Archivo.DescargarArchivo(id);
            if (archivo == null || archivo.fileBytes == null)
            {
                return HttpNotFound();
            }
            return File(archivo.fileBytes, Texto.TipoMime(archivo.ARCNOMBFISICO), archivo.NOMBLABEL);
        }

        public JsonResult ContarNotificacionesUsuario()
        {
            if (Session["sessionUsuario"] != null)
            {
                string usu = Session["sessionUsuario"].ToString();
                var cantidadNotificaciones = this._notificacionLogic.ListarNotificaciones(usu).Count();// modelEntity.SAF_NOTIFICACION.Where(c => c.USUREC == usu && c.INDNOT == "R" && c.ESTNOT == "R").Count();

                return Json(cantidadNotificaciones, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("0", JsonRequestBehavior.AllowGet);
        }
    }
}