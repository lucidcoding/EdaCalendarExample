using HumanResources.Application.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.NServiceBusServer.Core
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
