using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOCAUD.Data.Model;
using SOCAUD.Business.Core;
using SOCAUD.Intranet.Models;
using SOCAUD.Common.Constantes;

namespace SOCAUD.Intranet.Controllers
{
    public class PuntajeController : Controller
    {

        ISafParametricaLogic parametricaLogic;
        public PuntajeController()
        {
            this.parametricaLogic = new SafParametricaLogic();
        }


        public ActionResult Index() {
            var model = new PuntajeModel();
            var PuntajeHorasExp = this.parametricaLogic.BuscarPorId(26);
            var PuntajeHorasCap = this.parametricaLogic.BuscarPorId(27);
            Session["DatosExperienciaGeneral"] = PuntajeHorasExp;
            Session["DatosCapacitacionGeneral"] = PuntajeHorasCap;
            model.PuntajeExp = Convert.ToInt32(PuntajeHorasExp.VALOR);
            model.PuntajeCapa = Convert.ToInt32(PuntajeHorasCap.VALOR);
            return View(model);
        }

        public JsonResult GrabarPuntaje(int Experiencia, int Capacitacion) {
            try
            {

            var infoExpe = new SAF_PARAMETRICA() { VALOR = Experiencia.ToString(), CODPAR = 26 };
            var infoCapa = new SAF_PARAMETRICA() { VALOR = Capacitacion.ToString(), CODPAR = 27 };
            infoExpe.VALOR = Experiencia.ToString();
            infoCapa.VALOR = Capacitacion.ToString();
            this.parametricaLogic.ActualizaParametroPuntaje(infoExpe);
            this.parametricaLogic.ActualizaParametroPuntaje(infoCapa);
            return Json(new MensajeRespuesta("Se completo la modificacion de los puntajes de la convocatoria", true));
            }
            catch (Exception)
            {

                return Json(new MensajeRespuesta("Ocurrio un error vuelva a intentarlo", false));
            }

        }

    }
}