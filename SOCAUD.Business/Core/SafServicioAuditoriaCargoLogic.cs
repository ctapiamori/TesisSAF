using SOCAUD.Business.Infraestructure;
using SOCAUD.Data.Core;
using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SOCAUD.Business.Core
{
    public interface ISafServicioAuditoriaCargoLogic : IFacadeOperacionCRUD<SAF_SERAUDCARGO>
    {
        SAF_SERAUDCARGO BuscarPorServicioCargo(int idServicioAuditoria, int idCargo);
        void RegistrarCargoCompleto(SAF_SERAUDCARGO cargo, SAF_SERAUDCAREXP experiencia, SAF_SERAUDCARCAP capacitacion);
        void ActualizarCargoCompleto(SAF_SERAUDCARGO cargo, SAF_SERAUDCAREXP experiencia, SAF_SERAUDCARCAP capacitacion);
        bool EliminarCompleto(int id);
    }

    public class SafServicioAuditoriaCargoLogic : ISafServicioAuditoriaCargoLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafServicioAuditoriaCargoData _safServicioAuditoriaCargoData;
        private readonly ISafServAudCargoExperienciaLogic _safServAudCargoExperienciaLogic;
        private readonly ISafServAudCargoCapacitacionLogic _safServAudCargoCapacitacionLogic;

        public SafServicioAuditoriaCargoLogic()
        {
            this._uow = new UnitOfWork();
            this._safServicioAuditoriaCargoData = new SafServicioAuditoriaCargoData(_uow);
            this._safServAudCargoCapacitacionLogic = new SafServAudCargoCapacitacionLogic();
            this._safServAudCargoExperienciaLogic = new SafServAudCargoExperienciaLogic();

        }

        public SAF_SERAUDCARGO Registrar(SAF_SERAUDCARGO entidad)
        {
            return this._safServicioAuditoriaCargoData.Add(entidad);
        }

        public SAF_SERAUDCARGO Actualizar(SAF_SERAUDCARGO entidad)
        {
            return this._safServicioAuditoriaCargoData.Update(entidad);
        }

        public bool Eliminar(int id)
        {
            try
            {
                this._safServicioAuditoriaCargoData.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SAF_SERAUDCARGO BuscarPorId(int id)
        {
            return this._safServicioAuditoriaCargoData.GetById(id);
        }

        public SAF_SERAUDCARGO BuscarPorServicioCargo(int idServicioAuditoria, int idCargo)
        {
            return this._safServicioAuditoriaCargoData.GetMany(c => c.CODSERAUD == idServicioAuditoria && c.CODCAR == idCargo).FirstOrDefault();
        }

        public IEnumerable<SAF_SERAUDCARGO> ListarTodos()
        {
            return this._safServicioAuditoriaCargoData.GetAll();
        }


        public void RegistrarCargoCompleto(SAF_SERAUDCARGO cargo, SAF_SERAUDCAREXP experiencia, SAF_SERAUDCARCAP capacitacion)
        {
            try
            {
                using(var scope = new TransactionScope())
                {
                    cargo = this.Registrar(cargo);

                    experiencia.CODSERAUDCAR = cargo.CODSERAUDCAR;
                    capacitacion.CODSERAUDCAR = cargo.CODSERAUDCAR;

                    experiencia = this._safServAudCargoExperienciaLogic.Registrar(experiencia);

                    capacitacion = this._safServAudCargoCapacitacionLogic.Registrar(capacitacion);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ActualizarCargoCompleto(SAF_SERAUDCARGO cargo, SAF_SERAUDCAREXP experiencia, SAF_SERAUDCARCAP capacitacion)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var cargoUp = this.BuscarPorId(cargo.CODSERAUDCAR);
                    cargoUp.NUMMININTSERAUDCAR = cargo.NUMMININTSERAUDCAR;
                    cargoUp.NUMMINHORPARSERAUDCAR = cargo.NUMMINHORPARSERAUDCAR;
                    
                    this.Actualizar(cargoUp);

                    var experienciaUp = this._safServAudCargoExperienciaLogic.BuscarPorId(experiencia.CODSERAUDCAREXP);
                    experienciaUp.NUMMINHORSERAUDCAREXP = experiencia.NUMMINHORSERAUDCAREXP;

                    var capacitacionUp = this._safServAudCargoCapacitacionLogic.BuscarPorId(capacitacion.CODSERAUDCARCAP);
                    capacitacionUp.NUMMINHORSERAUDCAPCAP = capacitacion.NUMMINHORSERAUDCAPCAP;


                    this._safServAudCargoExperienciaLogic.Actualizar(experienciaUp);

                    this._safServAudCargoCapacitacionLogic.Actualizar(capacitacionUp);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool EliminarCompleto(int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var experiencia = this._safServAudCargoExperienciaLogic.BuscarPorServicioCargo(id);

                    if (this._safServAudCargoExperienciaLogic.Eliminar(experiencia.CODSERAUDCAREXP)) return false;

                    var capacitacion = this._safServAudCargoCapacitacionLogic.BuscarPorServicioCargo(id);

                    if (this._safServAudCargoCapacitacionLogic.Eliminar(capacitacion.CODSERAUDCARCAP)) return false;

                    if(this.Eliminar(id)) return false;

                    scope.Complete();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
