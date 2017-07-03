using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class ConsultaConcurso
    {
        [Display(Name = "Cronograma")]
        public int CodigoCronograma { get; set; }
        [Display(Name="N° Concurso")]
        public string NumeroConcurso { get; set; }
        [Display(Name = "Fecha Inicio")]
        public string FechaInicio { get; set; }
        [Display(Name = "Fecha Fin")]
        public string FechaFin { get; set; }

        public IEnumerable<SelectListItem> cboCronograma { get; set; }

        public ConsultaConcurso() {
            this.cboCronograma = new List<SelectListItem>();
        }
     
    }
}