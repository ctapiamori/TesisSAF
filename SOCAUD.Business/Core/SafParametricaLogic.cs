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
    public interface ISafParametricaLogic : IFacadeOperacionCRUD<SAF_PARAMETRICA>
    {

        void ActualizaParametroPuntaje(SAF_PARAMETRICA param);

    }

    public class SafParametricaLogic : ISafParametricaLogic {


        private readonly IUnitOfWork _uow;
        private readonly ISafParametricaData _safParametricaData;


        public SafParametricaLogic()
        {
            this._uow = new UnitOfWork();
            this._safParametricaData = new SafParametricaData(_uow);

        }

        public SAF_PARAMETRICA Registrar(SAF_PARAMETRICA entidad)
        {
            throw new NotImplementedException();
        }

        public SAF_PARAMETRICA Actualizar(SAF_PARAMETRICA entidad)
        {
            var result = _safParametricaData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public SAF_PARAMETRICA BuscarPorId(int id)
        {
            var result = _safParametricaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_PARAMETRICA> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public void ActualizaParametroPuntaje(SAF_PARAMETRICA param)
        {
            var parametro = this.BuscarPorId(param.CODPAR);
            parametro.VALOR = param.VALOR;
            this.Actualizar(parametro);
        }
    }
}
