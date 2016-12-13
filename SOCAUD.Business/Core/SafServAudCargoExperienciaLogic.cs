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
    public interface ISafServAudCargoExperienciaLogic : IFacadeOperacionCRUD<SAF_SERAUDCAREXP>
    {
        SAF_SERAUDCAREXP BuscarPorServicioCargo(int idServicioCargo);
    }

    public class SafServAudCargoExperienciaLogic : ISafServAudCargoExperienciaLogic
    {
        
        private readonly IUnitOfWork _uow;
        private readonly ISafServAudCargoExperienciaData _safServAudCargoExperienciaData;

        public SafServAudCargoExperienciaLogic()
        {
            this._uow = new UnitOfWork();
            this._safServAudCargoExperienciaData = new SafServAudCargoExperienciaData(_uow);
        }

        public SAF_SERAUDCAREXP BuscarPorServicioCargo(int idServicioCargo)
        {
            return this._safServAudCargoExperienciaData.Get(c => c.CODSERAUDCAR == idServicioCargo);
        }

        public SAF_SERAUDCAREXP Registrar(SAF_SERAUDCAREXP entidad)
        {
            return this._safServAudCargoExperienciaData.Add(entidad);
        }

        public SAF_SERAUDCAREXP Actualizar(SAF_SERAUDCAREXP entidad)
        {
            return this._safServAudCargoExperienciaData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safServAudCargoExperienciaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_SERAUDCAREXP BuscarPorId(int id)
        {
            return this._safServAudCargoExperienciaData.GetById(id);
        }

        public IEnumerable<SAF_SERAUDCAREXP> ListarTodos()
        {
            return this._safServAudCargoExperienciaData.GetAll();
        }
    }
}
