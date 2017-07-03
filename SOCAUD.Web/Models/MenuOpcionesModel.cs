using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCAUD.Web.Models
{
    public class MenuOpcionesModel
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Css { get; set; }
        public IEnumerable<SubMenuOpcionesModel> SubMenu { get; set; }

        public MenuOpcionesModel()
        {
            this.SubMenu = new List<SubMenuOpcionesModel>();
        }
    }

    public class SubMenuOpcionesModel
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
    }
}