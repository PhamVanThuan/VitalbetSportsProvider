using AutoMapper;
using Microsoft.Practices.Unity;
using VitalbetSportsProvider.DataModel;
using VitalbetSportsProvider.DataModel.Interfaces;
using VitalbetSportsProvider.Feed.Http;
using VitalbetSportsProvider.Feed.Interfaces;

namespace VitalbetSportsProvider.WebClient.Core
{
    public class Container
    {
        private static volatile IUnityContainer instance;
        private static object syncRoot = new object();

        private Container() { }

        public static IUnityContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UnityContainer();
                            Register(instance);
                        }
                    }
                }

                return instance;
            }
        }

        public static T Resolve<T>() => (T)Instance.Resolve(typeof(T));

        private static void Register(IUnityContainer container)
        {
            var config = new MapperConfiguration(s => s.AddProfile<XmlMappings>());
            container
                .RegisterInstance<IMapper>(new Mapper(config))
                .RegisterType<ISportsRepository, SportsRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IVitalbetSportsRepository, VitalbetSportsRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IFeedRunner, FeedRunner>(new ContainerControlledLifetimeManager());
        }
    }
}
