using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class EntidadModel
    {

        public int CodigoEntidad { get; set; }
        [Display(Name = "RUC")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string Ruc { get; set; }

        [Display(Name = "Razon Social")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string RazonSocial { get; set; }

        [Display(Name = "Mision")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string Mision { get; set; }

        [Display(Name = "Vision")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string Vision { get; set; }

        [Display(Name = "Base Legal")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string BaseLegal { get; set; }

        [Display(Name = "Actividades Principales")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string ActividadPrincipal { get; set; }

        [Display(Name = "Domicilio")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string DomicilioEntidad { get; set; }

        [Display(Name = "Pagina Web")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string PaginaWeb { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string Departamento { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string Provincia { get; set; }

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string Distrito { get; set; }
        
        public IEnumerable<SelectListItem> ListaDepartamento { get; set; }
        public IEnumerable<SelectListItem> ListaProvincia { get; set; }
        public IEnumerable<SelectListItem> ListaDistrito { get; set; }


        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string ApellidoRepLegal { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string NombreRepLegal { get; set; }

        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string CorreoRepLegal { get; set; }

        [Display(Name = "Telefono (Fijo)")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string TelefonoRepLegal { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string CelularRepLegal { get; set; }

        public EntidadModel() {
            this.ListaDepartamento = new List<SelectListItem>();
            this.ListaProvincia = new List<SelectListItem>();
            this.ListaDistrito = new List<SelectListItem>();
        }

    }
}