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
    public interface ISafPropuestaLogic : IFacadeOperacionCRUD<SAF_PROPUESTA>
    {
        TcCREARPROPUESTA CrearPropuesta(int idPublicacion, int idSoa);
        IEnumerable<SAF_PROPUESTA> ListarPropuestasPorPublicacion(int idPublicacion);
        IEnumerable<TcPROPUESTAS> ListadoPropuestasCalificar(int? idPublicacion);
        IEnumerable<TcPROPUESTAS> ListarPropuestas(int idPublicacion, int idSoa);
        TcPROPUESTAS PropuestaPorId(int idPropuesta);
        TcASIGNARGANADORPROPUESTA AsignarGanadorPropuesta(int idPropuesta, int idPublicacion);
        IEnumerable<TcPROPUESTARPT> ListarPropuestasRpt(int idPropuesta);
    }

    public class SafPropuestaLogic : ISafPropuestaLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafPropuestaData _safPropuestaData;

        public SafPropuestaLogic()
        {
            this._uow = new UnitOfWork();
            this._safPropuestaData = new SafPropuestaData(_uow);
        }


        public SAF_PROPUESTA Registrar(SAF_PROPUESTA entidad)
        {
            return this._safPropuestaData.Add(entidad);
        }

        public SAF_PROPUESTA Actualizar(SAF_PROPUESTA entidad)
        {
            return this._safPropuestaData.Update(entidad);
        }

        public SAF_PROPUESTA BuscarPorId(int id)
        {
            return this._safPropuestaData.GetById(id);
        }

        public IEnumerable<SAF_PROPUESTA> ListarTodos()
        {
            return this._safPropuestaData.GetAll();
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safPropuestaData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TcCREARPROPUESTA CrearPropuesta(int idPublicacion, int idSoa)
        {
            return this._safPropuestaData.CrearPropuesta(idPublicacion, idSoa);
        }


        public IEnumerable<SAF_PROPUESTA> ListarPropuestasPorPublicacion(int idPublicacion)
        {
            return this._safPropuestaData.GetMany(c => c.CODPUB == idPublicacion);
        }

        public IEnumerable<TcPROPUESTAS> ListadoPropuestasCalificar(int? idPublicacion)
        {
            return this._safPropuestaData.ListadoPropuestasCalificar(idPublicacion);
        }

        public IEnumerable<TcPROPUESTAS> ListarPropuestas(int idPublicacion, int idSoa)
        {
            return this._safPropuestaData.ListarPropuestas(idPublicacion, idSoa);
        }


        public TcPROPUESTAS PropuestaPorId(int idPropuesta)
        {
            return this._safPropuestaData.PropuestaPorId(idPropuesta);
        }


        public TcASIGNARGANADORPROPUESTA AsignarGanadorPropuesta(int idPropuesta, int idPublicacion)
        {
            return this._safPropuestaData.AsignarGanadorPropuesta(idPropuesta, idPublicacion);
        }

        public IEnumerable<TcPROPUESTARPT> ListarPropuestasRpt(int idPropuesta)
        {
            return this._safPropuestaData.ListarPropuestasRpt(idPropuesta);
        }
    }
}
