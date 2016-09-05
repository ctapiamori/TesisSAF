using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafBaseData : IBaseRepository<SAF_BASE>
    {
        IEnumerable<TcSAFBASERPT> BaseRpt(int idBase);
    }

    public class SafBaseData : BaseRepository<SAF_BASE>, ISafBaseData
    {
        private readonly IUnitOfWork _uow;

        public SafBaseData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }
        //public SafBaseData(IUnitOfWork databaseFactory) : base(databaseFactory) { }

        public IEnumerable<TcSAFBASERPT> BaseRpt(int idBase)
        {
            return this._uow.DataContext().SP_SAF_BASE_RPT(idBase).ToList();
        }
    }
}
