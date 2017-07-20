using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{

    public interface ISafAbsolucionConsultaData : IBaseRepository<SAF_ABSOLUCION_CONSULTA> {

        IEnumerable<VW_SAF_ABSOLUCION_CONSULTA> ListarAbsolucionConsultasCompleto();
    
    }

    public class SafAbsolucionConsultaData : BaseRepository<SAF_ABSOLUCION_CONSULTA>, ISafAbsolucionConsultaData
    {
       private readonly IUnitOfWork _uow;

       public SafAbsolucionConsultaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }


       public IEnumerable<VW_SAF_ABSOLUCION_CONSULTA> ListarAbsolucionConsultasCompleto()
       {
           return this._uow.DataContext().VW_SAF_ABSOLUCION_CONSULTA.ToList();
       }
    }
}
