namespace VitalbetSportsProvider.WebClient
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.Practices.Unity;
    using Owin;
    using VitalbetSportsProvider.Core;
    using VitalbetSportsProvider.WebClient.Hubs;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Container.Instance;
            container.RegisterType<SportsHub, SportsHub>();

            var config = new HubConfiguration { Resolver = new UnityDependencyResolver(container), EnableDetailedErrors = true };
            app.MapSignalR(config);
        }
    }
}
