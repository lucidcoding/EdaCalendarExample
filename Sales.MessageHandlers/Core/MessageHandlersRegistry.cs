using Sales.Application.Core;
using StructureMap.Configuration.DSL;

namespace Sales.MessageHandlers.Core
{
    class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}
