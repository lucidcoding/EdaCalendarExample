using Calendar.Data.Core;
using StructureMap.Configuration.DSL;

namespace Calendar.MessageHandlers.Core
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
