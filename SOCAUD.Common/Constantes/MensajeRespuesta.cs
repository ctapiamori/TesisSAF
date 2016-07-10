using SOCAUD.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Constantes
{
    public class MensajeRespuesta
    {
        public string Mensaje { get; set; }

        public TipoMensaje TipoMensaje { get; set; }

        public bool Exito { get; set; }

        public object Data { get; set; }

        public MensajeRespuesta(string _mensaje)
        {
            this.Mensaje = _mensaje;
            this.Exito = false;
            this.TipoMensaje = TipoMensaje.advertencia;
        }

        public MensajeRespuesta(string _mensaje, bool _exito)
        {
            this.Mensaje = _mensaje;
            this.TipoMensaje = (_exito) ? TipoMensaje.satisfaccion : TipoMensaje.error;
            this.Exito = _exito;
        }

        public MensajeRespuesta(string _mensaje, bool _exito, object _data)
        {
            this.Mensaje = _mensaje;
            this.TipoMensaje = (_exito) ? TipoMensaje.satisfaccion : TipoMensaje.error;
            this.Exito = _exito;
            this.Data = _data;
        }

        public MensajeRespuesta(string _mensaje, TipoMensaje _tipoMensaje)
        {
            this.Mensaje = _mensaje;
            this.TipoMensaje = _tipoMensaje;
            this.Exito = _tipoMensaje.Equals(TipoMensaje.satisfaccion) ? true : false;
        }

        public MensajeRespuesta(string _mensaje, TipoMensaje _tipoMensaje, object _data)
        {
            this.Mensaje = _mensaje;
            this.TipoMensaje = _tipoMensaje;
            this.Data = _data;
            this.Exito = _tipoMensaje.Equals(TipoMensaje.satisfaccion) ? true : false;
        }
    }
}
