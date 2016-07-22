using System.Web;
using System.Web.Optimization;

namespace ZADV.ZLeaderboard.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/flow").Include(
                        "~/Scripts/ng-flow-standalone.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                        "~/Scripts/fancyapps-fancyBox-18d1712/lib/jquery.mousewheel-3.0.6.pack.js",
                        "~/Scripts/fancyapps-fancyBox-18d1712/source/jquery.fancybox.pack.js",
                        "~/Scripts/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-buttons.js",
                        "~/Scripts/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-media.js",
                        "~/Scripts/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-thumbs.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css",
                      "~/Scripts/fancyapps-fancyBox-18d1712/source/jquery.fancybox.css",
                      "~/Scripts/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-buttons.css",
                      "~/Scripts/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox.css"));
        }
    }
}
