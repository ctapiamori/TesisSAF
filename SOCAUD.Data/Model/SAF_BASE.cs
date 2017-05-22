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
    
    public partial class SAF_BASE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAF_BASE()
        {
            this.SAF_SERVICIOAUDITORIA = new HashSet<SAF_SERVICIOAUDITORIA>();
            this.SAF_BASEENTREGABLE = new HashSet<SAF_BASEENTREGABLE>();
            this.SAF_PUBLICACION = new HashSet<SAF_PUBLICACION>();
            this.SAF_PUBLICACIONBASE = new HashSet<SAF_PUBLICACIONBASE>();
        }
    
        public int CODBAS { get; set; }
        public Nullable<System.DateTime> FECMAXCREPUBBAS { get; set; }
        public string DESBAS { get; set; }
        public string NUMBAS { get; set; }
        public Nullable<decimal> TOTRETECOBAS { get; set; }
        public Nullable<decimal> TOTVIABAS { get; set; }
        public string FIRPCAOBBAS { get; set; }
        public string FIRINTBAS { get; set; }
        public Nullable<decimal> TOTIGVBAS { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }
        public Nullable<int> CODCROENT { get; set; }
        public Nullable<int> ESTBAS { get; set; }
        public Nullable<int> CODCRO { get; set; }
        public Nullable<int> CORBAS { get; set; }
    
        public virtual SAF_CRONOENTIDAD SAF_CRONOENTIDAD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_SERVICIOAUDITORIA> SAF_SERVICIOAUDITORIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_BASEENTREGABLE> SAF_BASEENTREGABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_PUBLICACION> SAF_PUBLICACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAF_PUBLICACIONBASE> SAF_PUBLICACIONBASE { get; set; }
    }
}
