using Calendar.Data.Core;
using StructureMap.Configuration.DSL;

namespace Calendar.MessageHandlers.Core
{
    class NServiceBusServerRegistry : Registry
    {
        public NServiceBusServerRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}
