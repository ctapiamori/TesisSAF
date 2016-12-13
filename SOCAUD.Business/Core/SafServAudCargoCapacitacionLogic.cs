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
    public interface ISafServAudCargoCapacitacionLogic : IFacadeOperacionCRUD<SAF_SERAUDCARCAP>
    {
        SAF_SERAUDCARCAP BuscarPorServicioCargo(int idServicioCargo);
    }

    public class SafServAudCargoCapacitacionLogic : ISafServAudCargoCapacitacionLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafServAudCargoCapacitacionData _safServAudCargoCapacitacionData;

        public SafServAudCargoCapacitacionLogic()
        {
            this._uow = new UnitOfWork();
            this._safServAudCargoCapacitacionData = new SafServAudCargoCapacitacionData(_uow);
        }

        public SAF_SERAUDCARCAP BuscarPorServicioCargo(int idServicioCargo)
        {
            return this._safServAudCargoCapacitacionData.Get(c => c.CODSERAUDCAR == idServicioCargo);
        }

        public SAF_SERAUDCARCAP Registrar(SAF_SERAUDCARCAP entidad)
        {
            return this._safServAudCargoCapacitacionData.Add(entidad);
        }

        public SAF_SERAUDCARCAP Actualizar(SAF_SERAUDCARCAP entidad)
        {
            return this._safServAudCargoCapacitacionData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safServAudCargoCapacitacionData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_SERAUDCARCAP BuscarPorId(int id)
        {
            return this._safServAudCargoCapacitacionData.GetById(id);
        }

        public IEnumerable<SAF_SERAUDCARCAP> ListarTodos()
        {
            return this._safServAudCargoCapacitacionData.GetAll();
        }
    }
}
