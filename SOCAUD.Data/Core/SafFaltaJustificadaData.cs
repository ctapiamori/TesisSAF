using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafFaltaJustificadaData : IBaseRepository<SAF_FALTAJUSTIFICA> { }

    public class SafFaltaJustificadaData : BaseRepository<SAF_FALTAJUSTIFICA>, ISafFaltaJustificadaData
    {
        public SafFaltaJustificadaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
