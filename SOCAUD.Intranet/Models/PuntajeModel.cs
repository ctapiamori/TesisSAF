using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCAUD.Intranet.Models
{
    public class PuntajeModel
    {
        [Display(Name="Puntaje de Capacitacion")]
        public int PuntajeExp { get; set; }
        [Display(Name = "Puntaje de Experiencia")]
        public int PuntajeCapa { get; set; }
    }
}