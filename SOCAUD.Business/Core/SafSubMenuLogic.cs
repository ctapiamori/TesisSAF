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

    public interface ISafSubMenuLogic : IFacadeOperacionCRUD<SAF_SUBMENU>
    {
        IEnumerable<VW_SAF_SUBMENU> ListarSubMenuDetallado();
    }

    public class SafSubMenuLogic : ISafSubMenuLogic
    {


        private readonly IUnitOfWork _uow;
        private readonly ISafSubMenuData _safSubMenuData;
        private readonly IVwSafSubMenuData _SafViewSubMenuData;

        public SafSubMenuLogic()
        {
            this._uow = new UnitOfWork();
            this._safSubMenuData = new SafSubMenuData(_uow);
            this._SafViewSubMenuData = new VwSafSubMenuData(_uow);

        }


        public SAF_SUBMENU Registrar(SAF_SUBMENU entidad)
        {
            var result = this._safSubMenuData.Add(entidad);
            return result;
        }

        public SAF_SUBMENU Actualizar(SAF_SUBMENU entidad)
        {
            var result = this._safSubMenuData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safSubMenuData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_SUBMENU BuscarPorId(int id)
        {
            return this._safSubMenuData.GetById(id);
        }

        public IEnumerable<SAF_SUBMENU> ListarTodos()
        {
            return this._safSubMenuData.GetAll();
        }

        public IEnumerable<VW_SAF_SUBMENU> ListarSubMenuDetallado()
        {
            return this._SafViewSubMenuData.GetAll();

        }
    }
}
