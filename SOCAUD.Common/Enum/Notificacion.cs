using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Enum
{
    public class Notificacion
    {
        public const string asuntoSolicitudObservada = "Solicitud Observada";
        public const string asuntoSolicitudAceptada = "Solicitud Aceptada";
        public const string asuntoInvitacion = "Invitacion Auditoria";
        public const string asuntoInvitacionAceptada = "Invitacion Auditoria Aceptada";
        public const string asuntoInvitacionCancelado = "Invitacion Auditoria Cancelada";
        public const string asuntoCambiosConcurso = "Cambios en la Publicacion convocatoria";


        public const string bodySolicitudObservada = "Su solicitud fue observada";
        public const string bodySolicitudAceptada = "Su solicitud fue aceptada";

        public const string bodyCambiosConcurso = "Se realizaron cambios en el concurso";


    }
}
