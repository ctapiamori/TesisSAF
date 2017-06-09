using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Intranet.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ISeguridadLogic _seguridadLogic;
        private readonly ISafUsuarioLogic _usuarioLogic;

        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSoaLogic _soaLogic;
        public AccesoController()
        {
            _seguridadLogic = new SeguridadLogic();
            _usuarioLogic = new SafUsuarioLogic();
            _auditorLogic = new SafAuditorLogic();
            _soaLogic = new SafSoaLogic();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult AccederSistema(string tipoUsuario, int tipoExterno, string usuario, string contrasenia)
        {
            if (tipoUsuario == TiposLogin.TipoUsuarioInterno)
            {
                var result = this._usuarioLogic.AccederSistema(usuario, contrasenia);// this.modelEntity.SP_ACCEDERSISTEMAADMIN(usuario, contrasenia).ToList().FirstOrDefault();
                var datosUsuario = this._usuarioLogic.BuscarPorUsuario(usuario);// this.modelEntity.SAF_USUARIO.Where(c => c.NOMUSU == usuario).FirstOrDefault();
                if (result.EXITO.Equals(0))
                {
                    return Json(new { Mensaje = "Usuario y/o Contraseñia incorrectos", Correcto = false, TipoUsuario = "I" });
                }
                else
                {
                    Session["sessionUsuario"] = usuario.ToUpper();
                    Session["sessionUsuarioNombreCompleto"] = string.Format("{0} {1}", datosUsuario.NOMPERUSU, datosUsuario.APEPERUSU).ToUpper();
                    Session["tipoUsuario"] = datosUsuario.TIPCARUSU.GetValueOrDefault();
                    Session["idUsuario"] = datosUsuario.CODUSU;
                    Session["codigoEntidadDelUsuario"] = (datosUsuario.CODENT.HasValue) ? datosUsuario.CODENT.Value : 0;
                    return Json(new { Mensaje = "Ingreso al sistema", Correcto = true, TipoUsuario = "I" });
                }
            }
            else
            {
                var result = _seguridadLogic.AccederSistemaExtranet(usuario, contrasenia, tipoExterno);

                string usuarioSOAAuditor = "";
                int codigoUsuario = 0;
                string NombreUsuario = "";

                if (result.Exito)
                {
                    if (tipoExterno == (int)Tipo.TipoUsuarioExtranet.Auditor)
                    {
                        usuarioSOAAuditor = "A";
                        var auditor = _auditorLogic.GetAuditorByUsuario(usuario);
                        codigoUsuario = auditor.CODAUD;
                        NombreUsuario = string.Format("{0} {1}", auditor.NOMAUD, auditor.APEAUD);
                    }
                    else
                    {
                        usuarioSOAAuditor = "S";
                        var soa = _soaLogic.InformacionPorUsuario(usuario);
                        codigoUsuario = soa.CODSOA;
                        NombreUsuario = soa.RAZSOCSOA;
                    }

                    return Json(new
                    {
                        Mensaje = "Ingreso al sistema", 
                                      Correcto = true, 
                                      TipoUsuario = "E",
                                      CodigoUsuario = codigoUsuario,
                                      NombreUsuario = NombreUsuario.ToUpper(),
                                      UsuarioSOAAuditor = usuarioSOAAuditor.ToUpper(),
                                      TipoExterno = tipoExterno,
                                      Usuario = usuario.ToUpper()

                    });
                }
                else {
                    return Json(new { Mensaje = "Usuario y/o Contraseñia incorrectos", Correcto = false, TipoUsuario = "E" });
                }


            }
        }
    }
}