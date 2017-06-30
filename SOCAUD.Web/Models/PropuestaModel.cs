using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Models
{
    public class PropuestaModel
    {
        public int codigoPropuestaSustento { get; set; }

        public int CODPRO { get; set; }
        public string NUMPRO { get; set; }
        public int CODSOA { get; set; }
        [Display(Name = "Publicacion")]
        public int CODPUB { get; set; }
        [Display(Name = "RUC")]
        public string RUCSOA { get; set; }
        [Display(Name = "Razon Social")]
        public string RAZSOCSOA { get; set; }
        [Display(Name = "Nombre Representante Legal")]
        public string NOMREPLEGSOA { get; set; }
        [Display(Name = "Celular Representante Legal")]
        public string CELREPLEGSOA { get; set; }
        [Display(Name = "Correo Representante Legal")]
        public string CORREPLEGSOA { get; set; }

        [Display(Name = "Retribucion")]
        public decimal TOTRETECOBASREQ { get; set; }
        [Display(Name = "IGV")]
        public decimal TOTIGVBASREQ { get; set; }
        [Display(Name = "Viaticos")]
        public decimal TOTVIABASREQ { get; set; }


        [Display(Name = "Monto Retribucion")]
        public decimal RETRECO { get; set; }
        [Display(Name = "IGV")]
        public decimal IGVTOTAL { get; set; }
        [Display(Name = "Total Retribucion")]
        public decimal RETRECOTOTAL { get; set; }
        [Display(Name = "Viaticos")]
        public decimal MONTVIATICO { get; set; }



        public Nullable<int> ESTPRO { get; set; }

        [Display(Name = "Archivo Sustento Firma Internacional")]
        public string nombreArchivoFirmaInternacional { get; set; }
        public long? codArchivoFirmaInternacional { get; set; }

        [Display(Name = "Archivo Sustento Firma PCAOB")]
        public string nombreArchivoFirmaPCAOB { get; set; }
        public long? codArchivoFirmaPCAOB { get; set; }


        public string INDREQFIRINT { get; set; }
        public string INDREQFIRPCAOB { get; set; }

        public HttpPostedFileBase archivoFirmaInternacional { get; set; }
        public HttpPostedFileBase archivoFirmaPCAOB { get; set; }


        public List<SelectListItem> cboPublicaciones { get; set; }

        public int CODBAS { get; set; }
        public List<SelectListItem> cboBases { get; set; }
        
        public PropuestaModel()
        {
            this.cboPublicaciones = new List<SelectListItem>();
            this.cboBases = new List<SelectListItem>();
        }
    }
}