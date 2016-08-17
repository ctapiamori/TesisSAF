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
    public class WorkflowController : Controller
    {
        private readonly ISafWorkFlowLogic _workflowLogic;
        private readonly ISafCronogramaLogic _cronogramaLogic;
        private readonly ISafBaseLogic _baseLogic;
        private readonly ISafPublicacionLogic _publicacionLogic;

        public WorkflowController()
        {
            this._workflowLogic = new SafWorkFlowLogic();
            this._cronogramaLogic = new SafCronogramaLogic();
            this._baseLogic = new SafBaseLogic();
            this._publicacionLogic = new SafPublicacionLogic();
        }
        // GET: Workflow
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarWorkflow()
        {
            var listado = this._workflowLogic.ListarTodos();
            var data = listado.Select(c => new string[]{ 
                c.CODWORFLO.ToString(),
                c.DESTIPDOC,
                c.NOTWORFLO,
                WebHelper.GetShortDateString(c.FECREG),
                c.ESTWORFLO.GetValueOrDefault().ToString(),
                c.TIPDOC,
                c.CODDOC.GetValueOrDefault().ToString()
            }).ToArray();

            return Json(data);
        }

        public JsonResult ListarWorkflowDocumento(int idDocumento)
        {
            var listado = this._workflowLogic.ListarPorDocumento(idDocumento);
            var data = listado.Select(c => new string[]{
                c.DESTIPDOC,
                c.NOTWORFLO,
                WebHelper.GetShortDateString(c.FECREG)
            }).ToArray();

            return Json(data);
        }

        public PartialViewResult Solicitud(int idDocumento, string tipoDocumento, int? idFlujo)
        {
            var tiposUsuario = new List<SelectListItem>();
            var estadosFlujo = new List<SelectListItem>();
            var model = new SolicitarWorkflowViewModel();
            model.IdDocumento = idDocumento;
            model.IdTipoDocumento = tipoDocumento;
            model.IdFlujo = idFlujo;
            if (tipoDocumento.Equals(Variables.CRONOGRAMA_ANUAL_ENTIDADES))
            {
                var cronograma = this._cronogramaLogic.BuscarPorId(idDocumento);
                model.Descripcion = string.Format("Cronograma Anual de Entidades : {0}", cronograma.ANIOCRO);
                model.Comentario = string.Format("Solicitud de aprobación para del Cronograma Anual de Entidades {0}", cronograma.ANIOCRO);
            }

            if (tipoDocumento.Equals(Variables.BASES_CONCURSO))
            {
                var bases = this._baseLogic.BuscarPorId(idDocumento);
                var cronograma = this._cronogramaLogic.BuscarPorId(bases.CODCRO.GetValueOrDefault());
                model.Descripcion = string.Format("Bases de Concurso : {0}", bases.NUMBAS);
                model.Comentario = string.Format("Solicitud de aprobación para la Base de Concurso {0} del Cronograma Anual de Entidades {1}.", bases.NUMBAS, cronograma.ANIOCRO);
            }

            if (tipoDocumento.Equals(Variables.PUBLICACION_CONCURSO))
            {
                var publicacion = this._publicacionLogic.BuscarPorId(idDocumento);
                var cronograma = this._cronogramaLogic.BuscarPorId(publicacion.CODCRO.GetValueOrDefault());
                model.Descripcion = string.Format("Publicación de Concurso : {0}", publicacion.NUMPUB);
                model.Comentario = string.Format("Solicitud de aprobación para la Publicación de Concurso {0} del Cronograma Anual de Entidades {1}", publicacion.NUMPUB, cronograma.ANIOCRO);
            }

            var tipoUsuarioLogIn = int.Parse(Session["tipoUsuario"].ToString());
            if (tipoUsuarioLogIn.Equals(TipoUsuario.Operador.GetHashCode()))
            {
                tiposUsuario.Add(new SelectListItem() { Value = TipoUsuario.Jefe.GetHashCode().ToString(), Text = "Jefe" });
                estadosFlujo.Add(new SelectListItem() { Value = Estado.Workflow.PendienteAprobacion.GetHashCode().ToString(), Text = "Solicitar" });
            }

            if (tipoUsuarioLogIn.Equals(TipoUsuario.Jefe.GetHashCode()))
            {
                tiposUsuario.Add(new SelectListItem() { Value = TipoUsuario.Gerente.GetHashCode().ToString(), Text = "Gerente" });
                tiposUsuario.Add(new SelectListItem() { Value = TipoUsuario.Operador.GetHashCode().ToString(), Text = "Operador" });
            }

            if (tipoUsuarioLogIn.Equals(TipoUsuario.Gerente.GetHashCode()))
            {
                tiposUsuario.Add(new SelectListItem() { Value = TipoUsuario.Jefe.GetHashCode().ToString(), Text = "Jefe" });
                tiposUsuario.Add(new SelectListItem() { Value = TipoUsuario.Operador.GetHashCode().ToString(), Text = "Operador" });
            }

            if (tipoUsuarioLogIn.Equals(TipoUsuario.Jefe.GetHashCode()) || tipoUsuarioLogIn.Equals(TipoUsuario.Gerente.GetHashCode()))
            {
                estadosFlujo.Add(new SelectListItem() { Value = Estado.Workflow.Aprobado.GetHashCode().ToString(), Text = "Aprobado" });
                estadosFlujo.Add(new SelectListItem() { Value = Estado.Workflow.Rechazado.GetHashCode().ToString(), Text = "Rechazado" });

            }

            model.TiposUsuario = tiposUsuario;
            model.EstadosFlujo = estadosFlujo;
                
            return PartialView("_SolicitudWorkflow", model);
        }

        public JsonResult GrabarSolicitud(SolicitarWorkflowViewModel data)
        {
            try
            {
                var workflow = new SAF_WORKFLOW()
                {
                    CODUSUSOL = int.Parse(Session["idUsuario"].ToString()),
                    TIPDOC = data.IdTipoDocumento,
                    CODDOC = data.IdDocumento,
                    NOTWORFLO = data.Comentario,
                    DESTIPDOC = data.Descripcion,
                    TIPCARUSU = data.TipoUsuario,
                    CODWORFLOPAR = data.IdFlujo
                };

                var tipoUsuarioLogIn = int.Parse(Session["tipoUsuario"].ToString());

                if (tipoUsuarioLogIn.Equals(TipoUsuario.Operador.GetHashCode()))
                {
                    var result = this._workflowLogic.FlujoSolicitud(workflow);
                }

                if (tipoUsuarioLogIn.Equals(TipoUsuario.Jefe.GetHashCode()) || tipoUsuarioLogIn.Equals(TipoUsuario.Gerente.GetHashCode()))
                {
                    if(data.EstadoFlujo.Equals(Estado.Workflow.Aprobado.GetHashCode()))
                        this._workflowLogic.FlujoAprobacion(workflow, data.IdFlujo.GetValueOrDefault(), tipoUsuarioLogIn);
                    else
                        this._workflowLogic.FlujoRechazo(workflow, data.IdFlujo.GetValueOrDefault(), tipoUsuarioLogIn);
                }

                return Json(new MensajeRespuesta("Se registro satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new MensajeRespuesta("Ocurrio un error no controlado, comuniquese con su Administrador.", TipoMensaje.error));
            }
        }
    }
}