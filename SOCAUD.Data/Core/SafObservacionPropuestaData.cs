using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SOCAUD.Data.Core
{

    public interface ISafObservacionPropuestaData : IBaseRepository<SAF_OBSERVACION_PROPUESTA> { }


    public class SafObservacionPropuestaData : BaseRepository<SAF_OBSERVACION_PROPUESTA>, ISafObservacionPropuestaData
    {
        private readonly IUnitOfWork _uow;

        public SafObservacionPropuestaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

    }
}
