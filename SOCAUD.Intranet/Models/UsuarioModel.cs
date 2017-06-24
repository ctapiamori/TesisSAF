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
    public class UsuarioModel
    {



        public int CODUSU { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string NOMUSU { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string PASUSU { get; set; }

        [Display(Name = "Nombre Persona")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string NOMPERUSU { get; set; }

        [Display(Name = "Apellidos Persona")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string APEPERUSU { get; set; }

        [Display(Name = "DNI")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [RegularExpression(StringRegularExpression.SoloDNI8Digitos, ErrorMessage = Mensaje.MensajeSoloIngreseDNI)]
        public string DNIUSU { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> TIPCARUSU { get; set; }
        public Nullable<System.DateTime> FECREG { get; set; }
        public Nullable<System.DateTime> FECMOD { get; set; }
        public string USUREG { get; set; }
        public string USUMOD { get; set; }
        public string ESTREG { get; set; }

        [Display(Name = "Entidad")]
        public Nullable<int> CODENT { get; set; }

        [Display(Name = "Perfil")]
        public int CODPER { get; set; }

         [Display(Name = "Correo")]
        public string CORREOUSER { get; set; }


        public IEnumerable<SelectListItem> cboListaCargo { get; set; }
        public IEnumerable<SelectListItem> cboListaEntidad { get; set; }

        ISafParametricaLogic _safParametricaLogic;
        ISafEntidadLogic _safEntidadLogic;
        public UsuarioModel() { 
            this._safParametricaLogic = new SafParametricaLogic();
            this._safEntidadLogic = new SafEntidadLogic();

            var listaTipoUsuario = this._safParametricaLogic.ListarTipoUsuario();
            var listaEntidades = this._safEntidadLogic.ListarTodos();

            this.cboListaCargo = new List<SelectListItem>();
            this.cboListaEntidad = new List<SelectListItem>();

            this.cboListaCargo = (from c in listaTipoUsuario select new SelectListItem(){ Text = c.NOMPAR, Value = c.CODPAR.ToString() });

            this.cboListaEntidad = (from c in listaEntidades select new SelectListItem() { Text = c.RAZSOCENT, Value = c.CODENT.ToString() });

        }
    }
}