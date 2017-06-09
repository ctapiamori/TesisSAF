using SOCAUD.Business.Infraestructure;
using SOCAUD.Common.Enum;
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

        IEnumerable<SAF_PARAMETRICA> ListarTipoUsuario();

        IEnumerable<VW_SAF_PARAMETRICA> ListarParametricaCompleta();

    }

    public class SafParametricaLogic : ISafParametricaLogic {


        private readonly IUnitOfWork _uow;
        private readonly ISafParametricaData _safParametricaData;
        private readonly IVwSafParametroData _vwSafParametricaData;

        public SafParametricaLogic()
        {
            this._uow = new UnitOfWork();
            this._safParametricaData = new SafParametricaData(_uow);
            this._vwSafParametricaData = new VwSafParametroData(_uow);

        }

        public SAF_PARAMETRICA Registrar(SAF_PARAMETRICA entidad)
        {
            var result = this._safParametricaData.Add(entidad);
            return result;
        }

        public SAF_PARAMETRICA Actualizar(SAF_PARAMETRICA entidad)
        {
            var result = _safParametricaData.Update(entidad);
            return result;
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safParametricaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_PARAMETRICA BuscarPorId(int id)
        {
            var result = _safParametricaData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_PARAMETRICA> ListarTodos()
        {
            return this._safParametricaData.GetAll();
        }

        public void ActualizaParametroPuntaje(SAF_PARAMETRICA param)
        {
            var parametro = this.BuscarPorId(param.CODPAR);
            parametro.VALOR = param.VALOR;
            this.Actualizar(parametro);
        }


        public IEnumerable<SAF_PARAMETRICA> ListarTipoUsuario()
        {
            var codigo =TipoParametrica.Codigo.TipoUsuario.GetHashCode();
            var parametros = this._safParametricaData.GetMany(c => c.CODTIPPAR == codigo);
            return parametros;
        }


        public IEnumerable<VW_SAF_PARAMETRICA> ListarParametricaCompleta()
        {
            return this._vwSafParametricaData.GetAll();
        }
    }
}
