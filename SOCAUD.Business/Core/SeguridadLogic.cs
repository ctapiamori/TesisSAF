using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Business.Core
{
    public interface ISeguridadLogic
    {
        MensajeRespuesta AccederSistemaExtranet(string usuario, string password, int tipoUsuario);
        void AccederSistema(string usuario, string contrasenia);
    }

    public class SeguridadLogic : ISeguridadLogic
    {
        private readonly ISafAuditorLogic _safAuditorLogic;
        private readonly ISafSoaLogic _safSoaLogic;
        public SeguridadLogic()
        {
            this._safAuditorLogic = new SafAuditorLogic();
            this._safSoaLogic = new SafSoaLogic();
        }
        public MensajeRespuesta AccederSistemaExtranet(string usuario, string password, int tipoUsuario)
        {
            var result = false;
            if (tipoUsuario == (int)Tipo.TipoUsuarioExtranet.Auditor)
                result = this._safAuditorLogic.AccederAuditor(usuario, password);
            else
                result = this._safSoaLogic.AccederSoa(usuario, password);

            if (result)
                return new MensajeRespuesta("Ingreso al Sistema satisfactoriamente", true);
            else
                return new MensajeRespuesta("Usuario y/o contraseña no coinciden", false);

        }


        public void AccederSistema(string usuario, string contrasenia)
        {
            throw new NotImplementedException();
        }
    }


}
