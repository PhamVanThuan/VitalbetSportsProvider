namespace VitalbetSportsProvider.Core
{
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using Models;
    using VitalbetSportsProvider.Core.Mappings;
    using VitalbetSportsProvider.Core.Services;

    using VitalbetSportsProvider.DataModel;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Feed.Http;
    using VitalbetSportsProvider.Feed.Interfaces;
    using VitalbetSportsProvider.ViewModels;

    using WebClient.Core.Comparers;

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
            var config = new MapperConfiguration(s =>
            {
                s.AddProfile<XmlMappings>();
                s.AddProfile<ViewModelMappings>();
            });

            var betsUpdatableService = new BetsUpdatableService();

            container
                .RegisterInstance<IMapper>(new Mapper(config))
                .RegisterType<ISportsRepository, SportsRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IVitalbetSportsRepository, VitalbetSportsRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IFeedRunner, FeedRunner>(new ContainerControlledLifetimeManager())

                .RegisterType<IEqualityComparer<Bet>, BetsComparer>(new ContainerControlledLifetimeManager())
                .RegisterType<IEqualityComparer<Event>, EventsComparer>(new ContainerControlledLifetimeManager())
                .RegisterType<IEqualityComparer<Match>, MatchesComparer>(new ContainerControlledLifetimeManager())
                .RegisterType<IEqualityComparer<Odds>, OddsComparer>(new ContainerControlledLifetimeManager())
                .RegisterType<IEqualityComparer<Sport>, SportsComparer>(new ContainerControlledLifetimeManager())
                
                .RegisterInstance<IUpdatableService<BetViewModel>>(betsUpdatableService)
                .RegisterInstance<IUpdatableServiceInvoke<BetViewModel>>(betsUpdatableService);
        }
    }
}
