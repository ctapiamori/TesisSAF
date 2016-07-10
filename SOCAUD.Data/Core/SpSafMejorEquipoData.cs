using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISpSafMejorEquipoData : IBaseRepository<TcMEJOREQUIPO>
    {
        IEnumerable<TcMEJOREQUIPO> VerMejorEquipoAuditoria(int idPublicacion, int idServicioAud);
    }


    public class SpSafMejorEquipoData : BaseRepository<TcMEJOREQUIPO>, ISpSafMejorEquipoData
    {
        private readonly IUnitOfWork _uow;

        public SpSafMejorEquipoData(IDatabaseFactory databaseFactory, IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public IEnumerable<TcMEJOREQUIPO> VerMejorEquipoAuditoria(int idPublicacion, int idServicioAud)
        {
            return this._uow.DataContext().SP_SAF_MEJOREQUIPO(idPublicacion, idServicioAud);
        }
    }
}
