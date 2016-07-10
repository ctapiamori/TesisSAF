using SOCAUD.Business.Infraestructure;
using SOCAUD.Data.Core;
using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Business.Core
{
    public interface ISafUsuarioLogic : IFacadeOperacionCRUD<SAF_USUARIO>
    {
        SAF_USUARIO BuscarPorUsuario(string usuario);
        TcACCEDERSISTEMAADMIN AccederSistema(string usuario, string contrasenia);
    }

    public class SafUsuarioLogic : ISafUsuarioLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafUsuarioData _safUsuarioData;

        public SafUsuarioLogic()
        {
            this._uow = new UnitOfWork();
            this._safUsuarioData = new SafUsuarioData(_uow);
        }


        public SAF_USUARIO Registrar(SAF_USUARIO entidad)
        {
            return this._safUsuarioData.Add(entidad);
        }

        public SAF_USUARIO Actualizar(SAF_USUARIO entidad)
        {
            return this._safUsuarioData.Update(entidad);
        }

        public SAF_USUARIO BuscarPorId(int id)
        {
            return this._safUsuarioData.GetById(id);
        }

        public IEnumerable<SAF_USUARIO> ListarTodos()
        {
            return this._safUsuarioData.GetAll();
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safUsuarioData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TcACCEDERSISTEMAADMIN AccederSistema(string usuario, string contrasenia)
        {
            return this._safUsuarioData.AccederSistema(usuario, contrasenia);
        }

        public SAF_USUARIO BuscarPorUsuario(string usuario)
        {
            return this._safUsuarioData.Get(c => c.NOMUSU == usuario);
        }
    }
}
