using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCAUD.Web.Models
{
    public class SolicitudModel
    {

        [Display(Name = "Codigo Solicitud")]
        public int codSol { get; set; }
        [Display(Name = "Nº Solicitud")]
        public string numSol { get; set; }
        [Display(Name = "Descripción Solicitud")]
        public string desSol { get; set; }
        [Display(Name = "Tipo Solicitud")]
        public Nullable<int> codTipSol { get; set; }
        [Display(Name = "Estado")]
        public string estSol { get; set; }
        [Display(Name = "SOA")]
        public Nullable<int> codSoaSol { get; set; }
        [Display(Name = "Auditor")]
        public Nullable<int> codAudSol { get; set; }
    }
}