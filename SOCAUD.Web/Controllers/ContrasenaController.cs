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
    public class ContrasenaController : Controller
    {

        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSoaLogic _soaLogic;


        public ContrasenaController()
        {
            
            _auditorLogic = new SafAuditorLogic();
            _soaLogic = new SafSoaLogic();
        }

        public ActionResult CambiarContrasena()
        {
            var model = new ContrasenaModel();
            model.Usuario = Session["sessionUsuario"].ToString();
            return View(model);
        }


        public JsonResult GrabarCambiosContrasenia(string usuario, string contrasenia, string repitaContrasenia)
        {
            if (contrasenia != repitaContrasenia) {
                return Json(new MensajeRespuesta("Las contraseñas ingresadas no coinciden", false));
            }

            var tipoExterno = Convert.ToInt32(Session["sessionTipoUsuario"]);
            if (tipoExterno == Tipo.TipoUsuarioExtranet.Auditor.GetHashCode())
            {
                var auditorReg = this._auditorLogic.GetAuditorByUsuario(usuario);
                auditorReg.PASUSU = contrasenia;
                _auditorLogic.Actualizar(auditorReg);
            }
            else {
                var soaReg = this._soaLogic.InformacionPorUsuario(usuario);
                soaReg.PASUSU = contrasenia;
                _soaLogic.Actualizar(soaReg);            
            }

            return Json(new MensajeRespuesta("Se actualizo la contraseña satisfactoriamente", true));
        }

    }
}