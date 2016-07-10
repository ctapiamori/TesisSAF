using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCronogramaData : IBaseRepository<SAF_CRONOGRAMA> { }

    public class SafCronogramaData : BaseRepository<SAF_CRONOGRAMA>, ISafCronogramaData
    {
        public SafCronogramaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
