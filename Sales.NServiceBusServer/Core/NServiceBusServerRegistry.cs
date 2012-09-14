using Sales.Application.Core;
using StructureMap.Configuration.DSL;

namespace Sales.NServiceBusServer.Core
{
    class NServiceBusServerRegistry : Registry
    {
        public NServiceBusServerRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}
