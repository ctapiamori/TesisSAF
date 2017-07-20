using SOCAUD.Business.Infraestructure;
using SOCAUD.Common.Enum;
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

    public interface ISafConsultaLogic
    {
        IEnumerable<SAF_CONSULTA> ListarConsultaPorPublicacionyUsuario(int codSOA, int codPub);
        IEnumerable<VW_SAF_CONSULTA> ListarConsultaPorPublicacion_Base_SOA(int codSOA, int? codPub, int? idBase);
        IEnumerable<VW_SAF_CONSULTA> ListarConsultaPorPublicacion_Base(int? codPub, int? idBase);

        VW_SAF_CONSULTA ObtenerConsultaCompleta(int idConsulta);

        IEnumerable<SAF_CONSULTA> ListarConsultaPorPublicacion(int codPub);

        void DeleteConsulta(int idCon);

        SAF_CONSULTA InsertConsulta(int idSoa, int idPub, int idBase, string consulta);

        void SendConsulta(int idCon);

        SAF_CONSULTA buscarPorId(int id);

        void GrabarRespuesta(int codigoRes, string Respuesta);
    }


    public class SafConsultaLogic : ISafConsultaLogic
    {
       private readonly IUnitOfWork _uow;
       private readonly ISafConsultaData _safConsultaData;

       public SafConsultaLogic()
        {
            this._uow = new UnitOfWork();
            this._safConsultaData = new SafConsultaData(_uow);
        }



       public IEnumerable<SAF_CONSULTA> ListarConsultaPorPublicacionyUsuario(int codSOA, int codPub)
       {
           var result = this._safConsultaData.GetMany(c => c.CODPUB == codPub && c.CODSOA == codSOA);
           return result.ToList();
       }


       public void DeleteConsulta(int idCon)
       {
           this._safConsultaData.Delete(idCon);
       }

       public SAF_CONSULTA InsertConsulta(int idSoa, int idPub, int idBase, string consulta)
       {
           var entidad = new SAF_CONSULTA();
           entidad.CODSOA = idSoa;
           entidad.CODPUB = idPub;
           entidad.DESCON = consulta;
           entidad.CODBAS = idBase;
           entidad.ESTCON = 9; // NO ENVIADO
           var result = this._safConsultaData.Add(entidad);
           return (SAF_CONSULTA)result;
       }


       public void SendConsulta(int idCon)
       {
           var consulta = this._safConsultaData.GetById(idCon);
           consulta.ESTCON = 10;
           this._safConsultaData.Update(consulta);
           
       }


       public IEnumerable<SAF_CONSULTA> ListarConsultaPorPublicacion(int codPub)
       {
           var result = this._safConsultaData.GetMany(c => c.CODPUB == codPub);
           return result.ToList();
       }


       public SAF_CONSULTA buscarPorId(int id)
       {
           return (SAF_CONSULTA)this._safConsultaData.GetById(id);
       }


       public void GrabarRespuesta(int codigoRes, string Respuesta)
       {
           var consulta = (SAF_CONSULTA)this._safConsultaData.GetById(codigoRes);
           consulta.RESCON = Respuesta;
           consulta.ESTCON = Estado.ConsultasPublicacion.Respondida.GetHashCode();
           this._safConsultaData.Update(consulta);
       }


       public IEnumerable<VW_SAF_CONSULTA> ListarConsultaPorPublicacion_Base_SOA(int codSOA, int? codPub, int? idBase)
       {
 
           var listado = this._safConsultaData.ListadoCompletoConsulta();

           if (codPub.HasValue && !idBase.HasValue)
               listado = listado.Where(c => c.CODPUB == codPub.Value && c.CODSOA == codSOA);
           if (!codPub.HasValue && idBase.HasValue)
               listado = listado.Where(c => c.CODBAS == idBase.Value && c.CODSOA == codSOA);
           if (codPub.HasValue && idBase.HasValue)
               listado = listado.Where(c => c.CODPUB == codPub.Value && c.CODSOA == codSOA && c.CODBAS == idBase.Value);
           return listado;
       }


       public IEnumerable<VW_SAF_CONSULTA> ListarConsultaPorPublicacion_Base(int? codPub, int? idBase)
       {
           var listado = this._safConsultaData.ListadoCompletoConsulta();

           if (codPub.HasValue && !idBase.HasValue)
               listado = listado.Where(c => c.CODPUB == codPub.Value);
           if (!codPub.HasValue && idBase.HasValue)
               listado = listado.Where(c => c.CODBAS == idBase.Value);
           if (codPub.HasValue && idBase.HasValue)
               listado = listado.Where(c => c.CODPUB == codPub.Value && c.CODBAS == idBase.Value);
           return listado;
       }


       public VW_SAF_CONSULTA ObtenerConsultaCompleta(int idConsulta)
       {
           var consulta = this._safConsultaData.ListadoCompletoConsulta().Where(c=>c.CODCON == idConsulta).FirstOrDefault();
           return consulta;
       }
    }
}
