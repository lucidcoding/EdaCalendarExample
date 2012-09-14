using NServiceBus;
using Sales.NServiceBusServer.Core;
using StructureMap;

namespace Sales.NServiceBusServer
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .JsonSerializer()
                .MsmqSubscriptionStorage()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<NServiceBusServerRegistry>());
        }
    }
}
