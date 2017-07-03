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

    public interface ISafMenuLogic : IFacadeOperacionCRUD<SAF_MENU>
    {
        IEnumerable<TcMENUPORPERFIL> ObtenerMenuPorPerfil(int idPerfil);
        IEnumerable<TcSUBMENUPORMENU> ObtenerSubMenuPorMenu(int idMenu);
    }

    public class SafMenuLogic : ISafMenuLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafMenuData _safMenuData;


        public SafMenuLogic()
        {
            this._uow = new UnitOfWork();
            this._safMenuData = new SafMenuData(_uow);

        }

        public SAF_MENU Registrar(SAF_MENU entidad)
        {
            var result = this._safMenuData.Add(entidad);
            return result;
        }

        public SAF_MENU Actualizar(SAF_MENU entidad)
        {
            var result = this._safMenuData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safMenuData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_MENU BuscarPorId(int id)
        {
            return this._safMenuData.GetById(id);
        }

        public IEnumerable<SAF_MENU> ListarTodos()
        {
            return this._safMenuData.GetAll();
        }

        public IEnumerable<TcMENUPORPERFIL> ObtenerMenuPorPerfil(int idPerfil)
        {
            return this._safMenuData.ObtenerMenuPorPerfil(idPerfil);
        }

        public IEnumerable<TcSUBMENUPORMENU> ObtenerSubMenuPorMenu(int idMenu)
        {
            return this._safMenuData.ObtenerSubMenuPorMenu(idMenu);
        }
    }
}
