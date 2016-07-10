using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class PropuestaModel
    {

        [Display(Name = "Publicacion")]
        public int CODPUB { get; set; }
        public int CODPRO { get; set; }

        public int CodigoPublicacion { get; set; }
        public List<SelectListItem> cboPublicaciones { get; set; }

        public PropuestaModel()
        {
            this.cboPublicaciones = new List<SelectListItem>();
        }
    }
}