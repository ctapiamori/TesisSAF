//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOCAUD.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SAF_CONSULTA
    {
        public int CODCON { get; set; }
        public string DESCON { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
        public Nullable<int> CODPUB { get; set; }
        public Nullable<int> CODSOA { get; set; }
        public Nullable<int> ESTCON { get; set; }
    
        public virtual SAF_PUBLICACION SAF_PUBLICACION { get; set; }
        public virtual SAF_SOA SAF_SOA { get; set; }
    }
}
