using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class WorkflowController : Controller
    {
        // GET: Workflow
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarWorkflow()
        {
            return null;
        }

        public JsonResult AprobarSolicitudJefe(int idWorkflow)
        {
            return null;
        }

        public JsonResult CancelarSolicitudJefe(int idWorkflow)
        {
            return null;
        }

        public JsonResult AprobarSolicitudGerente(int idWorkFlow)
        {
            return null;
        }
    }
}