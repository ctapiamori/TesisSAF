using System.Web;
using System.Web.Optimization;

namespace SOCAUD.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/Ace").Include(
                "~/Scripts/ace-extra.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Login").Include(
                "~/Scripts/jQuery-2.1.4.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/raphael-min.js",
                "~/Scripts/morris.min.js", //check
                "~/Scripts/jquery.sparkline.min.js", //check
                "~/Scripts/jquery-jvectormap-1.2.2.min.js",
                "~/Scripts/jquery-jvectormap-world-mill-en.js",
                "~/Scripts/jquery.knob.js", //check
                "~/Scripts/globalize/globalize.js", //by rolo
                "~/Scripts/globalize/cultures/globalize.culture.es-PE.js", //by rolo
                "~/Scripts/moment.min.js",
                "~/Scripts/daterangepicker.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/bootstrap3-wysihtml5.all.min.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/fastclick.min.js",
                "~/Scripts/app.min.js",
                "~/Scripts/dashboard.min.js",
                "~/Scripts/pnotify.custom.min.js",
                "~/Scripts/demo.js",
                "~/Scripts/bootbox.js",



                "~/Scripts/ace.min.js",
                //"~/Scripts/ace-elements.min.js",


                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.js",



                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/locales/bootstrap-datepicker.es.js",


                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",

                "~/Scripts/jquery.textarea.counter.js",
                "~/Scripts/core.js"

                ));

            bundles.Add(new ScriptBundle("~/Scripts/Layout").Include(
                /*
                "~/Scripts/jQuery-2.1.4.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/raphael-min.js",
                "~/Scripts/morris.min.js", //check
                "~/Scripts/jquery.sparkline.min.js", //check
                "~/Scripts/jquery-jvectormap-1.2.2.min.js",
                "~/Scripts/jquery-jvectormap-world-mill-en.js",
                "~/Scripts/jquery.knob.js", //check
                "~/Scripts/moment.min.js",
                "~/Scripts/daterangepicker.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/bootstrap3-wysihtml5.all.min.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/fastclick.min.js",
                "~/Scripts/app.min.js",
                "~/Scripts/dashboard.min.js",
                "~/Scripts/pnotify.custom.min.js",
                "~/Scripts/demo.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/dataTables.bootstrap.js",
                "~/Scripts/core.js"*/
                "~/Scripts/jQuery-2.1.4.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/gantt/jquery.fn.gantt.min.js",
                "~/Scripts/globalize/globalize.js", //by rolo
                "~/Scripts/globalize/cultures/globalize.culture.es-PE.js", //by rolo
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/gantt/prettify.js",
                "~/Scripts/raphael-min.js",
                "~/Scripts/morris.min.js", //check
                "~/Scripts/jquery.sparkline.min.js", //check
                "~/Scripts/jquery-jvectormap-1.2.2.min.js",
                "~/Scripts/jquery-jvectormap-world-mill-en.js",
                "~/Scripts/jquery.knob.js", //check
                "~/Scripts/moment.min.js",
                "~/Scripts/daterangepicker.js",

                "~/Scripts/bootstrap3-wysihtml5.all.min.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/fastclick.min.js",
                "~/Scripts/app.min.js",
                "~/Scripts/dashboard.min.js",
                "~/Scripts/pnotify.custom.min.js",
                "~/Scripts/demo.js",
                "~/Scripts/bootbox.js",


                "~/Scripts/ace.min.js",
                "~/Scripts/ace-elements.min.js",


                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.js",





                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/locales/bootstrap-datepicker.es.js",
                "~/Scripts/bootstrap-timepicker.js",
                "~/Scripts/jquery.textarea.counter.js",
                "~/Scripts/core.js"


                ));

            bundles.Add(new ScriptBundle("~/Scripts/LayoutEmpty").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/globalize/globalize.js", //by rolo
                "~/Scripts/globalize/cultures/globalize.culture.es-PE.js", //by rolo
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/bootstrap-datepicker.js",//dependencia de core.js
                "~/Scripts/locales/bootstrap-datepicker.es.js",//dependencia de core.js
                "~/Scripts/pnotify.custom.js",//dependencia de core.js
                //"~/Scripts/ace-elements.min.js",
                //"~/Scripts/ace.min.js",
                "~/Scripts/core.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/LayoutBlank").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/core.js"
                ));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/ionicons.min.css",
                "~/Content/AdminLTE.css",

                "~/Content/_all-skins.min.css",
                "~/Content/skin-blue-light.min.css",
                "~/Content/skin-blue.min.css",

                "~/Content/blue.css",
                "~/Content/morris.css",
                "~/Content/jquery-jvectormap-1.2.2.css",
                "~/Content/datepicker3.css",
                "~/Content/daterangepicker-bs3.css",
                "~/Content/bootstrap3-wysihtml5.min.css",
                "~/Content/pnotify.custom.min.css",
                "~/Content/saf.css"

                ));

            bundles.Add(new StyleBundle("~/Content/Layout").Include(
                "~/Content/cssGantt/gantt.css",
                "~/Content/bootstrap.min.css",
                "~/Content/cssGantt/prettify.css",
                "~/Content/font-awesome.min.css",
                "~/Content/ionicons.min.css",
                "~/Content/AdminLTE.css",

                "~/Content/_all-skins.min.css",
                "~/Content/skin-blue-light.min.css",
                "~/Content/skin-blue.min.css",

                "~/Content/blue.css",
                "~/Content/morris.css",
                "~/Content/jquery-jvectormap-1.2.2.css",
                "~/Content/datepicker3.css",
                "~/Content/daterangepicker-bs3.css",
                "~/Content/bootstrap3-wysihtml5.min.css",
                "~/Content/pnotify.custom.min.css",
                "~/Content/dataTables.bootstrap.css",
                "~/Content/bootstrap-datepicker3.css",
                "~/Content/saf.css"


                ));

            bundles.Add(new StyleBundle("~/Content/LayoutEmpty").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/dataTables.bootstrap.css",
                "~/Content/fonts.css",
                "~/Content/ace.css",
                "~/Content/ace-skins.min.css",
                "~/Content/ace-rtl.min.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new StyleBundle("~/Content/LayoutBlank").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/fonts.css",
                "~/Content/ace.css"
                ));
  
        }
    }
}
