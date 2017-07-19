using SOCAUD.Common.Enum;
using SOCAUD.Data.Model;
using SOCAUD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Data.Core
{
    public interface ISafPropuestaData : IBaseRepository<SAF_PROPUESTA>
    {
        TcCREARPROPUESTA CrearPropuesta(int idPublicacion, int idBase, int idSoa);
        IEnumerable<TcPROPUESTAS> ListarPropuestas(int? idPublicacion, int? idBase, int? idSoa);
        TcPROPUESTAS PropuestaPorId(int idPropuesta);
        IEnumerable<TcPROPUESTAS> ListadoPropuestasCalificar(int? idPublicacion);
        TcASIGNARGANADORPROPUESTA AsignarGanadorPropuesta(int idPropuesta, int idPublicacion);
        IEnumerable<TcPROPUESTARPT> ListarPropuestasRpt(int idPropuesta);

        IEnumerable<SP_SAF_EQUIPO_PROPUESTA_Result> ListarEquipoPropuesta(int idPropuesta);

        IEnumerable<VW_SAF_PROPUESTAEJECUCION> ListarPropuestaEjecucion();
    }

    public class SafPropuestaData : BaseRepository<SAF_PROPUESTA>, ISafPropuestaData
    {
        private readonly IUnitOfWork _uow;

        public SafPropuestaData(IUnitOfWork uow)
            : base(uow)
        {
            this._uow = uow;
        }


        public TcCREARPROPUESTA CrearPropuesta(int idPublicacion, int idBase, int idSoa)
        {
            return this._uow.DataContext().SP_SAF_CREARPROPUESTA1(idPublicacion, idBase, idSoa).FirstOrDefault();
        }


        public IEnumerable<TcPROPUESTAS> ListarPropuestas(int? idPublicacion, int? idBase, int? idSoa)
        {
            var propuestas = this._uow.DataContext().SP_SAF_PROPUESTAS().ToList();
            if (idPublicacion.HasValue && !idBase.HasValue)
                return propuestas.Where(c => c.CODPUB == idPublicacion.Value && c.CODSOA == idSoa.Value);
            else if (idPublicacion.HasValue && idBase.HasValue)
                return propuestas.Where(c => c.CODPUB == idPublicacion.Value && c.CODBAS == idBase.Value && c.CODSOA == idSoa.Value);
            else
                return propuestas.Where(c => c.CODSOA == idSoa.Value);
        }


        public TcPROPUESTAS PropuestaPorId(int idPropuesta)
        {
            var propuestas = this._uow.DataContext().SP_SAF_PROPUESTAS().ToList();
            return propuestas.First(c => c.CODPRO == idPropuesta);
        }


        public IEnumerable<TcPROPUESTAS> ListadoPropuestasCalificar(int? idPublicacion)
        {
            var propuestas = this._uow.DataContext().SP_SAF_PROPUESTAS().ToList();
            return propuestas.Where(c => (c.CODPUB == idPublicacion || idPublicacion == null) && (c.ESTPROP == (int)Estado.Propuesta.Enviada || c.ESTPROP == (int)Estado.Propuesta.Ganadora || c.ESTPROP == (int)Estado.Propuesta.Descalifica));
        }


        public TcASIGNARGANADORPROPUESTA AsignarGanadorPropuesta(int idPropuesta, int idPublicacion)
        {
            return this._uow.DataContext().SP_SAF_ASIGNARGANADORPROPUESTA(idPropuesta, idPublicacion).FirstOrDefault();
        }

        public IEnumerable<TcPROPUESTARPT> ListarPropuestasRpt(int idPropuesta)
        {
            return this._uow.DataContext().SP_SAF_PROPUESTA_RPT(idPropuesta).ToList();
        }


        public IEnumerable<SP_SAF_EQUIPO_PROPUESTA_Result> ListarEquipoPropuesta(int idPropuesta)
        {
            return this._uow.DataContext().SP_SAF_EQUIPO_PROPUESTA1(idPropuesta).ToList();
        }


        public IEnumerable<VW_SAF_PROPUESTAEJECUCION> ListarPropuestaEjecucion()
        {
            return this._uow.DataContext().VW_SAF_PROPUESTAEJECUCION.ToList();
        }
    }
}
