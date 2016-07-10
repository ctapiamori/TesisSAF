using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISpSafResultadoCorteData : IBaseRepository<TcRESULTADOCORTE>
    {
        IEnumerable<TcRESULTADOCORTE> VerResultadoCorte(int idPublicacion);
    }


    public class SpSafResultadoCorteData : BaseRepository<TcRESULTADOCORTE>, ISpSafResultadoCorteData
    {
        private readonly IUnitOfWork _uow;

        public SpSafResultadoCorteData(IDatabaseFactory databaseFactory, IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public IEnumerable<TcRESULTADOCORTE> VerResultadoCorte(int idPublicacion)
        {
            return this._uow.DataContext().SP_SAF_RESULTADOCORTE(idPublicacion);
        }

    }
}
