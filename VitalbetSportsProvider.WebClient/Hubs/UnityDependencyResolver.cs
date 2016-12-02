namespace VitalbetSportsProvider.WebClient.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Practices.Unity;

    public class UnityDependencyResolver : DefaultDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }

        public override object GetService(Type serviceType) => base.GetService(serviceType) 
            ?? (this._container.IsRegistered(serviceType) ? this._container.Resolve(serviceType) : null);

        public override IEnumerable<object> GetServices(Type serviceType) => base.GetServices(serviceType).Concat(this._container.ResolveAll(serviceType));
    }
}