using NServiceBus;
using HumanResources.NServiceBusServer.Common;
using StructureMap;

namespace HumanResources.NServiceBusServer
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .JsonSerializer()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<NServiceBusServerRegistry>());
        }
    }
}
