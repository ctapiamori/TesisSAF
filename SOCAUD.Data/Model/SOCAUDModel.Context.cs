﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOCAUD.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SOCAUDEntities : DbContext
    {
        public SOCAUDEntities()
            : base("name=SOCAUDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SAF_ARCHIVO> SAF_ARCHIVO { get; set; }
        public virtual DbSet<SAF_AUDITOR> SAF_AUDITOR { get; set; }
        public virtual DbSet<SAF_AUDITORIA> SAF_AUDITORIA { get; set; }
        public virtual DbSet<SAF_BASE> SAF_BASE { get; set; }
        public virtual DbSet<SAF_BASEENTREGABLE> SAF_BASEENTREGABLE { get; set; }
        public virtual DbSet<SAF_CAPACITACION> SAF_CAPACITACION { get; set; }
        public virtual DbSet<SAF_CARGO> SAF_CARGO { get; set; }
        public virtual DbSet<SAF_CARRERA> SAF_CARRERA { get; set; }
        public virtual DbSet<SAF_CORTE_AUDITOR> SAF_CORTE_AUDITOR { get; set; }
        public virtual DbSet<SAF_CORTE_AUDITOR_CARGO> SAF_CORTE_AUDITOR_CARGO { get; set; }
        public virtual DbSet<SAF_CRONOENTIDAD> SAF_CRONOENTIDAD { get; set; }
        public virtual DbSet<SAF_CRONOGRAMA> SAF_CRONOGRAMA { get; set; }
        public virtual DbSet<SAF_DEPARTAMENTO> SAF_DEPARTAMENTO { get; set; }
        public virtual DbSet<SAF_DIAOCUPADO> SAF_DIAOCUPADO { get; set; }
        public virtual DbSet<SAF_DISTRITO> SAF_DISTRITO { get; set; }
        public virtual DbSet<SAF_DOCUMENTO> SAF_DOCUMENTO { get; set; }
        public virtual DbSet<SAF_EMPRESA> SAF_EMPRESA { get; set; }
        public virtual DbSet<SAF_EXPERIENCIA> SAF_EXPERIENCIA { get; set; }
        public virtual DbSet<SAF_FLUJO_DOCUMENTO> SAF_FLUJO_DOCUMENTO { get; set; }
        public virtual DbSet<SAF_INVITACION> SAF_INVITACION { get; set; }
        public virtual DbSet<SAF_INVITACIONDETALLE> SAF_INVITACIONDETALLE { get; set; }
        public virtual DbSet<SAF_NOTIFICACION> SAF_NOTIFICACION { get; set; }
        public virtual DbSet<SAF_PARAMETRICA> SAF_PARAMETRICA { get; set; }
        public virtual DbSet<SAF_PROPEQUIPO> SAF_PROPEQUIPO { get; set; }
        public virtual DbSet<SAF_PROPEQUIPODETALLE> SAF_PROPEQUIPODETALLE { get; set; }
        public virtual DbSet<SAF_PROPUESTA> SAF_PROPUESTA { get; set; }
        public virtual DbSet<SAF_PROVINCIA> SAF_PROVINCIA { get; set; }
        public virtual DbSet<SAF_PUBLICACION> SAF_PUBLICACION { get; set; }
        public virtual DbSet<SAF_SERAUDCARCAP> SAF_SERAUDCARCAP { get; set; }
        public virtual DbSet<SAF_SERAUDCAREXP> SAF_SERAUDCAREXP { get; set; }
        public virtual DbSet<SAF_SERAUDCARGO> SAF_SERAUDCARGO { get; set; }
        public virtual DbSet<SAF_SERVICIOAUDITORIA> SAF_SERVICIOAUDITORIA { get; set; }
        public virtual DbSet<SAF_SOA> SAF_SOA { get; set; }
        public virtual DbSet<SAF_SOLCAPACITACION> SAF_SOLCAPACITACION { get; set; }
        public virtual DbSet<SAF_SOLEXPERIENCIA> SAF_SOLEXPERIENCIA { get; set; }
        public virtual DbSet<SAF_SOLICITUD> SAF_SOLICITUD { get; set; }
        public virtual DbSet<SAF_TIPOPARAMETRICA> SAF_TIPOPARAMETRICA { get; set; }
        public virtual DbSet<SAF_TIPOSOLICITUD> SAF_TIPOSOLICITUD { get; set; }
        public virtual DbSet<SAF_UNIVERSIDAD> SAF_UNIVERSIDAD { get; set; }
        public virtual DbSet<SAF_DIALABORABLE> SAF_DIALABORABLE { get; set; }
        public virtual DbSet<VW_SAF_INVITACION> VW_SAF_INVITACION { get; set; }
        public virtual DbSet<VW_SAF_PUBLICACION> VW_SAF_PUBLICACION { get; set; }
        public virtual DbSet<VW_SAF_SOLICITUD> VW_SAF_SOLICITUD { get; set; }
        public virtual DbSet<SAF_ASISTENCIA> SAF_ASISTENCIA { get; set; }
        public virtual DbSet<SAF_FALTAJUSTIFICA> SAF_FALTAJUSTIFICA { get; set; }
        public virtual DbSet<VW_SAF_AUDITORIAEQUIPO> VW_SAF_AUDITORIAEQUIPO { get; set; }
        public virtual DbSet<SAF_PUBLICACIONBASE> SAF_PUBLICACIONBASE { get; set; }
        public virtual DbSet<SAF_WORKFLOW> SAF_WORKFLOW { get; set; }
        public virtual DbSet<SAF_ENTIDADES> SAF_ENTIDADES { get; set; }
        public virtual DbSet<SAF_CONSULTA> SAF_CONSULTA { get; set; }
        public virtual DbSet<VW_SAF_CARGOSENSERVICIO> VW_SAF_CARGOSENSERVICIO { get; set; }
        public virtual DbSet<SAF_PERFIL> SAF_PERFIL { get; set; }
        public virtual DbSet<SAF_PERFIL_MENU> SAF_PERFIL_MENU { get; set; }
        public virtual DbSet<SAF_SUBMENU> SAF_SUBMENU { get; set; }
        public virtual DbSet<VW_SAF_SUBMENU> VW_SAF_SUBMENU { get; set; }
        public virtual DbSet<VW_SAF_PERFILMENU> VW_SAF_PERFILMENU { get; set; }
        public virtual DbSet<VW_SAF_CRONOGRAMA> VW_SAF_CRONOGRAMA { get; set; }
        public virtual DbSet<VW_SAF_PUBLICACION_BASE> VW_SAF_PUBLICACION_BASE { get; set; }
        public virtual DbSet<VW_SAF_USUARIOS> VW_SAF_USUARIOS { get; set; }
        public virtual DbSet<VW_SAF_PARAMETRICA> VW_SAF_PARAMETRICA { get; set; }
        public virtual DbSet<VW_SAF_WORKFLOW> VW_SAF_WORKFLOW { get; set; }
        public virtual DbSet<SAF_USUARIO> SAF_USUARIO { get; set; }
        public virtual DbSet<VW_SAF_PUBLICACIONBASE> VW_SAF_PUBLICACIONBASE { get; set; }
        public virtual DbSet<SAF_MENU> SAF_MENU { get; set; }
        public virtual DbSet<VW_SAF_WORKFLOW_POR_USU> VW_SAF_WORKFLOW_POR_USU { get; set; }
        public virtual DbSet<VW_SAF_PROPUESTAEJECUCION> VW_SAF_PROPUESTAEJECUCION { get; set; }
    
        public virtual ObjectResult<TcCORTEPUBLICACION> SP_SAF_CORTEPUBLICACION(Nullable<int> cODPUB)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcCORTEPUBLICACION>("SP_SAF_CORTEPUBLICACION", cODPUBParameter);
        }
    
        public virtual ObjectResult<TcINVITACION> SP_SAF_INVITACION(Nullable<int> cODPUB, Nullable<int> cODSERAUD)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            var cODSERAUDParameter = cODSERAUD.HasValue ?
                new ObjectParameter("CODSERAUD", cODSERAUD) :
                new ObjectParameter("CODSERAUD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcINVITACION>("SP_SAF_INVITACION", cODPUBParameter, cODSERAUDParameter);
        }
    
        public virtual ObjectResult<TcMEJOREQUIPO> SP_SAF_MEJOREQUIPO(Nullable<int> cODPUB, Nullable<int> cODSERAUD)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            var cODSERAUDParameter = cODSERAUD.HasValue ?
                new ObjectParameter("CODSERAUD", cODSERAUD) :
                new ObjectParameter("CODSERAUD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcMEJOREQUIPO>("SP_SAF_MEJOREQUIPO", cODPUBParameter, cODSERAUDParameter);
        }
    
        public virtual ObjectResult<TcRESULTADOCORTE> SP_SAF_RESULTADOCORTE(Nullable<int> cODPUB)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcRESULTADOCORTE>("SP_SAF_RESULTADOCORTE", cODPUBParameter);
        }
    
        public virtual ObjectResult<TcCREARSOLICITUDAUDITOR> SP_SAF_CREARSOLICITUDAUDITOR(Nullable<int> cODAUD)
        {
            var cODAUDParameter = cODAUD.HasValue ?
                new ObjectParameter("CODAUD", cODAUD) :
                new ObjectParameter("CODAUD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcCREARSOLICITUDAUDITOR>("SP_SAF_CREARSOLICITUDAUDITOR", cODAUDParameter);
        }
    
        public virtual ObjectResult<TcCREARSOLICITUDSOA> SP_SAF_CREARSOLICITUDSOA(Nullable<int> cODSOA)
        {
            var cODSOAParameter = cODSOA.HasValue ?
                new ObjectParameter("CODSOA", cODSOA) :
                new ObjectParameter("CODSOA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcCREARSOLICITUDSOA>("SP_SAF_CREARSOLICITUDSOA", cODSOAParameter);
        }
    
        public virtual ObjectResult<TcACEPTARINVITACION> SP_SAF_ACEPTARINVITACION(Nullable<int> cODINV)
        {
            var cODINVParameter = cODINV.HasValue ?
                new ObjectParameter("CODINV", cODINV) :
                new ObjectParameter("CODINV", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcACEPTARINVITACION>("SP_SAF_ACEPTARINVITACION", cODINVParameter);
        }
    
        public virtual ObjectResult<TcAUDITORAPTOINVITAR> SP_SAF_AUDITORAPTOINVITAR(Nullable<int> cODPUB, Nullable<int> cODSERAUD)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            var cODSERAUDParameter = cODSERAUD.HasValue ?
                new ObjectParameter("CODSERAUD", cODSERAUD) :
                new ObjectParameter("CODSERAUD", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcAUDITORAPTOINVITAR>("SP_SAF_AUDITORAPTOINVITAR", cODPUBParameter, cODSERAUDParameter);
        }
    
        public virtual ObjectResult<TcDISPONIBILIDADAUDITOR> SP_DISPONIBILIDADAUDITOR(Nullable<int> cODAUD, Nullable<int> cODSOA, string fECINI, string fECFIN)
        {
            var cODAUDParameter = cODAUD.HasValue ?
                new ObjectParameter("CODAUD", cODAUD) :
                new ObjectParameter("CODAUD", typeof(int));
    
            var cODSOAParameter = cODSOA.HasValue ?
                new ObjectParameter("CODSOA", cODSOA) :
                new ObjectParameter("CODSOA", typeof(int));
    
            var fECINIParameter = fECINI != null ?
                new ObjectParameter("FECINI", fECINI) :
                new ObjectParameter("FECINI", typeof(string));
    
            var fECFINParameter = fECFIN != null ?
                new ObjectParameter("FECFIN", fECFIN) :
                new ObjectParameter("FECFIN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcDISPONIBILIDADAUDITOR>("SP_DISPONIBILIDADAUDITOR", cODAUDParameter, cODSOAParameter, fECINIParameter, fECFINParameter);
        }
    
        public virtual ObjectResult<TcAGENDAREGISTRAR> SP_SAF_AGENDAREGISTRAR(Nullable<int> cODINV, Nullable<int> nUMHOR, string sTRFECHAS)
        {
            var cODINVParameter = cODINV.HasValue ?
                new ObjectParameter("CODINV", cODINV) :
                new ObjectParameter("CODINV", typeof(int));
    
            var nUMHORParameter = nUMHOR.HasValue ?
                new ObjectParameter("NUMHOR", nUMHOR) :
                new ObjectParameter("NUMHOR", typeof(int));
    
            var sTRFECHASParameter = sTRFECHAS != null ?
                new ObjectParameter("STRFECHAS", sTRFECHAS) :
                new ObjectParameter("STRFECHAS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcAGENDAREGISTRAR>("SP_SAF_AGENDAREGISTRAR", cODINVParameter, nUMHORParameter, sTRFECHASParameter);
        }
    
        public virtual ObjectResult<TcELIMINARFECHASASIGINVITACION> SP_SAF_ELIMINARFECHASASIGINVITACION(Nullable<int> cODINV, string sTRCODINVDET)
        {
            var cODINVParameter = cODINV.HasValue ?
                new ObjectParameter("CODINV", cODINV) :
                new ObjectParameter("CODINV", typeof(int));
    
            var sTRCODINVDETParameter = sTRCODINVDET != null ?
                new ObjectParameter("STRCODINVDET", sTRCODINVDET) :
                new ObjectParameter("STRCODINVDET", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcELIMINARFECHASASIGINVITACION>("SP_SAF_ELIMINARFECHASASIGINVITACION", cODINVParameter, sTRCODINVDETParameter);
        }
    
        public virtual ObjectResult<TcINVITARAUDITORES> SP_SAF_INVITARAUDITORES(Nullable<int> cODSOA, Nullable<int> cODPUB, Nullable<int> cODSERAUD, string sTRAUDITORCARGO)
        {
            var cODSOAParameter = cODSOA.HasValue ?
                new ObjectParameter("CODSOA", cODSOA) :
                new ObjectParameter("CODSOA", typeof(int));
    
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            var cODSERAUDParameter = cODSERAUD.HasValue ?
                new ObjectParameter("CODSERAUD", cODSERAUD) :
                new ObjectParameter("CODSERAUD", typeof(int));
    
            var sTRAUDITORCARGOParameter = sTRAUDITORCARGO != null ?
                new ObjectParameter("STRAUDITORCARGO", sTRAUDITORCARGO) :
                new ObjectParameter("STRAUDITORCARGO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcINVITARAUDITORES>("SP_SAF_INVITARAUDITORES", cODSOAParameter, cODPUBParameter, cODSERAUDParameter, sTRAUDITORCARGOParameter);
        }
    
        public virtual ObjectResult<TcASIGNARFECHASPROPUESTA> SP_SAF_ASIGNARFECHASPROPUESTA(Nullable<int> cODPROEQU, string sTRFECHASASIGNAR)
        {
            var cODPROEQUParameter = cODPROEQU.HasValue ?
                new ObjectParameter("CODPROEQU", cODPROEQU) :
                new ObjectParameter("CODPROEQU", typeof(int));
    
            var sTRFECHASASIGNARParameter = sTRFECHASASIGNAR != null ?
                new ObjectParameter("STRFECHASASIGNAR", sTRFECHASASIGNAR) :
                new ObjectParameter("STRFECHASASIGNAR", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcASIGNARFECHASPROPUESTA>("SP_SAF_ASIGNARFECHASPROPUESTA", cODPROEQUParameter, sTRFECHASASIGNARParameter);
        }
    
        public virtual ObjectResult<TcDETALLEEQUIPOPORAUDITORIA> SP_SAF_DETALLEEQUIPOPORAUDITORIA(Nullable<int> cODAUDITORIA)
        {
            var cODAUDITORIAParameter = cODAUDITORIA.HasValue ?
                new ObjectParameter("CODAUDITORIA", cODAUDITORIA) :
                new ObjectParameter("CODAUDITORIA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcDETALLEEQUIPOPORAUDITORIA>("SP_SAF_DETALLEEQUIPOPORAUDITORIA", cODAUDITORIAParameter);
        }
    
        public virtual ObjectResult<TcELIMINARFECHASASIGPROP> SP_SAF_ELIMINARFECHASASIGPROP(Nullable<int> cODPRO, string sTRCODPROEQUDET)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            var sTRCODPROEQUDETParameter = sTRCODPROEQUDET != null ?
                new ObjectParameter("STRCODPROEQUDET", sTRCODPROEQUDET) :
                new ObjectParameter("STRCODPROEQUDET", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcELIMINARFECHASASIGPROP>("SP_SAF_ELIMINARFECHASASIGPROP", cODPROParameter, sTRCODPROEQUDETParameter);
        }
    
        public virtual ObjectResult<TcEQUIPOAUDITORIA> SP_SAF_EQUIPOAUDITORIA(Nullable<int> cODAUDITORIA)
        {
            var cODAUDITORIAParameter = cODAUDITORIA.HasValue ?
                new ObjectParameter("CODAUDITORIA", cODAUDITORIA) :
                new ObjectParameter("CODAUDITORIA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcEQUIPOAUDITORIA>("SP_SAF_EQUIPOAUDITORIA", cODAUDITORIAParameter);
        }
    
        public virtual ObjectResult<TcFECHASINVITADAS> SP_SAF_FECHASINVITADAS(Nullable<int> cODPROEQU)
        {
            var cODPROEQUParameter = cODPROEQU.HasValue ?
                new ObjectParameter("CODPROEQU", cODPROEQU) :
                new ObjectParameter("CODPROEQU", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcFECHASINVITADAS>("SP_SAF_FECHASINVITADAS", cODPROEQUParameter);
        }
    
        public virtual ObjectResult<TcACCEDERSISTEMAADMIN> SP_ACCEDERSISTEMAADMIN(string nOMUSU, string pASUSU)
        {
            var nOMUSUParameter = nOMUSU != null ?
                new ObjectParameter("NOMUSU", nOMUSU) :
                new ObjectParameter("NOMUSU", typeof(string));
    
            var pASUSUParameter = pASUSU != null ?
                new ObjectParameter("PASUSU", pASUSU) :
                new ObjectParameter("PASUSU", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcACCEDERSISTEMAADMIN>("SP_ACCEDERSISTEMAADMIN", nOMUSUParameter, pASUSUParameter);
        }
    
        public virtual ObjectResult<TcASIGNARGANADORPROPUESTA> SP_SAF_ASIGNARGANADORPROPUESTA(Nullable<int> cODPRO, Nullable<int> cODPUB)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcASIGNARGANADORPROPUESTA>("SP_SAF_ASIGNARGANADORPROPUESTA", cODPROParameter, cODPUBParameter);
        }
    
        public virtual ObjectResult<TcEQUIPOAUDITORIARPT> SP_SAF_EQUIPOAUDITORIA_RPT(Nullable<int> cODPRO)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcEQUIPOAUDITORIARPT>("SP_SAF_EQUIPOAUDITORIA_RPT", cODPROParameter);
        }
    
        public virtual ObjectResult<TcPROPUESTARPT> SP_SAF_PROPUESTA_RPT(Nullable<int> cODPRO)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcPROPUESTARPT>("SP_SAF_PROPUESTA_RPT", cODPROParameter);
        }
    
        public virtual ObjectResult<TcSAFCRONOENTIDADCRONORPT> SP_SAF_CRONOENTIDAD_CRONO_RPT(Nullable<int> cODCRO)
        {
            var cODCROParameter = cODCRO.HasValue ?
                new ObjectParameter("CODCRO", cODCRO) :
                new ObjectParameter("CODCRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcSAFCRONOENTIDADCRONORPT>("SP_SAF_CRONOENTIDAD_CRONO_RPT", cODCROParameter);
        }
    
        public virtual ObjectResult<TcSAFCRONOGRAMARPT> SP_SAF_CRONOGRAMA_RPT(Nullable<int> cODCRO)
        {
            var cODCROParameter = cODCRO.HasValue ?
                new ObjectParameter("CODCRO", cODCRO) :
                new ObjectParameter("CODCRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcSAFCRONOGRAMARPT>("SP_SAF_CRONOGRAMA_RPT", cODCROParameter);
        }
    
        public virtual ObjectResult<TcSAFBASERPT> SP_SAF_BASE_RPT(Nullable<int> cODBAS)
        {
            var cODBASParameter = cODBAS.HasValue ?
                new ObjectParameter("CODBAS", cODBAS) :
                new ObjectParameter("CODBAS", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcSAFBASERPT>("SP_SAF_BASE_RPT", cODBASParameter);
        }
    
        public virtual ObjectResult<TcSAFPUBLICACIONRPT> SP_SAF_PUBLICACION_RPT(Nullable<int> cODPUB)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcSAFPUBLICACIONRPT>("SP_SAF_PUBLICACION_RPT", cODPUBParameter);
        }
    
        public virtual ObjectResult<TcSAFPUBLICACIONBASERPT> SP_SAF_PUBLICACIONBASE_RPT(Nullable<int> cODPUB)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcSAFPUBLICACIONBASERPT>("SP_SAF_PUBLICACIONBASE_RPT", cODPUBParameter);
        }
    
        public virtual ObjectResult<string> USP_CORRELATIVO_SOLICITUD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("USP_CORRELATIVO_SOLICITUD");
        }
    
        public virtual ObjectResult<string> SP_SAF_CORRELATIVOSOLICITUD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_SAF_CORRELATIVOSOLICITUD");
        }
    
        public virtual ObjectResult<SP_SAF_CREARPROPUESTA_Result> SP_SAF_CREARPROPUESTA(Nullable<int> cODPUB, Nullable<int> cODBAS, Nullable<int> cODSOA)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            var cODBASParameter = cODBAS.HasValue ?
                new ObjectParameter("CODBAS", cODBAS) :
                new ObjectParameter("CODBAS", typeof(int));
    
            var cODSOAParameter = cODSOA.HasValue ?
                new ObjectParameter("CODSOA", cODSOA) :
                new ObjectParameter("CODSOA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SAF_CREARPROPUESTA_Result>("SP_SAF_CREARPROPUESTA", cODPUBParameter, cODBASParameter, cODSOAParameter);
        }
    
        public virtual ObjectResult<TcCREARPROPUESTA> SP_SAF_CREARPROPUESTA1(Nullable<int> cODPUB, Nullable<int> cODBAS, Nullable<int> cODSOA)
        {
            var cODPUBParameter = cODPUB.HasValue ?
                new ObjectParameter("CODPUB", cODPUB) :
                new ObjectParameter("CODPUB", typeof(int));
    
            var cODBASParameter = cODBAS.HasValue ?
                new ObjectParameter("CODBAS", cODBAS) :
                new ObjectParameter("CODBAS", typeof(int));
    
            var cODSOAParameter = cODSOA.HasValue ?
                new ObjectParameter("CODSOA", cODSOA) :
                new ObjectParameter("CODSOA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcCREARPROPUESTA>("SP_SAF_CREARPROPUESTA1", cODPUBParameter, cODBASParameter, cODSOAParameter);
        }
    
        public virtual ObjectResult<SP_SAF_AUDITORIAS_Result> SP_SAF_AUDITORIAS(Nullable<int> cODPRO)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SAF_AUDITORIAS_Result>("SP_SAF_AUDITORIAS", cODPROParameter);
        }
    
        public virtual ObjectResult<TcAUDITORIAS> SP_SAF_AUDITORIAS1(Nullable<int> cODPRO)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcAUDITORIAS>("SP_SAF_AUDITORIAS1", cODPROParameter);
        }
    
        public virtual ObjectResult<TcPROPUESTAS> SP_SAF_PROPUESTAS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcPROPUESTAS>("SP_SAF_PROPUESTAS");
        }
    
        public virtual ObjectResult<TcMENUPORPERFIL> SP_SAF_MENUPORPERFIL(Nullable<int> cODPER)
        {
            var cODPERParameter = cODPER.HasValue ?
                new ObjectParameter("CODPER", cODPER) :
                new ObjectParameter("CODPER", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcMENUPORPERFIL>("SP_SAF_MENUPORPERFIL", cODPERParameter);
        }
    
        public virtual ObjectResult<TcSUBMENUPORMENU> SP_SAF_SUBMENUPORMENU(Nullable<int> cODMEN)
        {
            var cODMENParameter = cODMEN.HasValue ?
                new ObjectParameter("CODMEN", cODMEN) :
                new ObjectParameter("CODMEN", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcSUBMENUPORMENU>("SP_SAF_SUBMENUPORMENU", cODMENParameter);
        }
    
        public virtual ObjectResult<TcCORRELATIVO> SP_SAF_CORRELATIVOSOLICITUD1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TcCORRELATIVO>("SP_SAF_CORRELATIVOSOLICITUD1");
        }
    
        public virtual ObjectResult<SP_SAF_EQUIPO_PROPUESTA_Result> SP_SAF_EQUIPO_PROPUESTA(Nullable<int> cODPRO)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SAF_EQUIPO_PROPUESTA_Result>("SP_SAF_EQUIPO_PROPUESTA", cODPROParameter);
        }
    
        public virtual ObjectResult<SP_SAF_EQUIPO_PROPUESTA_Result> SP_SAF_EQUIPO_PROPUESTA1(Nullable<int> cODPRO)
        {
            var cODPROParameter = cODPRO.HasValue ?
                new ObjectParameter("CODPRO", cODPRO) :
                new ObjectParameter("CODPRO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SAF_EQUIPO_PROPUESTA_Result>("SP_SAF_EQUIPO_PROPUESTA1", cODPROParameter);
        }
    
        public virtual ObjectResult<SP_SAF_WORKFLOW_POR_USU_Result> SP_SAF_WORKFLOW_POR_USU(string uSU, string tIPO, string fECINI, string fECFIN)
        {
            var uSUParameter = uSU != null ?
                new ObjectParameter("USU", uSU) :
                new ObjectParameter("USU", typeof(string));
    
            var tIPOParameter = tIPO != null ?
                new ObjectParameter("TIPO", tIPO) :
                new ObjectParameter("TIPO", typeof(string));
    
            var fECINIParameter = fECINI != null ?
                new ObjectParameter("FECINI", fECINI) :
                new ObjectParameter("FECINI", typeof(string));
    
            var fECFINParameter = fECFIN != null ?
                new ObjectParameter("FECFIN", fECFIN) :
                new ObjectParameter("FECFIN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SAF_WORKFLOW_POR_USU_Result>("SP_SAF_WORKFLOW_POR_USU", uSUParameter, tIPOParameter, fECINIParameter, fECFINParameter);
        }
    
        public virtual ObjectResult<SP_SAF_WORKFLOW_POR_USU_Result> SP_SAF_WORKFLOW_POR_USU1(string uSU, string tIPO, string fECINI, string fECFIN)
        {
            var uSUParameter = uSU != null ?
                new ObjectParameter("USU", uSU) :
                new ObjectParameter("USU", typeof(string));
    
            var tIPOParameter = tIPO != null ?
                new ObjectParameter("TIPO", tIPO) :
                new ObjectParameter("TIPO", typeof(string));
    
            var fECINIParameter = fECINI != null ?
                new ObjectParameter("FECINI", fECINI) :
                new ObjectParameter("FECINI", typeof(string));
    
            var fECFINParameter = fECFIN != null ?
                new ObjectParameter("FECFIN", fECFIN) :
                new ObjectParameter("FECFIN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SAF_WORKFLOW_POR_USU_Result>("SP_SAF_WORKFLOW_POR_USU1", uSUParameter, tIPOParameter, fECINIParameter, fECFINParameter);
        }
    }
}
