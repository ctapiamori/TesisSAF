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

    public interface ISafPerfilMenuLogic : IFacadeOperacionCRUD<SAF_PERFIL_MENU>
    {
        IEnumerable<VW_SAF_PERFILMENU> ListarPerfilMenuCompleto();

        IEnumerable<SAF_PERFIL_MENU> BuscarPorCodigoPerfil(int idPerfil);

    }

    public class SafPerfilMenuLogic : ISafPerfilMenuLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafPefilMenuData _safPerfilMenuData;
        private readonly IVwSafPerfilMenuData _vwSafPerfilMenuData;

        public SafPerfilMenuLogic()
        {
            this._uow = new UnitOfWork();
            this._safPerfilMenuData = new SafPefilMenuData(_uow);
            this._vwSafPerfilMenuData = new VwSafPerfilMenuData(_uow);

        }


        public SAF_PERFIL_MENU Registrar(SAF_PERFIL_MENU entidad)
        {
            var result = _safPerfilMenuData.Add(entidad);
            return result;
        }

        public SAF_PERFIL_MENU Actualizar(SAF_PERFIL_MENU entidad)
        {
            var result = _safPerfilMenuData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                _safPerfilMenuData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_PERFIL_MENU BuscarPorId(int id)
        {
            var result = _safPerfilMenuData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_PERFIL_MENU> ListarTodos()
        {
            var result = _safPerfilMenuData.GetAll();
            return result;
        }

        public IEnumerable<VW_SAF_PERFILMENU> ListarPerfilMenuCompleto()
        {
            var result = _vwSafPerfilMenuData.GetAll();
            return result;
        }


        public IEnumerable<SAF_PERFIL_MENU> BuscarPorCodigoPerfil(int idPerfil)
        {
            var result = _safPerfilMenuData.GetMany(c => c.CODPER == idPerfil);
            return result;
        }
    }
}
