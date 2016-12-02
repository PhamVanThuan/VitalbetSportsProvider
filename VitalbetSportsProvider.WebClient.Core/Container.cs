using Microsoft.Practices.Unity;
using VitalbetSportsProvider.DataModel;
using VitalbetSportsProvider.DataModel.Interfaces;

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

        private static void Register(IUnityContainer container)
        {
            container.RegisterType<ISportsRepository, SportsRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
