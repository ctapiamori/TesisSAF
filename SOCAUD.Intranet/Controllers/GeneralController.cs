using SOCAUD.Common.Helpers;
using SOCAUD.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DescargarArchivo(long id)
        {
            var archivo = Archivo.DescargarArchivo(id);
            if (archivo == null || archivo.fileBytes == null)
            {
                return HttpNotFound();
            }
            return File(archivo.fileBytes, Texto.TipoMime(archivo.ARCNOMBFISICO), archivo.NOMBLABEL);
        }
    }
}