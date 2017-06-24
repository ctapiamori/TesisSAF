using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
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
        public ConsultasGeneralController()
        {
            _publicacionLogic = new SafPublicacionLogic();
            _cronogramaLogic = new SafCronogramaLogic();
        }

        public ActionResult ConsultaConcurso()
        {
             return View();
        }


    }
}