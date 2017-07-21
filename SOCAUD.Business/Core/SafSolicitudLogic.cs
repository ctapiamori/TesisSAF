using SOCAUD.Business.Infraestructure;
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
    public interface ISafSolicitudLogic : IFacadeOperacionCRUD<SAF_SOLICITUD> {

        IEnumerable<VW_SAF_SOLICITUD> ListarSolicitudes();
        IEnumerable<VW_SAF_SOLICITUD> ListarSolicitudesNoElaborados();
        TcCREARSOLICITUDAUDITOR CrearSolicitudAuditor(int idAuditor);
        TcCREARSOLICITUDSOA CrearSolicitudSoa(int idSoa);
        bool GrabarSolicitudAuditor(SAF_SOLICITUD solicitud, SAF_AUDITOR auditor);
        bool GrabarSolicitudSoa(SAF_SOLICITUD solicitud, SAF_SOA soa);
        void ActualizarResafAuditor(SAF_SOLICITUD solicitud);
    }

    public class SafSolicitudLogic : ISafSolicitudLogic
    {
        private readonly IUnitOfWork _uow;
        private readonly ISafSolicitudData _safSolicitudData;
        private readonly IVwSafSolicitudData _viewSafSolicitudData;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafSolExperienciaLogic _solExperienciaLogic;
        private readonly ISafSolCapacitacionLogic _solCapacitacionLogic;
        private readonly ISafCapacitacionLogic _capacitacionLogic;
        private readonly ISafExperienciaLogic _experienciaLogic;

        private readonly ISpSafCorrelativoSolicitudData _spSafSolicitudCorrelativo;
        private readonly IDatabaseFactory _dataFactory;

        public SafSolicitudLogic()
        {
            this._uow = new UnitOfWork();
             this._dataFactory = new DatabaseFactory();
            this._safSolicitudData = new SafSolicitudData(_uow);
            this._viewSafSolicitudData = new VwSafSolicitudData(_uow);
            this._auditorLogic = new SafAuditorLogic();
            this._soaLogic = new SafSoaLogic();
            this._solExperienciaLogic = new SafSolExperienciaLogic();
            this._solCapacitacionLogic = new SafSolCapacitacionLogic();
            this._experienciaLogic = new SafExperienciaLogic();
            this._capacitacionLogic = new SafCapacitacionLogic();
            this._spSafSolicitudCorrelativo = new SpSafCorrelativoSolicitudData(_dataFactory, _uow);
        }

        public SAF_SOLICITUD Registrar(SAF_SOLICITUD entidad)
        {
            var result = _safSolicitudData.Add(entidad);
            return result;
        }

        public SAF_SOLICITUD Actualizar(SAF_SOLICITUD entidad)
        {
            var result = _safSolicitudData.Update(entidad);
            return result;
        }

        public SAF_SOLICITUD BuscarPorId(int id)
        {
            var result = _safSolicitudData.GetById(id);
            return result;
        }

        public IEnumerable<SAF_SOLICITUD> ListarTodos()
        {
            var result = _safSolicitudData.GetAll();
            return result;
        }


        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_SAF_SOLICITUD> ListarSolicitudes()
        {
            return this._viewSafSolicitudData.GetAll();
        }

        public IEnumerable<VW_SAF_SOLICITUD> ListarSolicitudesNoElaborados()
        {
            return this._viewSafSolicitudData.GetMany(c => c.ESTSOL != (int)Estado.Solicitud.Elaboracion).ToList();
        }


        public TcCREARSOLICITUDAUDITOR CrearSolicitudAuditor(int idAuditor)
        {
            return this._safSolicitudData.CrearSolicitudAuditor(idAuditor);
        }

        public TcCREARSOLICITUDSOA CrearSolicitudSoa(int idSoa)
        {
            return this._safSolicitudData.CrearSolicitudSoa(idSoa);
        }


        public bool GrabarSolicitudAuditor(SAF_SOLICITUD solicitud, SAF_AUDITOR auditor)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    var numeroSol = _spSafSolicitudCorrelativo.GenerarCorrelativo();
                    var resultRegAuditor = this._auditorLogic.Registrar(auditor);
                    solicitud.CODAUD = resultRegAuditor.CODAUD;
                    solicitud.NUMSOL = numeroSol;
                    var resultRegSolicitud = this.Registrar(solicitud);
                    tran.Complete();
                    return true;
                }
                catch (ExcepcionNegocio)
                {
                    tran.Dispose();
                    throw;
                }
                catch (Exception)
                {
                    tran.Dispose();
                    throw;
                }
            }
        }

        public bool GrabarSolicitudSoa(SAF_SOLICITUD solicitud, SAF_SOA soa)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    var numeroSol = _spSafSolicitudCorrelativo.GenerarCorrelativo();
                    var resultRegSoa = this._soaLogic.Registrar(soa);
                    solicitud.CODSOA = resultRegSoa.CODSOA;
                    solicitud.NUMSOL = numeroSol;
                    var resultRegSolicitud = this.Registrar(solicitud);
                    tran.Complete();
                    return true;
                }
                catch (ExcepcionNegocio)
                {
                    tran.Dispose();
                    throw;
                }
                catch (Exception)
                {
                    tran.Dispose();
                    throw;
                }
            }
        }

        public void ActualizarResafAuditor(SAF_SOLICITUD solicitud)
        {
            var infoSolicitud = this.BuscarPorId(solicitud.CODSOL); //modelEntity.SAF_SOLICITUD.Where(c => c.CODSOL == solicitud.CODSOL).FirstOrDefault();

            var capacSol = this._solCapacitacionLogic.ListarPorSolicitud(solicitud.CODSOL);// modelEntity.SAF_SOLCAPACITACION.Where(x => x.CODSOL == solicitud.CODSOL);
            var expSol = this._solExperienciaLogic.ListarPorSolicitud(solicitud.CODSOL);// modelEntity.SAF_SOLEXPERIENCIA.Where(x => x.CODSOL == solicitud.CODSOL);
            var capacResaf = this._capacitacionLogic.ListarPorAuditor(solicitud.CODAUD.GetValueOrDefault());// modelEntity.SAF_CAPACITACION.Where(x => x.CODAUD == solicitud.CODAUD);
            var expResaf = this._experienciaLogic.ListarPorAuditor(solicitud.CODAUD.GetValueOrDefault());// modelEntity.SAF_EXPERIENCIA.Where(x => x.CODAUD == solicitud.CODAUD);

            using (var scope = new TransactionScope())
            {                

                foreach (var item in capacResaf)
                {
                    //modelEntity.SAF_CAPACITACION.Remove(item);
                    //modelEntity.SaveChanges();
                    this._capacitacionLogic.Eliminar(item.CODCAP);
                }
                foreach (var item in expResaf)
                {
                    //modelEntity.SAF_EXPERIENCIA.Remove(item);
                    //modelEntity.SaveChanges();
                    this._experienciaLogic.Eliminar(item.CODEXP);
                }

                foreach (var item in capacSol)
                {
                    var resaf = new SAF_CAPACITACION
                    {
                        CODAUD = solicitud.CODAUD,
                        DESCAP = item.DESSOLCAP,
                        FECINICAP = item.FECINISOLCAP,
                        FECFINCAP = item.FECFINSOLCAP,
                        NUMHORCAP = item.NUMHORSOLCAP,
                        FECREG = DateTime.Now,
                        USUREG = "SYSTEM",
                        ESTREG = "1",
                        CODUNI = item.CODUNI,
                        CODCAR = item.CODCAR,
                        CODTIPCAPA = item.CODTIPCAPA,
                        CODCATCAPA = item.CODCATCAPA,
                        CODARC = item.CODARC,
                        NOMBLABEL = item.NOMBLABEL
                    };
                    //modelEntity.SAF_CAPACITACION.Add(resaf);
                    //modelEntity.SaveChanges();
                    this._capacitacionLogic.Registrar(resaf);
                }
                foreach (var item in expSol)
                {
                    var resaf = new SAF_EXPERIENCIA
                    {
                        CODAUD = solicitud.CODAUD,
                        DESEXP = item.DESSOLEXP,
                        FECINIEXP = item.FECINISOLEXP,
                        FECFINEXP = item.FECFINSOLEXP,
                        NUMHOREXP = item.NUMHORSOLEXP,
                        FECREG = DateTime.Now,
                        USUREG = "SYSTEM",
                        ESTREG = "1",
                        CODEMP = item.CODEMP,
                        CODTIPEXP = item.CODTIPEXP,
                        CODARC = item.CODARC,
                        NOMBLABEL = item.NOMBLABEL
                    };
                    //modelEntity.SAF_EXPERIENCIA.Add(resaf);
                    //modelEntity.SaveChanges();
                    this._experienciaLogic.Registrar(resaf);
                }

                infoSolicitud.ESTSOL = (int)Estado.Solicitud.Aprobado;
                infoSolicitud.OBSSOL = "La solicitud del Auditor ha sido <strong>APROBADO</strong>. <br />Bienvenido al Sistema de Sociedades y Auditores!!!!";
                //modelEntity.SaveChanges();
                this.Actualizar(infoSolicitud);
                scope.Complete();
            }

        }
    }
}
