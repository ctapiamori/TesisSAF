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
    public interface ISafExperienciaLogic : IFacadeOperacionCRUD<SAF_EXPERIENCIA>
    {
        IEnumerable<SAF_EXPERIENCIA> ListarPorAuditor(int idAuditor);

    }

    public class SafExperienciaLogic : ISafExperienciaLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafExperienciaData _safExperienciaData;


        public SafExperienciaLogic()
        {
            this._uow = new UnitOfWork();
            this._safExperienciaData = new SafExperienciaData(_uow);

        }

        public SAF_EXPERIENCIA Registrar(SAF_EXPERIENCIA entidad)
        {
            var result = _safExperienciaData.Add(entidad);
            return result;
        }

        public SAF_EXPERIENCIA Actualizar(SAF_EXPERIENCIA entidad)
        {
            var result = _safExperienciaData.Update(entidad);
            return result;
        }

        public SAF_EXPERIENCIA BuscarPorId(int id)
        {
            var result = _safExperienciaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_EXPERIENCIA> ListarTodos()
        {
            var result = _safExperienciaData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safExperienciaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<SAF_EXPERIENCIA> ListarPorAuditor(int idAuditor)
        {
            return this._safExperienciaData.GetMany(x => x.CODAUD == idAuditor);
        }
    }
}
