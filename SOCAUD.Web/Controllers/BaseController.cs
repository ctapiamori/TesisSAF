using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: BaseController
        public static string usuarioLogueado { get; set; }
    }
}