using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class ConsultaModel {

        public int CodigoPublicacion { get; set; }
        public IEnumerable<SelectListItem> lista { get; set; }
        public ConsultaModel() {
            this.lista = new List<SelectListItem>();
        }
    }

    public class ConsultaEntidadModel{
        public int CodigoConsulta { get; set; }
        public string DescripcionConsulta { get; set; }
        public string RespuestaConsulta { get; set; }
        public int Estado { get; set; }
    }

    public class PublicacionConsultaModel
    {
        public int CodigoPublicacion { get; set; }
        public string NumeroPublicacion { get; set; }
        public string DescripcionPublicacion { get; set; }
    }
}