using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCronoEntidadData : IBaseRepository<SAF_CRONOENTIDAD>
    {
        IEnumerable<TcSAFCRONOENTIDADCRONORPT> ListarEntidadesCronogramaRpt(int idCronograma);
    }

    public class SafCronoEntidadData : BaseRepository<SAF_CRONOENTIDAD>, ISafCronoEntidadData
    {
        private readonly IUnitOfWork _uow;

        public SafCronoEntidadData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        //public SafCronoEntidadData(IUnitOfWork databaseFactory) : base(databaseFactory) { }

        public IEnumerable<TcSAFCRONOENTIDADCRONORPT> ListarEntidadesCronogramaRpt(int idCronograma)
        {
            return this._uow.DataContext().SP_SAF_CRONOENTIDAD_CRONO_RPT(idCronograma).ToList();
        }
    }
}
