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

    public interface ISafPenalidadPropuestaLogic : IFacadeOperacionCRUD<SAF_PENALIDAD_PROPUESTA>
    {
    }

    public class SafPenalidadPropuestaLogic : ISafPenalidadPropuestaLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafPenalidadPropuestaData _safPenalidadPropuestaData;


        public SafPenalidadPropuestaLogic()
        {
            this._uow = new UnitOfWork();
            this._safPenalidadPropuestaData = new SafPenalidadPropuestaData(_uow);

        }

        public SAF_PENALIDAD_PROPUESTA Registrar(SAF_PENALIDAD_PROPUESTA entidad)
        {
            var result = _safPenalidadPropuestaData.Add(entidad);
            return result;
        }

        public SAF_PENALIDAD_PROPUESTA Actualizar(SAF_PENALIDAD_PROPUESTA entidad)
        {
            var result = _safPenalidadPropuestaData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                _safPenalidadPropuestaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_PENALIDAD_PROPUESTA BuscarPorId(int id)
        {
            var result = _safPenalidadPropuestaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_PENALIDAD_PROPUESTA> ListarTodos()
        {
            var result = _safPenalidadPropuestaData.GetAll();
            return result;
        }
    }
}
