using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface IVwSafInvitacionData : IBaseRepository<VW_SAF_INVITACION> { }

    public class VwSafInvitacionData : BaseRepository<VW_SAF_INVITACION>, IVwSafInvitacionData
    {
        public VwSafInvitacionData(IUnitOfWork databaseFactory) : base(databaseFactory) { }
    }
}
