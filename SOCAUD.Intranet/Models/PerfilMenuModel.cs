using SOCAUD.Business.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class PerfilMenuModel
    {
        private readonly ISafPerfilLogic _perfilLogic;


        public int CODPERMEN { get; set; }
        public string DESPER { get; set; }
        public string DESMEN { get; set; }
        public int CANT { get; set; }
        public Nullable<int> CODMEN { get; set; }
        public Nullable<int> CODPER { get; set; }
        [Display(Name="Perfil")]
        public int CODPER_SELECT { get; set; }
        public IList<SelectListItem> ListaPerfil { get; set; }

        public PerfilMenuModel() {
            _perfilLogic = new SafPerfilLogic();
            this.ListaPerfil = new List<SelectListItem>();

            ListaPerfil = (from c in _perfilLogic.ListarTodos() select new SelectListItem() { Text = c.NOMPER, Value = c.CODPER.ToString() }).ToList();
        }

    }
}