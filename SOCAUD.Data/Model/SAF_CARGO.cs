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
    
    public partial class SAF_CARGO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAF_CARGO()
        {
            this.SAF_SERAUDCARGO = new HashSet<SAF_SERAUDCARGO>();
            this.SAF_CORTE_AUDITOR_CARGO = new HashSet<SAF_CORTE_AUDITOR_CARGO>();
        }
    
        public int CODCAR { get; set; }
        public string NOMCAR { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_SERAUDCARGO> SAF_SERAUDCARGO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_CORTE_AUDITOR_CARGO> SAF_CORTE_AUDITOR_CARGO { get; set; }
    }
}
