using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SOCAUD.Web.Hubs
{
    public class NotificacionProceso
    {
        public void Notificar(string codigo)/*, ISAF_NOTIFICACIONService pNotificacionService*/
        {
            //var cantNoLeidas = pNotificacionService.ObtenerNoLeidas(codigo, TipoRespuesta.No, (int)TipoUsuario.Interno);
            Hubs.Notificacion.Instance.NotificarTotalMensajes(codigo, /*cantNoLeidas.ToString()*/ "");
        }

        public void NotificarEnvioSolicitud(string codigo) /*, ISAF_NOTIFICACIONService pNotificacionService*/
        {
            //var cantNoLeidas = pNotificacionService.ObtenerNoLeidas(codigo, TipoRespuesta.No, (int)TipoUsuario.Interno);
            Hubs.Notificacion.Instance.NotificarTotalMensajes(codigo, "" /*cantNoLeidas.ToString()*/);
        }
    }
}