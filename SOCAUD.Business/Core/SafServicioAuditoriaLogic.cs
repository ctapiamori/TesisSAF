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
    public interface ISafServicioAuditoriaLogic : IFacadeOperacionCRUD<SAF_SERVICIOAUDITORIA>
    {
        IEnumerable<SAF_SERVICIOAUDITORIA> ServiciosPorBase(int idBase);
    }
    public class SafServicioAuditoriaLogic : ISafServicioAuditoriaLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafServicioAuditoriaData _safServicioAuditoriaData;

        public SafServicioAuditoriaLogic()
        {
            this._uow = new UnitOfWork();
            this._safServicioAuditoriaData = new SafServicioAuditoriaData(_uow);
        }

        public SAF_SERVICIOAUDITORIA Registrar(SAF_SERVICIOAUDITORIA entidad)
        {
            return this._safServicioAuditoriaData.Add(entidad);
        }

        public SAF_SERVICIOAUDITORIA Actualizar(SAF_SERVICIOAUDITORIA entidad)
        {
            return this._safServicioAuditoriaData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safServicioAuditoriaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_SERVICIOAUDITORIA BuscarPorId(int id)
        {
            return this._safServicioAuditoriaData.GetById(id);
        }

        public IEnumerable<SAF_SERVICIOAUDITORIA> ListarTodos()
        {
            return this._safServicioAuditoriaData.GetAll();
        }

        public IEnumerable<SAF_SERVICIOAUDITORIA> ServiciosPorBase(int idBase)
        {
            return this._safServicioAuditoriaData.GetMany(c => c.CODBAS == idBase);
        }
    }
}
