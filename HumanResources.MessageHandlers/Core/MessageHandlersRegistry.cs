using HumanResources.Data.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.MessageHandlers.Core
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
