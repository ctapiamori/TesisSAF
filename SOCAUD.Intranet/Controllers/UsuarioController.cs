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

        public JsonResult ListarUsuarios()
        {
            var listado = this._usuarioLogic.ListarUsuariosCompleto();


            var data = listado.Select(c => new string[]{ 
               c.CODUSU.ToString(),
               c.DNIUSU,
               c.NOMPERUSU,
               c.APEPERUSU,
               c.NOMUSU,
               c.NOMCARGO
            }).ToArray();
            return Json(data);
        }


        public ActionResult AgregarUsuario()
        {
            var model = new UsuarioModel();
            return View(model);
        }

        public JsonResult AgregarNuevoUsuario(UsuarioModel model)
        {
            try
            {

                var listaUsuarios = this._usuarioLogic.ListarTodos().ToList();

                var existeUsuario = listaUsuarios.Where(c => c.NOMUSU == model.NOMUSU.ToUpper()).Count();

                var existeDNI = listaUsuarios.Where(c => c.DNIUSU == model.DNIUSU.ToUpper()).Count();

                if (existeUsuario > 0)
                    return Json(new MensajeRespuesta("El usuario que intenta registrar ya existe en la Base de Datos", false));

                if (existeDNI > 0)
                    return Json(new MensajeRespuesta("Ya existe un registro de usuario para el DNI ingresado", false));

                var entity = new SAF_USUARIO();
                entity.NOMUSU = model.NOMUSU.ToUpper();
                entity.PASUSU = model.PASUSU;
                entity.NOMPERUSU = model.NOMPERUSU;
                entity.APEPERUSU = model.APEPERUSU;
                entity.DNIUSU = model.DNIUSU;
                entity.TIPCARUSU = model.TIPCARUSU;
                entity.CODENT = model.CODENT;
                entity.CODPER = model.CODPER;
                var result = this._usuarioLogic.Registrar(entity);
                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
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
            model.CODPER = entity.CODPER;
            return View(model);
        }

        public JsonResult EditarDatosUsuario(UsuarioModel model)
        {
            try
            {
                var usuario = this._usuarioLogic.BuscarPorId(model.CODUSU);
                usuario.PASUSU = model.PASUSU;
                usuario.TIPCARUSU = model.TIPCARUSU;
                usuario.CODENT = model.CODENT;
                usuario.CODPER = model.CODPER;
                this._usuarioLogic.Actualizar(usuario);

                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
            }
        }

        public JsonResult EliminarUsuario(int id)
        {
            try
            {
                this._usuarioLogic.Eliminar(id);
                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
            }
        }
    }
}