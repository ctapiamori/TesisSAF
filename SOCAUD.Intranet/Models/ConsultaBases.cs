using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SOCAUD.Intranet.Models
{
    public class ConsultaBases
    {
        [Display(Name = "Cronograma")]
        public int CodigoCronograma { get; set; }
        public IEnumerable<SelectListItem> cboCronograma { get; set; }

        public ConsultaBases()
        {
            this.cboCronograma = new List<SelectListItem>();
        }
    }
}