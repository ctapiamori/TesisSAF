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
    public class MenuController : Controller
    {

        private readonly ISafMenuLogic _menuLogic;
        public MenuController()
        {
            _menuLogic = new SafMenuLogic();
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult ListarMenu()
        {
            var listado = this._menuLogic.ListarTodos();
            var data = listado.Select(c => new string[]{ 
               c.CODMEN.ToString(),
               c.DESMEN,
               c.RUTAMEN,
               c.ICONCSS
            }).ToArray();

            return Json(data);
        }

        public ActionResult Agregar()
        {
            var model = new MenuModel();
            return View(model);
        }


        public JsonResult AgregarMenu(MenuModel model)
        {
            try
            {
                var MenuEntity = new SAF_MENU();
                MenuEntity.DESMEN = model.DESMEN;
                MenuEntity.ICONCSS = model.ICONCSS;
                MenuEntity.RUTAMEN = model.RUTAMEN;
                MenuEntity.ORDEN = model.ORDEN;
                var result = this._menuLogic.Registrar(MenuEntity);
                return Json(new MensajeRespuesta("Se agrego un nuevo menu satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo agregar el menu.", false));
            }
        }

        public ActionResult Editar(int id)
        {
            var entity = this._menuLogic.BuscarPorId(id);

            var model = new MenuModel();
            model.CODMEN = entity.CODMEN;
            model.DESMEN = entity.DESMEN;
            model.ICONCSS = entity.ICONCSS;
            model.RUTAMEN = entity.RUTAMEN;
            model.ORDEN = entity.ORDEN.GetValueOrDefault();
            return View(model);
        }


        public JsonResult EditarMenu(MenuModel model)
        {
            try
            {
                var menu = this._menuLogic.BuscarPorId(model.CODMEN);

                menu.CODMEN = model.CODMEN;
                menu.RUTAMEN = model.RUTAMEN;
                menu.ICONCSS = model.ICONCSS;
                menu.DESMEN = model.DESMEN;
                menu.ORDEN = model.ORDEN;
                this._menuLogic.Actualizar(menu);

                return Json(new MensajeRespuesta("Se modifico el menu satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo modificar el menu.", false));
            }
        }

        public JsonResult EliminarMenu(int id)
        {
            try
            {
                this._menuLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Elimino el menu satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo eliminar el menu", true));
            }
        }
    }
}