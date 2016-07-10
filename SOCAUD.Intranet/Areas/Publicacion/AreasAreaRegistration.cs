using System.Web.Mvc;

namespace SOCAUD.Intranet.Areas.Areas
{
    public class AreasAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Publicacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Publicacion_default",
                url: "Publicacion/{controller}/{action}/{id}",
                defaults: new { area = "Publicacion", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "SOCAUD.Intranet.Areas.Publicacion.Controllers" }
            );
        }
    }
}