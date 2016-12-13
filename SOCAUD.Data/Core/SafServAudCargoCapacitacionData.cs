using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SOCAUD.Data.Core
{
    public interface ISafServAudCargoCapacitacionData : IBaseRepository<SAF_SERAUDCARCAP> { }

    public class SafServAudCargoCapacitacionData : BaseRepository<SAF_SERAUDCARCAP>, ISafServAudCargoCapacitacionData
    {
        public SafServAudCargoCapacitacionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
