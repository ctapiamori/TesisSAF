using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Enum
{
    public class Tipo
    {
        public enum TipoSolicitud
        {
            [Description("Inscripcion Sociedad")]
            InscripcionSoa = 1,
            [Description("Inscripcion Auditor")]
            InscripcionAuditor = 2,
            [Description("Actualizacion Sociedad")]
            ActualizacionSoa = 3,
            [Description("Actualizacion Auditor")]
            ActualizacionAuditor = 4
        }
        public enum TipoUsuarioExtranet
        {
            [Description("Auditor")]
            Auditor = 1,
            [Description("Soa")]
            SociedadAuditoria = 2
        }

    }
}
