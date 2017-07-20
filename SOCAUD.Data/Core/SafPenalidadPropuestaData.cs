using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface ISafPenalidadPropuestaData : IBaseRepository<SAF_PENALIDAD_PROPUESTA> { }

    public class SafPenalidadPropuestaData : BaseRepository<SAF_PENALIDAD_PROPUESTA>, ISafPenalidadPropuestaData
    {
        private readonly IUnitOfWork _uow;

        public SafPenalidadPropuestaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

    }
}
