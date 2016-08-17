using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCAUD.Intranet.Models
{
    public class CronogramaModel
    {
        [Display(Name = "Codigo")]
        public int Codigo { get; set; }
        [Display(Name="Año")]
        public int Anio { get; set; }
        [Display(Name="Fecha Publicación")]
        public string FechaPublicacion { get; set; }
        [Display(Name = "Fecha Máxima Creación Base")]
        public string FechaMaximaCreacionBase { get; set; }
        public int NumperoRepublicaciones { get; set; }
        [Display(Name = "Estado")]
        public string EstadoCronograma { get; set; }

        public CronoEntidadModel Entidad { get; set; }
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