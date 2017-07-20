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

    public interface ISafObservacionPropuestaLogic : IFacadeOperacionCRUD<SAF_OBSERVACION_PROPUESTA>
    {
    }
    public class SafObservacionPropuestaLogic : ISafObservacionPropuestaLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafObservacionPropuestaData _safObservacionPropuestaData;


        public SafObservacionPropuestaLogic()
        {
            this._uow = new UnitOfWork();
            this._safObservacionPropuestaData = new SafObservacionPropuestaData(_uow);

        }


        public SAF_OBSERVACION_PROPUESTA Registrar(SAF_OBSERVACION_PROPUESTA entidad)
        {
            var result = _safObservacionPropuestaData.Add(entidad);
            return result;
        }

        public SAF_OBSERVACION_PROPUESTA Actualizar(SAF_OBSERVACION_PROPUESTA entidad)
        {
            var result = _safObservacionPropuestaData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                _safObservacionPropuestaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_OBSERVACION_PROPUESTA BuscarPorId(int id)
        {
            var result = _safObservacionPropuestaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_OBSERVACION_PROPUESTA> ListarTodos()
        {
            var result = _safObservacionPropuestaData.GetAll();
            return result;
        }
    }
}
