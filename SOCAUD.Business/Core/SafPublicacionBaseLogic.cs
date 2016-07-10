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
    public interface ISafPublicacionBaseLogic : IFacadeOperacionCRUD<SAF_PUBLICACIONBASE>
    {
        IEnumerable<SAF_PUBLICACIONBASE> ListarPorPublicacion(int publicacionId);
    }

    public class SafPublicacionBaseLogic : ISafPublicacionBaseLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly IDatabaseFactory _dataFactory;
        private readonly ISafPublicacionBaseData _safPublicacionBaseData;

        public SafPublicacionBaseLogic()
        {
            this._uow = new UnitOfWork();
            this._dataFactory = new DatabaseFactory();
            this._safPublicacionBaseData = new SafPublicacionBaseData(_uow);
        }

        public IEnumerable<SAF_PUBLICACIONBASE> ListarPorPublicacion(int publicacionId)
        {
            return _safPublicacionBaseData.GetMany(c => c.CODPUB == publicacionId);
        }

        public SAF_PUBLICACIONBASE Registrar(SAF_PUBLICACIONBASE entidad)
        {
            return this._safPublicacionBaseData.Add(entidad);
        }

        public SAF_PUBLICACIONBASE Actualizar(SAF_PUBLICACIONBASE entidad)
        {
            return this._safPublicacionBaseData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safPublicacionBaseData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_PUBLICACIONBASE BuscarPorId(int id)
        {
            return this._safPublicacionBaseData.GetById(id);
        }

        public IEnumerable<SAF_PUBLICACIONBASE> ListarTodos()
        {
            return this._safPublicacionBaseData.GetAll();
        }
    }
}
