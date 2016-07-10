﻿using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Data.Model;
using SOCAUD.Intranet.Helper;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafCronoEntidadLogic _cronoEntidadLogic;
        private readonly ISafBaseLogic _baseLogic;

        public BaseController()
        {
            this._cronogramaLogic = new SafCronogramaLogic();
            this._cronoEntidadLogic = new SafCronoEntidadLogic();
            this._baseLogic = new SafBaseLogic();
        }

        // GET: Base
        public ActionResult Index()
        {
            var cronogramas = this._cronogramaLogic.ListarTodos();
            ViewBag.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() }).ToList();
            return View();
        }

        public ActionResult Registrar()
        {
            var cronogramas = this._cronogramaLogic.ListarTodos();

            var model = new BaseModel();
            model.FirmaPcaob = "N";
            model.FirmaInternacional = "N";
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString() });
            model.Entidades = new List<SelectListItem>();
            model.EstadoBase = Estado.Bases.Elaboracion.GetHashCode();
            return View(model);
        }

        public ActionResult Editar(int id)
        {
            var _base = this._baseLogic.BuscarPorId(id);

            var model = new BaseModel() { 
                Codigo = _base.CODBAS,
                Numero = _base.NUMBAS,
                FechaMaxPublicacion = WebHelper.GetShortDateString(_base.FECMAXCREPUBBAS),
                Descripcion = _base.DESBAS,
                TotalRetribucion = _base.TOTRETECOBAS.GetValueOrDefault(),
                TotalViaticos = _base.TOTVIABAS.GetValueOrDefault(),
                FirmaPcaob = _base.FIRPCAOBBAS,
                FirmaInternacional = _base.FIRINTBAS,
                Cronograma = _base.CODCRO.GetValueOrDefault(),
                CronoEntidad = _base.CODCROENT.GetValueOrDefault(),
                EstadoBase = _base.ESTBAS.GetValueOrDefault()
            };

            var cronogramas = this._cronogramaLogic.ListarTodos();
            model.Cronogramas = cronogramas.Select(c => new SelectListItem() { Value = c.CODCRO.ToString(), Text = c.ANIOCRO.GetValueOrDefault().ToString(), Selected = c.CODCRO.Equals(_base.CODCRO.GetValueOrDefault()) });

            var entidades = this._cronoEntidadLogic.ListarPorCronograma(_base.CODCRO.GetValueOrDefault());
            model.Entidades = entidades.Select(c => new SelectListItem() { Value = c.CODCROENT.ToString(), Text = c.DESCROENT, Selected = c.DESCROENT.Equals(_base.CODCROENT.GetValueOrDefault()) });
            return View(model);
        }

        public JsonResult ListarEntidadCronograma(int cronograma)
        {
            var entidades = this._cronoEntidadLogic.ListarPorCronograma(cronograma);
            return Json(entidades.Select(c => new SelectListItem() { Value = c.CODCROENT.ToString(), Text = c.DESCROENT }));
        }

        public JsonResult ListarBases(int cronograma)
        {
            var listado = this._baseLogic.BuscarPorCronograma(cronograma);
            var data = listado.Select(c => new string[]{ 
                c.CODBAS.ToString(),
                c.NUMBAS,
                WebHelper.GetShortDateString(c.FECMAXCREPUBBAS),
                c.DESBAS,
                c.TOTRETECOBAS.GetValueOrDefault().ToString(),
                c.TOTVIABAS.GetValueOrDefault().ToString(),
                WebHelper.GetBooleanString(c.FIRPCAOBBAS),
                WebHelper.GetBooleanString(c.FIRINTBAS)

            }).ToArray();

            return Json(data);
        }

        public JsonResult SaveBase(BaseModel model)
        {
            var entidad = new SAF_BASE();
            try
            {
                if (!model.Codigo.Equals(0))
                    entidad = this._baseLogic.BuscarPorId(model.Codigo);

                entidad.FECMAXCREPUBBAS = WebHelper.GetDateTimeOrNull(model.FechaMaxPublicacion);
                entidad.TOTRETECOBAS = model.TotalRetribucion;
                entidad.TOTVIABAS = model.TotalViaticos;
                entidad.FIRPCAOBBAS = model.FirmaPcaob;
                entidad.FIRINTBAS = model.FirmaInternacional;
               
                if (model.Codigo.Equals(0))
                {
                    var numeroBase = string.Empty;

                    var correlativo = this._cronogramaLogic.NewCorrelativoBase(model.Cronograma);
                    var cronograma = this._cronogramaLogic.BuscarPorId(model.Cronograma);
                    numeroBase = cronograma.ANIOCRO.GetValueOrDefault().ToString() + "-" + correlativo.ToString("D5");


                    entidad.CODCROENT = model.CronoEntidad;
                    entidad.CODCRO = model.Cronograma;
                    entidad.DESBAS = this._cronoEntidadLogic.BuscarPorId(model.CronoEntidad).DESCROENT;
                    entidad.ESTBAS = Estado.Bases.Elaboracion.GetHashCode();
                    entidad.NUMBAS = numeroBase;
                    entidad.CORBAS = correlativo;
                    var result = this._baseLogic.Registrar(entidad);
                    return Json(new MensajeRespuesta("Registro satisfactorio", true, result));
                }
                else
                {
                    entidad.CODBAS = model.Codigo;
                    var result = this._baseLogic.Actualizar(entidad);
                    return Json(new MensajeRespuesta("Actualización satisfactoria", true, result));
                }
            }
            catch (Exception ex)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error, ex.Message));
            }
        }

        public JsonResult EliminarBase(int id)
        {
            try
            {
                this._baseLogic.Eliminar(id);
                return Json(new MensajeRespuesta("Se elimino satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar", false));
            }
        }

    }
}