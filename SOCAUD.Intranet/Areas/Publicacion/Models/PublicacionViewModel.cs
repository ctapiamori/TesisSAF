using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Areas.Publicacion.Models
{
    public class PublicacionViewModel
    {
        //[Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Cronograma")]
        public int Cronograma { get; set; }
        //[Display(Name = "Base")]
        //public int Base { get; set; }
        [Display(Name = "Codigo Publicaicion")]
        public int? CodigoPublicacion { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha Maxima elaborar consulta")]
        public string FechaMaximaCreacionConsulta { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha maxima publicar concurso")]
        public string FechaMaximaPublicacionConcurso { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha maxima responder consulta")]
        public string FechaMaximaResponderConsultas { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha maxima presentacion propuestas")]
        public string FechaMaximaPresentacionPropuestas { get; set; }

        public IEnumerable<SelectListItem> Cronogramas { get; set; }
        //public IEnumerable<SelectListItem> Bases { get; set; }

        public int estadoPublicacion { get; set; }

        public PublicacionViewModel()
        {
            Cronogramas = new List<SelectListItem>();
            //Bases = new List<SelectListItem>();
        }
    }
}