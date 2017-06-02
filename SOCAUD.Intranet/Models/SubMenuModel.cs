using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class SubMenuModel
    {

        private readonly ISafMenuLogic _menuLogic;

        public IList<SelectListItem> ListaMenu { get; set; }

        [Display(Name="Codigo Sub Menu")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public int CODSUBMEN { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string DESSUBMEN { get; set; }

        [Display(Name = "Ruta")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string RUTASUBMEN { get; set; }

        [Display(Name = "Menu Principal")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> CODMEN { get; set; }

        

        public SubMenuModel() {
            _menuLogic = new SafMenuLogic();
            this.ListaMenu = new List<SelectListItem>();
            try
            {
                ListaMenu = (from c in _menuLogic.ListarTodos() select new SelectListItem() { Value = c.CODMEN.ToString(), Text = c.DESMEN }).ToList();
            }
            catch (Exception)
            {
            }
        }
    }
}