using System.Web;
using System.Web.Optimization;

namespace TodoListWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.smoothscroll.js",
                        "~/Scripts/jquery.mixitup.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/misc").Include(
                "~/Scripts/move.js",
                "~/Scripts/swipe.js",
                "~/Scripts/unslider-min.js",
                "~/Scripts/detectmobilebrowser.js",
                "~/Scripts/vivus.min.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/function").Include(
                "~/Scripts/team-function.js",
                "~/Scripts/technology-function.js",
                "~/Scripts/about-function.js",
                "~/Scripts/index-function.js",
                "~/Scripts/future-function.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/main.css",
                      "~/Content/unslider.css"));

            /*
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
                      */
        }
    }
}
