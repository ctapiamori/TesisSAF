using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class AbsolucionConsultasModel
    {
        [Display(Name = "Publicacion")]
        public int CodigoPubAbs { get; set; }
        [Display(Name = "Entidad")]
        public int CodigoBaseAbs { get; set; }
        [Display(Name="Fecha y Hora")]
        public string Hora { get; set; }

        public IEnumerable<SelectListItem> lista { get; set; }

        public IEnumerable<SelectListItem> listaBase { get; set; }

        public AbsolucionConsultasModel() {
            this.lista = new List<SelectListItem>();
            this.listaBase = new List<SelectListItem>();
        }
    }
}