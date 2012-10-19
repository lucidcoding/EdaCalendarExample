using NServiceBus;
using Sales.MessageHandlers.Core;
using StructureMap;

namespace Sales.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .JsonSerializer()
                .MsmqSubscriptionStorage()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());
        }
    }
}
