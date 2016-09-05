using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafPublicacionData : IBaseRepository<SAF_PUBLICACION> {
        IEnumerable<TcMEJOREQUIPO> VerMejorEquipoAuditoria(int idPublicacion, int idServicioAud);
        IEnumerable<TcSAFPUBLICACIONRPT> PublicacionRpt(int idPublicacion);
    }

    public class SafPublicacionData : BaseRepository<SAF_PUBLICACION>, ISafPublicacionData
    {
        private readonly IUnitOfWork _uow;
        public SafPublicacionData(IUnitOfWork uow) : base(uow) {
            this._uow = uow;
        }

        public IEnumerable<TcMEJOREQUIPO> VerMejorEquipoAuditoria(int idPublicacion, int idServicioAud)
        {
            return this._uow.DataContext().SP_SAF_MEJOREQUIPO(idPublicacion, idServicioAud).ToList(); 
        }

        public IEnumerable<TcSAFPUBLICACIONRPT> PublicacionRpt(int idPublicacion)
        {
            return this._uow.DataContext().SP_SAF_PUBLICACION_RPT(idPublicacion).ToList();
        }
    }
}
