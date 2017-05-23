using SOCAUD.Business.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class ConsultaController : Controller
    {

        private readonly ISafConsultaLogic _consultaLogic;
        private readonly ISafPublicacionLogic _publicacionLogic;
        public ConsultaController()
        {
            _publicacionLogic = new SafPublicacionLogic();
            _consultaLogic = new SafConsultaLogic();
        }

        public ActionResult Index() {

            var model = new ConsultaModel();

            var listaPublicaciones = this._publicacionLogic.ListarPublicacion();
            var listaPublicacionesPublicadas = listaPublicaciones.Where(c => c.ESTPUB == 13).ToList(); // estado PUBLICADA

            model.lista = (from c in listaPublicacionesPublicadas select new SelectListItem() { Value = c.CODPUB.ToString(),  Text = string.Format("{0}-{1}", c.NUMPUB, c.DESBAS) }).ToList();

            return View(model);
        }


        public JsonResult ListarConsultas(int idPublicacion) {
            var lista = _consultaLogic.ListarConsultaPorPublicacion(idPublicacion);
            var result = (from c in lista select new ConsultaEntidadModel() { CodigoConsulta = c.CODCON, DescripcionConsulta = c.DESCON, Estado = c.ESTCON.Value, RespuestaConsulta = c.RESCON });

            var data = lista.Select(c => new string[]{ 
                c.CODCON.ToString(),
                c.DESCON.ToString(),
                c.RESCON,
                c.ESTCON.Value.ToString(),
            }).ToArray();


            return Json(data);
        }

        public PartialViewResult ResponderConsulta(int idConsulta) {

            var consulta = _consultaLogic.buscarPorId(idConsulta);
            var consultaEntidad = new ConsultaEntidadModel();
            consultaEntidad.CodigoConsulta = consulta.CODCON;
            consultaEntidad.DescripcionConsulta = consulta.DESCON;
            consultaEntidad.RespuestaConsulta = consulta.RESCON;
            return PartialView("_ResponderConsulta", consultaEntidad);
        }

    }
}