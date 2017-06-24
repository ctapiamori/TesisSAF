using SOCAUD.Common.Constantes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Models
{
    public class ServicioAuditoriaModel
    {
        [Display(Name = "N°. Base")]
        public string NumeroBase { get; set; }
        [Display(Name = "Entidad")]
        public string EntidadBase { get; set; }
        [Display(Name = "Periodo (Año)")]
        public string PeriodoBase { get; set; }

        [Display(Name = "Retribución Económica")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> RetribucionBase { get; set; }


        public int IdServicioAuditoria { get; set; }

        [Display(Name = "Inicio (Segun Cronograma")]
        public string FechaInicioSegunCronograma { get; set; }
        [Display(Name = "Fin (Segun Cronograma")]
        public string FechaFinSegunCronograma { get; set; }

        [Display(Name = "Inicio")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string FechaInicio { get; set; }

        [Display(Name = "Fin")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public string FechaTermino { get; set; }


        [Display(Name = "Retribución Económica")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> RetribucionServicio { get; set; }

        [Display(Name = "Viaticos")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> ViaticosServicio { get; set; }

        [Display(Name = "IGV")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<decimal> IgvServicio { get; set; }
        public int IdCodigoBase { get; set; }

        public IEnumerable<SelectListItem> Cargos { get; set; }

        [Display(Name = "Cargo")]
        public int IdCardoSeleted { get; set; }

        public IEnumerable<CargoServicioAuditoriaModel> CargosServicioAuditoria { get; set; }
    }

    public class CargoEquipoServicioAuditoriaModel
    {
        public int IdServicioAuditoria { get; set; }
        public int IdCargoServicioAuditoria { get; set; }

        [Display(Name = "Cargo")]
        public int IdCargoSeleted { get; set; }
        
        public IEnumerable<SelectListItem> CargosServicioAuditoria { get; set; }

        [Display(Name = "Cantidad de Integrantes")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> CantidadIntegrantes { get; set; }

        [Display(Name = "Participación Mínima (Horas)")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> MinimoHoras { get; set; }

        public int IdCapacitacionServicioAuditoria { get; set; }

        [Display(Name = "Capacitación Mínima (Horas)")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> MinimoHorasCapacitacion { get; set; }

        public int IdExperienciaServicioAuditoria { get; set; }

        [Display(Name = "Experiencia Mínima (Horas)")]
        [Required(ErrorMessage = Mensaje.MensajeCampoRequerido)]
        public Nullable<int> MinimoHorasExperiencia { get; set; }

    }

    public class CargoServicioAuditoriaModel
    {
        public int IdServicioAuditoria { get; set; }
        public int IdCargoServicioAuditoria { get; set; }

        [Display(Name = "Cantidad de Integrantes")]
        public int CantidadIntegrantes { get; set; }
        [Display(Name = "Participación Mínima (Horas)")]
        public int MinimoHoras { get; set; }

        public IEnumerable<CapacitacionServicioAuditoriaModel> Capacitaciones { get; set; }
        public IEnumerable<ExperienciaServicioAuditoriaModel> Experiencias { get; set; }
    }

    public class CapacitacionServicioAuditoriaModel
    {
        public int IdCargoServicioAuditoria { get; set; }
        public int IdCapacitacionServicioAuditoria { get; set; }
        [Display(Name = "Cantidad horas mínimas de Capacitación")]
        public decimal MinimoHoras { get; set; }
    }

    public class ExperienciaServicioAuditoriaModel
    {
        public int IdCargoServicioAuditoria { get; set; }
        public int IdExperienciaServicioAuditoria { get; set; }
        [Display(Name = "Cantidad horas mínimas Experiencia")]
        public decimal MinimoHoras { get; set; }
    }


}