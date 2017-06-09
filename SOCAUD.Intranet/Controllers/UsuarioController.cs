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
    public class UsuarioController : Controller
    {


        private readonly ISafUsuarioLogic _usuarioLogic;
        public UsuarioController()
        {
            _usuarioLogic = new SafUsuarioLogic();
        }


        public ActionResult BandejaUsuarios()
        {

            return View();
        }


        public ActionResult AgregarUsuario()
        {
            var model = new UsuarioModel();
            return View(model);
        }

        public JsonResult AgregarUsuario(UsuarioModel model)
        {
            try
            {
                var entity = new SAF_USUARIO();
                entity.NOMUSU = model.NOMUSU;
                entity.PASUSU = model.PASUSU;
                entity.NOMPERUSU = model.NOMPERUSU;
                entity.APEPERUSU = model.APEPERUSU;
                entity.DNIUSU = model.DNIUSU;
                entity.TIPCARUSU = model.TIPCARUSU;
                entity.CODENT = model.CODENT;
                var result = this._usuarioLogic.Registrar(entity);
                return Json(new MensajeRespuesta("Se agrego un nuevo Usuario satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo agregar el Usuario.", false));
            }
        }

        public ActionResult EditarUsuario(int id)
        {
            var entity = this._usuarioLogic.BuscarPorId(id);
            var model = new UsuarioModel();
            model.CODUSU = entity.CODUSU;
            model.NOMUSU = entity.NOMUSU;
            model.PASUSU = entity.PASUSU;
            model.NOMPERUSU = entity.NOMPERUSU;
            model.APEPERUSU = entity.APEPERUSU;
            model.DNIUSU = entity.DNIUSU;
            model.TIPCARUSU = entity.TIPCARUSU;
            model.CODENT = entity.CODENT;
            return View(model);
        }

        public JsonResult EditarUsuario(UsuarioModel model)
        {
            try
            {
                var usuario = this._usuarioLogic.BuscarPorId(model.CODUSU);
                usuario.PASUSU = model.PASUSU;
                usuario.TIPCARUSU = model.TIPCARUSU;
                usuario.CODENT = model.CODENT;
                this._usuarioLogic.Actualizar(usuario);

                return Json(new MensajeRespuesta("Se modifico el Usuario satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo modificar el Usuario.", false));
            }
        }

        public JsonResult EliminarUsuario(int id)
        {
            try
            {
                this._usuarioLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Elimino el Usuario satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo eliminar el Usuario", true));
            }
        }
    }
}