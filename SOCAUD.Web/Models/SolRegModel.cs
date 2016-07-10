using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Models
{
    public class SolRegModel
    {
        [Display(Name = "Tipo de Solicitud")]
        public IEnumerable<SelectListItem> cboTipoSolicitud { get; set; }
        public AuditorModel auditor { get; set; }
        public SolicitudModel solicitud { get; set; }
        public SoaModel soa { get; set; }

        public SolRegModel()
        {
            this.cboTipoSolicitud = new List<SelectListItem>();
            this.auditor = new AuditorModel();
            this.soa = new SoaModel();
        }
    }
}