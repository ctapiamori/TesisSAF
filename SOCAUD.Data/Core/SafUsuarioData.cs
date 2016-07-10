using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafUsuarioData : IBaseRepository<SAF_USUARIO> {
        TcACCEDERSISTEMAADMIN AccederSistema(string usuario, string contrasenia);
    }

    public class SafUsuarioData : BaseRepository<SAF_USUARIO>, ISafUsuarioData
    {
       private readonly IUnitOfWork _uow;
       public SafUsuarioData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }

       public TcACCEDERSISTEMAADMIN AccederSistema(string usuario, string contrasenia)
       {
           return this._uow.DataContext().SP_ACCEDERSISTEMAADMIN(usuario, contrasenia).FirstOrDefault();
       }
    }
}
