using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafSolicitudData : IBaseRepository<SAF_SOLICITUD> {
        TcCREARSOLICITUDAUDITOR CrearSolicitudAuditor(int idAuditor);
        TcCREARSOLICITUDSOA CrearSolicitudSoa(int idSoa);
    }

    public class SafSolicitudData : BaseRepository<SAF_SOLICITUD>, ISafSolicitudData
    {
        private readonly IUnitOfWork _uow;
        public SafSolicitudData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

        public TcCREARSOLICITUDAUDITOR CrearSolicitudAuditor(int idAuditor)
        {
            return this._uow.DataContext().SP_SAF_CREARSOLICITUDAUDITOR(idAuditor).First();
        }

        public TcCREARSOLICITUDSOA CrearSolicitudSoa(int idSoa)
        {
            return this._uow.DataContext().SP_SAF_CREARSOLICITUDSOA(idSoa).First();
        }
    }
}
