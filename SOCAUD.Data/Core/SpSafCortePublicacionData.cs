using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISpSafCortePublicacionData : IBaseRepository<TcCORTEPUBLICACION>
    {
        TcCORTEPUBLICACION GenerarCortePublicacion(int idPublicacion);
    }
    public class SpSafCortePublicacionData : BaseRepository<TcCORTEPUBLICACION>, ISpSafCortePublicacionData
    {
        private readonly IUnitOfWork _uow;

        public SpSafCortePublicacionData(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public TcCORTEPUBLICACION GenerarCortePublicacion(int idPublicacion)
        {
            return this._uow.DataContext().SP_SAF_CORTEPUBLICACION(idPublicacion).FirstOrDefault();
        }
    }
}
