using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafPropuestaEquipoData : IBaseRepository<SAF_PROPEQUIPO>
    {
        IEnumerable<TcEQUIPOAUDITORIA> ListarEquipoAuditoria(int idAuditoria);
        IEnumerable<TcEQUIPOAUDITORIARPT> ListarEquipoAuditoriaRpt(int idPropuesta);
    }

    public class SafPropuestaEquipoData : BaseRepository<SAF_PROPEQUIPO>, ISafPropuestaEquipoData
    {
        private readonly IUnitOfWork _uow;
        public SafPropuestaEquipoData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<TcEQUIPOAUDITORIA> ListarEquipoAuditoria(int idAuditoria)
        {
            return this._uow.DataContext().SP_SAF_EQUIPOAUDITORIA(idAuditoria).ToList();
        }


        public IEnumerable<TcEQUIPOAUDITORIARPT> ListarEquipoAuditoriaRpt(int idPropuesta)
        {
            return this._uow.DataContext().SP_SAF_EQUIPOAUDITORIA_RPT(idPropuesta).ToList();
        }
    }
}
