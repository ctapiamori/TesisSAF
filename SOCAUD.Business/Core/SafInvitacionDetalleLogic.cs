using SOCAUD.Business.Infraestructure;
using SOCAUD.Data.Core;
using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Business.Core
{
    public interface ISafInvitacionDetalleLogic : IFacadeOperacionCRUD<SAF_INVITACIONDETALLE>
    {
        IEnumerable<SAF_INVITACIONDETALLE> ListarPorInvitacion(int idInvitacion);
        IEnumerable<TcFECHASINVITADAS> FechasInvitadas(int idEquipo);
    }

    public class SafInvitacionDetalleLogic : ISafInvitacionDetalleLogic
    {

        private readonly IUnitOfWork _uow;
        private readonly ISafInvitacionDetalleData _safInvitacionDetalleData;
        public SafInvitacionDetalleLogic()
        {
            this._uow = new UnitOfWork();
            this._safInvitacionDetalleData = new SafInvitacionDetalleData(_uow);
        }

        public SAF_INVITACIONDETALLE Registrar(SAF_INVITACIONDETALLE entidad)
        {
            return this._safInvitacionDetalleData.Add(entidad);
        }

        public SAF_INVITACIONDETALLE Actualizar(SAF_INVITACIONDETALLE entidad)
        {
            return this._safInvitacionDetalleData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try { this._safInvitacionDetalleData.Delete(id); return true; }
            catch (Exception) { return false; }
        }

        public SAF_INVITACIONDETALLE BuscarPorId(int id)
        {
            return this._safInvitacionDetalleData.GetById(id);
        }

        public IEnumerable<SAF_INVITACIONDETALLE> ListarTodos()
        {
            return this._safInvitacionDetalleData.GetAll();
        }

        public IEnumerable<SAF_INVITACIONDETALLE> ListarPorInvitacion(int idInvitacion)
        {
            return this._safInvitacionDetalleData.GetMany(c => c.CODINV == idInvitacion).ToList();
        }


        public IEnumerable<TcFECHASINVITADAS> FechasInvitadas(int idEquipo)
        {
            return this._safInvitacionDetalleData.FechasInvitadas(idEquipo);
        }
    }
}
