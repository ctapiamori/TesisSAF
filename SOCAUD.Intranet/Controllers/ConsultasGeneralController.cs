using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Intranet.Helper;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class ConsultasGeneralController : Controller
    {
 
        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafBaseLogic _baseLogic;

        private readonly ISafAbsolucionConsultaLogic _absolucionConsultaLogic;

        public ConsultasGeneralController()
        {
            _publicacionLogic = new SafPublicacionLogic();
            _cronogramaLogic = new SafCronogramaLogic();
            _absolucionConsultaLogic = new SafAbsolucionConsultaLogic();
            _baseLogic = new SafBaseLogic();
        }

        public ActionResult ConsultaConcurso()
        {
            var model = new ConsultaConcurso();
            var cronogramas = this._cronogramaLogic.ListarTodos().Where(c=>c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); // Aprobada
            model.cboCronograma = (from c in cronogramas select new SelectListItem() { Text = c.ANIOCRO.GetValueOrDefault().ToString(), Value = c.CODCRO.ToString() });
            return View(model);
        }

        public ActionResult Cronograma()
        {
            return View();
        }

        public ActionResult Bases()
        {
            var model = new ConsultaBases();
            var cronogramas = this._cronogramaLogic.ListarTodos().Where(c => c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); // Aprobada
            model.cboCronograma = (from c in cronogramas select new SelectListItem() { Text = c.ANIOCRO.GetValueOrDefault().ToString(), Value = c.CODCRO.ToString() });
            return View(model);
        }

        public ActionResult Absolucion()
        {
            return View();
        }


        public JsonResult ListarAbsolucionConsulta() {
            var listado = _absolucionConsultaLogic.ListarAbsolucionConsultasCompleto();
            var data = listado.Select(c => new string[]{ 
                c.CODABSCON.ToString(),
                c.NUMPUB,
                c.DESBAS,
                c.FECPUBABSOLUCION.HasValue? "": c.FECPUBABSOLUCION.Value.ToString("dd/MM/yyyy HH:mm:ss")
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarPublicaciones(int? idCronograma, string numeroPublicacion)
        {
            var lista = this._publicacionLogic.ListarTodos().ToList();

            if (idCronograma.HasValue && string.IsNullOrEmpty(numeroPublicacion))
                lista = lista.Where(c => c.CODCRO == idCronograma.Value).ToList();
            if (idCronograma.HasValue && !string.IsNullOrEmpty(numeroPublicacion))
                lista = lista.Where(c => c.CODCRO == idCronograma.Value && c.NUMPUB.Contains(numeroPublicacion)).ToList();
            if (!idCronograma.HasValue && !string.IsNullOrEmpty(numeroPublicacion))
                lista = lista.Where(c => c.NUMPUB.Contains(numeroPublicacion)).ToList();

            var data = lista.Select(c => new string[] {
                c.CODPUB.ToString(),
                c.NUMPUB,
                WebHelper.GetShortDateString(c.FECMAXCONS),
                WebHelper.GetShortDateString(c.FECMAXCRECON),
                WebHelper.GetShortDateString(c.FECMAXPREPROP),
                WebHelper.GetShortDateString(c.FECMAXRESCONS),
                c.ESTPUB == Estado.Publicacion.Elaboracion.GetHashCode() ? "Elaboracion" : (c.ESTPUB == Estado.Publicacion.Publicado.GetHashCode() ? "Publicado" : (c.ESTPUB == Estado.Publicacion.Aprobado.GetHashCode() ? "Aprobado" : "Pendiente Aprobación")),
                c.ESTPUB.GetValueOrDefault().ToString()
            }).ToArray();
            return Json(data);
        }

        public JsonResult ListarCronogramas()
        {
            var listado = this._cronogramaLogic.ListarPorAnioCompleto(null).Where(c => c.ESTCRO == Estado.Cronograma.Aprobado.GetHashCode()); 
            var data = listado.Select(c => new string[]{ 
                c.CODCRO.ToString(),
                c.ANIOCRO.ToString(),
                WebHelper.GetShortDateString(c.FECPUBCRO),
                WebHelper.GetShortDateString(c.FECMAXCREBASCRO),
                c.NOMPAR
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarBases(int? cronograma)
        {
            var listado = this._baseLogic.BuscarPorCronograma(cronograma).Where(c=>c.ESTBAS == Estado.Bases.Aprobado.GetHashCode());
            var data = listado.Select(c => new string[]{ 
                c.CODBAS.ToString(),
                c.NUMBAS,
                WebHelper.GetShortDateString(c.FECMAXCREPUBBAS),
                c.DESBAS,
                c.TOTRETECOBAS.GetValueOrDefault().ToString(),
                c.TOTVIABAS.GetValueOrDefault().ToString(),
                WebHelper.GetBooleanString(c.FIRPCAOBBAS),
                WebHelper.GetBooleanString(c.FIRINTBAS),
                c.ESTBAS.GetValueOrDefault().Equals(Estado.Bases.Elaboracion.GetHashCode()) ? "1" : "0"

            }).ToArray();

            return Json(data);
        }

    }
}