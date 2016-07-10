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
    public interface ISafEntidadLogic : IFacadeOperacionCRUD<SAF_ENTIDADES>
    {


    }

    public class SafEntidadLogic : ISafEntidadLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafEntidadData _safEntidadData;


        public SafEntidadLogic()
        {
            this._uow = new UnitOfWork();
            this._safEntidadData = new SafEntidadData(_uow);

        }

        public SAF_ENTIDADES Registrar(SAF_ENTIDADES entidad)
        {
            var result = _safEntidadData.Add(entidad);
            return result;
        }

        public SAF_ENTIDADES Actualizar(SAF_ENTIDADES entidad)
        {
            var result = _safEntidadData.Update(entidad);
            return result;
        }

        public SAF_ENTIDADES BuscarPorId(int id)
        {
            var result = _safEntidadData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_ENTIDADES> ListarTodos()
        {
            var result = _safEntidadData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safEntidadData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
