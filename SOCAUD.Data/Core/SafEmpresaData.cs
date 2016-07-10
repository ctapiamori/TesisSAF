using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafEmpresaData : IBaseRepository<SAF_EMPRESA> { }

    public class SafEmpresaData : BaseRepository<SAF_EMPRESA>, ISafEmpresaData
    {
        public SafEmpresaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
