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
    public class ParametroModel
    {
        public int CODPAR { get; set; }
        [Display(Name = "Nombre Parametro")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string NOMPAR { get; set; }

        [Display(Name = "Valor Parametro")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string VALOR { get; set; }

        [Display(Name = "Tipo Parametro")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> CODTIPPAR { get; set; }
        public IEnumerable<SelectListItem> cboListaTipoParametro { get; set; }


        ISafTipoParametricaLogic safTipoparametricaLogic;

        public ParametroModel() {
            this.safTipoparametricaLogic = new SafTipoParametricaLogic();

            this.cboListaTipoParametro = new List<SelectListItem>();

            var listaTipoParametrica = this.safTipoparametricaLogic.ListarTodos();

            this.cboListaTipoParametro = (from c in listaTipoParametrica select new SelectListItem(){ Value = c.CODTIPPAR.ToString(), Text = c.NOMTIPPAR });

        }

    }
}