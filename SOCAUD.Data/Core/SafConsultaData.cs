using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafConsultaData : IBaseRepository<SAF_CONSULTA> {
        IEnumerable<VW_SAF_CONSULTA> ListadoCompletoConsulta();
    }

    public class SafConsultaData : BaseRepository<SAF_CONSULTA>, ISafConsultaData
    {
        private readonly IUnitOfWork _uow;

        public SafConsultaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<VW_SAF_CONSULTA> ListadoCompletoConsulta()
        {
            return this._uow.DataContext().VW_SAF_CONSULTA.ToList();
        }
    }
}
