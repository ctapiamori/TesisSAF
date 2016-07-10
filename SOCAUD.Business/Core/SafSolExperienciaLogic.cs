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
    public interface ISafSolExperienciaLogic : IFacadeOperacionCRUD<SAF_SOLEXPERIENCIA> {
        IEnumerable<SAF_SOLEXPERIENCIA> ListarPorSolicitud(int idSolicitud);
    }

    public class SafSolExperienciaLogic : ISafSolExperienciaLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafSolExperienciaData _safSolExperienciaData;

        public SafSolExperienciaLogic()
        {
            this._uow = new UnitOfWork();
            this._safSolExperienciaData = new SafSolExperienciaData(_uow);
        }


        public SAF_SOLEXPERIENCIA Registrar(SAF_SOLEXPERIENCIA entidad)
        {
            var result = _safSolExperienciaData.Add(entidad);
            return result;
        }

        public SAF_SOLEXPERIENCIA Actualizar(SAF_SOLEXPERIENCIA entidad)
        {
            var result = _safSolExperienciaData.Update(entidad);
            return result;
        }

        public SAF_SOLEXPERIENCIA BuscarPorId(int id)
        {
            try
            {
                var result = _safSolExperienciaData.GetById(id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<SAF_SOLEXPERIENCIA> ListarTodos()
        {
            var result = _safSolExperienciaData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safSolExperienciaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SAF_SOLEXPERIENCIA> ListarPorSolicitud(int idSolicitud)
        {
            return this._safSolExperienciaData.GetMany(x => x.CODSOL == idSolicitud).ToList();
        }
    }
}
