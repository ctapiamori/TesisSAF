using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ISeguridadLogic _seguridadLogic;
        private readonly ISafUsuarioLogic _usuarioLogic;
        public AccesoController()
        {
            _seguridadLogic = new SeguridadLogic();
            _usuarioLogic = new SafUsuarioLogic();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult AccederSistema(string usuario, string contrasenia)
        {
            var result = this._usuarioLogic.AccederSistema(usuario, contrasenia);// this.modelEntity.SP_ACCEDERSISTEMAADMIN(usuario, contrasenia).ToList().FirstOrDefault();
            var datosUsuario = this._usuarioLogic.BuscarPorUsuario(usuario);// this.modelEntity.SAF_USUARIO.Where(c => c.NOMUSU == usuario).FirstOrDefault();
            if (result.EXITO.Equals(0))
            {
                return Json(new MensajeRespuesta(result.MENSAJE, false));
            }
            else
            {
                Session["sessionUsuarioNombreCompleto"] = string.Format("{0} {1}", datosUsuario.NOMPERUSU, datosUsuario.APEPERUSU);
                Session["tipoUsuario"] = datosUsuario.TIPCARUSU.GetValueOrDefault();
                Session["idUsuario"] = datosUsuario.CODUSU;
                return Json(new MensajeRespuesta(result.MENSAJE, true));
            }
        }
    }
}