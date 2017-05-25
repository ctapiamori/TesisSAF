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
    public interface ISafBaseLogic : IFacadeOperacionCRUD<SAF_BASE>
    {
        int CorrelativoCronograma(int cronograma);
        IEnumerable<SAF_BASE> BuscarPorCronograma(int? cronograma);
        IEnumerable<TcSAFBASERPT> BaseRpt(int idBase);

    }

    public class SafBaseLogic : ISafBaseLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafBaseData _safBaseData;


        public SafBaseLogic()
        {
            this._uow = new UnitOfWork();
            this._safBaseData = new SafBaseData(_uow);

        }

        public SAF_BASE Registrar(SAF_BASE entidad)
        {
            var result = _safBaseData.Add(entidad);
            return result;
        }

        public SAF_BASE Actualizar(SAF_BASE entidad)
        {
            var result = _safBaseData.Update(entidad);
            return result;
        }

        public SAF_BASE BuscarPorId(int id)
        {
            var result = _safBaseData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_BASE> ListarTodos()
        {
            var result = _safBaseData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safBaseData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public int CorrelativoCronograma(int cronograma)
        {
            var bases = this._safBaseData.GetMany(c => c.CODCRO == cronograma);
            if(bases.Any())
            {
               var correlativo = bases.Max(c => c.CORBAS.GetValueOrDefault());
               return ++correlativo;
            }

            return 1;
        }


        public IEnumerable<SAF_BASE> BuscarPorCronograma(int? cronograma)
        {
            if (cronograma.HasValue)
                return this._safBaseData.GetMany(c => c.CODCRO == cronograma);
            else
                return this._safBaseData.GetAll();
        }

        public IEnumerable<TcSAFBASERPT> BaseRpt(int idBase)
        {
            return this._safBaseData.BaseRpt(idBase);
        }
    }
}
