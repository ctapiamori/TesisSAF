using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Models
{
    public class SoaModel
    {

        public int codSolicitud { get; set; }
        public string numSolicitud { get; set; }

        public int estadoSolicitud { get; set; }

        [AllowHtml]
        [Display(Name = "Observaciones")]
        public string observacionSolicitud { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Codigo SOA")]
        public int codSoa { get; set; }

        [Display(Name = "Razon Social")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[A-Za-z áéíóúÁÉÍÓÚñÑ]+", ErrorMessage = Mensaje.MensajeSoloLetras)]
        public string razSocSoa { get; set; }

        [Display(Name = "R.U.C")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string rucSoa { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[A-Za-z áéíóúÁÉÍÓÚñÑ]+", ErrorMessage = Mensaje.MensajeSoloLetras)]
        public string nomRepLegSoa { get; set; }


        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[A-Za-z áéíóúÁÉÍÓÚñÑ]+", ErrorMessage = Mensaje.MensajeSoloLetras)]
        public string apeRepLegSoa { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = Mensaje.MensajeCampoDebeSerCorreo)]
        [Display(Name = "Correo Electronico")]
        public string corRepLegSoa { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string celRepLegSoa { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string telRepLegSoa { get; set; }


        [Display(Name = "Codigo Colegio Contadores")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string codColConSoa { get; set; }


        [Display(Name = "Nº Partida Sunarp")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression("[0-9]+", ErrorMessage = Mensaje.MensajeSoloNumeros)]
        public string numParSunSoa { get; set; }

        [Display(Name = "Firma PCAOB")]
        public string firPcaobSoa { get; set; }

        [Display(Name = "Firma Internacional")]
        public string firIntSoa { get; set; }

        [Display(Name = "Mision")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        //[MaxLength(300, ErrorMessage = Mensaje.MensajeCampoLongitudIncorrecta)]
        public string misSoa { get; set; }


        [Display(Name = "Vision")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        //[MaxLength(300, ErrorMessage = Mensaje.MensajeCampoLongitudIncorrecta)]
        public string visSoa { get; set; }


        [Display(Name = "Archivo Colegio Contadores")]
        public byte[] ARCCOLCONSOA { get; set; }


        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Usuario")]
        public string nomUsu { get; set; }



        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string pasUsu { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        //[System.ComponentModel.DataAnnotations.Compare("pasUsu", ErrorMessage = "Debe ser igual a la contraseña")]       
        public string repPasUsu { get; set; }
    }
}