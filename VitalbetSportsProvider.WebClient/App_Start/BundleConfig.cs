using System.Web.Optimization;

namespace VitalbetSportsProvider.WebClient
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.signalR-{version}.js",
                "~/Scripts/bootstrap.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css"));
        }
    }
}
