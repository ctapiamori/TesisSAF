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
        [Display(Name = "Base Nro.")]
        public string NumeroBase { get; set; }
        [Display(Name = "Entidad")]
        public string EntidadBase { get; set; }
        [Display(Name = "Periodo (Año)")]
        public string PeriodoBase { get; set; }
        [Display(Name = "Retribución Económica")]
        public decimal RetribucionBase { get; set; }


        public int IdServicioAuditoria { get; set; }
        [Display(Name = "Inicio")]
        public string FechaInicio { get; set; }
        [Display(Name = "Fin")]
        public string FechaTermino { get; set; }
        [Display(Name = "Retribución Económica")]
        public decimal RetribucionServicio { get; set; }
        [Display(Name = "Viaticos")]
        public decimal ViaticosServicio { get; set; }
        [Display(Name = "IGV")]
        public decimal IgvServicio { get; set; }
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
        //public IEnumerable<CargoServicioAuditoriaModel> CargosServicioAuditoria { get; set; }
        public IEnumerable<SelectListItem> CargosServicioAuditoria { get; set; }
        [Display(Name = "Cantidad de Integrantes")]
        public int CantidadIntegrantes { get; set; }
        [Display(Name = "Mínimo horas Participación")]
        public int MinimoHoras { get; set; }

        public int IdCapacitacionServicioAuditoria { get; set; }
        [Display(Name = "Cantidad horas mínimas de Capacitación")]
        public int MinimoHorasCapacitacion { get; set; }

        public int IdExperienciaServicioAuditoria { get; set; }
        [Display(Name = "Cantidad horas mínimas Experiencia")]
        public int MinimoHorasExperiencia { get; set; }
    }

    public class CargoServicioAuditoriaModel
    {
        public int IdServicioAuditoria { get; set; }
        public int IdCargoServicioAuditoria { get; set; }
        [Display(Name = "Cantidad de Integrantes")]
        public int CantidadIntegrantes { get; set; }
        [Display(Name = "Mínimo horas Participación")]
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