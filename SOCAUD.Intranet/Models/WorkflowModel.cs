﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class WorkflowModel
    {
    }

    public class SolicitarWorkflowViewModel
    {
        public int CodigoCargoIngresado { get; set; }


        public int? IdFlujo { get; set; }
        [Display(Name = "Acción")]
 
        public int EstadoFlujo { get; set; }
        public int IdDocumento { get; set; }
        public string IdTipoDocumento { get; set;}
        public string TipoDocumento { get; set; }
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Asignar")]
     
        public int TipoUsuario { get; set; }
        public int TipoUsuarioSelect { get; set; }

        public IEnumerable<SelectListItem> TiposUsuario { get; set; }
        public IEnumerable<SelectListItem> EstadosFlujo { get; set; } 
    }
}