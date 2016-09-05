using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafPublicacionBaseData : IBaseRepository<SAF_PUBLICACIONBASE>
    {
        IEnumerable<TcSAFPUBLICACIONBASERPT> ListarBasesPublicacionRpt(int idPublicacion);
    }


    public class SafPublicacionBaseData : BaseRepository<SAF_PUBLICACIONBASE>, ISafPublicacionBaseData
    {
        private readonly IUnitOfWork _uow;
        public SafPublicacionBaseData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }
        //public SafPublicacionBaseData(IUnitOfWork databaseFactory) : base(databaseFactory) { }

        public IEnumerable<TcSAFPUBLICACIONBASERPT> ListarBasesPublicacionRpt(int idPublicacion)
        {
            return this._uow.DataContext().SP_SAF_PUBLICACIONBASE_RPT(idPublicacion).ToList();
        }
    }
}
