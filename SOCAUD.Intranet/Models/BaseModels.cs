using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class BaseModel
    {
        public int Codigo { get; set; }

        [Display(Name="Fecha máxima de aprobación de Base")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string FechaMaxPublicacion { get; set; }

        [Display(Name = "Cronograma")]

        public int Cronograma { get; set; }

        [Display(Name = "Entidad")]
        public int CronoEntidad { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "N° de Base")]
        public string Numero { get; set; }

        [Display(Name="Total Retribución")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> TotalRetribucion { get; set; }

        [Display(Name="Total Viaticos")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> TotalViaticos { get; set; }

        [Display(Name = "Firma de PCAOB")]
        public string FirmaPcaob { get; set; }

        [Display(Name="Firma Internacional")]
        public string FirmaInternacional { get; set; }

        [Display(Name = "Total IGV")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> TotalIgv { get; set; }


        public int EstadoBase { get; set; }

        [Display(Name = "Estado")]
        public string EstadoBaseDescripcion { get; set; }

        [Display(Name = "Inicio Auditoria (Cronograma)")]
        public string FecIniAuditoriaCronograma { get; set; }
        [Display(Name = "Fin Auditoria (Cronograma)")]
        public string FecFinAuditoriaCronograma { get; set; }

        public IEnumerable<SelectListItem> Cronogramas { get; set; }
        public IEnumerable<SelectListItem> Entidades { get; set; }

        public int CodigoWorkFlow { get; set; }
        public string FlgMostrarFlujoAprobacion { get; set; }

    }
}