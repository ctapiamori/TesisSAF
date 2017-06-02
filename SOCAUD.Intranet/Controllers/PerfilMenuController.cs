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
    public class PerfilMenuController : Controller
    {
        private readonly ISafPerfilMenuLogic _perfilMenuLogic;
        private readonly ISafMenuLogic _MenuLogic;
        public PerfilMenuController()
        {
            _perfilMenuLogic = new SafPerfilMenuLogic();
            _MenuLogic = new SafMenuLogic();
        }

        public ActionResult Index()
        {
            var model = new PerfilMenuModel();

            return View(model);
        }

        public JsonResult ListarPerfilMenu()
        {
            var listado = this._perfilMenuLogic.ListarPerfilMenuCompleto();


            var data = listado.Select(c => new string[]{ 
               c.CODPERMEN.ToString(),
               c.NOMPER,
               c.DESMEN
            }).ToArray();
            return Json(data);
        }


        public JsonResult OpcionesAsignadas(int idPer)
        {
            var opciones = this._perfilMenuLogic.ListarPerfilMenuCompleto();
            var opcionesAsignadas = opciones.Where(c => c.CODPER == idPer);
            return Json(opcionesAsignadas);
        }


        public JsonResult OpcionesDisponibles(int idPer)
        {
            var opciones = this._perfilMenuLogic.ListarPerfilMenuCompleto().ToList();

            var opcionesAsignadas = opciones.Where(c => c.CODPER == idPer).ToList();

            var opcionesLibres = _MenuLogic.ListarTodos();

            List<PerfilMenuModel> lista = new List<PerfilMenuModel>();
            foreach (var libres in opcionesLibres)
            {
                bool estaAsignado = false;
                foreach (var asignadas in opcionesAsignadas)
                {
                    if (libres.CODMEN == asignadas.CODMEN)
                    {
                        estaAsignado = true;
                    }
                }
                if (!estaAsignado)
                {
                    lista.Add(new PerfilMenuModel() { CODMEN = libres.CODMEN, DESMEN = libres.DESMEN });
                }
            }

            return Json(lista);
        }


        public JsonResult AsignarMenu(int idPer, string[] idsMenu)
        {
            try
            {
                foreach (var item in idsMenu)
                {
                    this._perfilMenuLogic.Registrar(new SAF_PERFIL_MENU() { CODPER = idPer, CODMEN = Convert.ToInt32(item) });
                }
                return Json(new MensajeRespuesta("Se asigno los Menus seleccionados", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo asignar el Menu seleccionado", false));
            }
        }

        public JsonResult EliminarPerfilMenu(int id)
        {
            try
            {
                this._perfilMenuLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Elimino las asignaciones", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo eliminar las asignaciones", false));
            }
        }

        public JsonResult DesasignarMultiple(string[] idMenus)
        {
            try
            {
                foreach (var item in idMenus)
                {
                    this._perfilMenuLogic.Eliminar(Convert.ToInt32(item));
                }
                return Json(new MensajeRespuesta("Elimino las asignaciones", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No pudo eliminar las asignaciones", false));
            }

        }

    }
}