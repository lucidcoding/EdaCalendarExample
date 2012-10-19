using MasterData.Data.Core;
using StructureMap.Configuration.DSL;

namespace MasterData.MessageHandlers.Core
{
    class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}
