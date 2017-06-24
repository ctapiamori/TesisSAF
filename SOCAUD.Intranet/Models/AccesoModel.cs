using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCAUD.Intranet.Models
{
    public class AccesoModel
    {
        [Display(Name = "Tipo Usuario")]
        public int CodigoTipoUsuario { get; set; }
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Correo")]
        public string Correo { get; set; }
    }
}