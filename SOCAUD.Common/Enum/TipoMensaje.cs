using System;
using System.ComponentModel;

namespace SOCAUD.Common.Enum
{
    public enum TipoMensaje
    {
        [Description("Error")]
        error,
        [Description("Advertencia")]
        advertencia,
        [Description("Satisfaccion")]
        satisfaccion

    }
}
