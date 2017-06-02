using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class PerfilModel
    {
        public int CODPER { get; set; }


        [Display(Name = "Nombre Perfil")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string NOMPER { get; set; }

        [Display(Name = "Tipo Perfil")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string TIPOPER { get; set; }

        [Display(Name = "Descripcion Perfil")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string DESPER { get; set; }

         

        public IList<SelectListItem> ListaTipoPerfil { get; set; }

        public PerfilModel() {
            this.ListaTipoPerfil = new List<SelectListItem>();
            ListaTipoPerfil.Add(new SelectListItem() { Text = "INTERNOS", Value = "I" });
            ListaTipoPerfil.Add(new SelectListItem() { Text = "EXTERNOS", Value = "E" });
        }
    }
}