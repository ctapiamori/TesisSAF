using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCAUD.Intranet.Models
{
    public class WorkFlowUsuarioModel
    {
        public string TIPDOC { get; set; }
        public string DESTIPDOC { get; set; }
        public Nullable<int> CODDOC { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public string USUREG { get; set; }
    }
}