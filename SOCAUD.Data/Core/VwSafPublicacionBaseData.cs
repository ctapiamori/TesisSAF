using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafPublicacionBaseData : IBaseRepository<VW_SAF_PUBLICACIONBASE> { }


    public class VwSafPublicacionBaseData : BaseRepository<VW_SAF_PUBLICACIONBASE>, IVwSafPublicacionBaseData
    {
        public VwSafPublicacionBaseData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
