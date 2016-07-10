using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class SoaModel
    {
        public int codSolicitud { get; set; }
        public string numSolicitud { get; set; }

        public int estadoSolicitud { get; set; }

        [AllowHtml]
        public string observacionSolicitud { get; set; }


        [Display(Name = "Codigo SOA")]
        public int codSoa { get; set; }
        [Display(Name = "Razon Social")]
        public string razSocSoa { get; set; }
        [Display(Name = "R.U.C")]
        public string rucSoa { get; set; }
        [Display(Name = "Nombre")]
        public string nomRepLegSoa { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string apeRepLegSoa { get; set; }
        [Display(Name = "Correo Electronico")]
        public string corRepLegSoa { get; set; }
        [Display(Name = "Celular")]
        public string celRepLegSoa { get; set; }
        [Display(Name = "Telefono")]
        public string telRepLegSoa { get; set; }
        [Display(Name = "Codigo Colegio Contadores")]
        public string codColConSoa { get; set; }
        [Display(Name = "Nº Partida Sunarp")]
        public string numParSunSoa { get; set; }
        [Display(Name = "Firma PCAOB")]
        public string firPcaobSoa { get; set; }
        [Display(Name = "Firma Internacional")]
        public string firIntSoa { get; set; }
        [Display(Name = "Mision")]
        public string misSoa { get; set; }
        [Display(Name = "Vision")]
        public string visSoa { get; set; }
        [Display(Name = "Archivo Colegio Contadores")]
        public byte[] ARCCOLCONSOA { get; set; }
        [Display(Name = "Usuario")]
        public string nomUsu { get; set; }
        [Display(Name = "Contraseña")]
        public string pasUsu { get; set; }
        [Display(Name = "Repetir Contraseña")]
        public string repPasUsu { get; set; }
    }
}