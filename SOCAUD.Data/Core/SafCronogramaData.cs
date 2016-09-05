using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafCronogramaData : IBaseRepository<SAF_CRONOGRAMA> 
    {
        IEnumerable<TcSAFCRONOGRAMARPT> CronogramaRpt(int idCronograma);
    }

    public class SafCronogramaData : BaseRepository<SAF_CRONOGRAMA>, ISafCronogramaData
    {
        private readonly IUnitOfWork _uow;

        public SafCronogramaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        //public SafCronogramaData(IUnitOfWork databaseFactory) : base(databaseFactory) { }

        public IEnumerable<TcSAFCRONOGRAMARPT> CronogramaRpt(int idCronograma)
        {
            return this._uow.DataContext().SP_SAF_CRONOGRAMA_RPT(idCronograma).ToList();
        }
    }
}
