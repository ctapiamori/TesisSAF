using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class AuditorModel
    {

        public int codigoSolicitud { get; set; }
        public string numSolicitud { get; set; }

        public int estadoSolicitud { get; set; }

        [AllowHtml]
        public string observacionSolicitud { get; set; }

        [Display(Name = "Codigo Auditor")]
        public int codAud { get; set; }
        [Display(Name = "Nombre")]
        public string nomAud { get; set; }
        [Display(Name = "Apellidos")]
        public string apeComAud { get; set; }
        [Display(Name = "DNI")]
        public string dniAud { get; set; }
        [Display(Name = "Celular")]
        public string celAud { get; set; }
        [Display(Name = "Telefono")]
        public string telAud { get; set; }
        [Display(Name = "Direccion")]
        public string dirAud { get; set; }
        [Display(Name = "Departamento")]
        public Nullable<int> codDeparAud { get; set; }
        [Display(Name = "Provincia")]
        public Nullable<int> codProvAud { get; set; }
        [Display(Name = "Distrito")]
        public Nullable<int> codDisAud { get; set; }
        [Display(Name = "Correo")]
        public string corAud { get; set; }
        [Display(Name = "Sexo")]
        public string sexAud { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        public string fecNacAud { get; set; }
        [Display(Name = "Auditor")]
        public string indEsAud { get; set; }
        [Display(Name = "Especialista")]
        public string indEsEsp { get; set; }
        [Display(Name = "Socio")]
        public string indEsSoc { get; set; }
        [Display(Name = "N° Certificado")]
        public string codCerAud { get; set; }
        [Display(Name = "Archivo Certificado")]
        public byte[] arcCerAud { get; set; }
        [Display(Name = "Usuario")]
        public string nomUsu { get; set; }
        [Display(Name = "Contraseña")]
        public string pasUsu { get; set; }
        [Display(Name = "Repetir Contraseña")]
        public string repPasUsu { get; set; }
        [Display(Name = "Archivo")]
        public string nombreArchivo { get; set; }

        public List<SelectListItem> cboDepartamento { get; set; }
        public List<SelectListItem> cboProvincia { get; set; }
        public List<SelectListItem> cboDistrito { get; set; }
        public Nullable<long> codArchivo { get; set; }


        public AuditorModel()
        {
            this.cboDepartamento = new List<SelectListItem>();
            this.cboProvincia = new List<SelectListItem>();
            this.cboDistrito = new List<SelectListItem>();
        }
    }
}