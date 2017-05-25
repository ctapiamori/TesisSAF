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
    public interface ISafCronogramaLogic : IFacadeOperacionCRUD<SAF_CRONOGRAMA>
    {

        IEnumerable<SAF_CRONOGRAMA> ListarPorAnio(int? anio);
        int NewCorrelativoBase(int cronograma);
        int NewCorrelativoPublicacion(int cronograma);
        IEnumerable<TcSAFCRONOGRAMARPT> CronogramaRpt(int idCronograma);
    }

    public class SafCronogramaLogic : ISafCronogramaLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafCronogramaData _safCronogramaData;


        public SafCronogramaLogic()
        {
            this._uow = new UnitOfWork();
            this._safCronogramaData = new SafCronogramaData(_uow);

        }

        public SAF_CRONOGRAMA Registrar(SAF_CRONOGRAMA entidad)
        {
            var result = _safCronogramaData.Add(entidad);
            return result;
        }

        public SAF_CRONOGRAMA Actualizar(SAF_CRONOGRAMA entidad)
        {
            var result = _safCronogramaData.Update(entidad);
            return result;
        }

        public SAF_CRONOGRAMA BuscarPorId(int id)
        {
            var result = _safCronogramaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_CRONOGRAMA> ListarTodos()
        {
            var result = _safCronogramaData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            try
            {
                _safCronogramaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<SAF_CRONOGRAMA> ListarPorAnio(int? anio)
        {
            if (anio.HasValue)
                return this._safCronogramaData.GetMany(c => c.ANIOCRO == anio);
            else
                return this._safCronogramaData.GetAll();
        }


        public int NewCorrelativoBase(int cronograma)
        {
            var _cronograma = BuscarPorId(cronograma);
            _cronograma.NUMBAS = _cronograma.NUMBAS.GetValueOrDefault() + 1;
            Actualizar(_cronograma);
            return _cronograma.NUMBAS.GetValueOrDefault();
        }

        public int NewCorrelativoPublicacion(int cronograma)
        {
            var _cronograma = BuscarPorId(cronograma);
            _cronograma.NUMPUB = _cronograma.NUMPUB.GetValueOrDefault() + 1;
            Actualizar(_cronograma);
            return _cronograma.NUMPUB.GetValueOrDefault();
        }


        public IEnumerable<TcSAFCRONOGRAMARPT> CronogramaRpt(int idCronograma)
        {
            return _safCronogramaData.CronogramaRpt(idCronograma);
        }
    }
}
