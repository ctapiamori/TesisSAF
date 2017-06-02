using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class MenuModel
    {
        public int CODMEN { get; set; }
        [Display(Name="Descripcion")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [MaxLength(199, ErrorMessage = Mensaje.MensajeCampoLongitudIncorrecta)]
        public string DESMEN { get; set; }

        [Display(Name = "Ruta")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [MaxLength(100, ErrorMessage = Mensaje.MensajeCampoLongitudIncorrecta)]
        public string RUTAMEN { get; set; }

        [Display(Name = "Icono")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string ICONCSS { get; set; }

        public IList<SelectListItem> ListaIconos { get; set; }

        public MenuModel() {
            ListaIconos = new List<SelectListItem>();
            ListaIconos.Add(new SelectListItem() { Text = "DISCO DURO", Value = "fa fa-save" });
            ListaIconos.Add(new SelectListItem() { Text = "LAPIZ EDITAR", Value = "fa fa-edit" });
 
        }
    }
}