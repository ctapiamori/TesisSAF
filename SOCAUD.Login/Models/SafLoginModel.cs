using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Login.Models
{
    public class SafLoginModel
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string TipoUsuario { get; set; }
        IEnumerable<SelectListItem> ListaTipoUsuario { get; set; }

        public SafLoginModel() {
            ListaTipoUsuario = new List<SelectListItem>();
        }
    }
}