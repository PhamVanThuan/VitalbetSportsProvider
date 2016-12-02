using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using VitalbetSportsProvider.Core;
using VitalbetSportsProvider.WebClient;
using VitalbetSportsProvider.WebClient.Hubs;

[assembly: OwinStartup(typeof(Startup))]
namespace VitalbetSportsProvider.WebClient
{
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
