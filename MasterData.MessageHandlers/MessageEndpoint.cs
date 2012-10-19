using MasterData.MessageHandlers.Core;
using NServiceBus;
using StructureMap;

namespace MasterData.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .JsonSerializer();

            ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());
        }
    }
}
