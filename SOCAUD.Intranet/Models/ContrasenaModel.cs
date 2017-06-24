using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCAUD.Intranet.Models
{
    public class ContrasenaModel
    {
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        [Display(Name = "Repita Contraseña")]
        public string RepitaConstrasena { get; set; }

        public ContrasenaModel() { 
        }
    }
}