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
    public interface ISafPerfilLogic : IFacadeOperacionCRUD<SAF_PERFIL>
    {

    }

    public class SafPerfilLogic : ISafPerfilLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafPerfilData _safPerfilData;


        public SafPerfilLogic()
        {
            this._uow = new UnitOfWork();
            this._safPerfilData = new SafPerfilData(_uow);

        }

        public SAF_PERFIL Registrar(SAF_PERFIL entidad)
        {
            var result = this._safPerfilData.Add(entidad);
            return result;
        }

        public SAF_PERFIL Actualizar(SAF_PERFIL entidad)
        {
            var result = this._safPerfilData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safPerfilData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_PERFIL BuscarPorId(int id)
        {
            return this._safPerfilData.GetById(id);
        }

        public IEnumerable<SAF_PERFIL> ListarTodos()
        {
            return this._safPerfilData.GetAll();
        }
    }
}
