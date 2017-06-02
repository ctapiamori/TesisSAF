using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Login.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ISeguridadLogic _seguridadLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSoaLogic _soaLogic;

        public AccesoController()
        {
            _seguridadLogic = new SeguridadLogic();
            _auditorLogic = new SafAuditorLogic();
            _soaLogic = new SafSoaLogic();
        }

        public ActionResult Login()
        {
            var model = new SafLoginModel();
            return View(model);
        }


        public JsonResult AccederSistema(SafLoginModel model)
        {

            try
            {


                return Json(new { Resultado = true, Mensaje = "Entro al sistema satisfactoriamente" });
            }
            catch (Exception)
            {
                return Json(new { Resultado = false, Mensaje = "No pudo ingresar al sistema" });
            }
        }

    }
}
