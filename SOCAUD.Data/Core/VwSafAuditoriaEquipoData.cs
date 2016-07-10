using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface IVwSafAuditoriaEquipoData : IBaseRepository<VW_SAF_AUDITORIAEQUIPO> { }

    public class VwSafAuditoriaEquipoData : BaseRepository<VW_SAF_AUDITORIAEQUIPO>, IVwSafAuditoriaEquipoData
    {
        public VwSafAuditoriaEquipoData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
