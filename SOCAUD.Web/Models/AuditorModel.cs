using SOCAUD.Common.Constantes;
using SOCAUD.Common.Infraestructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Models
{
    public class AuditorModel
    {

        [AllowHtml]
        [Display(Name = "Observaciones")]
        public string observacionSolicitud { get; set; }

        public int codigoSolicitud { get; set; }

        [Display(Name = "Codigo Auditor")]
        public int codAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[A-Za-z áéíóúÁÉÍÓÚñÑ]+", ErrorMessage = Mensaje.MensajeSoloLetras)]
        [Display(Name = "Nombre")]
        public string nomAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[A-Za-z áéíóúÁÉÍÓÚñÑ]+", ErrorMessage = Mensaje.MensajeSoloLetras)]
        [Display(Name = "Apellidos")]
        public string apeComAud { get; set; }



        [Display(Name = "DNI")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        [MaxLength(8, ErrorMessage = "Ingrese un DNI valido")]
        public string dniAud { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string celAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Telefono")]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string telAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Direccion")]
        public string dirAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Departamento")]
        public Nullable<int> codDeparAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Provincia")]
        public Nullable<int> codProvAud { get; set; }

        public Nullable<int> codProvAudSelect { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Distrito")]
        public Nullable<int> codDisAud { get; set; }

        public Nullable<int> codDisAudSelect { get; set; }

        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = Mensaje.MensajeCampoDebeSerCorreo)]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Correo")]
        public string corAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Sexo")]
        public string sexAud { get; set; }

        public List<SelectListItem> cboSexo { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha Nacimiento")]
        public string fecNacAud { get; set; }
        [Display(Name = "Auditor")]
        public string indEsAud { get; set; }
        [Display(Name = "Especialista")]
        public string indEsEsp { get; set; }
        [Display(Name = "Socio")]
        public string indEsSoc { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        [Display(Name = "N° Certificado")]
        public string codCerAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Archivo Certificado")]
        public byte[] arcCerAud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Usuario")]
        public string nomUsu { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Contraseña")]
        public string pasUsu { get; set; }

        //[System.ComponentModel.DataAnnotations.Compare("pasUsu", ErrorMessage= "Debe ser igual a la contraseña")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Repetir Contraseña")]
        public string repPasUsu { get; set; }


        [Display(Name = "Archivo")]
        public string nombreArchivo { get; set; }

        public List<SelectListItem> cboDepartamento { get; set; }
        public List<SelectListItem> cboProvincia { get; set; }
        public List<SelectListItem> cboDistrito { get; set; }
        public Nullable<long> codArchivo { get; set; }
        public int estadoSolicitud { get; set; }

        public HttpPostedFileBase archivoCertificadoAuditor { get; set; }

        public AuditorModel()
        {
            this.cboDepartamento = new List<SelectListItem>();
            this.cboProvincia = new List<SelectListItem>();
            this.cboDistrito = new List<SelectListItem>();
            this.cboSexo = new List<SelectListItem>();

            cboSexo.Add(new SelectListItem() { Value = "M", Text = "Masculino" });
            cboSexo.Add(new SelectListItem() { Value = "F", Text = "Femenino" });
        }



    }

    public class CapacitacionModel
    {
        public int? codSolCap { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string desSolCap { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string fechaInicioSolCap { get; set; }

        public string fechaInicioSolCapYMD { get { return string.IsNullOrEmpty(this.fechaInicioSolCap) ? "" : this.fechaInicioSolCap.Split('/')[2] + "-" + this.fechaInicioSolCap.Split('/')[1] + "-" + this.fechaInicioSolCap.Split('/')[0]; } }

        [Display(Name = "Fecha Fin")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [IsDateAfter("fechaInicioSolCap", true, ErrorMessage = "Esta fecha debe ser mayor a la fecha inicial")]
        public string fechaFinSolCap { get; set; }

        public string fechaFinSolCapYMD { get { return string.IsNullOrEmpty(this.fechaFinSolCap) ? "" : this.fechaFinSolCap.Split('/')[2] + "-" + this.fechaFinSolCap.Split('/')[1] + "-" + this.fechaFinSolCap.Split('/')[0]; } }

        [Display(Name = "Horas")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int? numHorasSolCap { get; set; }

        public int? codSol { get; set; }

        [Display(Name = "Universidad")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int? codUni { get; set; }

        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int? codCar { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int? codTipCapa { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int? codCatCapa { get; set; }

        [Display(Name = "Archivo")]
        public string nombreArchivoCapa { get; set; }
        public long? codArchivoCapa { get; set; }

        public List<SelectListItem> Universidades { get; set; }
        public List<SelectListItem> Especialidades { get; set; }
        public List<SelectListItem> Tipos { get; set; }
        public List<SelectListItem> Categorias { get; set; }

        public HttpPostedFileBase archivoCapaFile { get; set; }
    }

    public class ExperienciaModel
    {
        public int? codSolExp { get; set; }
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Descripción")]
        public string descSolExp { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha Inicio")]
        public string fechaInicioSolExp { get; set; }

        public string fechaInicioSolExpYMD { get { return string.IsNullOrEmpty(this.fechaInicioSolExp) ? "" : this.fechaInicioSolExp.Split('/')[2] + "-" + this.fechaInicioSolExp.Split('/')[1] + "-" + this.fechaInicioSolExp.Split('/')[0]; } }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Fecha Fin")]
        [IsDateAfter("fechaInicioSolExp", true, ErrorMessage = "Esta fecha debe ser mayor a la fecha inicial")]
        public string fechaFinSolExp { get; set; }

        public string fechaFinSolExpYMD { get { return string.IsNullOrEmpty(this.fechaFinSolExp) ? "" : this.fechaFinSolExp.Split('/')[2] + "-" + this.fechaFinSolExp.Split('/')[1] + "-" + this.fechaFinSolExp.Split('/')[0]; } }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Horas")]
        public int? numHorasSolExp { get; set; }
        public int? codSol { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Empresa")]
        public int? codEmpresa { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Tipo")]
        public int? codTipExp { get; set; }
        public long? codArchivoExp { get; set; }

        [Display(Name = "Archivo")]
        public string nombreArchivoExp { get; set; }

        public List<SelectListItem> Tipos { get; set; }
        public List<SelectListItem> Empresas { get; set; }

        public HttpPostedFileBase archivoExpFile { get; set; }
    }
}