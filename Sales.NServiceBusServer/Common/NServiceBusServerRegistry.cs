using Sales.Application.Common;
using StructureMap.Configuration.DSL;

namespace Sales.NServiceBusServer.Common
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
