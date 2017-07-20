using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class ConsultaWorkFlowController : Controller
    {
        private readonly ISafWorkFlowLogic _workFlowLogic;

        public ConsultaWorkFlowController() {
            _workFlowLogic = new SafWorkFlowLogic();
        }

        public ActionResult Bandeja()
        {
            return View();
        }

        public JsonResult ListarFlujoCabecera(string tipoDoc, string fecIni, string fecFin)
        {
            var usuario = Session["sessionUsuario"].ToString();
            var listado = this._workFlowLogic.ListarWorkFlowCabeceraPorUsuario(usuario,tipoDoc, fecIni, fecFin).ToList();

            var result = listado.GroupBy(c => new
            {
                c.TIPDOC,
                c.DESCRIPCION,
                c.DESTIPDOC,
                c.CODDOC,
                c.FECREG,
                c.USUREG
            }).OrderBy(g => g.Key.FECREG)
            .Select(g => new WorkFlowUsuarioModel
            {
                TIPDOC = g.Key.TIPDOC,
                DESCRIPCION = g.Key.DESCRIPCION,
                DESTIPDOC = g.Key.DESTIPDOC,
                CODDOC = g.Key.CODDOC,
                FECREG = g.Key.FECREG,
                USUREG = g.Key.USUREG
            }).ToList();
 
            var data = result.Select(c => new string[]{ 
                c.TIPDOC,
                c.CODDOC.GetValueOrDefault().ToString(),
                c.DESTIPDOC,
                c.DESCRIPCION,
                c.FECREG.HasValue?c.FECREG.Value.ToString("dd/MM/yyyy HH:mm:ss"): ""
            }).ToArray();

            return Json(data);
        }

        public PartialViewResult Solicitud(string tipodoc, int doc)
        {
            var model = new WorkFlowUsuarioModel();
            model.TIPDOC = tipodoc;
            model.CODDOC = doc;
            return PartialView("_DetalleFlujo", model);
        }
 
    }
}