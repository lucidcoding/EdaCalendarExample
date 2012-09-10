using NServiceBus;
using Sales.NServiceBusServer.Common;
using StructureMap;

namespace Sales.NServiceBusServer
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                //.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("HumanResources.Messages.Events"))
                .StructureMapBuilder()
                .JsonSerializer()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<NServiceBusServerRegistry>());
        }
    }
}
