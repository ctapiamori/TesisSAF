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
        [Display(Name="Nombre")]
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

        [Display(Name = "Orden")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int ORDEN { get; set; }

        public IList<SelectListItem> ListaIconos { get; set; }

        public MenuModel() {
            ListaIconos = new List<SelectListItem>();
            ListaIconos.Add(new SelectListItem() { Text = "DISCO DURO", Value = "fa fa-save" });
            ListaIconos.Add(new SelectListItem() { Text = "LINEAS", Value = "fa fa-bars" });
            ListaIconos.Add(new SelectListItem() { Text = "LAPIZ EDITAR", Value = "fa fa-edit" });
            ListaIconos.Add(new SelectListItem() { Text = "REPORTE", Value = "fa fa-bar-chart" });
            ListaIconos.Add(new SelectListItem() { Text = "PUBLICACION", Value = "fa fa-file-powerpoint-o" });
            ListaIconos.Add(new SelectListItem() { Text = "USUARIOS", Value = "fa fa-users" });
            ListaIconos.Add(new SelectListItem() { Text = "BUSQUEDA", Value = "fa fa-search" });
            ListaIconos.Add(new SelectListItem() { Text = "CHECK LIST", Value = "fa fa-check-square-o" });
            ListaIconos.Add(new SelectListItem() { Text = "CONFIGURACION", Value = "fa fa-cogs" });
            ListaIconos.Add(new SelectListItem() { Text = "LAPIZ", Value = "fa fa-pencil" });
            ListaIconos.Add(new SelectListItem() { Text = "HOJA LINEAS", Value = "fa fa-file-text-o" });
            ListaIconos.Add(new SelectListItem() { Text = "HOJA BLANCO", Value = "fa fa-file-o" });
            ListaIconos.OrderBy(c => c.Text);
        }
    }
}