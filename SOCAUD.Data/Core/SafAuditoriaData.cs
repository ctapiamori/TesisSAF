using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafAuditoriaData : IBaseRepository<SAF_AUDITORIA>
    {
        IEnumerable<TcAUDITORIAS> ListarAuditoriasPorPropuesta(int idPropuesta);
    }

    public class SafAuditoriaData : BaseRepository<SAF_AUDITORIA>, ISafAuditoriaData
    {
        private readonly IUnitOfWork _uow;
        public SafAuditoriaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<TcAUDITORIAS> ListarAuditoriasPorPropuesta(int idPropuesta)
        {
            return this._uow.DataContext().SP_SAF_AUDITORIAS(idPropuesta).ToList();
        }
    }
}
