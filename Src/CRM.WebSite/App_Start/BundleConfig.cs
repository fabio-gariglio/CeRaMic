using System.Web;
using System.Web.Optimization;

namespace CRM.WebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
						//bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						//						"~/Scripts/jquery-{version}.js"));

						//bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						//						"~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
						//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						//						"~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

						bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
											"~/Scripts/jquery-{version}.js",
											"~/Scripts/jquery.signalR-{version}.js"));

						bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
											"~/Scripts/angular-file-upload-shim.js",
											"~/Scripts/angular.js",
											"~/Scripts/angular-file-upload.js",
											"~/Scripts/angular-resource.js", 
											"~/Scripts/angular-ui-router.js",
											"~/Scripts/angular-ui/ui-bootstrap.js",
											"~/Scripts/angular-ui/ui-bootstrap-tpls.js",
											"~/Scripts/ng-infinite-scroll.js",
											"~/Scripts/uuid.js",
											"~/Scripts/linq.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
											"~/Content/font-awesome.css",
											"~/Content/AdminLTE.css"));
        }
    }
}
