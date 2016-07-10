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
    public interface ISafBaseEntregableLogic : IFacadeOperacionCRUD<SAF_BASEENTREGABLE>
    {


    }

    public class SafBaseEntregableLogic : ISafBaseEntregableLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafBaseEntregableData _safBaseEntregableData;

        public SafBaseEntregableLogic()
        {
            this._uow = new UnitOfWork();
            this._safBaseEntregableData = new SafBaseEntregableData(_uow);

        }

        public SAF_BASEENTREGABLE Registrar(SAF_BASEENTREGABLE entidad)
        {
            var result = _safBaseEntregableData.Add(entidad);
            return result;
        }

        public SAF_BASEENTREGABLE Actualizar(SAF_BASEENTREGABLE entidad)
        {
            var result = _safBaseEntregableData.Update(entidad);
            return result;
        }

        public SAF_BASEENTREGABLE BuscarPorId(int id)
        {
            var result = _safBaseEntregableData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_BASEENTREGABLE> ListarTodos()
        {
            var result = _safBaseEntregableData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safBaseEntregableData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
