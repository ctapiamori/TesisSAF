using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class ConsultaController : Controller
    {

        private readonly ISafPublicacionLogic _publicacionLogic;
        private readonly ISafServicioAuditoriaLogic _servicioAuditoriaLogic;
        private readonly ISafInvitacionLogic _invitacionLogic;
        private readonly ISafInvitacionDetalleLogic _invitacionDetalleLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafGeneralLogic _generalLogic;
        private readonly ISafNotificacionLogic _notificacionLogic;

        private readonly ISafPublicacionBaseLogic _publicacionYBasesLogic;

        private readonly ISafConsultaLogic _consultaLogic;

        private readonly ISafAbsolucionConsultaLogic _absolucionConsultaLogic;


        public ConsultaController()
        {
            _publicacionLogic = new SafPublicacionLogic();
            _servicioAuditoriaLogic = new SafServicioAuditoriaLogic();
            _invitacionLogic = new SafInvitacionLogic();
            _auditorLogic = new SafAuditorLogic();
            _generalLogic = new SafGeneralLogic();
            _invitacionDetalleLogic = new SafInvitacionDetalleLogic();
            _notificacionLogic = new SafNotificacionLogic();
            _publicacionYBasesLogic = new SafPublicacionBaseLogic();
            _consultaLogic = new SafConsultaLogic();

            _absolucionConsultaLogic = new SafAbsolucionConsultaLogic();
        }


        public ActionResult Listado()
        {
            var model = new ConsultaModel();
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();

            var listaPublicacion = (from c in publicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();
            var result = listaPublicacion.GroupBy(c => new
            {
                c.Value,
                c.Text
            }).OrderBy(g => g.Key.Value)
            .Select(g => new SelectListItem
            {
                Text = g.Key.Text,
                Value = g.Key.Value
            });
            model.cboPublicaciones = result.ToList();
            return View(model);
        }


        public JsonResult listarBases(int idPub)
        {
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();
            var Bases = publicaciones.Where(c => c.CODPUB == idPub);
            return Json(Bases);
        }


        public JsonResult ListarConsultas(int? idPub, int? idBase)
        {

            var codigoSOA = (int)Session["sessionCodigoResponsableLogin"];
            var listadoConsultas = this._consultaLogic.ListarConsultaPorPublicacion_Base_SOA(codigoSOA, idPub, idBase);
            var data = listadoConsultas.Select(c => new string[] { 
                c.CODCON.ToString(),
                c.NUMPUB,
                c.DESBAS,
                c.DESCON,
                c.ESTDES,
                c.ESTCON.ToString()
            });

            return Json(data);

        }

        public PartialViewResult ModalRegistrarConsulta() {
            var model = new RegistroConsultaModel();
            var publicaciones = this._publicacionYBasesLogic.ListarPublicacionesEstadoPublicadaYBases();

            var listaPublicacion = (from c in publicaciones select new SelectListItem() { Value = c.CODPUB.ToString(), Text = c.NUMPUB }).ToList();
            var result = listaPublicacion.GroupBy(c => new
            {
                c.Value,
                c.Text
            }).OrderBy(g => g.Key.Value)
            .Select(g => new SelectListItem
            {
                Text = g.Key.Text,
                Value = g.Key.Value
            });
            model.cboPublicaciones = result.ToList();
            return PartialView("_NuevaConsulta", model);
        }


        public JsonResult GrabarConsulta(int idPub, int idBase, string consulta) {
            try
            {

                var listaAbso = this._absolucionConsultaLogic.ListarTodos().ToList();

                var existe = listaAbso.Where(c => c.CODPUB == idPub && c.CODBASE == idBase).Any();
                if (existe)
                    return Json(new MensajeRespuesta("Ya existe una absolución registrada para esta Publicación y Entidad, ya no se pueden registrar consultas", false));



                var codigoSOA = (int)Session["sessionCodigoResponsableLogin"];
                this._consultaLogic.InsertConsulta(codigoSOA, idPub, idBase, consulta);
                return Json(new MensajeRespuesta("Grabo la consulta satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo grabar la consulta", false));
            }
        }

        public JsonResult EliminarConsulta(int id) {
            try
            {
                this._consultaLogic.DeleteConsulta(id);
                return Json(new MensajeRespuesta("Elimino la consulta satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo eliminar la consulta", false));
            }                
        }

        public JsonResult EnviarConsulta(int id)
        {
            try
            {
                this._consultaLogic.SendConsulta(id);
                return Json(new MensajeRespuesta("Envio la consulta a la Contraloria General de la Republica satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("No se pudo enviar la consulta", false));
            }
        }

    }
}