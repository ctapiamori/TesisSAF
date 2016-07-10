using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISpSafInvitacionData : IBaseRepository<TcINVITACION>
    {
        IEnumerable<TcINVITACION> VerInvitaciones(int idPublicacion, int idServicioAud);
    }

    public class SpSafInvitacionData : BaseRepository<TcINVITACION>, ISpSafInvitacionData
    {
        private readonly IUnitOfWork _uow;

        public SpSafInvitacionData(IDatabaseFactory databaseFactory, IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public IEnumerable<TcINVITACION> VerInvitaciones(int idPublicacion, int idServicioAud)
        {
            return this._uow.DataContext().SP_SAF_INVITACION(idPublicacion, idServicioAud);
        }
    }
}
