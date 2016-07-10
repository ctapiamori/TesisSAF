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
    
    public partial class SAF_PUBLICACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAF_PUBLICACION()
        {
            this.SAF_CONSULTA = new HashSet<SAF_CONSULTA>();
            this.SAF_CORTE_AUDITOR = new HashSet<SAF_CORTE_AUDITOR>();
            this.SAF_CORTE_AUDITOR_CARGO = new HashSet<SAF_CORTE_AUDITOR_CARGO>();
            this.SAF_INVITACION = new HashSet<SAF_INVITACION>();
            this.SAF_PROPUESTA = new HashSet<SAF_PROPUESTA>();
            this.SAF_PUBLICACIONBASE = new HashSet<SAF_PUBLICACIONBASE>();
        }
    
        public int CODPUB { get; set; }
        public string NUMPUB { get; set; }
        public Nullable<System.DateTime> FECMAXCRECON { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
        public Nullable<int> CODBAS { get; set; }
        public Nullable<System.DateTime> FECMAXCONS { get; set; }
        public Nullable<System.DateTime> FECMAXRESCONS { get; set; }
        public Nullable<System.DateTime> FECMAXPREPROP { get; set; }
        public Nullable<int> ESTPUB { get; set; }
        public Nullable<int> CODCRO { get; set; }
    
        public virtual SAF_BASE SAF_BASE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_CONSULTA> SAF_CONSULTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_CORTE_AUDITOR> SAF_CORTE_AUDITOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_CORTE_AUDITOR_CARGO> SAF_CORTE_AUDITOR_CARGO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_INVITACION> SAF_INVITACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_PROPUESTA> SAF_PROPUESTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_PUBLICACIONBASE> SAF_PUBLICACIONBASE { get; set; }
        public virtual SAF_CRONOGRAMA SAF_CRONOGRAMA { get; set; }
    }
}