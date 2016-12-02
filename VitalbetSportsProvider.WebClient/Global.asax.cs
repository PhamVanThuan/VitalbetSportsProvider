namespace VitalbetSportsProvider.WebClient
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using VitalbetSportsProvider.Core;
    
    public class Global : HttpApplication
    {
        private IFeedRunner _feedRunner;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this._feedRunner = Container.Resolve<IFeedRunner>();
            this._feedRunner.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            this._feedRunner.Stop();
        }
    }
}
