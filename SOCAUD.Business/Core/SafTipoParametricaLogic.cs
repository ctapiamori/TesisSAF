using SOCAUD.Business.Infraestructure;
using SOCAUD.Common.Enum;
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

    public interface ISafTipoParametricaLogic : IFacadeOperacionCRUD<SAF_TIPOPARAMETRICA>
    {

    }

    public class SafTipoParametricaLogic : ISafTipoParametricaLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafTipoParametricaData _safTipoParametricaData;

        public SafTipoParametricaLogic()
        {
            this._uow = new UnitOfWork();
            this._safTipoParametricaData = new SafTipoParametricaData(_uow);
        }



        public SAF_TIPOPARAMETRICA Registrar(SAF_TIPOPARAMETRICA entidad)
        {
            var result = this._safTipoParametricaData.Add(entidad);
            return result;
        }

        public SAF_TIPOPARAMETRICA Actualizar(SAF_TIPOPARAMETRICA entidad)
        {
            var result = _safTipoParametricaData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safTipoParametricaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_TIPOPARAMETRICA BuscarPorId(int id)
        {
            var result = _safTipoParametricaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_TIPOPARAMETRICA> ListarTodos()
        {
            return this._safTipoParametricaData.GetAll();
        }
    }
}
