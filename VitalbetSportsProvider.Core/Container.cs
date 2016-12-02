namespace VitalbetSportsProvider.Core
{
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using VitalbetSportsProvider.DataModel;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Feed.Http;
    using VitalbetSportsProvider.Feed.Interfaces;

    public class Container
    {
        private static volatile IUnityContainer _instance;
        private static object _syncRoot = new object();

        private Container() { }

        public static IUnityContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new UnityContainer();
                            Register(_instance);
                        }
                    }
                }

                return _instance;
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
