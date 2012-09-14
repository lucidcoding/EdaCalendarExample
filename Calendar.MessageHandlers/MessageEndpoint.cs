using Calendar.MessageHandlers.Core;
using NServiceBus;
using StructureMap;

namespace Calendar.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .JsonSerializer();

            ObjectFactory.Container.Configure(x => x.AddRegistry<NServiceBusServerRegistry>());
        }
    }
}
