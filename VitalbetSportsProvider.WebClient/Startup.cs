using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using VitalbetSportsProvider.DataModel;
using VitalbetSportsProvider.WebClient.Hubs;

[assembly: OwinStartup(typeof(VitalbetSportsProvider.WebClient.Startup))]
namespace VitalbetSportsProvider.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var repo = new SportsRepository();
            GlobalHost.DependencyResolver.Register(
               typeof(SportsHub), () => new SportsHub(repo));
            app.MapSignalR();
        }
    }
}
