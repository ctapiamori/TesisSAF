using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface IVwSafCargosEnServicioAuditoriaData : IBaseRepository<VW_SAF_CARGOSENSERVICIO> { }
    public class VwSafCargosEnServicioAuditoriaData : BaseRepository<VW_SAF_CARGOSENSERVICIO>, IVwSafCargosEnServicioAuditoriaData
    {
        public VwSafCargosEnServicioAuditoriaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
