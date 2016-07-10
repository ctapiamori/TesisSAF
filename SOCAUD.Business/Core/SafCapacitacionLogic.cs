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
    public interface ISafCapacitacionLogic : IFacadeOperacionCRUD<SAF_CAPACITACION>
    {
        IEnumerable<SAF_CAPACITACION> ListarPorAuditor(int idAuditor);

    }

    public class SafCapacitacionLogic : ISafCapacitacionLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafCapacitacionData _safCapacitacionData;


        public SafCapacitacionLogic()
        {
            this._uow = new UnitOfWork();
            this._safCapacitacionData = new SafCapacitacionData(_uow);

        }

        public SAF_CAPACITACION Registrar(SAF_CAPACITACION entidad)
        {
            var result = _safCapacitacionData.Add(entidad);
            return result;
        }

        public SAF_CAPACITACION Actualizar(SAF_CAPACITACION entidad)
        {
            var result = _safCapacitacionData.Update(entidad);
            return result;
        }

        public SAF_CAPACITACION BuscarPorId(int id)
        {
            var result = _safCapacitacionData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_CAPACITACION> ListarTodos()
        {
            var result = _safCapacitacionData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safCapacitacionData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<SAF_CAPACITACION> ListarPorAuditor(int idAuditor)
        {
            return this._safCapacitacionData.GetMany(x => x.CODAUD == idAuditor);
        }
    }
}
