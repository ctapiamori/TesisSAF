using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafPublicacionBaseData : IBaseRepository<SAF_PUBLICACIONBASE> { }


    public class SafPublicacionBaseData : BaseRepository<SAF_PUBLICACIONBASE>, ISafPublicacionBaseData
    {
        public SafPublicacionBaseData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
