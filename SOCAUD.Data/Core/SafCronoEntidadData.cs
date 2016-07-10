using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCronoEntidadData : IBaseRepository<SAF_CRONOENTIDAD> { }

    public class SafCronoEntidadData : BaseRepository<SAF_CRONOENTIDAD>, ISafCronoEntidadData
    {
        public SafCronoEntidadData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
