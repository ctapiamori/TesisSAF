using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Helper;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class CronogramaController : Controller
    {
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafEntidadLogic _entidadLogic;
        private readonly ISafCronoEntidadLogic _cronoEntidadLogic;

        public CronogramaController()
        {
            this._cronogramaLogic = new SafCronogramaLogic();
            this._entidadLogic = new SafEntidadLogic();
            this._cronoEntidadLogic = new SafCronoEntidadLogic();
        }

        // GET: Cronograma
        public ActionResult Index()
        {
            var anios = new List<SelectListItem>();
            anios.Add(new SelectListItem() { Value = "2015", Text = "2015" });
            anios.Add(new SelectListItem() { Value = "2016", Text = "2016" });
            anios.Add(new SelectListItem() { Value = "2017", Text = "2017" });
            ViewBag.Anios = anios;
            return View();
        }

        public ActionResult Registrar()
        {
            var model = new CronogramaModel();

            model.Anio = DateTime.Now.Year;

            return View(model);
        }

        public ActionResult Configurar(int id)
        {
            var cronograma = this._cronogramaLogic.BuscarPorId(id);
            var model = new CronogramaModel();

            model.Codigo = cronograma.CODCRO;
            model.Anio = cronograma.ANIOCRO.GetValueOrDefault();
            model.FechaPublicacion = WebHelper.GetShortDateString(cronograma.FECPUBCRO);
            model.FechaMaximaCreacionBase = WebHelper.GetShortDateString(cronograma.FECMAXCREBASCRO);

            var entidades = this._entidadLogic.ListarTodos();
            ViewBag.Entidades = entidades.Select(c => new SelectListItem() { Value = c.CODENT.ToString(), Text = c.RAZSOCENT });

            model.Entidad = new CronoEntidadModel()
            {
                Cronograma = cronograma.CODCRO
            };

            return View(model);
        }

        public JsonResult SaveCronograma(CronogramaModel model)
        {
            var entidad = new SAF_CRONOGRAMA();
            try
            {
                if (!model.Codigo.Equals(0))
                    entidad = this._cronogramaLogic.BuscarPorId(model.Codigo);

                entidad.ANIOCRO = model.Anio;
                entidad.FECPUBCRO = WebHelper.GetDateTimeOrNull(model.FechaPublicacion);
                entidad.FECMAXCREBASCRO = WebHelper.GetDateTimeOrNull(model.FechaMaximaCreacionBase);

                if (model.Codigo.Equals(0))
                {
                    entidad.ESTCRO = 1;
                    var result = this._cronogramaLogic.Registrar(entidad);
                    return Json(new MensajeRespuesta("Registro satisfactorio", true, result));
                }
                else
                {
                    entidad.CODCRO = model.Codigo;
                    var result = this._cronogramaLogic.Actualizar(entidad);
                    return Json(new MensajeRespuesta("Actualización satisfactoria", true, result));
                }
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
            
        }

        public JsonResult EliminarCronograma(int id)
        {
            try
            {
                this._cronogramaLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }

        public JsonResult ListarCronogramas(int anio)
        {
            var listado = this._cronogramaLogic.ListarPorAnio(anio);
            var data = listado.Select(c => new string[]{ 
                c.CODCRO.ToString(),
                c.ANIOCRO.ToString(),
                WebHelper.GetShortDateString(c.FECPUBCRO),
                WebHelper.GetShortDateString(c.FECMAXCREBASCRO)
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarEntidadCronograma(int cronograma)
        {
            var listado = this._cronoEntidadLogic.ListarPorCronograma(cronograma);
            var data = listado.Select(c => new string[]{ 
                c.CODCROENT.ToString(),
                c.DESCROENT,
                WebHelper.GetShortDateString(c.FECINICROENT),
                WebHelper.GetShortDateString(c.FECFINCROENT),
                c.CODCRO.ToString(),
                c.CODENT.ToString()
            }).ToArray();

            return Json(data);
        }

        public JsonResult SaveCronoEntidad(CronoEntidadModel model)
        {
            var entidad = new SAF_CRONOENTIDAD();
            try
            {
                entidad.CODCRO = model.Cronograma;
                entidad.CODENT = model.Entidad;
                entidad.DESCROENT = this._entidadLogic.BuscarPorId(model.Entidad).RAZSOCENT;
                entidad.FECINICROENT = WebHelper.GetDateTimeOrNull(model.FechaInicio);
                entidad.FECFINCROENT = WebHelper.GetDateTimeOrNull(model.FechaTermino);

                if (model.Codigo.Equals(0))
                {
                    var result = this._cronoEntidadLogic.Registrar(entidad);
                    return Json(new MensajeRespuesta("Se registro satisfactoriamente", true));
                }
                else
                {
                    entidad.CODCROENT = model.Codigo;
                    var result = this._cronoEntidadLogic.Actualizar(entidad);
                    return Json(new MensajeRespuesta("Se actualizo satisfactoriamente", true));
                }
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error));
            }
        }

        public JsonResult EliminarEntidad(int id)
        {
            try
            {
                this._cronoEntidadLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }
    }
}