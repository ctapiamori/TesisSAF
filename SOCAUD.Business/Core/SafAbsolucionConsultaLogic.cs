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

    public interface ISafAbsolucionConsultaLogic : IFacadeOperacionCRUD<SAF_ABSOLUCION_CONSULTA>
    {
        IEnumerable<VW_SAF_ABSOLUCION_CONSULTA> ListarAbsolucionConsultasCompleto();
    }


    public class SafAbsolucionConsultaLogic : ISafAbsolucionConsultaLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafAbsolucionConsultaData _safAbsolucionConsultaData;


        public SafAbsolucionConsultaLogic()
        {
            this._uow = new UnitOfWork();
            this._safAbsolucionConsultaData = new SafAbsolucionConsultaData(_uow);
        }


        public SAF_ABSOLUCION_CONSULTA Registrar(SAF_ABSOLUCION_CONSULTA entidad)
        {
            var result = _safAbsolucionConsultaData.Add(entidad);
            return result;
        }

        public SAF_ABSOLUCION_CONSULTA Actualizar(SAF_ABSOLUCION_CONSULTA entidad)
        {
            var result = _safAbsolucionConsultaData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                _safAbsolucionConsultaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_ABSOLUCION_CONSULTA BuscarPorId(int id)
        {
            var result = _safAbsolucionConsultaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_ABSOLUCION_CONSULTA> ListarTodos()
        {
            var result = _safAbsolucionConsultaData.GetAll();
            return result;
        }

        public IEnumerable<VW_SAF_ABSOLUCION_CONSULTA> ListarAbsolucionConsultasCompleto()
        {
            return this._safAbsolucionConsultaData.ListarAbsolucionConsultasCompleto();
        }
    }
}
