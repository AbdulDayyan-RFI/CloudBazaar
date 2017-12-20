using System.Web.Optimization;

namespace TEB.Web.Areas.Admin
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerya").Include(
                        "~/Scripts/jquery.caret.min.js",
                        "~/Scripts/jquery.tag-editor.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Areas/Admin/Content/Asset/AdminLTE/bootstrap").Include(
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/bootstrap/dist/js/bootstrap.min.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/datatables.net/js/jquery.dataTables.min.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/fastclick/lib/fastclick.js",
                     "~/Areas/Admin/Content/Asset/AdminLTE/dist/js/adminlte.min.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",
                     "~/Areas/Admin/Content/Asset/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                     "~/Areas/Admin/Content/Asset/AdminLTE/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/Chart.js/Chart.js",
                                         "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/kendo/scripts/kendo.web.min.js",

                     "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Content/Asset/AdminLTE/css").Include(
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/bootstrap/dist/css/bootstrap.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/kendo/kendo.rtl.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/kendo/kendo.default.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/kendo/kendo.common.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/font-awesome/css/font-awesome.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/Ionicons/css/ionicons.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/bower_components/jvectormap/jquery-jvectormap.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/dist/css/AdminLTE.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/dist/css/skins/_all-skins.min.css",
                     "~/Areas/Admin/Content/Asset/AdminLTE/dist/css/jquery.tag-editor.css"));
        }
    }
}