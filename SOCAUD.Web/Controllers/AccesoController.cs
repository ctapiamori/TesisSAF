using SOCAUD.Business.Core;
using SOCAUD.Common.Constantes;
using SOCAUD.Common.Enum;
using SOCAUD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOCAUD.Web.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ISeguridadLogic _seguridadLogic;
        private readonly ISafAuditorLogic _auditorLogic;
        private readonly ISafSoaLogic _soaLogic;
        private readonly ISafMenuLogic _menuLogic;
        public AccesoController()
        {
            _seguridadLogic = new SeguridadLogic();
            _auditorLogic = new SafAuditorLogic();
            _soaLogic = new SafSoaLogic();
            _menuLogic = new SafMenuLogic();
        }

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public JsonResult IngresarSistema(int tipoUsuario, string usuario, string password)
        {
            var result = this._seguridadLogic.AccederSistemaExtranet(usuario, password, tipoUsuario);

            if (result.Exito)
            {
                if (tipoUsuario == (int)Tipo.TipoUsuarioExtranet.Auditor)
                {
                    var auditor = _auditorLogic.GetAuditorByUsuario(usuario);
                    Session["sessionCodigoResponsableLogin"] = auditor.CODAUD;
                    Session["sessionNombreCompletoUsuario"] = string.Format("{0} {1}", auditor.NOMAUD, auditor.APEAUD);
                }
                else
                {
                    var soa = _soaLogic.InformacionPorUsuario(usuario);
                    Session["sessionCodigoResponsableLogin"] = soa.CODSOA;
                    Session["sessionNombreCompletoUsuario"] = soa.RAZSOCSOA;
                }
                Session["sessionUsuario"] = usuario;
                Session["sessionTipoUsuario"] = tipoUsuario;

                var tipoExterno = Convert.ToInt32(Session["sessionTipoUsuario"]);
                int perfilUsuarioExterno = 0;
                if (tipoExterno == Tipo.TipoUsuarioExtranet.Auditor.GetHashCode())
                    perfilUsuarioExterno = 6;
                else
                    perfilUsuarioExterno = 5;

                var perfil = perfilUsuarioExterno;


                var MenuBD = _menuLogic.ObtenerMenuPorPerfil(perfil).ToList();

                var MenuFinal = (from c in MenuBD
                                 select new MenuOpcionesModel()
                                 {
                                     Css = c.ICONCSS,
                                     Nombre = c.DESMEN,
                                     Ruta = c.RUTAMEN,
                                     SubMenu = (from x in this._menuLogic.ObtenerSubMenuPorMenu(c.CODMEN).ToList()
                                                select new SubMenuOpcionesModel()
                                                {
                                                    Nombre = x.DESSUBMEN,
                                                    Ruta = x.RUTASUBMEN
                                                })
                                 });

                Session["sessionMenuSistema"] = MenuFinal.ToList();

            }
            return Json(result);
        }

        public ActionResult ByPass(int CodigoUsuario, string NombreUsuario, string UsuarioSOAAuditor, string TipoExterno, string Usuario)
        {
            Session["sessionCodigoResponsableLogin"] = CodigoUsuario;
            Session["sessionNombreCompletoUsuario"] = NombreUsuario;
            Session["sessionTipoUsuario"] = TipoExterno;
            Session["sessionUsuario"] = Usuario;
            

            var tipoExterno = Convert.ToInt32(Session["sessionTipoUsuario"]);
            int perfilUsuarioExterno = 0;
            if (tipoExterno == Tipo.TipoUsuarioExtranet.Auditor.GetHashCode())
                perfilUsuarioExterno = 6;
            else
                perfilUsuarioExterno = 5;

            var perfil = perfilUsuarioExterno;

            
            var MenuBD = _menuLogic.ObtenerMenuPorPerfil(perfil).ToList();

            var MenuFinal = (from c in MenuBD
                             select new MenuOpcionesModel()
                             {
                                 Css = c.ICONCSS,
                                 Nombre = c.DESMEN,
                                 Ruta = c.RUTAMEN,
                                 SubMenu = (from x in this._menuLogic.ObtenerSubMenuPorMenu(c.CODMEN).ToList()
                                            select new SubMenuOpcionesModel()
                                            {
                                                Nombre = x.DESSUBMEN,
                                                Ruta = x.RUTASUBMEN
                                            })
                             });

            Session["sessionMenuSistema"] = MenuFinal.ToList();
            return RedirectToAction("Bandeja", "Notificacion");
            
        }
    }
}