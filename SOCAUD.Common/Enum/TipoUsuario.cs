using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Common.Enum
{
    public enum TipoUsuario
    {
        [Description("Operador")]
        Operador = 36,
        [Description("Jefe")]
        Jefe = 37,
        [Description("Gerente")]
        Gerente = 38

    }
}
