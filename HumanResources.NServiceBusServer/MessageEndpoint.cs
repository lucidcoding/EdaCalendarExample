using HumanResources.NServiceBusServer.Core;
using NServiceBus;
using StructureMap;

namespace HumanResources.NServiceBusServer
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
