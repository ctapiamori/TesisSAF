using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class ConsultaModel {

        [Display(Name="Publicación")]
        public int CodigoPublicacion { get; set; }
        [Display(Name = "Base")]
        public int CodigoBase { get; set; }
        public IEnumerable<SelectListItem> lista { get; set; }

        public IEnumerable<SelectListItem> listaBase { get; set; }
        public ConsultaModel() {
            this.lista = new List<SelectListItem>();
            this.listaBase = new List<SelectListItem>();
        }
    }

    public class ConsultaEntidadModel{
        public int CodigoConsulta { get; set; }
        [Display(Name="Consulta")]
        public string DescripcionConsulta { get; set; }
        [Display(Name = "Respuesta")]
        public string RespuestaConsulta { get; set; }
        public int Estado { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDes { get; set; }

        [Display(Name = "Publicación")]
        public string Publicacion { get; set; }
        [Display(Name = "Entidad")]
        public string DesBase { get; set; }
        [Display(Name = "Sociedad de Auditoria")]
        public string SOA { get; set; }

        public int IdPublicacion { get; set; }
        public int idBase { get; set; }
    }

    public class PublicacionConsultaModel
    {
        public int CodigoPublicacion { get; set; }
        public string NumeroPublicacion { get; set; }
        public string DescripcionPublicacion { get; set; }
    }
}