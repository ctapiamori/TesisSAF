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
    public class SeguimientoController : Controller
    {

        ISafSoaLogic _soaLogic;
        ISafPublicacionBaseLogic _publicacionYBasesLogic;
        ISafPropuestaLogic _propuestaLogic;
        ISafGeneralLogic _generalLogic;
        public SeguimientoController()
        {
            _soaLogic = new SafSoaLogic();
            _publicacionYBasesLogic = new SafPublicacionBaseLogic();
            _propuestaLogic = new SafPropuestaLogic();
            _generalLogic = new SafGeneralLogic();
        }


        public ActionResult PropuestasObservar()
        {
            var model = new SeguimientoModel();
            var listaSOA = _soaLogic.ListarTodos();
            var listaPublicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var listaCboPublicacion = (from c in listaPublicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();

            var result = listaCboPublicacion.GroupBy(c => new
            {
                c.Value,
                c.Text
            }).OrderBy(g => g.Key.Value)
               .Select(g => new SelectListItem
               {
                   Text = g.Key.Text,
                   Value = g.Key.Value
               });

            model.cboPublicacion = result;
            model.cboSOA = (from c in listaSOA select new SelectListItem() { Text = c.RAZSOCSOA, Value = c.CODSOA.ToString() });
            return View(model);
        }

        public ActionResult PropuestasPenalidad()
        {
            var model = new SeguimientoModel();
            var listaSOA = _soaLogic.ListarTodos();
            var listaPublicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var listaCboPublicacion = (from c in listaPublicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();

            var listaTipoPenalidad = this._generalLogic.ListarParametricas(15); // TipoPenalidad

            var result = listaCboPublicacion.GroupBy(c => new
            {
                c.Value,
                c.Text
            }).OrderBy(g => g.Key.Value)
               .Select(g => new SelectListItem
               {
                   Text = g.Key.Text,
                   Value = g.Key.Value
               });
            model.cboPublicacion = result;
            model.cboSOA = (from c in listaSOA select new SelectListItem() { Text = c.RAZSOCSOA, Value = c.CODSOA.ToString() });
            model.cboTipoPenalidad = (from c in listaTipoPenalidad select new SelectListItem() { Text = c.NOMPAR, Value = c.CODPAR + "-" + c.VALOR });
            return View(model);
        }

        public JsonResult ListarPropEjecucion() {

            var lista = _propuestaLogic.ListarPropuestaEjecucion();

            var data = lista.Select(c => new string[]{ 
                c.CODPRO.ToString(),
                
                c.PERAUD,
                c.DESBAS,
                c.RUCSOA,
                c.RAZSOCSOA
            }).ToArray();
            return Json(data);
        }


    }
}