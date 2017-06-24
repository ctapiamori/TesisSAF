using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SOCAUD.Intranet.Controllers
{
    public class ContrasenaController : Controller
    {

        private readonly ISafUsuarioLogic _usuarioLogic;

        public ContrasenaController(){
            _usuarioLogic = new SafUsuarioLogic();
        }
 

        public ActionResult CambiarContrasena()
        {
            var model = new ContrasenaModel();
            model.Usuario = Session["sessionUsuario"].ToString();
            return View(model);
        }

        public JsonResult GrabarCambiosContrasenia(string usuario, string contrasenia, string repitaContrasenia) {
            var usuarioReg = this._usuarioLogic.BuscarPorUsuario(usuario);

            if (contrasenia == repitaContrasenia)
            {
                usuarioReg.PASUSU = contrasenia;
                this._usuarioLogic.Actualizar(usuarioReg);
                return Json(new MensajeRespuesta("Se actualizo la contraseña satisfactoriamente", true));
            }
            else {
                return Json(new MensajeRespuesta("Las contraseñas ingresadas no coinciden", false));
            }
        }

    }
}