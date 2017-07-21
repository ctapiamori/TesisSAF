using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Models
{
    public class InvitacionModel
    {
        #region BUSQUEDA DE AUDITORES

        public int codigoPublicacionBusqueda { get; set; }
        public int codigoServicioAudBusqueda { get; set; }

        #endregion

        #region REGISTRO DE FECHAS LABORALES


        public string InicioAuditoria { get; set; }
        public string FinAuditoria { get; set; }
        [Display(Name = "Entidad")]
        public string EntidadBase { get; set; }
        [Display(Name = "Periodo")]
        public string FechaInicioFin { get; set; }

        public int codigoInvitacionAgenda { get; set; }

        [Display(Name = "N° Horas laborales")]
        public int numeroHorasLaboral { get; set; }
        public int codigoAuditorAgenda { get; set; }
        [Display(Name = "Nombre Completo Auditor")]
        public string nomCompletoAuditor { get; set; }
        [Display(Name = "Cargo del Auditor invitado")]
        public string cargoInvitacionAuditor { get; set; }

        [Display(Name = "Fecha Inicial")]
        public string fechaInicio { get; set; }
        [Display(Name = "Fecha Final")]
        public string fechaFinal { get; set; }

 

        #endregion




        [Display(Name = "Base")]
        public int CODBAS { get; set; }

        [Display(Name = "Codigo Invitacion")]
        public int CODINV { get; set; }

        [Display(Name = "Numero Invitacion")]
        public string NUMINV { get; set; }
        public string INDCANINV { get; set; }
        [Display(Name = "Fec. Acepta Invitacion")]
        public string FECACEPINV { get; set; }
        [Display(Name = "Fec. Acepta Invitacion")]
        public string FECMAXPREPROINV { get; set; }
        public Nullable<int> CODSOA { get; set; }
        public Nullable<int> CODAUD { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Servicio Auditoria")]
        public Nullable<int> CODSERAUD { get; set; }

        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        [Display(Name = "Publicacion")]
        public string CODPUB { get; set; }

        public string ESTINV { get; set; }

        public List<SelectListItem> cboPublicaciones { get; set; }

        public List<SelectListItem> cboBases { get; set; }

        public List<SelectListItem> cboServiciosAuditoria { get; set; }

        public InvitacionModel()
        {
            this.cboPublicaciones = new List<SelectListItem>();
            this.cboServiciosAuditoria = new List<SelectListItem>();
            this.cboBases = new List<SelectListItem>();
        }
    }
}