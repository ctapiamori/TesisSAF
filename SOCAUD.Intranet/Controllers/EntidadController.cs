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
    public class EntidadController : Controller
    {

        ISafEntidadLogic entidadLogic;
        ISafGeneralLogic generalLogic;
        public EntidadController() {
            this.entidadLogic = new SafEntidadLogic();
            this.generalLogic = new SafGeneralLogic();
        }
        
        // GET: Entidad
        public ActionResult Index()
        {
            var codigoEntidad = Convert.ToInt32(Session["codigoEntidadDelUsuario"]);
            var infoEntidad = this.entidadLogic.BuscarPorId(codigoEntidad);
            var model = new EntidadModel();
            model.Ruc = infoEntidad.RUCENT.Trim();
            model.CodigoEntidad = infoEntidad.CODENT;
            model.Mision = infoEntidad.MISENT;
            model.Vision = infoEntidad.VISENT;
            model.ActividadPrincipal = infoEntidad.ACTPRIENT;
            model.BaseLegal = infoEntidad.BASLEGENT;
            model.RazonSocial = infoEntidad.RAZSOCENT;
            model.DomicilioEntidad = infoEntidad.DOMLEGENT;
            model.PaginaWeb = infoEntidad.PAGWEBENT;

            model.Departamento = string.IsNullOrEmpty(infoEntidad.CODDEPENT)? string.Empty : infoEntidad.CODDEPENT.Trim();
            model.Provincia = string.IsNullOrEmpty(infoEntidad.CODPROENT) ? string.Empty : infoEntidad.CODPROENT.Trim();  
            model.Distrito = string.IsNullOrEmpty(infoEntidad.CODDISENT) ? string.Empty : infoEntidad.CODDISENT.Trim(); 

            model.NombreRepLegal = infoEntidad.NOMREPLEGENT;
            model.ApellidoRepLegal = infoEntidad.APEREPLEGENT;
            model.TelefonoRepLegal = infoEntidad.TELREPLEGENT;
            model.CorreoRepLegal = infoEntidad.CORREPLEGENT;
            model.CelularRepLegal = infoEntidad.CELREPLEGENT;

            var listaDepartamentos = generalLogic.ListarDepartamentos();

            if (string.IsNullOrEmpty(model.Departamento))
                model.ListaDepartamento = (from c in listaDepartamentos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString() });
            else { 
                model.ListaDepartamento = (from c in listaDepartamentos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDEP.ToString(), Selected = (c.CODDEP.ToString() == model.Departamento) });
                var listaProvincias = generalLogic.ListarProvincias(Convert.ToInt32(model.Departamento));
                model.ListaProvincia = (from c in listaProvincias select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString(), Selected = (c.CODPROV.ToString() == model.Provincia) });
            }

            if (!string.IsNullOrEmpty(model.Provincia)) {
                var listaDistritos = generalLogic.ListarDistritos(Convert.ToInt32(model.Provincia));
                model.ListaDistrito = (from c in listaDistritos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString(), Selected = (c.CODDIS.ToString() == model.Distrito) });            
            }

            return View(model);
        }


        public JsonResult CargarProvincia(int id)
        {
            var provincias = this.generalLogic.ListarProvincias(id);
            //var lista = (from c in modelEntity.SAF_PROVINCIA.ToList().Where(c => c.CODDEP == id && c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            var lista = (from c in provincias select new SelectListItem() { Text = c.NOMDEP, Value = c.CODPROV.ToString() }).ToList();
            return Json(lista);
        }

        public JsonResult CargarDistrito(int id)
        {
            var distritos = this.generalLogic.ListarDistritos(id);
            //var lista = (from c in modelEntity.SAF_DISTRITO.ToList().Where(c => c.CODPROV == id && c.ESTREG == "1") select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            var lista = (from c in distritos select new SelectListItem() { Text = c.NOMDEP, Value = c.CODDIS.ToString() }).ToList();
            return Json(lista);
        }

        public JsonResult GrabarInfoEntidad(EntidadModel model) {
            try
            {
                var entidad = new SAF_ENTIDADES();
                entidad.CODENT = model.CodigoEntidad;
                entidad.RAZSOCENT = model.RazonSocial;
                entidad.VISENT = model.Vision;
                entidad.MISENT = model.Mision;
                entidad.ACTPRIENT = model.ActividadPrincipal;
                entidad.BASLEGENT = model.BaseLegal;
                entidad.APEREPLEGENT = model.ApellidoRepLegal;
                entidad.NOMREPLEGENT = model.NombreRepLegal;
                entidad.CELREPLEGENT = model.CelularRepLegal;
                entidad.TELREPLEGENT = model.TelefonoRepLegal;
                entidad.CORREPLEGENT = model.CorreoRepLegal;
                entidad.DOMLEGENT = model.DomicilioEntidad;
                entidad.PAGWEBENT = model.PaginaWeb;
                entidad.CODDEPENT = model.Departamento;
                entidad.CODDISENT = model.Distrito;
                entidad.CODPROENT = model.Provincia;

                this.entidadLogic.ModificarInformacion(entidad);
                return Json(new MensajeRespuesta("Se completo la modificacion de la entidad Satisfactoriamente", true));
            }
            catch (Exception)
            {
                return Json(new { Mensaje = "Ocurrio un inconveniente vuelva a intentarlo" });
            }


            
        }

    }
}