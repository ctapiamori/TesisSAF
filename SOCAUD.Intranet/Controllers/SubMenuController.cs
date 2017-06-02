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
    public class SubMenuController : Controller
    {
        private readonly ISafSubMenuLogic _submenuLogic;
        public SubMenuController()
        {
            _submenuLogic = new SafSubMenuLogic();
        }

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult ListarSubMenu()
        {
            var listado = this._submenuLogic.ListarSubMenuDetallado();
            var data = listado.Select(c => new string[]{ 
               c.CODSUBMEN.ToString(),
               c.DESMEN,
               c.DESSUBMEN,
               c.RUTASUBMEN
            }).ToArray();

            return Json(data);
        }

        public ActionResult Agregar()
        {
            var model = new SubMenuModel();
            return View(model);
        }


        public JsonResult AgregarSubMenu(SubMenuModel model)
        {
            try
            {
                var MenuEntity = new SAF_SUBMENU();
                MenuEntity.CODMEN = model.CODMEN;
                MenuEntity.DESSUBMEN = model.DESSUBMEN;
                MenuEntity.RUTASUBMEN = model.RUTASUBMEN;
                var result = this._submenuLogic.Registrar(MenuEntity);
                return Json(new MensajeRespuesta("Se agrego un nuevo menu satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo agregar el menu.", false));
            }
        }

        public ActionResult Editar(int id)
        {
            var entity = this._submenuLogic.BuscarPorId(id);
            var model = new SubMenuModel();
            model.CODMEN = entity.CODMEN;
            model.CODSUBMEN = entity.CODSUBMEN;
            model.DESSUBMEN = entity.DESSUBMEN;
            model.RUTASUBMEN = entity.RUTASUBMEN;
            return View(model);
        }


        public JsonResult EditarSubMenu(SubMenuModel model)
        {
            try
            {
                var menu = this._submenuLogic.BuscarPorId(model.CODSUBMEN);
                menu.CODSUBMEN = model.CODSUBMEN;
                menu.CODMEN = model.CODMEN;
                menu.DESSUBMEN = model.DESSUBMEN;
                menu.RUTASUBMEN = model.RUTASUBMEN;
                this._submenuLogic.Actualizar(menu);
                return Json(new MensajeRespuesta("Se modifico el menu satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo modificar el menu.", false));
            }
        }

        public JsonResult EliminarSubMenu(int id)
        {
            try
            {
                this._submenuLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Elimino el menu satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo eliminar el menu", true));
            }
        }

    }
}