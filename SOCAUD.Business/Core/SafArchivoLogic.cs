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
    public interface ISafArchivoLogic : IFacadeOperacionCRUD<SAF_ARCHIVO>
    {

        
    }

    public class SafArchivoLogic : ISafArchivoLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafArchivoData _safArchivoData;


        public SafArchivoLogic()
        {
            this._uow = new UnitOfWork();
            this._safArchivoData = new SafArchivoData(_uow);

        }

        public SAF_ARCHIVO Registrar(SAF_ARCHIVO entidad)
        {
            var result = _safArchivoData.Add(entidad);
            return result;
        }

        public SAF_ARCHIVO Actualizar(SAF_ARCHIVO entidad)
        {
            var result = _safArchivoData.Update(entidad);
            return result;
        }

        public SAF_ARCHIVO BuscarPorId(int id)
        {
            var result = _safArchivoData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_ARCHIVO> ListarTodos()
        {
            var result = _safArchivoData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safArchivoData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
