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
    public class ParametroController: Controller
    {

        private readonly ISafParametricaLogic _parametricaLogic;

        
        public ParametroController()
        {
            _parametricaLogic = new SafParametricaLogic();
        }

        public ActionResult BandejaParametros()
        {
            var model = new ParametroModel();

            return View(model);
        }

        public JsonResult ListarParametros(int? idTipo)
        {


            IEnumerable<VW_SAF_PARAMETRICA> listado;

            if (idTipo.HasValue)
                listado = this._parametricaLogic.ListarParametricaCompleta().ToList().Where(c => c.CODTIPPAR == idTipo.Value);
            else
                listado = this._parametricaLogic.ListarParametricaCompleta().ToList();


            var data = listado.Select(c => new string[]{ 
               c.CODPAR.ToString(),
               c.NOMTIPPAR,
               c.NOMPAR,
               c.VALOR
            }).ToArray();
            return Json(data);
        }


        public ActionResult AgregarParametro()
        {
            var model = new ParametroModel();
            return View(model);
        }

        public JsonResult AgregarNuevoParametro(ParametroModel model)
        {
            try
            {
                var entity = new SAF_PARAMETRICA();
                entity.NOMPAR = model.NOMPAR;
                entity.VALOR = model.VALOR;
                entity.CODTIPPAR = model.CODTIPPAR;
     
                var result = this._parametricaLogic.Registrar(entity);
                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
            }
        }

        public ActionResult EditarParametro(int id)
        {
            var entity = this._parametricaLogic.BuscarPorId(id);
            var model = new ParametroModel();
            model.CODPAR = entity.CODPAR;
            model.NOMPAR = entity.NOMPAR;
            model.VALOR = entity.VALOR;
            model.CODTIPPAR = entity.CODTIPPAR;
            return View(model);
        }

        public JsonResult EditarDatosParametro(ParametroModel model)
        {
            try
            {
                var entidad = this._parametricaLogic.BuscarPorId(model.CODPAR);
                entidad.CODPAR = model.CODPAR;
                entidad.NOMPAR = model.NOMPAR;
                entidad.VALOR = model.VALOR;
                entidad.CODTIPPAR = model.CODTIPPAR;
                this._parametricaLogic.Actualizar(entidad);

                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
            }
        }

        public JsonResult EliminarParametro(int id)
        {
            try
            {
                this._parametricaLogic.Eliminar(id);
                return Json(new MensajeRespuesta(Mensaje.MensajeOperacionRealizadaExito, true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta(Mensaje.MensajeErrorNoControlado, false));
            }
        }

    }
}