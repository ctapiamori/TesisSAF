using SOCAUD.Business.Infraestructure;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Common.Exceptions;
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
    public interface ISafWorkFlowLogic : IFacadeOperacionCRUD<SAF_WORKFLOW>
    {
        SAF_WORKFLOW FlujoSolicitud(SAF_WORKFLOW entidad);
        SAF_WORKFLOW FlujoAprobacion(SAF_WORKFLOW entidad, int IdFlujo, int tipoUsuario);
        SAF_WORKFLOW FlujoRechazo(SAF_WORKFLOW entidad, int IdFlujo, int tipoUsuario);
        IEnumerable<SAF_WORKFLOW> ListarPorUsuario(int idUsuario);
        IEnumerable<SAF_WORKFLOW> ListarPorTipoUsuario(int idTipoUsuario);
        IEnumerable<SAF_WORKFLOW> ListarPorUsuarioAndTipoUsuario(int idUsuario, int idTipoUsuario);
        IEnumerable<SAF_WORKFLOW> ListarPorDocumento(int idDocumento);

    }

    public class SafWorkFlowLogic : ISafWorkFlowLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafWorkFlowData _safWorkFlowData;
        private readonly ISafCronogramaData _safCronogramaData;
        private readonly ISafBaseData _safBaseData;
        private readonly ISafPublicacionData _safPublicacionData;
        
        public SafWorkFlowLogic()
        {
            this._uow = new UnitOfWork();
            this._safWorkFlowData = new SafWorkFlowData(this._uow);
            this._safCronogramaData = new SafCronogramaData(this._uow);
            this._safBaseData = new SafBaseData(this._uow);
            this._safPublicacionData = new SafPublicacionData(this._uow);
        }

        public SAF_WORKFLOW Registrar(SAF_WORKFLOW entidad)
        {
            var result = _safWorkFlowData.Add(entidad);
            return result;
        }

        public SAF_WORKFLOW FlujoSolicitud(SAF_WORKFLOW entidad)
        {
            entidad.ESTWORFLO = Estado.Workflow.PendienteAprobacion.GetHashCode();

            if (entidad.TIPDOC.Equals(Variables.CRONOGRAMA_ANUAL_ENTIDADES))
            {
                var cronograma = this._safCronogramaData.GetById(entidad.CODDOC);
                cronograma.ESTCRO = Estado.Cronograma.PendienteAprobacion.GetHashCode();
                this._safCronogramaData.Update(cronograma);
            }

            if (entidad.TIPDOC.Equals(Variables.BASES_CONCURSO))
            {
                var bases = this._safBaseData.GetById(entidad.CODDOC);
                bases.ESTBAS = Estado.Bases.PendienteAprobacion.GetHashCode();
                this._safBaseData.Update(bases);
            }

            if (entidad.TIPDOC.Equals(Variables.PUBLICACION_CONCURSO))
            {
                var publicacion = this._safPublicacionData.GetById(entidad.CODDOC);
                publicacion.ESTPUB = Estado.Publicacion.PendienteAprobacion.GetHashCode();
                this._safPublicacionData.Update(publicacion);
            }

            entidad.FLGNOTREP = "0";

            var result = this.Registrar(entidad);

            return result;

        }

        public SAF_WORKFLOW FlujoAprobacion(SAF_WORKFLOW entidad, int IdFlujo, int tipoUsuario)
        {
            entidad.ESTWORFLO = Estado.Workflow.PendienteAprobacion.GetHashCode();
            var flagFlujo = "0";

            if (tipoUsuario.Equals(TipoUsuario.Gerente.GetHashCode()))
            {
                flagFlujo = "1";

                if (entidad.TIPDOC.Equals(Variables.CRONOGRAMA_ANUAL_ENTIDADES))
                {
                    var cronograma = this._safCronogramaData.GetById(entidad.CODDOC);
                    cronograma.ESTCRO = Estado.Cronograma.Aprobado.GetHashCode();
                    this._safCronogramaData.Update(cronograma);
                }

                if (entidad.TIPDOC.Equals(Variables.BASES_CONCURSO))
                {
                    var bases = this._safBaseData.GetById(entidad.CODDOC);
                    bases.ESTBAS = Estado.Bases.Aprobado.GetHashCode();
                    this._safBaseData.Update(bases);
                }

                if (entidad.TIPDOC.Equals(Variables.PUBLICACION_CONCURSO))
                {
                    var publicacion = this._safPublicacionData.GetById(entidad.CODDOC);
                    publicacion.ESTPUB = Estado.Publicacion.Aprobado.GetHashCode();
                    this._safPublicacionData.Update(publicacion);
                }
            }

            entidad.FLGNOTREP = flagFlujo;

            var result = this.Registrar(entidad);

            var flujo = this.BuscarPorId(IdFlujo);
            flujo.FLGNOTREP = "1";
            this.Actualizar(flujo);

            return result;

        }

        public SAF_WORKFLOW FlujoRechazo(SAF_WORKFLOW entidad, int IdFlujo, int tipoUsuario)
        {
            entidad.ESTWORFLO = Estado.Workflow.PendienteAprobacion.GetHashCode();
            var flagFlujo = "0";

            if (entidad.TIPCARUSU.Equals(TipoUsuario.Operador.GetHashCode()))
            {
                flagFlujo = "1";

                if (entidad.TIPDOC.Equals(Variables.CRONOGRAMA_ANUAL_ENTIDADES))
                {
                    var cronograma = this._safCronogramaData.GetById(entidad.CODDOC);
                    cronograma.ESTCRO = Estado.Cronograma.Elaboracion.GetHashCode();
                    this._safCronogramaData.Update(cronograma);
                }

                if (entidad.TIPDOC.Equals(Variables.BASES_CONCURSO))
                {
                    var bases = this._safBaseData.GetById(entidad.CODDOC);
                    bases.ESTBAS = Estado.Bases.Elaboracion.GetHashCode();
                    this._safBaseData.Update(bases);
                }

                if (entidad.TIPDOC.Equals(Variables.PUBLICACION_CONCURSO))
                {
                    var publicacion = this._safPublicacionData.GetById(entidad.CODDOC);
                    publicacion.ESTPUB = Estado.Publicacion.Elaboracion.GetHashCode();
                    this._safPublicacionData.Update(publicacion);
                }
            }

            entidad.FLGNOTREP = flagFlujo;
            var result = this.Registrar(entidad);

            var flujo = this.BuscarPorId(IdFlujo);
            flujo.FLGNOTREP = "1";
            this.Actualizar(flujo);

            return result;

        }

        public SAF_WORKFLOW Actualizar(SAF_WORKFLOW entidad)
        {
            var result = _safWorkFlowData.Update(entidad);

            return result;
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public SAF_WORKFLOW BuscarPorId(int id)
        {
            var result = _safWorkFlowData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_WORKFLOW> ListarTodos()
        {
            var result = _safWorkFlowData.GetAll();
            return result;
        }

        public IEnumerable<SAF_WORKFLOW> ListarPorUsuario(int idUsuario)
        {
            var result = _safWorkFlowData.GetMany(c=> c.CODUSUSOL == idUsuario);
            return result;
        }

        public IEnumerable<SAF_WORKFLOW> ListarPorTipoUsuario(int idTipoUsuario)
        {
            var result = _safWorkFlowData.GetMany(c=> c.TIPCARUSU == idTipoUsuario);
            return result;
        }

        public IEnumerable<SAF_WORKFLOW> ListarPorUsuarioAndTipoUsuario(int idUsuario, int idTipoUsuario)
        {
            var result = _safWorkFlowData.GetMany(c=> c.CODUSUSOL == idUsuario || c.TIPCARUSU == idTipoUsuario);
            return result;
        }

        public IEnumerable<SAF_WORKFLOW> ListarPorDocumento(int idDocumento)
        {
            var result = _safWorkFlowData.GetMany(c => c.CODDOC == idDocumento);
            return result;
        }
    }
}
