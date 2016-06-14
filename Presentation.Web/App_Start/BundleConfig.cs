using System.Web.Optimization;

namespace Presentation.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jslibs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            // When in debug configuration bundling is disabled by default and enabled for release.
            // This can however be forcefully changed by setting the EnableOptimizations.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=301862
            // BundleTable.EnableOptimizations = true; // true: bundling is on, false: bundling is off
        }
    }
}
