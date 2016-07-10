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
    public interface ISafCronoEntidadLogic : IFacadeOperacionCRUD<SAF_CRONOENTIDAD>
    {
        IEnumerable<SAF_CRONOENTIDAD> ListarPorCronograma(int cronograma);

    }

    public class SafCronoEntidadLogic : ISafCronoEntidadLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafCronoEntidadData _safCronoEntidadData;


        public SafCronoEntidadLogic()
        {
            this._uow = new UnitOfWork();
            this._safCronoEntidadData = new SafCronoEntidadData(_uow);

        }

        public SAF_CRONOENTIDAD Registrar(SAF_CRONOENTIDAD entidad)
        {
            var result = _safCronoEntidadData.Add(entidad);
            return result;
        }

        public SAF_CRONOENTIDAD Actualizar(SAF_CRONOENTIDAD entidad)
        {
            var result = _safCronoEntidadData.Update(entidad);
            return result;
        }

        public SAF_CRONOENTIDAD BuscarPorId(int id)
        {
            var result = _safCronoEntidadData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_CRONOENTIDAD> ListarTodos()
        {
            var result = _safCronoEntidadData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safCronoEntidadData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SAF_CRONOENTIDAD> ListarPorCronograma(int cronograma)
        {
            return this._safCronoEntidadData.GetMany(c => c.CODCRO == cronograma);
        }


    }
}
