using System;
using System.Web.Optimization;

namespace RepairReach.WebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                      "~/signalr/hubs",
                      "~/Scripts/jquery.signalR-2.0.0.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //flat template bundles
            bundles.Add(new StyleBundle("~/Content/flat/accountcss").Include(
                "~/Content/flat/css/bootstrap.min.css",
                "~/Content/flat/css/bootstrap-responsive.min.css",
                "~/Content/flat/css/plugins/icheck/all.css",
                "~/Content/flat/css/style.css",
                "~/Content/flat/css/themes.css"));

            bundles.Add(new ScriptBundle("~/bundles/accountjquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/flat/js/plugins/nicescroll/jquery.nicescroll.min.js",
                        "~/Content/flat/js/plugins/validation/jquery.validate.min.js",
                        "~/Content/flat/js/plugins/validation/additional-methods.min.js",
                        "~/Content/flat/js/plugins/icheck/jquery.icheck.min.js",
                        "~/Content/flat/js/bootstrap.min.js",
                        "~/Content/flat/js/eakroko.js"));

            bundles.Add(new StyleBundle("~/Content/flat/css").Include(
                "~/Content/flat/css/bootstrap.min.css",
                "~/Content/flat/css/bootstrap-responsive.min.css",
                //"~/Content/flat/css/plugins/jquery-ui/smoothness/jquery-ui.css",
                //"~/Content/flat/css/plugins/jquery-ui/smoothness/jquery.ui.theme.css",
                //"~/Content/themes/base/jquery.ui.dialog.css",
                //"~/Content/themes/base/jquery-ui.css",
                "~/Content/flat/css/plugins/gritter/jquery.gritter.css",
                "~/Content/flat/css/style.css",
                "~/Content/flat/css/themes.css",
                "~/Content/flat/css/pager.css",
                "~/Content/flat/css/plugins/icheck/all.css",
                "~/Content/flat/css/plugins/qtip/jquery.qtip.min.css",
                "~/Content/flat/css/plugins/datepicker/datepicker.css",
                "~/Content/flat/css/plugins/timepicker/jquery.timepicker.css",
                "~/Content/flat/css/plugins/fullcalendar/fullcalendar.css"));
                //"~/Content/flat/css/plugins/fullcalendar/fullcalendar.print.css",
            bundles.Add(new StyleBundle("~/Content/flat/css/jquery-ui/smoothness").Include(
                "~/Content/flat/css/plugins/jquery-ui/smoothness/jquery-ui.css",
                "~/Content/flat/css/plugins/jquery-ui/smoothness/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/mainjquery").Include(
                        //"~/Content/flat/js/jquery.min.js",
                        "~/Content/flat/js/plugins/nicescroll/jquery.nicescroll.min.js",
                        "~/Content/flat/js/plugins/imagesLoaded/jquery.imagesloaded.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.core.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.widget.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.mouse.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.resizable.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.sortable.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.draggable.min.js",
                        "~/Content/flat/js/plugins/jquery-ui/jquery.ui.droppable.min.js",
                        "~/Content/flat/js/plugins/slimscroll/jquery.slimscroll.min.js",
                        "~/Content/flat/js/bootstrap.min.js",
                        "~/Content/flat/js/plugins/bootbox/jquery.bootbox.js",
                        "~/Content/flat/js/plugins/form/jquery.form.min.js",
                        "~/Content/flat/js/plugins/validation/jquery.validate.min.js",
                        "~/Content/flat/js/plugins/validation/additional-methods.min.js",
                        "~/Content/flat/js/eakroko.min.js",
                        "~/Content/flat/js/application.min.js",
                        //"~/Content/flat/js/demonstration.js",
                        "~/Content/flat/js/plugins/gmap/gmap3-menu.js",
                        "~/Content/flat/js/plugins/gmap/gmap3.min.js",
                        "~/Content/flat/js/plugins/maskedinput/jquery.maskedinput.min.js",
                        "~/Content/flat/js/plugins/icheck/jquery.icheck.min.js",
                        "~/Content/flat/js/plugins/qtip/jquery.qtip.min.js",
                        "~/Content/flat/js/plugins/chartjs/Chart.min.js",
                        "~/Content/flat/js/plugins/datepicker/bootstrap-datepicker.js",
                        "~/Content/flat/js/plugins/timepicker/jquery.timepicker.min.js",
                        "~/Content/flat/js/plugins/fullcalendar/fullcalendar.min.js"));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}
