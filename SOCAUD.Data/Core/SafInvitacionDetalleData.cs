using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafInvitacionDetalleData : IBaseRepository<SAF_INVITACIONDETALLE>
    {
        IEnumerable<TcFECHASINVITADAS> FechasInvitadas(int idEquipo);
    }

    public class SafInvitacionDetalleData : BaseRepository<SAF_INVITACIONDETALLE>, ISafInvitacionDetalleData
    {
        private readonly IUnitOfWork _uow;

        public SafInvitacionDetalleData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<TcFECHASINVITADAS> FechasInvitadas(int idEquipo)
        {
            return this._uow.DataContext().SP_SAF_FECHASINVITADAS(idEquipo).ToList();
        }
    }
}
