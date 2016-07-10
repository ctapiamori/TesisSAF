using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Enum
{
    public class Estado
    {
        public enum Auditoria
        {
            [Description("Inactivo")]
            Inactivo = 0,
            [Description("Activo")]
            Activo = 1
        }

        public enum Cronograma
        {
            [Description("Elaboracion")]
            Elaboracion = 1,
            [Description("Aprobado")]
            Aprobado = 2
        }

        public enum Bases
        {
            [Description("Elaboracion")]
            Elaboracion = 3,
            [Description("Aprobado")]
            Aprobado = 4
        }

        public enum Publicacion
        {
            [Description("Elaboracion")]
            Elaboracion = 5,
            [Description("Aprobado")]
            Aprobado = 6,
            [Description("Publicado")]
            Publicado = 13

        }

        public enum Invitacion
        {
            [Description("Elaboracion")]
            Elaboracion = 7,
            [Description("Aceptado")]
            Aceptado = 8,
            [Description("Enviada")]
            Enviada = 30,
            [Description("Cancelada")]
            Cancelada = 31
        }

        public enum ConsultasPublicacion
        {
            [Description("Elaboracion")]
            Elaboracion = 9,
            [Description("Enviado")]
            Enviado = 10
        }

        public enum Solicitud
        {
            [Description("Elaboracion")]
            Elaboracion = 11,
            [Description("Aprobado")]
            Aprobado = 12,
            [Description("Observada")]
            Observada = 28,
            [Description("Enviada")]
            Enviada = 29
        }

        public enum Propuesta
        {
            [Description("Elaboracion")]
            Elaboracion = 32,
            [Description("Enviada")]
            Enviada = 33,
            [Description("Ganadora")]
            Ganadora = 34,
            [Description("Descalifica")]
            Descalifica = 35

        }
    }
}
