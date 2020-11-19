using System.Web;
using System.Web.Optimization;

namespace TCK_Off_Fac
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/CreateOrder/js").Include(
                         "~/Scripts/Disbursement/js_CreateOrder.js"));

            bundles.Add(new ScriptBundle("~/WarehouseI/js").Include(
                         "~/Scripts/Warehouse/js_Item.js"));

            bundles.Add(new ScriptBundle("~/WarehouseM/js").Include(
                         "~/Scripts/Warehouse/js_MoveLocation.js"));

            bundles.Add(new ScriptBundle("~/Warehouse/js").Include(
                         "~/Scripts/Warehouse/js_Overview.js"));

            bundles.Add(new ScriptBundle("~/WarehouseR/js").Include(
                         "~/Scripts/Warehouse/js_Receive.js"));

            bundles.Add(new ScriptBundle("~/WarehouseRT/js").Include(
                         "~/Scripts/Warehouse/js_Return.js"));

            bundles.Add(new ScriptBundle("~/WarehouseRS/js").Include(
                         "~/Scripts/Warehouse/js_WHReserve.js"));

        }
    }
}
