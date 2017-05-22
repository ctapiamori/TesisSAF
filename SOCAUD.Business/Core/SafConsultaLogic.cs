﻿using SOCAUD.Business.Infraestructure;
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

        void DeleteConsulta(int idCon);

        SAF_CONSULTA InsertConsulta(int idSoa, int idPub, string consulta);

        void SendConsulta(int idCon);
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

       public SAF_CONSULTA InsertConsulta(int idSoa, int idPub, string consulta)
       {
           var entidad = new SAF_CONSULTA();
           entidad.CODSOA = idSoa;
           entidad.CODPUB = idPub;
           entidad.DESCON = consulta;
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
    }
}
