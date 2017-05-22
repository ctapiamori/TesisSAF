using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class EntidadModel
    {

        public int CodigoEntidad { get; set; }

        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string Mision { get; set; }
        public string Vision { get; set; }
        public string BaseLegal { get; set; }
        public string ActividadPrincipal { get; set; }
        public string DomicilioEntidad { get; set; }
        public string PaginaWeb { get; set; }

        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        
        public IEnumerable<SelectListItem> ListaDepartamento { get; set; }
        public IEnumerable<SelectListItem> ListaProvincia { get; set; }
        public IEnumerable<SelectListItem> ListaDistrito { get; set; }


        
        public string ApellidoRepLegal { get; set; }
        public string NombreRepLegal { get; set; }
        public string CorreoRepLegal { get; set; }
        public string TelefonoRepLegal { get; set; }
        public string CelularRepLegal { get; set; }

        public EntidadModel() {
            this.ListaDepartamento = new List<SelectListItem>();
            this.ListaProvincia = new List<SelectListItem>();
            this.ListaDistrito = new List<SelectListItem>();
        }

    }
}