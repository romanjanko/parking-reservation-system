using System.Web;
using System.Web.Optimization;

namespace ParkingSystem.WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/parking").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/parkingreservationsystem.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/parking").Include(
                        "~/Content/font-awesome.css",
                        "~/Content/bootstrap.css",
                        "~/Content/parkingreservationsystem.css"));
        }
    }
}