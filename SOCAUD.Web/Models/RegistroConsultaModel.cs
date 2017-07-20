using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Models
{


    public class ConsultaModel
    {
        [Display(Name="Publicacion")]
        public int codPublicacion { get; set; }
        public List<SelectListItem> cboPublicaciones { get; set; }

        [Display(Name = "Entidad")]
        public int codBase { get; set; }
        public List<SelectListItem> cboBases { get; set; }

        public ConsultaModel()
        {
            cboPublicaciones = new List<SelectListItem>();
            cboBases = new List<SelectListItem>();
        }

    }

    public class RegistroConsultaModel
    {

        [Display(Name = "Publicacion")]
        public int codRegPublicacion { get; set; }
        public List<SelectListItem> cboPublicaciones { get; set; }

        [Display(Name = "Entidad")]
        public int codRegBase { get; set; }
        
        public List<SelectListItem> cboBases { get; set; }


        public string Pregunta { get; set; }

        public RegistroConsultaModel() {
            cboPublicaciones = new List<SelectListItem>();
            cboBases = new List<SelectListItem>();
        }

    }
}