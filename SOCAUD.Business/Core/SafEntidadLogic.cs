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

        SAF_ENTIDADES ModificarInformacion(SAF_ENTIDADES entidad);
            
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


        public SAF_ENTIDADES ModificarInformacion(SAF_ENTIDADES entidad)
        {
            var info = this._safEntidadData.GetById(entidad.CODENT);

            info.RAZSOCENT = entidad.RAZSOCENT;
            info.VISENT = entidad.VISENT;
            info.MISENT = entidad.MISENT;
            info.ACTPRIENT = entidad.ACTPRIENT;
            info.BASLEGENT = entidad.BASLEGENT;
            info.APEREPLEGENT = entidad.APEREPLEGENT;
            info.NOMREPLEGENT = entidad.NOMREPLEGENT;
            info.CELREPLEGENT = entidad.CELREPLEGENT;
            info.TELREPLEGENT = entidad.TELREPLEGENT;
            info.CORREPLEGENT = entidad.CORREPLEGENT;
            info.DOMLEGENT = entidad.DOMLEGENT;
            info.PAGWEBENT = entidad.PAGWEBENT;
            info.CODDEPENT = entidad.CODDEPENT;
            info.CODDISENT = entidad.CODDISENT;
            info.CODPROENT = entidad.CODPROENT;

            this._safEntidadData.Update(info);
            return info;
        }
    }
}
