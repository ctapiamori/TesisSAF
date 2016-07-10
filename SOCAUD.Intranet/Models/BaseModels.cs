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
        [Display(Name="Fecha máxima de Publiación")]
        public string FechaMaxPublicacion { get; set; }
        [Display(Name = "Cronograma")]
        public int Cronograma { get; set; }
        [Display(Name = "Entidad")]
        public int CronoEntidad { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        public string Numero { get; set; }
        [Display(Name="Total Retribución")]
        public decimal TotalRetribucion { get; set; }
        [Display(Name="Total Viaticos")]
        public decimal TotalViaticos { get; set; }
        [Display(Name = "Firma de PCAOB")]
        public string FirmaPcaob { get; set; }
        [Display(Name="Firma Internacional")]
        public string FirmaInternacional { get; set; }
        public decimal TotalIgv { get; set; }
        public int EstadoBase { get; set; }
        public IEnumerable<SelectListItem> Cronogramas { get; set; }
        public IEnumerable<SelectListItem> Entidades { get; set; }

    }
}