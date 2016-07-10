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

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public JsonResult IngresarSistema(int tipoUsuario, string usuario, string password)
        {
            var result = this._seguridadLogic.AccederSistemaExtranet(usuario, password, tipoUsuario);

            if (result.Exito)
            {
                if (tipoUsuario == (int)Tipo.TipoUsuarioExtranet.Auditor)
                {
                    var auditor = _auditorLogic.GetAuditorByUsuario(usuario);
                    Session["sessionCodigoResponsableLogin"] = auditor.CODAUD;
                    Session["sessionNombreCompletoUsuario"] = string.Format("{0} {1}", auditor.NOMAUD, auditor.APEAUD);
                }
                else
                {
                    var soa = _soaLogic.InformacionPorUsuario(usuario);
                    Session["sessionCodigoResponsableLogin"] = soa.CODSOA;
                    Session["sessionNombreCompletoUsuario"] = soa.RAZSOCSOA;
                }
                Session["sessionUsuario"] = usuario;
                Session["sessionTipoUsuario"] = tipoUsuario;
            }
            return Json(result);
        }
    }
}