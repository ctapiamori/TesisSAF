using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Enum
{
    public class Notificacion
    {
        public const string asuntoSolicitudObservada = "Solicitud OBSERVADA";
        public const string asuntoSolicitudAceptada = "Solicitud ACEPTADA";
        public const string asuntoInvitacion = "Invitación Auditoria";
        public const string asuntoInvitacionAceptada = "Invitación ACEPTADA";
        public const string asuntoInvitacionCancelado = "Invitación CANCELADA";
        public const string asuntoCambiosConcurso = "Cambios en la CONVOCATORIA";
        public const string asuntoPublicacionConcurso = "Publicación de nueva CONVOCATORIA";
        public const string asuntoAbsolucionConsulta = "Publicación de ABSOLUCION CONSULTA";


        public const string bodySolicitudObservada = "La solicitud de actualización pendiente de aprobación ha sido OBSERVADA, revise las observaciones y comentarios.";
        public const string bodySolicitudAceptada = "La solicitud de actualización ha sido ACEPTADA";

        public const string bodyCambiosConcurso = "Se realizaron cambios en el concurso";


    }
}

