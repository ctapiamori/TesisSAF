using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface ISafPerfilData : IBaseRepository<SAF_PERFIL> { }

    public class SafPerfilData : BaseRepository<SAF_PERFIL>, ISafPerfilData
    {
        public SafPerfilData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
