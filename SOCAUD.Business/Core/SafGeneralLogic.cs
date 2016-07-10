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
    public interface ISafGeneralLogic
    {
        IEnumerable<SAF_DEPARTAMENTO> ListarDepartamentos();
        IEnumerable<SAF_PROVINCIA> ListarProvincias(int departamentoId);
        IEnumerable<SAF_PROVINCIA> ListarProvincias();
        IEnumerable<SAF_DISTRITO> ListarDistritos(int provinciaId);
        IEnumerable<SAF_DISTRITO> ListarDistritos();
        IEnumerable<SAF_CARRERA> ListarCarreras();
        IEnumerable<SAF_UNIVERSIDAD> ListarUniversidades();
        IEnumerable<SAF_PARAMETRICA> ListarParametricas(int tipo);
        IEnumerable<SAF_EMPRESA> ListarEmpresas();
        IEnumerable<SAF_CARGO> ListarCargos();
        IEnumerable<SAF_TIPOSOLICITUD> ListarTipoSolicitud();
        IEnumerable<SAF_TIPOSOLICITUD> ListarRegistroTipoSolicitud();
        SAF_CARGO GetCargo(int idCargo);
    }

    public class SafGeneralLogic : ISafGeneralLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafDepartamentoData _safDepartamentoData;
        private readonly ISafProvinciaData _safProvinciaData;
        private readonly ISafDistritoData _safDistritoData;
        private readonly ISafCarreraData _safCarreraData;
        private readonly ISafUniversidadData _safUniversidadData;
        private readonly ISafParametricaData _safParametricaData;
        private readonly ISafEmpresaData _safEmpresaData;
        private readonly ISafCargoData _safCargoData;
        private readonly ISafTipoSolicitudData _safTiposolicitudData;

        public SafGeneralLogic()
        {
            this._uow = new UnitOfWork();
            this._safDepartamentoData = new SafDepartamentoData(_uow);
            this._safProvinciaData = new SafProvinciaData(_uow);
            this._safDistritoData = new SafDistritoData(_uow);
            this._safCarreraData = new SafCarreraData(_uow);
            this._safUniversidadData = new SafUniversidadData(_uow);
            this._safParametricaData = new SafParametricaData(_uow);
            this._safEmpresaData = new SafEmpresaData(_uow);
            this._safCargoData = new SafCargoData(_uow);
            this._safTiposolicitudData = new SafTipoSolicitudData(_uow);
        }

        public IEnumerable<SAF_DEPARTAMENTO> ListarDepartamentos()
        {
            return this._safDepartamentoData.GetAll();
        }

        public IEnumerable<SAF_PROVINCIA> ListarProvincias(int departamentoId)
        {
            return this._safProvinciaData.GetMany(c=> c.CODDEP == departamentoId).ToList();
        }

        public IEnumerable<SAF_PROVINCIA> ListarProvincias()
        {
            return this._safProvinciaData.GetAll();
        }

        public IEnumerable<SAF_DISTRITO> ListarDistritos(int provinciaId)
        {
            return this._safDistritoData.GetMany(c => c.CODPROV == provinciaId).ToList();
        }

        public IEnumerable<SAF_DISTRITO> ListarDistritos()
        {
            return this._safDistritoData.GetAll();
        }

        public IEnumerable<SAF_CARRERA> ListarCarreras()
        {
            return this._safCarreraData.GetAll();
        }


        public IEnumerable<SAF_UNIVERSIDAD> ListarUniversidades()
        {
            return this._safUniversidadData.GetAll();
        }


        public IEnumerable<SAF_PARAMETRICA> ListarParametricas(int tipo)
        {
            return this._safParametricaData.GetMany(x => x.CODTIPPAR == tipo).ToList();
        }


        public IEnumerable<SAF_EMPRESA> ListarEmpresas()
        {
            return this._safEmpresaData.GetAll();
        }


        public IEnumerable<SAF_CARGO> ListarCargos()
        {
            return this._safCargoData.GetAll();
        }

        public SAF_CARGO GetCargo(int idCargo)
        {
            return this._safCargoData.GetById(idCargo);
        }


        public IEnumerable<SAF_TIPOSOLICITUD> ListarTipoSolicitud()
        {
            return this._safTiposolicitudData.GetAll();
        }

        public IEnumerable<SAF_TIPOSOLICITUD> ListarRegistroTipoSolicitud()
        {
            return this._safTiposolicitudData.GetMany(c => c.CODTIPSOL >= 1 && c.CODTIPSOL <= 2).ToList();
        }
    }
}
