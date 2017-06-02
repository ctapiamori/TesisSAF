using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class CronogramaModel
    {
        [Display(Name = "Codigo")]
        public int Codigo { get; set; }
        [Display(Name="Año")]
        public int Anio { get; set; }

        [Display(Name="Fecha Publicación")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string FechaPublicacion { get; set; }


        [Display(Name = "Fecha Máxima Creación Base")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string FechaMaximaCreacionBase { get; set; }

        public int NumperoRepublicaciones { get; set; }
        [Display(Name = "Estado")]
        public string EstadoCronograma { get; set; }

        public CronoEntidadModel Entidad { get; set; }

        public IList<SelectListItem> ListaAnios { get; set; }

        public CronogramaModel() {
            this.ListaAnios = new List<SelectListItem>();

            for (int i = DateTime.Now.Year + 5; i >= DateTime.Now.Year; i--)
            {
                ListaAnios.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }

        }

    }

    public class CronoEntidadModel
    {
        [Display(Name = "Codigo")]
        public int Codigo { get; set; }
        [Display(Name = "Cronograma")]
        public int Cronograma { get; set; }
        [Required]
        [Display(Name = "Entidad")]
        public int Entidad { get; set; }
        [Required]
        [Display(Name = "Fecha Inicio")]
        public string FechaInicio { get; set; }
        [Required]
        [Display(Name = "Fecha Termino")]
        public string FechaTermino { get; set; }
    }
}