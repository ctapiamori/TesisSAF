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
    
    public partial class SAF_SERVICIOAUDITORIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAF_SERVICIOAUDITORIA()
        {
            this.SAF_INVITACION = new HashSet<SAF_INVITACION>();
            this.SAF_SERAUDCARGO = new HashSet<SAF_SERAUDCARGO>();
        }
    
        public int CODSERAUD { get; set; }
        public string PERSERAUD { get; set; }
        public Nullable<System.DateTime> FECINISERAUD { get; set; }
        public Nullable<System.DateTime> FECFINSERAUD { get; set; }
        public Nullable<decimal> RETECOSERAUD { get; set; }
        public Nullable<decimal> VIASERAUD { get; set; }
        public Nullable<decimal> IGVSERAUD { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
        public Nullable<int> CODBAS { get; set; }
    
        public virtual SAF_BASE SAF_BASE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_INVITACION> SAF_INVITACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_SERAUDCARGO> SAF_SERAUDCARGO { get; set; }
    }
}
