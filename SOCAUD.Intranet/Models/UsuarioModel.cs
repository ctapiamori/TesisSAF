using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class UsuarioModel
    {
        public int CODUSU { get; set; }
        public string NOMUSU { get; set; }
        public string PASUSU { get; set; }
        public string NOMPERUSU { get; set; }
        public string APEPERUSU { get; set; }
        public string DNIUSU { get; set; }
        public Nullable<int> TIPCARUSU { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
        public Nullable<int> CODENT { get; set; }

        public IEnumerable<SelectListItem> cboListaCargo { get; set; }
        public IEnumerable<SelectListItem> cboListaEntidad { get; set; }



        public UsuarioModel() { 
        
            this.cboListaCargo = new List<SelectListItem>();
            this.cboListaEntidad = new List<SelectListItem>();
        }
    }
}