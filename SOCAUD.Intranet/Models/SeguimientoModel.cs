using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class SeguimientoModel
    {
        public int IdSoa { get; set; }
        public int IdPublicacion { get; set; }
        public int IdPropSelec { get; set; }
        public string IdPenalidadPuntosMenos { get; set; }
        public IEnumerable<SelectListItem> cboSOA { get; set; }
        public IEnumerable<SelectListItem> cboPublicacion { get; set; }

        public IEnumerable<SelectListItem> cboTipoPenalidad { get; set; }

        public IEnumerable<SeguimientoPropuestaModel> listaPropuestas { get; set; }
        public SeguimientoModel() {
            this.cboSOA = new List<SelectListItem>();
            this.cboPublicacion = new List<SelectListItem>();
            this.listaPropuestas = new List<SeguimientoPropuestaModel>();
            this.cboTipoPenalidad = new List<SelectListItem>();
        }
    }

    public class SeguimientoPropuestaModel {
        public int CodPropuesta { get; set; }
        public int CodSoa { get; set; }
        public int CodPublicacion { get; set; }
        public string Periodo { get; set; }
        public string Base { get; set; }
        
    }
}


