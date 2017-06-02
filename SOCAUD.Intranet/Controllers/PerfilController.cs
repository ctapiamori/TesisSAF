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
    public class PerfilController : Controller
    {

        private readonly ISafPerfilLogic _perfilLogic;
        public PerfilController()
        {
            _perfilLogic = new SafPerfilLogic();
        }


        public ActionResult Index() {
             
            return View();
        }

        public JsonResult ListarPerfil()
        {
            var listado = this._perfilLogic.ListarTodos();
            var data = listado.Select(c => new string[]{ 
               c.CODPER.ToString(),
               c.NOMPER,
               c.DESPER,
               (c.TIPOPER == "I")? "INTERNO" : "EXTERNO"
            }).ToArray();

            return Json(data);
        }

        public ActionResult Agregar()
        {
            var model = new PerfilModel();
            return View(model);
        }


        public JsonResult AgregarPerfil(PerfilModel model)
        {
            try
            {
                var entity = new SAF_PERFIL();
                entity.DESPER = model.DESPER;
                entity.NOMPER = model.NOMPER;
                var result = this._perfilLogic.Registrar(entity);
                return Json(new MensajeRespuesta("Se agrego un nuevo perfil satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo agregar el perfil.", false));
            }
        }

        public ActionResult Editar(int id) {
            var entity = this._perfilLogic.BuscarPorId(id);
            var model = new PerfilModel();
            model.CODPER = entity.CODPER;
            model.DESPER = entity.DESPER;
            model.NOMPER = entity.NOMPER;
            model.TIPOPER = entity.TIPOPER;
            return View(model);
        }


        public JsonResult EditarPerfil(PerfilModel model)
        {
            try
            {
                var perfil = this._perfilLogic.BuscarPorId(model.CODPER);

                perfil.DESPER = model.DESPER;
                perfil.NOMPER = model.NOMPER;
                perfil.TIPOPER = model.TIPOPER;
                this._perfilLogic.Actualizar(perfil);

                return Json(new MensajeRespuesta("Se modifico el perfil satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo modificar el perfil.", false));
            }
        }

        public JsonResult EliminarPerfil(int id)
        {
            try
            {
                this._perfilLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Elimino el perfil satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo eliminar el perfil", true));
            }
        }

    }
}