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
    public interface ISafSolCapacitacionLogic : IFacadeOperacionCRUD<SAF_SOLCAPACITACION> {
        IEnumerable<SAF_SOLCAPACITACION> ListarPorSolicitud(int idSolicitud);
    }

    public class SafSolCapacitacionLogic : ISafSolCapacitacionLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafSolCapacitacionData _safSolCapacitacionData;
        public SafSolCapacitacionLogic()
        {
            this._uow = new UnitOfWork();
            this._safSolCapacitacionData = new SafSolCapacitacionData(_uow);
        }


        public SAF_SOLCAPACITACION Registrar(SAF_SOLCAPACITACION entidad)
        {
            var result = _safSolCapacitacionData.Add(entidad);
            return result;
        }

        public SAF_SOLCAPACITACION Actualizar(SAF_SOLCAPACITACION entidad)
        {
            var result = _safSolCapacitacionData.Update(entidad);
            return result;
        }

        public SAF_SOLCAPACITACION BuscarPorId(int id)
        {
            var result = _safSolCapacitacionData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_SOLCAPACITACION> ListarTodos()
        {
            var result = _safSolCapacitacionData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safSolCapacitacionData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SAF_SOLCAPACITACION> ListarPorSolicitud(int idSolicitud)
        {
            return this._safSolCapacitacionData.GetMany(x => x.CODSOL == idSolicitud).ToList();
        }
    }
}
