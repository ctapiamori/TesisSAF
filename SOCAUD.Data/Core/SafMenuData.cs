using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOCAUD.Data.Core
{

    public interface ISafMenuData : IBaseRepository<SAF_MENU> {
        IEnumerable<TcMENUPORPERFIL> ObtenerMenuPorPerfil(int idPerfil);
        IEnumerable<TcSUBMENUPORMENU> ObtenerSubMenuPorMenu(int idMenu);
    }
    public class SafMenuData : BaseRepository<SAF_MENU>, ISafMenuData
    {

        private readonly IUnitOfWork _uow;


        public SafMenuData(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public IEnumerable<TcMENUPORPERFIL> ObtenerMenuPorPerfil(int idPerfil)
        {
            return this._uow.DataContext().SP_SAF_MENUPORPERFIL(idPerfil);
        }


        public IEnumerable<TcSUBMENUPORMENU> ObtenerSubMenuPorMenu(int idMenu)
        {
            return this._uow.DataContext().SP_SAF_SUBMENUPORMENU(idMenu);
        }
    }
}
