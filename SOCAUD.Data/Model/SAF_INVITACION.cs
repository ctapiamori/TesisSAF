//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOCAUD.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SAF_INVITACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAF_INVITACION()
        {
            this.SAF_INVITACIONDETALLE = new HashSet<SAF_INVITACIONDETALLE>();
        }
    
        public int CODINV { get; set; }
        public string NUMINV { get; set; }
        public string INDCANINV { get; set; }
        public Nullable<System.DateTime> FECACEPINV { get; set; }
        public Nullable<System.DateTime> FECMAXPREPROINV { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
        public Nullable<int> CODSOA { get; set; }
        public Nullable<int> CODAUD { get; set; }
        public Nullable<int> CODSERAUD { get; set; }
        public Nullable<int> CODPUB { get; set; }
        public Nullable<int> ESTINV { get; set; }
        public Nullable<int> CODCAR { get; set; }
    
        public virtual SAF_AUDITOR SAF_AUDITOR { get; set; }
        public virtual SAF_SOA SAF_SOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_INVITACIONDETALLE> SAF_INVITACIONDETALLE { get; set; }
        public virtual SAF_SERVICIOAUDITORIA SAF_SERVICIOAUDITORIA { get; set; }
        public virtual SAF_PUBLICACION SAF_PUBLICACION { get; set; }
    }
}
