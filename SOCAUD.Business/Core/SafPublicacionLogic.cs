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
using System.Transactions;

namespace SOCAUD.Business.Core
{
    public interface ISafPublicacionLogic : IFacadeOperacionCRUD<SAF_PUBLICACION>
    {
        IEnumerable<TcMEJOREQUIPO> ListarMejorEquipoAuditoria(int idPublicacion, int idServicioAud);
        IEnumerable<VW_SAF_PUBLICACION> ListarPublicacion();
        SAF_PUBLICACION PublicarPublicacion(int id);
        TcCORTEPUBLICACION GenerarCortePublicacion(int idPublicacion);
        IEnumerable<TcRESULTADOCORTE> VerResultadoCorte(int idPublicacion);
    }

    public class SafPublicacionLogic : ISafPublicacionLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly IDatabaseFactory _dataFactory;
        private readonly ISafPublicacionData _safPublicacionData;
        private readonly IVwSafPublicacionData _vwSafPublicacionData;
        private readonly ISpSafCortePublicacionData _spSafCortePublicacionData;
        private readonly ISpSafResultadoCorteData _spSafResultadoCorteLogic;

        public SafPublicacionLogic()
        {
            this._uow = new UnitOfWork();
            this._dataFactory = new DatabaseFactory();
            this._safPublicacionData = new SafPublicacionData(_uow);
            this._vwSafPublicacionData = new VwSafPublicacionData(_uow);
            this._spSafCortePublicacionData = new SpSafCortePublicacionData(_uow);
            this._spSafResultadoCorteLogic = new SpSafResultadoCorteData(_dataFactory, _uow);
        }


        public SAF_PUBLICACION Registrar(SAF_PUBLICACION entidad)
        {
            return this._safPublicacionData.Add(entidad);
        }

        public SAF_PUBLICACION Actualizar(SAF_PUBLICACION entidad)
        {
            return this._safPublicacionData.Update(entidad);
        }

        public SAF_PUBLICACION BuscarPorId(int id)
        {
            return this._safPublicacionData.GetById(id);
        }

        public IEnumerable<SAF_PUBLICACION> ListarTodos()
        {
            return this._safPublicacionData.GetAll();
        }


        public bool Eliminar(int id)
        {
            try
            {
                this._safPublicacionData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TcMEJOREQUIPO> ListarMejorEquipoAuditoria(int idPublicacion, int idServicioAud)
        {
            return this._safPublicacionData.VerMejorEquipoAuditoria(idPublicacion, idServicioAud);
        }


        public IEnumerable<VW_SAF_PUBLICACION> ListarPublicacion()
        {
            return this._vwSafPublicacionData.GetAll();
        }


        public SAF_PUBLICACION PublicarPublicacion(int id)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    var publicacion = this.BuscarPorId(id);// this._safPublicacionLogic.BuscarPorId(id);
                    publicacion.ESTPUB = (int)Estado.Publicacion.Publicado;
                    var entidad = this.Actualizar(publicacion);// this._safPublicacionLogic.Actualizar(publicacion);
                    //var result = Mapper.Map<SAF_PUBLICACION, PublicacionDTO>(entidad);
                    var corte = this.GenerarCortePublicacion(id);

                    if (corte.EXITO.Equals(0))
                    {
                        tran.Dispose();
                        throw new Exception();
                    }

                    tran.Complete();
                    return entidad;
                }
                catch (Exception)
                {
                    tran.Dispose();
                    throw;
                }
            }
        }


        public TcCORTEPUBLICACION GenerarCortePublicacion(int idPublicacion)
        {
            return this._spSafCortePublicacionData.GenerarCortePublicacion(idPublicacion);
        }


        public IEnumerable<TcRESULTADOCORTE> VerResultadoCorte(int idPublicacion)
        {
            return _spSafResultadoCorteLogic.VerResultadoCorte(idPublicacion);
        }
    }
}
